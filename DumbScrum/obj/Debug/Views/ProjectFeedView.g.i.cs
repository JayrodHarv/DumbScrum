﻿#pragma checksum "..\..\..\Views\ProjectFeedView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E70D8A051DE9EDB473694B27A3C1BFDE30586941025BB294CFA5DE6369C049DB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DumbScrum.UserControls;
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
    /// ProjectFeedView
    /// </summary>
    public partial class ProjectFeedView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Views\ProjectFeedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbInputText;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\ProjectFeedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreatePost;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\ProjectFeedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblThing;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\ProjectFeedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxSprintFeed;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Views\ProjectFeedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl icFeedPosts;
        
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
            System.Uri resourceLocater = new System.Uri("/DumbScrum;component/views/projectfeedview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ProjectFeedView.xaml"
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
            
            #line 9 "..\..\..\Views\ProjectFeedView.xaml"
            ((DumbScrum.Views.ProjectFeedView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbInputText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.btnCreatePost = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Views\ProjectFeedView.xaml"
            this.btnCreatePost.Click += new System.Windows.RoutedEventHandler(this.btnCreatePost_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblThing = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.cbxSprintFeed = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\Views\ProjectFeedView.xaml"
            this.cbxSprintFeed.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbxSprintFeed_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.icFeedPosts = ((System.Windows.Controls.ItemsControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
