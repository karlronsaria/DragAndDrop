// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DragDropWindow
{
    public sealed partial class ItemCard : UserControl
    {
        public static readonly DependencyProperty CardBackgroundProperty = DependencyProperty.Register(
            nameof(CardBackground),
            typeof(string),
            typeof(ItemCard),
            new PropertyMetadata("#1f1f1f")
        );

        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register(
            nameof(ItemName),
            typeof(string),
            typeof(ItemCard),
            new PropertyMetadata(default(string))
        );

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description),
            typeof(string),
            typeof(ItemCard),
            new PropertyMetadata(default(string))
        );

        public static readonly DependencyProperty FrameRadiusProperty = DependencyProperty.Register(
            nameof(FrameRadius),
            typeof(int),
            typeof(ItemCard),
            new PropertyMetadata(0)
        );

        public static readonly DependencyProperty FrameFillColorProperty = DependencyProperty.Register(
            nameof(FrameFillColor),
            typeof(string),
            typeof(ItemCard),
            new PropertyMetadata($"#ffffff")
        );

        public static readonly DependencyProperty FrameImageProperty = DependencyProperty.Register(
            nameof(FrameImage),
            typeof(string),
            typeof(ItemCard),
            new PropertyMetadata(default(string))
        );

        public ItemCard()
        {
            this.InitializeComponent();
        }

        public string CardBackground
        {
            get => (string)GetValue(CardBackgroundProperty);
            set => SetValue(CardBackgroundProperty, value);
        }

        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public int FrameRadius
        {
            get => (int)GetValue(FrameRadiusProperty);
            set => SetValue(FrameRadiusProperty, value);
        }

        public string FrameFillColor
        {
            get => (string)GetValue(FrameFillColorProperty);
            set => SetValue(FrameFillColorProperty, value);
        }

        public string FrameImage
        {
            get => (string)GetValue(FrameImageProperty);
            set => SetValue(FrameImageProperty, value);
        }
    }
}



