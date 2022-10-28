using System;
using System.Net;
using HospitalAPI.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace HospitalAPI.ErrorHandling
{
    public static class ErrorHandlingMiddleware
    {
            public static void ConfigureGlobalErrorHandling(this IApplicationBuilder app)
            {
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                int statusCode = (int)HttpStatusCode.InternalServerError;
                                var exception = context.Features.Get<IExceptionHandlerFeature>();

                                if (exception == null) return;
                                    
                                if (exception.Error.GetType().IsSubclassOf(typeof(BaseException)))
                                {
                                    Console.WriteLine(exception.ToString());
                                    BaseException baseEx = (BaseException)exception.Error;
                                    statusCode = baseEx.StatusCode;
                                }

                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = statusCode;
                                
                                StandardErrorResponse respBody = new StandardErrorResponse { Message = exception.Error.Message, StatusCode = statusCode };
                                
                                await context.Response.WriteAsync( respBody.ToString() );
                                await context.Response.CompleteAsync();

                            }
                        );
                    }
                );
            }
        }
}