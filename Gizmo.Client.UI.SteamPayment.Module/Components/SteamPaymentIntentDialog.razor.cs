using Gizmo.Client.UI.Services.View.Services;
using Gizmo.Client.UI.SteamPayment.Module.ViewStates;
using Gizmo.UI.Services;
using Gizmo.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Gizmo.Client.UI.SteamPayment.Module.Components
{
    public partial class SteamPaymentIntentDialog : CustomDOMComponentBase
    {
        [Inject]
        private ILocalizationService LocalizationService { get; set; } = null!;

        [Inject]
        private SteamPaymentIntentViewState ViewState { get; set; } = null!;

        [Inject]
        private SteamPaymentIntentViewService SteamPaymentIntentViewService { get; set; } = null!;

        [Parameter]
        public DialogDisplayOptions DisplayOptions { get; set; } = null!;

        [Parameter]
        public EventCallback DismissCallback { get; set; }

        [Parameter]
        public EventCallback<EmptyComponentResult> ResultCallback { get; set; }

        private async Task OnDismissHandler()
        {
            await DismissCallback.InvokeAsync();
        }

        private async Task OnClickPayHandler(decimal? amount)
        {
            await JsRuntime.InvokeVoidAsync("open", $@"https://payframe.ckassa.ru/?service=111-18559-338&amount={amount * 100}");

            await ResultCallback.InvokeAsync();
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

