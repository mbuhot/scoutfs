module scout.web.Startup

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Microsoft.EntityFrameworkCore
open scout.web.Models

let buildConfiguration (env: IHostingEnvironment) = 
    ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional = false, reloadOnChange = true)
        .AddJsonFile((sprintf "appsettings.%s.json" env.EnvironmentName), optional = true)
        .AddEnvironmentVariables()
        .Build()

let configureLogging (config: IConfigurationRoot) (loggerFactory: ILoggerFactory) = 
    let loggingConfigSection = config.GetSection("Logging")
    loggerFactory
        .AddConsole(loggingConfigSection)
        .AddDebug()
        |> ignore

let addDbService (services: IServiceCollection) = 
    services.AddDbContext<TodoContext>(fun dbopts -> dbopts.UseInMemoryDatabase() |> ignore)

let addMvcService (services: IServiceCollection) = 
    services.AddMvc()

let addServices (services: IServiceCollection) = 
    services
    |> addDbService
    |> addMvcService
    |> ignore

let configureHttpPipeline (app: IApplicationBuilder) = 
    app.UseMvc() |> ignore

type t(env: IHostingEnvironment) = 
    let configuration = buildConfiguration env
        
    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) : unit = 
        addServices services

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment, loggerFactory: ILoggerFactory) : unit = 
        configureLogging configuration loggerFactory
        configureHttpPipeline app
