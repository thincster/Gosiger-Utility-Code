﻿#pragma checksum "..\..\WizardWindow1.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2F4F70181A8E91B1192612AB721BDDB5DD9D152EB80B13010354CE9653A1B2F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ActiproSoftware.Products.Shared;
using ActiproSoftware.Products.Wizard;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.ColorSelection;
using ActiproSoftware.Windows.Controls.Primitives;
using ActiproSoftware.Windows.Controls.Rendering;
using ActiproSoftware.Windows.Controls.Wizard;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.Windows.Data.Filtering;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Media.Animation;
using ActiproSoftware.Windows.Shapes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Okuma_Monitor_Tools {
    
    
    /// <summary>
    /// WizardWindow1
    /// </summary>
    public partial class WizardWindow1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\WizardWindow1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ActiproSoftware.Windows.Controls.Wizard.Wizard wizard;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\WizardWindow1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ActiproSoftware.Windows.Controls.Wizard.WizardPage welcomePage;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\WizardWindow1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ActiproSoftware.Windows.Controls.Wizard.WizardPage interiorPage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Okuma Monitor Tools;component/wizardwindow1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WizardWindow1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.wizard = ((ActiproSoftware.Windows.Controls.Wizard.Wizard)(target));
            return;
            case 2:
            this.welcomePage = ((ActiproSoftware.Windows.Controls.Wizard.WizardPage)(target));
            return;
            case 3:
            this.interiorPage = ((ActiproSoftware.Windows.Controls.Wizard.WizardPage)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

