using Adapter.Api.Adapter;
using Adapter.Api.SQLite;
using Adapter.Api.SQLite.DataAccess;
using Microsoft.Extensions.DependencyInjection;

var api = new AdapterApi(args, options =>
{
    options.AddScoped<ICertDbContext, CertDbContext>();
    options.AddScoped<ICertificateCreator, CertificateDataAdapterCreate>();
    options.AddScoped<ICertificateRetriever, CertificateDataAdapterRetrieve>();
    options.AddScoped<ICertificateRemover, CertificateDataAdapterRemove>();
    options.AddScoped<ICertificateUpdater, CertificateDataAdapterUpdate>();
    options.AddScoped<ICertificateService, ApiAdapter>();
});

await api.RunAsync();
