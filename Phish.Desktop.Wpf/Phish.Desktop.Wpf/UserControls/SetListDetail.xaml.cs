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
using Phish.Desktop.Wpf.ViewModels;
using Phish.Domain;
using Phish.ViewModels;

namespace Phish.Desktop.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for SetListDetail.xaml
    /// </summary>
    public partial class SetListDetail : UserControl
    {
        public SetListDetail()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SetListProperty =
            DependencyProperty.Register("SetList", typeof(SetListViewModel),
                typeof(SetListDetail), new UIPropertyMetadata(null));

        public SetListViewModel SetList
        {
            get => (SetListViewModel)GetValue(SetListProperty);
            set => SetValue(SetListProperty, value);
        }
    }
}
