using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApi.Abstractions;
using WebApi.Models;
using WebApi.Mutations;
using WebApi.Queries;
using WebApi.Repo;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new ProductContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddMemoryCache();            

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IStoreRepository, StoreRepository>();
            builder.Services.AddTransient<IProductGroupRepository, ProductGroupRepository>();

            builder.Services.AddGraphQLServer()
                .AddQueryType<QueryDb>()
                .AddMutationType<ProductMutation>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // https://localhost:7006/graphQl/
            app.MapGraphQL();

            app.MapControllers();            
            

            app.Run();
        }
    }
}
