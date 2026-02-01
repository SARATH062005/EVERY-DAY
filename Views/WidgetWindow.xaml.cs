using System;
using System.Windows;
using System.Windows.Interop;
using EveryDay.Helpers;
using EveryDay.Models;
using EveryDay.ViewModels;

namespace EveryDay.Views
{
    public partial class WidgetWindow : Window
    {
        public WidgetWindow()
        {
            InitializeComponent();
            Loaded += WidgetWindow_Loaded;
        }

        private void WidgetWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Desktop pinning disabled for better usability
            // PinToDesktop();
        }

        private void PinToDesktop()
        {
            try 
            {
                var windowHandle = new WindowInteropHelper(this).Handle;
                
                // Fetch the Progman window
                IntPtr progman = Win32Helper.FindWindow("Progman", null!);

                // Send 0x052C to Progman. This message spawns a WorkerW behind the desktop icons.
                Win32Helper.SendMessage(progman, 0x052C, new IntPtr(0), IntPtr.Zero);

                IntPtr workerw = IntPtr.Zero;

                // Enum windows to find the WorkerW that contains SHELLDLL_DefView
                Win32Helper.EnumWindows((tophandle, topparamhandle) =>
                {
                    IntPtr p = Win32Helper.FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", null!);

                    if (p != IntPtr.Zero)
                    {
                        // Gets the WorkerW Window after the current one.
                        workerw = Win32Helper.FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", null!);
                    }

                    return true;
                }, IntPtr.Zero);

                if (workerw != IntPtr.Zero)
                {
                    Win32Helper.SetParent(windowHandle, workerw);
                }
                else
                {
                    // Fallback to Progman if WorkerW not found
                    Win32Helper.SetParent(windowHandle, progman);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                // Double-click to maximize/restore
                MaximizeRestore();
            }
            else
            {
                // Single click to drag
                this.DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MaximizeRestore();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void MaximizeRestore()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        // Drag and Drop Logic
        private System.Windows.Point _startPoint;
        private Block? _draggedItem;
        private bool _isDragging;

        private void ItemsControl_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
            var element = e.OriginalSource as FrameworkElement;
            
            // Find parent if original source is inside templates
            _draggedItem = element?.DataContext as Block;
        }

        private void ItemsControl_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed && _draggedItem != null && !_isDragging)
            {
                System.Windows.Point position = e.GetPosition(null);
                if (Math.Abs(position.X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    // Check if we are dragging a TextBox - let normal selection happen if so
                    if (e.OriginalSource is System.Windows.Controls.TextBox) return;

                    _isDragging = true;
                    DragDrop.DoDragDrop(this, _draggedItem, System.Windows.DragDropEffects.Move);
                    _isDragging = false;
                    _draggedItem = null;
                }
            }
        }

        private void ItemsControl_DragOver(object sender, System.Windows.DragEventArgs e)
        {
             e.Effects = System.Windows.DragDropEffects.Move;
             e.Handled = true;
        }

        private void ItemsControl_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (_draggedItem == null) return;
            
            // Find target element under mouse
            var element = e.OriginalSource as FrameworkElement;
            var targetBlock = element?.DataContext as Block;
            
            if (targetBlock != null && targetBlock != _draggedItem)
            {
                var vm = DataContext as EveryDay.ViewModels.WidgetViewModel;
                if (vm != null)
                {
                    var oldIndex = vm.Blocks.IndexOf(_draggedItem);
                    var newIndex = vm.Blocks.IndexOf(targetBlock);
                    
                    if (oldIndex != -1 && newIndex != -1)
                    {
                        vm.Blocks.Move(oldIndex, newIndex);
                        
                        // Update Order
                        for(int i = 0; i < vm.Blocks.Count; i++)
                        {
                            vm.Blocks[i].Order = i;
                        }
                    }
                }
            }
        }
    }
}
