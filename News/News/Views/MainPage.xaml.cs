﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using News.Services;
using News.Models;

namespace News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
 
    public partial class MainPage : TabbedPage
    {
        
        public MainPage()
        { 
          var  services = new NewsService();
            InitializeComponent();
            
           
            //TabbedPage = MenuItemsListView;

        }
    }
}