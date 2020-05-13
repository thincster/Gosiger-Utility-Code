using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Windows.Controls.Wizard;

namespace Okuma_Monitor_Tools.Views
{

    /// <summary>
    /// Interaction logic for WizardView.xaml.
    /// </summary>
    public partial class WizardView : Window
    {

        public WizardView()
        {
            InitializeComponent();
        }
        //private void wizard_Cancel(object sender, RoutedEventArgs e)
        //{
        //    //if ((BrowserInteropHelper.IsBrowserHosted) || (!wizard.CancelButtonClosesWindow))
        //    MessageBox.Show("You clicked the Cancel button while on the '" + wizard.SelectedPage.Caption + "' page.", "Wizard Sample");
        //}
    }
}
