using System;
using System.Collections.Generic;
using System.Text;
using News.Models;
using News.Services;
namespace News.Views
{
    public class MainpageTappedMenuItem
    {
        public MainpageTappedMenuItem(string title, NewsCategory category)
        {
            this.Title = title;
            this.Category = category;
            TargetType = typeof(MainpageTappedMenuItem);
        }
        public NewsCategory Category { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
