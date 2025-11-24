using Gizmo.Client.UI.SteamPayment.Module.Components;
using Gizmo.UI;
using Gizmo.UI.Services;
using Gizmo.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Gizmo.Client.UI.SteamPayment.Module;

[UserMenuUIModule]
[ModuleDisplayOrder(1)]
public partial class SteamPaymentModule : CustomDOMComponentBase
{
    [Inject]
    private IDialogService DialogService { get; set; } = null!;
    
    [Inject]
    private IAssemblyResourcesLocalizationService AssemblyResourcesLocalizationService { get; set; } = null!;

    private async Task HandleClickAsync()
    {
        await DialogService.ShowDialogAsync<SteamPaymentIntentDialog>(new Dictionary<string, object>(), new DialogDisplayOptions()
        {
            Closable = true,
            CloseOnClick = false,
        }, null, CancellationToken.None);
    }
}