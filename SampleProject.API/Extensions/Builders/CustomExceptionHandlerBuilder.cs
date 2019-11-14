using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SampleProject.API.Attributes;
using SampleProject.Common.Exceptions;
using SampleProject.Common.Utilities;

namespace SampleProject.API.Extensions.Builders
{
    public static class CustomExceptionHandlerBuilder
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature?.Error != null)
                    {
                        logger.LogError(contextFeature.Error.Message);

                        ValidationResultModel validationResultModel;

                        if (contextFeature.Error?.GetType() == typeof(CustomException))
                        {
                            var exception = (CustomException)contextFeature.Error;
                            context.Response.StatusCode = (int)exception.StatusCode;
                            validationResultModel = new ValidationResultModel(exception.Message);

                            foreach (var identityError in exception.IdentityErrors)
                            {
                                validationResultModel.Errors.Add(new ValidationError(
                                    field: identityError.Code, 
                                    message: identityError.Description));
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            validationResultModel = new ValidationResultModel("Oops. Something went wrong.");

                            var exceptions = ExceptionUtility.GetExceptions(contextFeature.Error);
                            foreach (var exception in exceptions)
                            {
                                validationResultModel.Errors.Add(new ValidationError(
                                    field: exception.GetType().FullName,
                                    message: exception.Message));
                            }
                        }

                        await context.Response.WriteAsync(validationResultModel.ToString());
                    }
                });
            });
        }
    }
}
