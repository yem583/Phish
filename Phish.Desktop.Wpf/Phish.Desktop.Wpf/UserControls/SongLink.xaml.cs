using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for SongLink.xaml
    /// </summary>
    public partial class SongLink : UserControl
    {
        public SongLink()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SetListSongProperty =
            DependencyProperty.Register("SetListSong", typeof(SetListSongViewModel),
                typeof(SongLink), new UIPropertyMetadata(new PropertyChangedCallback((o, args) =>
                {
                    var songLink = o as SongLink;
                    songLink.CommaTextBlock.Visibility =
                        songLink != null && !songLink.SetListSong.RightIntoNextSong && !songLink.SetListSong.IsSetCloser
                            ? Visibility.Visible
                            : Visibility.Hidden;
                })));

        public SetListSongViewModel SetListSong
        {
            get => (SetListSongViewModel)GetValue(SetListSongProperty);
            set => SetValue(SetListSongProperty, value);
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var hl = (Hyperlink)sender;
            var process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = hl.NavigateUri.ToString();
            process.Start();
            e.Handled = true;
        }
    }
}
