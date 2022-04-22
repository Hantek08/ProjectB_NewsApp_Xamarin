#define UseNewsApiSample  // Remove or undefine to use your own code to read live data

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Net.Http.Json;
using News.Models;
using News.ModelsSampleData;
using System.Collections.Generic;

namespace News.Services
{
    public class NewsService
    {

        public EventHandler<string> NewsAvailable;
        
        HttpClient httpClient = new HttpClient(); 
    
       readonly string apiKey = "d318329c40734776a014f9d9513e14ae";
      
        public async Task<NewsGroup> GetNewsAsync(NewsCategory category)
        {
            
           // NewsApiData nd = await NewsApiSampleData.GetNewsApiSampleAsync(category);

            var uri = $"https://newsapi.org/v2/top-headlines?country=se&category={category}&apiKey={apiKey}";

           HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

          NewsApiData nd = await response.Content.ReadFromJsonAsync<NewsApiData>();


            NewsGroup news = new NewsGroup();

            news.Articles = new List<NewsItem>();

            nd.Articles.ForEach(a => { news.Articles.Add(GetNewsItem(a)); });

            OnNewsAvailable($"News in category availble:{category}");


            return news;
            
        

        }
       

        protected virtual void OnNewsAvailable(string c)
        {
            NewsAvailable?.Invoke(this, c);
        }

        private NewsItem GetNewsItem(Article wdListItem)
        {
            NewsItem newsitem = new NewsItem();

            newsitem.DateTime = wdListItem.PublishedAt;

            newsitem.Title = wdListItem.Title;

            newsitem.UrlToImage = wdListItem.UrlToImage;    
           
            newsitem.Url = wdListItem.Url;
            newsitem.Description = wdListItem.Description;  

            return newsitem;

        }



    }
}

