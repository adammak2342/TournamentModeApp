﻿#pragma checksum "E:\Users\Marci\OneDrive\WindowsApp2\WindowsApp2\Views\Shell.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A415DEE9DA0B3E3C6D2323AAB0335B86"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsApp2.Views
{
    partial class Shell : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.ViewModel = (global::WindowsApp2.ViewModels.MainPageViewModel)(target);
                }
                break;
            case 2:
                {
                    this.MyHamburgerMenu = (global::Template10.Controls.HamburgerMenu)(target);
                }
                break;
            case 3:
                {
                    this.LoginButton = (global::Template10.Controls.HamburgerButtonInfo)(target);
                }
                break;
            case 4:
                {
                    this.SettingsButton = (global::Template10.Controls.HamburgerButtonInfo)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

