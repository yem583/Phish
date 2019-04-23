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
using Prism.Commands;

namespace Phish.Desktop.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for StandardImageButton.xaml
    /// </summary>
    public partial class StandardImageButton : UserControl
    {
        public StandardImageButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(Double),
                typeof(StandardImageButton), new UIPropertyMetadata(125d));

        public Double ButtonWidth
        {
            get => (Double)GetValue(ButtonWidthProperty);
            set => SetValue(ButtonWidthProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(DelegateCommand),
                typeof(StandardImageButton), new UIPropertyMetadata(null));

        public DelegateCommand Command
        {
            get => (DelegateCommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(BitmapImage),
                typeof(StandardImageButton), new UIPropertyMetadata(null));

        public BitmapImage ImageSource
        {
            get => (BitmapImage)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
                typeof(StandardImageButton), new UIPropertyMetadata(null));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
