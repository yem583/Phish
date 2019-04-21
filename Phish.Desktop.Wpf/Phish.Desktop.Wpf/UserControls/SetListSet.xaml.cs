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
using Phish.ViewModels;

namespace Phish.Desktop.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for SetListSet.xaml
    /// </summary>
    public partial class SetListSet : UserControl
    {
        public SetListSet()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SetListSetModelProperty =
            DependencyProperty.Register("SetListSetModel", typeof(SetListSetModel),
                typeof(SetListSet), new UIPropertyMetadata(null));

        public SetListSetModel SetListSetModel
        {
            get => (SetListSetModel)GetValue(SetListSetModelProperty);
            set => SetValue(SetListSetModelProperty, value);
        }
    }
}
