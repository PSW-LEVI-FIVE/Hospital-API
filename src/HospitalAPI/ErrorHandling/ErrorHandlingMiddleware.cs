using System;
using System.Net;
using HospitalAPI.ErrorHandling;
using HospitalAPI.ErrorHandling.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace HospitalAPI
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

                                

                                if (exception != null)
                                {
                                    
                                    if (exception.Error.GetType().IsSubclassOf(typeof(BaseException)))
                                    {
                                        BaseException baseEx = (BaseException)exception.Error;
                                        statusCode = baseEx.StatusCode;
                                    }

                                    context.Response.ContentType = "application/json";
                                    context.Response.StatusCode = statusCode;
                                    StandardErrorResponse respBody = new StandardErrorResponse { Message = exception.Error.Message, StatusCode = statusCode };
                                    
                                    Console.WriteLine("Printing status code: " + respBody.StatusCode);
                                    Console.WriteLine("Status code for response: " + context.Response.StatusCode);
                                    await context.Response.WriteAsync(
                                        respBody.ToString()
                                    );
                                    await context.Response.CompleteAsync();

                                }
                            }
                        );
                    }
                );
            }
        }
}