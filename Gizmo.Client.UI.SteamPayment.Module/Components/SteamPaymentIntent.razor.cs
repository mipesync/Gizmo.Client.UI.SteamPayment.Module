using Gizmo.Client.UI.Services.View.Services;
using Gizmo.Client.UI.SteamPayment.Module.ViewStates;
using Gizmo.UI.Services;
using Gizmo.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Gizmo.Client.UI.SteamPayment.Module.Components
{
    public partial class SteamPaymentIntent : CustomDOMComponentBase
    {
        [Inject]
        private ILocalizationService LocalizationService { get; set; } = null!;

        [Inject]
        private SteamPaymentIntentViewService SteamPaymentIntentViewService { get; set; } = null!;

        [Inject]
        private SteamPaymentIntentViewState ViewState { get; set; } = null!;

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClickPay { get; set; }

        private Task OnClickPayHandler(MouseEventArgs args)
        {
            return OnClickPay.InvokeAsync(args);
        }

        protected override void OnInitialized()
        {
            this.SubscribeChange(ViewState);

            base.OnInitialized();
        }

        public override void Dispose()
        {
            this.UnsubscribeChange(ViewState);

            base.Dispose();
        }
    }
}

