using Gizmo.Client.UI.View.States;
using Gizmo.UI;
using Gizmo.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Gizmo.Client.UI.SteamPayment.Module.Components;

[ModuleGuid("928E77E0-227C-4184-AABA-3D82982E8F68")]
[UserMenuUIModule(Title = "Food", Description = "Food page")]
[ModuleDisplayOrder(int.MinValue)]
public partial class SteamPaymentModule : CustomDOMComponentBase
{
    [Inject]
    public UserMenuViewState UserMenuViewState { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    protected override void OnInitialized()
    {
        //this.SubscribeChange(UserMenuViewState);

        base.OnInitialized();
    }

    public override void Dispose()
    {
        //this.UnsubscribeChange(UserMenuViewState);

        base.Dispose();
    }
}