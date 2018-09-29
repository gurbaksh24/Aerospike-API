using Aerospike.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TweetDataAPI.Models;

namespace TweetDataAPI.Controllers
{
    public class TweetsController : ApiController
    {
        // GET: api/Tweets/5
        [HttpPost]
        [Route("api/Tweets")]
        public List<Tweet> GetTweetById([FromBody] long[] tweetId)
        {
            try
            {
                var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                string nameSpace = "AirEngine";
                string setName = "GurbakshSingh";
                List<Tweet> tweetData = new List<Tweet>();
                foreach (long tid in tweetId)
                {
                    Record record = aerospikeClient.Get(new BatchPolicy(), new Key(nameSpace, setName, tid.ToString()));
                    Tweet tweet = new Tweet();
                    tweet.TweetId = Convert.ToInt64(record.GetValue("id").ToString());
                    tweet.TweetDescription = record.GetValue("text").ToString();
                    tweet.StatusSource = record.GetValue("statusSource").ToString();
                    tweetData.Add(tweet);
                }
                return tweetData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        // PUT: api/Tweets/5
        [HttpPut]
        [Route("api/Tweets/{tweetId}")]
        public void UpdateTweet(long tweetId, [FromBody] ModifyTweet modifyTweet)
        {
            try
            {
                var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                string nameSpace = "AirEngine";
                string setName = "GurbakshSingh";
                var key = new Key(nameSpace, setName, tweetId.ToString());
                aerospikeClient.Put(new WritePolicy(), key, new Bin[] { new Bin("text", modifyTweet.NewValue) });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                throw;
            }
        }

        // DELETE: api/Tweets/5
        [HttpDelete]
        [Route("api/Tweets/{tweetId}")]
        public void Delete(long tweetId)
        {
            try
            {
                var aerospikeClient = new AerospikeClient("18.235.70.103", 3000);
                string nameSpace = "AirEngine";
                string setName = "GurbakshSingh";
                var key = new Key(nameSpace, setName, tweetId.ToString());
                aerospikeClient.Delete(new WritePolicy(), key);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                throw;
            }
        }
    }
}
