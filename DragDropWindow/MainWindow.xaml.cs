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

namespace DragDropWindow
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public const char ITEM_DELIM = ',';
        public delegate void OutputJsonType(string json);

        public string Json
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                _items = JsonConvert.DeserializeObject<List<Item>>(value);
                gridView.ItemsSource = _items;
            }
        }

        public OutputJsonType OutputJson { get; set; } = s => { };

        private void SaveSelection()
        {
            OutputJson(JsonConvert.SerializeObject(_selectedItems));
        }

        public MainWindow()
        {
            this.InitializeComponent();
            Closed += (s, e) => SaveSelection();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSelection();
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
                    .Split(ITEM_DELIM)
                    .Select(s => int.Parse(s))
                    .ToList()
                    .ForEach(itemId => {
                        _selectedItems.Add(Item.All[itemId]);
                    });

                listView.ItemsSource = null;
                listView.ItemsSource = _selectedItems;
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
                e.Data.SetText(string.Join(ITEM_DELIM, e.Items.Select(i => (i as Item).ItemId)));
                e.Data.RequestedOperation = DataPackageOperation.Copy;
            }
        }

        public void Debug(string message)
        {
            statusLine.Text = message;
        }

        private List<Item> _items = new();
        private readonly List<Item> _selectedItems = new();
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

