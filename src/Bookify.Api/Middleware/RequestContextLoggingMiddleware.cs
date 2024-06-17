﻿using Serilog.Context;

namespace Bookify.Api.Middleware
{
    public class RequestContextLoggingMiddleware(RequestDelegate next)
    {
        private const string CorrelationHeaderName = "X-Correlation-Id";

        public Task Invoke(HttpContext httpContext)
        {
            using (LogContext.PushProperty("CorrelationId", GetCorrelationId(httpContext)))
            {
                return next(httpContext);
            }
        }

        private static string GetCorrelationId(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue(CorrelationHeaderName, out var correlationId);

            return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }
    }
}
