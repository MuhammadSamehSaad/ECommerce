using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            ///Old Way Probelm Code Duplication
            ///builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            ///builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            ///builder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

            //New Way to make add services gerneric
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddAutoMapper(typeof(MappingProfiles));

            #region Validation Error

            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //ModelState => Dic [KeyValuePair]
                    //Key => Name Of Parameters
                    //Value => Error

                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToList();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Error = errors
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };

            });
            #endregion

            return Services;

        }
    }
}
