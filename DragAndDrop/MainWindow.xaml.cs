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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DragAndDrop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public const string INFILE_PATH =
            "items.json";
        public const string OUTFILE_PATH_PREFIX =
            "selecteditems";
        public const string OUTFILE_PATH_EXT =
            ".json";

        public List<Item> Items = new();
        public List<Item> SelectedItems = new();

        public MainWindow()
        {
            // string json = File.ReadAllText(inFilePath);
            // Items = JsonConvert.DeserializeObject<List<Item>>(json);

            this.InitializeComponent();

            // todo: test content
            for (int i = 0; i < 500; i++)
            {
                Items.Add(new Item() { Name = "Sus", Description = "A sus." });
                Items.Add(new Item() { Name = "Ihr", Description = "An ihr." });
                Items.Add(new Item() { Name = "Oth", Description = "An oth.", ImageSource = @"C:\pic\download\my\ProfilePhoto\kamenlou_2023_02_22_143540.jpg" });
            }

            gridView.ItemsSource = Items;
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            var outFilePath = $"{OUTFILE_PATH_PREFIX}_{DateTime.Now:yyyy_MM_dd_HHmmss}{OUTFILE_PATH_EXT}";
            var json = JsonConvert.SerializeObject(SelectedItems);
            File.WriteAllText(outFilePath , json);
            myButton.Content = "Saved";
            myButton.IsEnabled = false;
        }

        private async void listView_Drop(object sender, DragEventArgs e)
        {
            bool hasText = e.DataView.Contains(StandardDataFormats.Text);
            Debug($"Has text: {hasText}");

            if (hasText)
            {
                e.AcceptedOperation = DataPackageOperation.Copy;

                (await e.DataView.GetTextAsync())
                    .Split(',')
                    .Select(s => int.Parse(s))
                    .ToList()
                    .ForEach(itemId => {
                        SelectedItems.Add(Item.All[itemId]);
                    });

                listView.ItemsSource = null;
                listView.ItemsSource = SelectedItems;
            }
        }

        private void listView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = e.DataView.Contains(StandardDataFormats.Text)
                ? DataPackageOperation.Copy
                : DataPackageOperation.None;
        }

        private void gridView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Debug($"Has items: {e.Items.Count}");

            if (e.Items.Count > 0)
            {
                e.Data.SetText(string.Join(',', e.Items.Select(i => (i as Item).ItemId)));
                e.Data.RequestedOperation = DataPackageOperation.Copy;
            }
        }

        public void Debug(string message)
        {
            statusLine.Text = message;
        }
    }

    public class Item
    {
        public int ItemId { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string ImageSource { get; set; }

        private static int _numberItems = 0;
        public static readonly List<Item> All = new();

        public Item()
        {
            ItemId = Item._numberItems;
            Item.All.Add(this);
            Item._numberItems++;
            Color = $"#{new Random().Next(0, 0xFFFFFF):X6}";
        }
    }
}

