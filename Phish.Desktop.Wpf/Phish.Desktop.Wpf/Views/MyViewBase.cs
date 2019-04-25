using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Unity;

namespace Phish.Desktop.Wpf.Views
{
    public class MyViewBase : UserControl
    {
        public IUnityContainer ViewContainer { get; set; }
       
    }
}
