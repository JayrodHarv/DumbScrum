﻿#pragma checksum "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DEACF59BACCBF0CC38FCB4F18FEEB4F765EF9CA3516A78492B1C0F8B74820AA4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DumbScrum.ToolWindows;
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


namespace DumbScrum.ToolWindows {
    
    
    /// <summary>
    /// CreateNewTaskFileWindow
    /// </summary>
    public partial class CreateNewTaskFileWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbFilePrefix;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbFileName;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateFile;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/DumbScrum;component/toolwindows/createnewtaskfilewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
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
            
            #line 8 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
            ((DumbScrum.ToolWindows.CreateNewTaskFileWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbFilePrefix = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.tbFileName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnCreateFile = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
            this.btnCreateFile.Click += new System.Windows.RoutedEventHandler(this.btnCreateFile_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\ToolWindows\CreateNewTaskFileWindow.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

