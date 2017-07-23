module scout.web.Program

open System.IO
open Microsoft.AspNetCore.Hosting

[<EntryPoint>]
let main args = 
    WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup.t>()
        .Build()
        .Run()
    0
