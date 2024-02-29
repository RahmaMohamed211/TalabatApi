using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.core;
using Talabat.core.Repositiories;
using Talabat.core.Services;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.APIs.Exentations
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddAplicationServices( this IServiceCollection Services)
        {
            //services

            Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));

            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IUnitOfWork,UnitOfWork>();
            Services.AddScoped<IPaymentServices,PaymentService>();

           // Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

           Services.AddAutoMapper(typeof(MappingProfiles));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(P => P.Value.Errors)
                                                                                         .Select(E => E.ErrorMessage).ToArray();


                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };

            });
            return Services;
        }
    }
}
