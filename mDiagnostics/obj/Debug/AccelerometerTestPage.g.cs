﻿

#pragma checksum "E:\1_MYFOLDER\2_PROJECTS\WP\mDiagnostics\mDiagnostics\AccelerometerTestPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8539CE52EEDF71D0861AA851CCFCCFC3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mDiagnostics
{
    partial class AccelerometerTestPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 29 "..\..\AccelerometerTestPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ATPtorepeat_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 30 "..\..\AccelerometerTestPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ATPtonext_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 100 "..\..\AccelerometerTestPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ATPstrbtn_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 71 "..\..\AccelerometerTestPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerPressed += this.ATPback_PointerPressed;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


