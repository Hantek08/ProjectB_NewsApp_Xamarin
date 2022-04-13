using System.Web;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using News.Models;
using News.Services;


namespace News.Views
{
    public partial class ArticleView : ContentPage
    {
        //Here is where you show the news in Full page
        NewsService service;
        NewsGroup newsgroup;
       
        public ArticleView()
        {
            InitializeComponent();
            service = new NewsService();
            newsgroup = new NewsGroup();


            
        }

       
        public ArticleView(string Url)
        {
            InitializeComponent();
            BindingContext = new UrlWebViewSource
            {
                Url = HttpUtility.UrlDecode(Url)
            };

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Code here will run right before the screen appears
            //You want to set the Title or set the City

            //This is making the first load of data

            MainThread.BeginInvokeOnMainThread(async () => { await LoadNews(); });

        }


            private async Task LoadNews()
        {
            //Heare you load the forecast 
            NewsGroup t1 = await service.GetNewsAsync(Title);



            {

                //t1.Items.ForEach(x => x.Icon = $"http://openweathermap.org/img/wn/{x.Icon}@2x.png");


                NewsListView.ItemsSource = t1.Articles.GroupBy(x => x.Title);

                //NewsListView.BindingContext = t1.Articles.GroupBy(x => x.Title);
            }

        }
       
        private async void refresh(object sender,EventArgs args)
        {
            await LoadNews();
      }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {

            if (e.Url != null)
            {
                NewsListView.SelectedItem= null;
                Navigation.PushAsync(new ArticleView
                {
                    BindingContext = e.Url
                });
            }

        }
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
           
            if (args.SelectedItem != null)
            {
                NewsListView.SelectedItem = null;
                await Navigation.PushAsync(new ArticleView
                {
                    BindingContext = args.SelectedItem
                });
            }
        }

    }
   
}
    

