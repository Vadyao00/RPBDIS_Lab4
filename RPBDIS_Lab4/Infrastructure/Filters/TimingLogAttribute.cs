﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace RPBDIS_Lab4.Infrastructure.Filters
{
    //Фильтр ресурсов
    public class TimingLogAttribute(ILoggerFactory loggerFactory) : Attribute, IResourceFilter
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger("TimingResourceFilter");

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation($"Path - {context.HttpContext.Request.Path}");
            _logger.LogInformation($"OnResourceExecuted - {DateTime.Now}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation($"OnResourceExecuting - {DateTime.Now}");
        }
    }
}