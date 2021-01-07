using App.Infrastructure.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace App.Infrastructure.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IOptions<ExceptionFilterOptions> _options;

        public CustomExceptionFilter(IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider,
            IOptions<ExceptionFilterOptions> options)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _options = options;
        }

        public void OnException(ExceptionContext context)
        {
            var acceptHeader = context.HttpContext.Request.GetTypedHeaders().Accept;
            var isJson = acceptHeader.Contains(new MediaTypeHeaderValue("application/json"));

            if (isJson)
                GetErrorJson(context);
            else
                GetErrorView(context);
        }

        protected void GetErrorView(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = _options.Value.ErrorViewName };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);

            if (_hostingEnvironment.IsDevelopment())
                result.ViewData.Add("Exception", context.Exception);

            context.ExceptionHandled = true;
            context.Result = result;
        }

        protected void GetErrorJson(ExceptionContext context)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while processing your request.",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = string.Empty
            };

            if (_hostingEnvironment.IsDevelopment())
                problemDetails.Detail = $@"{context.Exception.Message}{Environment.NewLine}{context.Exception.StackTrace}";

            var json = JsonSerializer.Serialize(problemDetails);
            context.Result = new ObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }
}
