using Microsoft.AspNetCore.Mvc;
using TaskMangmentSystem.API.Mapper;
using TaskMangmentSystem.Core.Interfaces;
using TaskMangmentSystem.Infrastructure.Repositories;

namespace TaskMangmentSystem.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200")
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
            }));
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("CorsPolicy",
            //        policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"); });
            //});
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value!.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
