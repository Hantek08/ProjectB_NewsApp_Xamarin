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
            //You want to set the Title or set the City

            //This is making the first load of data

           MainThread.BeginInvokeOnMainThread(async () =>
            {
               try
               {

                    await LoadNews();

                }

                catch (Exception)
                {
                    await DisplayAlert("Page crashed", "No internet connection", "try again");
               }




            });
            //MainThread.BeginInvokeOnMainThread(async () => { await LoadNews(); });


        }
        private async Task LoadNews()
        {
            //Heare you load the forecast 
            //NewsGroup t1 = await service.GetNewsAsync(Title);
            NewsCategory nCat = (NewsCategory)Enum.Parse(typeof(NewsCategory), Title);
            await Task.Run(() =>
            {
                Task<NewsGroup> t1 = service.GetNewsAsync(nCat);
                Device.BeginInvokeOnMainThread(() =>
               {
                  NewsListView.ItemsSource = t1.Result.Articles;
                });

            });

          

            

        }

        private async void refresh(object sender, EventArgs args)
        {
            await LoadNews();
        }

        private async void NewsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var newsPage = (NewsItem)e.Item;
            await Navigation.PushAsync(new ArticleView(newsPage.Url));

        }
    }
}