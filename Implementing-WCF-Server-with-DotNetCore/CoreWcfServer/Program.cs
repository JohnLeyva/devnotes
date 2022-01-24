using CoreWCF;
using CoreWCF.Configuration;
using System.Diagnostics;
using WcfServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceModelServices();
builder.WebHost.UseKestrel(options =>
{
    options.ListenLocalhost(8188);
    options.Listen(address: System.Net.IPAddress.Loopback, 8443, listenOptions =>
    {
        if (Debugger.IsAttached)
        {
            listenOptions.UseConnectionLogging();
        }
    });
});
builder.WebHost.UseNetTcp(8189);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseServiceModel(builder =>
{
    void ConfigureSoapService<TService, TContract>(string serviceprefix) where TService : class
    {
        builder.AddService<TService>()
            .AddServiceEndpoint<TService, TContract>(new BasicHttpBinding(),
                "http://localhost:8188/basic")
            .AddServiceEndpoint<TService, TContract>(new WSHttpBinding(SecurityMode.None),
                "http://localhost:8188/wshttp")
            .AddServiceEndpoint<TService, TContract>(new NetTcpBinding(),
                "net.tcp://localhost:8189");
    }

    ConfigureSoapService<EchoService, Contract.IEchoService>(nameof(EchoService));
});


app.Run();
