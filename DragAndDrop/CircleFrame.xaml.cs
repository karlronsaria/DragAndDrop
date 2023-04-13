// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Streaming.Adaptive;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DragAndDrop
{
    public sealed partial class CircleFrame : UserControl
    {
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius),
            typeof(int),
            typeof(CircleFrame),
            new PropertyMetadata(0)
        );

        public static readonly DependencyProperty FillColorProperty = DependencyProperty.Register(
            nameof(FillColor),
            typeof(string),
            typeof(CircleFrame),
            new PropertyMetadata($"#ffffff")
        );

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            nameof(ImageSource),
            typeof(string),
            typeof(CircleFrame),
            new PropertyMetadata(default(string))
        );

        public CircleFrame()
        {
            this.InitializeComponent();
        }

        public int Radius
        {
            get => (int)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public string FillColor
        {
            get => (string)GetValue(FillColorProperty);

            set
            {
                if (value == null || value == "")
                    return;

                SetValue(FillColorProperty, value);
                mainEllipse.Fill = GetSolidColorBrush(value);
            }
        }

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);

            set
            {
                if (value == null || value == "")
                    return;

                SetValue(ImageSourceProperty, value);

                mainEllipse.Fill = new ImageBrush()
                {
                    ImageSource = new BitmapImage()
                    {
                        UriSource = new Uri(value)
                    }
                };
            }
        }

        // link
        // - url: http://joeljoseph.net/converting-hex-to-color-in-universal-windows-platform-uwp/
        // - retrieved: 2023_04_12
        // - note: I shouldn't have to do this. I SHOULD NOT HAVE TO DO THIS!! THIS IS STUPID!!
        public static SolidColorBrush GetSolidColorBrush(string hex)  
        {
            hex = hex.Replace("#", string.Empty);
            byte a;
            int start;

            switch (hex.Length)
            {
                case 6:
                    a = 255;
                    start = 0;
                    break;
                case 8:
                    a = (byte)(Convert.ToUInt32(hex[..2], 16));
                    start = 2;
                    break;
                default:
                    return new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            }

            byte r = (byte)Convert.ToUInt32(hex.Substring(start, 2), 16);
            byte g = (byte)Convert.ToUInt32(hex.Substring(start + 2, 2), 16);
            byte b = (byte)Convert.ToUInt32(hex.Substring(start + 2, 2), 16);
            return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
        }
    }
}
