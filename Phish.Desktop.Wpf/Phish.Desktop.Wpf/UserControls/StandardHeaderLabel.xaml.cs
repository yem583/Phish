using System;
using System.Collections.Generic;
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

namespace Phish.Desktop.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for StandardHeaderLabel.xaml
    /// </summary>
    public partial class StandardHeaderLabel : UserControl
    {
        public StandardHeaderLabel()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PageHeaderTextProperty =
            DependencyProperty.Register("PageHeaderText", typeof(string),
                typeof(StandardHeaderLabel), new UIPropertyMetadata(null));

        public string PageHeaderText
        {
            get => (string)GetValue(PageHeaderTextProperty);
            set => SetValue(PageHeaderTextProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(BitmapImage),
                typeof(StandardHeaderLabel), new UIPropertyMetadata(null));

        public BitmapImage Icon
        {
            get => (BitmapImage)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}
