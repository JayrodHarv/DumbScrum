﻿#pragma checksum "..\..\..\Views\ProjectView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "01BC384D2664DC0F0E5CDC66B6AAE687009E69D267265F2A9A8B426783B914F0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DumbScrum.Views;
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


namespace DumbScrum.Views {
    
    
    /// <summary>
    /// ProjectView
    /// </summary>
    public partial class ProjectView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Views\ProjectView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnManage;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\ProjectView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnFeed;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\ProjectView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnSprints;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\ProjectView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnBacklog;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\ProjectView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton btnBoard;
        
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
            System.Uri resourceLocater = new System.Uri("/DumbScrum;component/views/projectview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ProjectView.xaml"
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
            this.btnManage = ((System.Windows.Controls.RadioButton)(target));
            
            #line 16 "..\..\..\Views\ProjectView.xaml"
            this.btnManage.Click += new System.Windows.RoutedEventHandler(this.btnManage_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnFeed = ((System.Windows.Controls.RadioButton)(target));
            
            #line 17 "..\..\..\Views\ProjectView.xaml"
            this.btnFeed.Click += new System.Windows.RoutedEventHandler(this.btnFeed_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnSprints = ((System.Windows.Controls.RadioButton)(target));
            
            #line 18 "..\..\..\Views\ProjectView.xaml"
            this.btnSprints.Click += new System.Windows.RoutedEventHandler(this.btnSprints_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnBacklog = ((System.Windows.Controls.RadioButton)(target));
            
            #line 19 "..\..\..\Views\ProjectView.xaml"
            this.btnBacklog.Click += new System.Windows.RoutedEventHandler(this.btnBacklog_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnBoard = ((System.Windows.Controls.RadioButton)(target));
            
            #line 20 "..\..\..\Views\ProjectView.xaml"
            this.btnBoard.Click += new System.Windows.RoutedEventHandler(this.btnBoard_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

