﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DragAndDrop
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public const string INFILE_PATH =
            @"C:\temp\DragAndDrop\items.json";
        // "items.json";
        public const string OUTFILE_PATH_PREFIX =
            @"C:\temp\DragAndDrop\selecteditems";
        public const string OUTFILE_PATH_EXT =
            ".json";
        public const string EMPTY_JSON = "[]";

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new DragDropWindow.MainWindow()
            {
                Json = File.Exists(INFILE_PATH)
                    ? File.ReadAllText(INFILE_PATH)
                    : "",
                OutputJson = json =>
                {
                    if (json == EMPTY_JSON)
                        return;

                    var outFilePath = $"{OUTFILE_PATH_PREFIX}_{DateTime.Now:yyyy_MM_dd_HHmmss}{OUTFILE_PATH_EXT}";
                    File.WriteAllText(outFilePath, json);
                }
            };

            m_window.Activate();
        }

        private Window m_window;
    }
}
