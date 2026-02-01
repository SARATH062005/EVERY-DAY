using System.Collections.ObjectModel;
using EveryDay.Data;
using EveryDay.Models;
using EveryDay.Services;
using System.Linq;
using System.Collections.Specialized;

namespace EveryDay.ViewModels
{
    public class WidgetViewModel : BaseViewModel
    {
        private LiteDbContext? _context;
        private BlockRepository? _repository;
        private bool _isInitialized = false;
        private string _searchText = "";
        private string _currentSection = "Notes";
        
        public ObservableCollection<Block> Blocks { get; set; } = new ObservableCollection<Block>();
        
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); Search(); }
        }

        public string CurrentSection
        {
            get => _currentSection;
            set 
            { 
                if (_currentSection != value)
                {
                    _currentSection = value;
                    OnPropertyChanged();
                    EnsureInitialized();
                    LoadBlocksForSection();
                }
            }
        }

        public RelayCommand AddBlockCommand { get; }
        public RelayCommand ToggleThemeCommand { get; }
        public RelayCommand ChangeSectionCommand { get; }
        public RelayCommand DeleteBlockCommand { get; }

        public WidgetViewModel()
        {
             // Defer initialization until first use for faster startup
             AddBlockCommand = new RelayCommand(o => {
                 EnsureInitialized();
                 string type = o as string ?? "Text";
                 Block block;
                 if (type == "Check") block = new CheckboxBlock { Content = "New Todo", Section = CurrentSection };
                 else if (type == "Header") block = new HeaderBlock { Content = "New Header", Section = CurrentSection };
                 else block = new TextBlock { Content = "New Text", Section = CurrentSection };
                 
                 block.Order = Blocks.Count > 0 ? Blocks.Max(b => b.Order) + 1 : 0;
                 
                 _repository!.Insert(block);
                 Blocks.Add(block);
                 
                 // Subscribe to property changes for the new block
                 SubscribeToBlockChanges(block);
             });
             
             ToggleThemeCommand = new RelayCommand(o => {
                  var tm = ThemeManager.Instance;
                  tm.ApplyTheme(tm.CurrentTheme == ThemeManager.Theme.Dark ? ThemeManager.Theme.Light : ThemeManager.Theme.Dark);
             });

             ChangeSectionCommand = new RelayCommand(o => {
                 if (o is string section)
                 {
                     CurrentSection = section;
                 }
             });

             DeleteBlockCommand = new RelayCommand(o => {
                 EnsureInitialized();
                 if (o is Block block)
                 {
                     _repository!.Delete(block.Id);
                     Blocks.Remove(block);
                 }
             });
             
             // Subscribe to collection changes
             Blocks.CollectionChanged += Blocks_CollectionChanged;
             
             // Initialize data asynchronously when first accessed
             System.Threading.Tasks.Task.Run(() => EnsureInitialized());
        }

        private void EnsureInitialized()
        {
            if (_isInitialized) return;
            
            lock (this)
            {
                if (_isInitialized) return;
                
                _context = new LiteDbContext();
                _repository = new BlockRepository(_context);
                _isInitialized = true;
                
                // Load initial data
                System.Windows.Application.Current?.Dispatcher.Invoke(() =>
                {
                    LoadBlocksForSection();
                });
            }
        }

        private void Blocks_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Block block in e.NewItems)
                {
                    SubscribeToBlockChanges(block);
                }
            }
        }

        private void SubscribeToBlockChanges(Block block)
        {
            block.PropertyChanged += (s, e) =>
            {
                // Auto-save when any property changes
                if (s is Block changedBlock)
                {
                    _repository.Update(changedBlock);
                }
            };
        }

        private void LoadBlocksForSection()
        {
            if (!_isInitialized || _repository == null) return;
            
            Blocks.Clear();
            var items = _repository.GetAll()
                .Where(b => b.Section == CurrentSection)
                .OrderBy(b => b.Order)
                .Take(50);
            
            foreach(var item in items) 
            {
                Blocks.Add(item);
                SubscribeToBlockChanges(item);
            }
        }
        
        private void Search()
        {
            if (!_isInitialized || _repository == null) return;
            
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadBlocksForSection();
                return;
            }
            
            var results = _repository.Search(SearchText)
                .Where(b => b.Section == CurrentSection)
                .OrderBy(b => b.Order);
            Blocks.Clear();
            foreach(var item in results) 
            {
                Blocks.Add(item);
                SubscribeToBlockChanges(item);
            }
        }
    }
}
