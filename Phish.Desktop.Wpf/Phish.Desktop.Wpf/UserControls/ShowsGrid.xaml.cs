using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Phish.ViewModels;

namespace Phish.Desktop.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ShowsGrid.xaml
    /// </summary>
    public partial class ShowsGrid : UserControl
    {
        public ShowsGrid()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ShowsProperty =
            DependencyProperty.Register("Shows", typeof(ObservableCollection<ShowViewModel>),
                typeof(ShowsGrid), new UIPropertyMetadata(null));

        public ObservableCollection<ShowViewModel> Shows
        {
            get => (ObservableCollection<ShowViewModel>)GetValue(ShowsProperty);
            set => SetValue(ShowsProperty, value);
        }
    }
}
