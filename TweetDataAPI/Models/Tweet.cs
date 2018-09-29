using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TweetDataAPI.Models
{
    public class Tweet
    {
        public long TweetId { get; set; }
        public string TweetDescription { get; set; }
        public string StatusSource { get; set; }
    }
}