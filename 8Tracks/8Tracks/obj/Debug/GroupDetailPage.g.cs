﻿

#pragma checksum "C:\Users\Ian\Documents\Visual Studio 2012\Projects\8Tracks\8Tracks\GroupDetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "07BD8191056892777B030356D7CBA0DC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace _8Tracks
{
    partial class GroupDetailPage : _8Tracks.Common.LayoutAwarePage, IComponentConnector
    {
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 62 "..\..\GroupDetailPage.xaml"
                ((Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.ItemView_ItemClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 91 "..\..\GroupDetailPage.xaml"
                ((Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.ItemView_ItemClick;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 45 "..\..\GroupDetailPage.xaml"
                ((Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


