// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DragAndDrop
{
    public sealed partial class CircleFrame : UserControl
    {
        public static readonly DependencyProperty FillColorProperty = DependencyProperty.Register(
            nameof(FillColor),
            typeof(string),
            typeof(CircleFrame),
            new PropertyMetadata($"#ffffff")
        );

        public CircleFrame()
        {
            this.InitializeComponent();
        }

        public string FillColor
        {
            get => (string)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        // link
        // - url: http://joeljoseph.net/converting-hex-to-color-in-universal-windows-platform-uwp/
        // - retrieved: 2023_04_12
        // - note: I shouldn't have to do this. I SHOULD NOT HAVE TO DO THIS!! THIS IS STUPID!!
        public static SolidColorBrush GetSolidColorBrush(string hex)  
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex[..2], 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            var myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }
    }
}
