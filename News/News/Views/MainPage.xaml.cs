using System;
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
          
            InitializeComponent();

            //this.Children.Add(new ArticleView());
            //Routing.RegisterRoute("articleview" ,typeof(NewsPage));
            

        }

        //private void CurrentPageHasChanged(object sender, EventArgs e) => Title = CurrentPage.Title;

        //protected override void OnAppearing()
        //{
        //   base.OnAppearing();

        //   //Code here will run right before the screen appears
        //   //You want to set the Title or set the City

        //    //This is making the first load of data
           

        //    Title = CurrentPage.Title;

        //}

       

    }







}