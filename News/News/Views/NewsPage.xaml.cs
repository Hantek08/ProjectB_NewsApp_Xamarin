using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using Xamarin.Forms;

using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using News.Models;
using News.Services;

namespace News.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        NewsService service;
        NewsGroup newsgroup;
        bool isTaskRunning;

        public NewsPage()
        {
            InitializeComponent();
            service = new NewsService();
            newsgroup = new NewsGroup();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Code here will run right before the screen appears
            headlines.Text = $"Todays {Title} Headlines";


            //This is making the first load of data

            MainThread.BeginInvokeOnMainThread(async () =>
             {
                 try
                 {
                     activityIndicator.IsRunning = true;
                     await Task.Delay(5000);
                     await LoadNews();
                 }

                 catch (Exception)
                 {
                     await DisplayAlert("Error", "No internet connection", "try again");
                 }
                 activityIndicator.IsRunning = false;

             });
        }

            private async Task LoadNews()
            {


                NewsCategory newsCat = (NewsCategory)Enum.Parse(typeof(NewsCategory), Title);
                await Task.Run(() =>
                {
                   
                        Task<NewsGroup> t1 = service.GetNewsAsync(newsCat);
                        Device.BeginInvokeOnMainThread(() =>
                       {
                           NewsListView.ItemsSource = t1.Result.Articles;
                       });
                    
                });
            }


            private async void RefreshPage(object sender, EventArgs args)
            {
                activityIndicator.IsRunning = true;
                await LoadNews();
                activityIndicator.IsRunning = false;

        }

        //
        private async void NewsListView_ItemTapped(object sender, ItemTappedEventArgs e)
            {
               var newsPage = (NewsItem)e.Item;
                await Navigation.PushAsync(new ArticleView(newsPage.Url));

        }


    }
    }


