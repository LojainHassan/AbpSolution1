using AbpSolution1.WorkFlow;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using static System.Net.WebRequestMethods;

namespace AbpSolution1;

[DependsOn(
    typeof(AbpSolution1DomainModule),
    typeof(AbpSolution1ApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class AbpSolution1ApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddSingleton<Zeebe.Client.IZeebeClient>(provider =>
        {
            // var gatewayAddress = configuration["Zeebe:GatewayAddress"] ?? "localhost:26500";
            var gatewayAddress = "213.6.249.126:8568";
            return Zeebe.Client.ZeebeClient.Builder()
                .UseGatewayAddress(gatewayAddress)
                .UsePlainText()
                .Build();
        });
        context.Services.AddScoped<IDocumentWorkflowAppService, DocumentWorkflowAppService>();

    }
}
