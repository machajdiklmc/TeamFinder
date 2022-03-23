using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration.UserSecrets;
using TeamFinder.Client;
using TeamFinder.Client.Repository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
args.ToList().ForEach(Console.WriteLine);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("TeamFinder.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TeamFinder.ServerAPI"));

builder.Services.AddApiAuthorization();
builder.Services.AddMatBlazor();
builder.Services.AddTransient<UserRepository>();
/*builder.Services.AddSingleton(p =>
    {
        var config = p.GetService<IConfiguration>();
        var a = config.GetSection("App").Get<Configuration>();
        return a;
    });*/
//string serverlessBaseURI = builder.Configuration["ServerlessBaseURI"];
await builder.Build().RunAsync();