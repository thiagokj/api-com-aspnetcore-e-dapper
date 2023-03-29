using Store.Domain.StoreContext.Handlers;
using Store.Domain.StoreContext.Repositories;
using Store.Domain.StoreContext.Services;
using Store.Infra.StoreContext.DataContexts;
using Store.Infra.StoreContext.Repositories;
using Store.Infra.StoreContext.Services;
using Store.Shared;

namespace Store.Api.Extensions;

public static class AppExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // builder.Services.AddApplicationInsightsTelemetry(
        //     builder.Configuration.GetValue<string>("InstrumentationKey"));

        builder.Services.AddControllers();
        builder.Services.AddResponseCompression();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        Settings.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddScoped<DbDataContext, DbDataContext>();
        builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
        builder.Services.AddTransient<IEmailService, EmailService>();
        builder.Services.AddTransient<CustomerHandler, CustomerHandler>();
    }


}