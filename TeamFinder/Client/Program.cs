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
builder.Configuration.AddUserSecrets<BingOptions>();
builder.Services.AddHttpClient("TeamFinder.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

DotNetEnv.Env.Load("secrets.env");
var bingApiKey = DotNetEnv.Env.GetString("BING_API_KEY");
Console.WriteLine("!!@#!@$#!$!@!!!!!! " + bingApiKey);
// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TeamFinder.ServerAPI"));

builder.Services.AddApiAuthorization();
builder.Services.AddMatBlazor();
builder.Services.AddOptions<BingOptions>();
builder.Services.AddTransient<UserRepository>();
/*builder.Services.AddSingleton(p =>
    {
        var config = p.GetService<IConfiguration>();
        var a = config.GetSection("App").Get<Configuration>();
        return a;
    });*/
//string serverlessBaseURI = builder.Configuration["ServerlessBaseURI"];
await builder.Build().RunAsync();

public class BingOptions
{
    public string BingApiKey { get; set; }
}