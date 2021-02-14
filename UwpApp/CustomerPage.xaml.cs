﻿using DataAccess.Data;
using DataAccess.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            this.InitializeComponent();
        }
        private void NavigateToIssuePage()
        {
            Frame.Navigate(typeof(IssuePage));
        }
        private async void AddCustomer_Click( object sender, RoutedEventArgs e )
        {

            await SqliteContext.CreateCustomerAsync(new Customer { Name = tbxFirstName.Text + " " + Guid.NewGuid().ToString() });
            NavigateToIssuePage();

        }
    }
}
