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
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace Phish.Desktop.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for StandardHeaderGridView.xaml
    /// </summary>
    public partial class StandardHeaderGridView : UserControl
    {
        public StandardHeaderGridView()
        {
            InitializeComponent();
        }

        #region ButtonVisibility



        public static readonly DependencyProperty AddNewButtonVisibilityProperty =
            DependencyProperty.Register("AddNewButtonVisibility", typeof(Visibility),
                typeof(StandardHeaderGridView), new UIPropertyMetadata(Visibility.Visible));

        public Visibility AddNewButtonVisibility
        {
            get => (Visibility)GetValue(AddNewButtonVisibilityProperty);
            set => SetValue(AddNewButtonVisibilityProperty, value);
        }

        #endregion

        public static readonly DependencyProperty PageHeaderTextProperty =
            DependencyProperty.Register("PageHeaderText", typeof(string),
                typeof(StandardHeaderGridView), new UIPropertyMetadata());

        public string PageHeaderText
        {
            get => (string)GetValue(PageHeaderTextProperty);
            set => SetValue(PageHeaderTextProperty, value);
        }

        public static readonly DependencyProperty WatermarkContentProperty =
            DependencyProperty.Register("WatermarkContent", typeof(string),
                typeof(StandardHeaderGridView), new UIPropertyMetadata());

        public string WatermarkContent
        {
            get => (string)GetValue(WatermarkContentProperty);
            set => SetValue(WatermarkContentProperty, value);
        }

        public static readonly DependencyProperty RadGridViewProperty =
            DependencyProperty.Register("RadGridView", typeof(RadGridView),
                typeof(StandardHeaderGridView), new UIPropertyMetadata(Target));

        private static void Target(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var headerGridView = d as StandardHeaderGridView;
            headerGridView.RadGridView.Filtered += (sender, args) => headerGridView.ClearFiltersCommand.RaiseCanExecuteChanged();
            headerGridView.ClearFiltersCommand.RaiseCanExecuteChanged();
        }

        public RadGridView RadGridView
        {
            get => (RadGridView)GetValue(RadGridViewProperty);
            set => SetValue(RadGridViewProperty, value);
        }

        #region Commands

        public static readonly DependencyProperty ReloadCommandProperty =
            DependencyProperty.Register("ReloadCommand", typeof(DelegateCommand),
                typeof(StandardHeaderGridView), new UIPropertyMetadata());

        public DelegateCommand ReloadCommand
        {
            get => (DelegateCommand)GetValue(ReloadCommandProperty);
            set => SetValue(ReloadCommandProperty, value);
        }

        private DelegateCommand _clearFiltersCommand;
        public DelegateCommand ClearFiltersCommand => _clearFiltersCommand ?? (_clearFiltersCommand = new DelegateCommand(ClearFiltersCommandExecute,
                                                          () => RadGridView != null && RadGridView.FilterDescriptors.Any()));

        private void ClearFiltersCommandExecute()
        {
            RadGridView.FilterDescriptors.SuspendNotifications();
            foreach (var column in RadGridView.Columns)
            {
                column.ClearFilters();
            }
            RadGridView.FilterDescriptors.ResumeNotifications();
        }

        public static readonly DependencyProperty AddNewCommandProperty =
            DependencyProperty.Register("AddNewCommand", typeof(DelegateCommand),
                typeof(StandardHeaderGridView), new UIPropertyMetadata());

        public DelegateCommand AddNewCommand
        {
            get => (DelegateCommand)GetValue(AddNewCommandProperty);
            set => SetValue(AddNewCommandProperty, value);
        }


        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(BitmapImage),
                typeof(StandardHeaderGridView), new UIPropertyMetadata(null));

        public BitmapImage Icon
        {
            get => (BitmapImage)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        #endregion
    }
}
