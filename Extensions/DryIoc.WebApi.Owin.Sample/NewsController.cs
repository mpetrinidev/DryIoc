﻿using System.Web.Http;

namespace DryIoc.WebApi.Owin.Sample
{
    public class NewsController : ApiController
    {
        private IGetNews _getNews;
        private ILoggingService _logger;

        public NewsController(ILoggingService logger, IGetNews getNews)
        {
            _getNews = getNews;
            _logger = logger;
        }

        [HttpGet]
        public IHttpActionResult GetNews()
        {

            var newsItems = _getNews.News();

            if (newsItems == null || newsItems.Length == 0)
            {
                _logger.Error("News Items couldn't be loaded.");
                return NotFound();
            }

            return Ok(newsItems);
        }
    }

    public interface IGetNews
    {
        string[] News();
    }

    public interface ILoggingService
    {
        void Error(string message);
    }
}
