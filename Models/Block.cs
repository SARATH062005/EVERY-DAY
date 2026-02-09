using LiteDB;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EveryDay.Models
{
    public abstract class Block : INotifyPropertyChanged
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Type { get; set; }
        public string Section { get; set; } = "Notes"; // Default section
        
        private int _order;
        public int Order 
        { 
            get => _order;
            set 
            {
                if (_order != value)
                {
                    _order = value;
                    OnPropertyChanged();
                }
            } 
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class TextBlock : Block
    {
        private string _content = "";
        public string Content 
        { 
            get => _content;
            set 
            { 
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged();
                }
            }
        }
        public TextBlock() { Type = "Text"; }
    }

    public class CheckboxBlock : Block
    {
        private string _content = "";
        private bool _isChecked;
        
        public string Content 
        { 
            get => _content;
            set 
            { 
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public bool IsChecked 
        { 
            get => _isChecked;
            set 
            { 
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged();
                }
            }
        }
        public CheckboxBlock() { Type = "Checkbox"; }
    }
    
    public class HeaderBlock : Block
    {
        private string _content = "";
        private bool _isChecked;

        public string Content 
        { 
            get => _content;
            set 
            { 
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsChecked 
        { 
            get => _isChecked;
            set 
            { 
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        public HeaderBlock() { Type = "Header"; }
    }
}
