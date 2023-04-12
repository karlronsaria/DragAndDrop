// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DragAndDrop
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public List<Item> Items = new();
        public List<Item> SelectedItems = new();

        public MainWindow()
        {
            this.InitializeComponent();

            // todo: test content
            for (int i = 0; i < 500; i++)
            {
                Items.Add(new Item() { Name = "Sus", Description = "A sus." });
                Items.Add(new Item() { Name = "Ihr", Description = "An ihr." });
                Items.Add(new Item() { Name = "Oth", Description = "An oth." });
            }

            gridView.ItemsSource = Items;
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }

        private async void listView_Drop(object sender, DragEventArgs e)
        {
            bool hasText = e.DataView.Contains(StandardDataFormats.Text);
            Debug($"Has text: {hasText}");

            if (hasText)
            {
                var itemId = int.Parse(await e.DataView.GetTextAsync());
                Debug($"Item ID: {itemId}");
                SelectedItems.Add(Item.All[itemId]);
                e.AcceptedOperation = DataPackageOperation.Copy;
                listView.ItemsSource = null;
                listView.ItemsSource = SelectedItems;
            }
        }

        private void gridView_DragOver(object sender, DragEventArgs e)
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
                e.Data.SetText($"{(e.Items[0] as Item).Id}");
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
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        private static int _numberItems = 0;
        public static readonly List<Item> All = new();

        public Item()
        {
            Id = Item._numberItems;
            Item.All.Add(this);
            Item._numberItems++;
            Color = $"#{new Random().Next(0, 0xFFFFFF):X6}";
        }
    }
}

