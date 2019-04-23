using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.GridView.SearchPanel;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace Phish.Desktop.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty WatermarkContentProperty =
         DependencyProperty.Register("WatermarkContent", typeof(string),
             typeof(SearchBox), new UIPropertyMetadata(null));

        public string WatermarkContent
        {
            get => (string)GetValue(WatermarkContentProperty);
            set => SetValue(WatermarkContentProperty, value);
        }


        public static readonly DependencyProperty RadGridViewProperty =
            DependencyProperty.Register("RadGridView", typeof(RadGridView),
                typeof(SearchBox), new UIPropertyMetadata(null));

        public RadGridView RadGridView
        {
            get => (RadGridView)GetValue(RadGridViewProperty);
            set => SetValue(RadGridViewProperty, value);
        }

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string),
                typeof(SearchBox), new UIPropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var searchBox = d as SearchBox;


            if (!string.IsNullOrWhiteSpace(e.NewValue?.ToString()))
            {
                var searchBytextCommand = RadGridViewCommands.SearchByText as RoutedUICommand;
                searchBytextCommand.Execute(e.NewValue, searchBox.RadGridView);
            }
            else
            {
                var clearSearchValue = GridViewSearchPanelCommands.ClearSearchValue as RoutedUICommand;
                clearSearchValue.Execute(null, searchBox.RadGridView.ChildrenOfType<GridViewSearchPanel>().FirstOrDefault());

            }


        }
        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        private DelegateCommand _deleteCommand;

        public DelegateCommand DeleteCommand => _deleteCommand ??
                                                (_deleteCommand =
                                                    new DelegateCommand(DeleteCommandExecute, () => true));

        private void DeleteCommandExecute()
        {
            SearchText = null;
        }
    }
}
