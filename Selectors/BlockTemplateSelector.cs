using System.Windows;
using System.Windows.Controls;
using EveryDay.Models;

namespace EveryDay.Selectors
{
    public class BlockTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? TextTemplate { get; set; }
        public DataTemplate? CheckboxTemplate { get; set; }
        public DataTemplate? HeaderTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is CheckboxBlock) return CheckboxTemplate;
            if (item is HeaderBlock) return HeaderTemplate;
            return TextTemplate;
        }
    }
}
