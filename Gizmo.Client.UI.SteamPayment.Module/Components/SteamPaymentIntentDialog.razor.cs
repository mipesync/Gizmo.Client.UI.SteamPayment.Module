using System.Linq.Expressions;
using Gizmo.UI;
using Gizmo.UI.Services;
using Gizmo.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Gizmo.Client.UI.SteamPayment.Module.Components
{
    public partial class SteamPaymentIntentDialog : CustomDOMComponentBase
    {
        [Inject]
        private IAssemblyResourcesLocalizationService LocalizationService { get; set; } = null!;

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

        // private async Task OnClickPayHandler()
        // {
        //     await ResultCallback.InvokeAsync();
        // }
        
        private decimal? _amount;
        private bool _isValid;
        private EditContext _editContext;
        private ValidationMessageStore _validationMessageStore;
        
        protected override void OnInitialized()
        {
            _editContext = new EditContext(typeof(SteamPaymentIntentDialog));
            _validationMessageStore = new ValidationMessageStore(_editContext);
            
            base.OnInitialized();
        }
        
        private async Task OnClickPayHandlerAsync()
        {
            await JsRuntime.InvokeVoidAsync("open", $@"https://payframe.ckassa.ru/?service=111-18559-338&amount={_amount * 100}");
            await ResultCallback.InvokeAsync();
        }
        
        private void SetAmount(decimal? value)
        {
            if (!value.HasValue)
            {
                _amount = null;
            }

            _amount = value;
            ValidateProperty(() => _amount);
        }
        
        private void ValidateProperty(Expression<Func<decimal?>> accessor)
        {
            var fieldIdentifier = FieldIdentifier.Create(accessor);
            _validationMessageStore.Clear(fieldIdentifier);
            
            if (fieldIdentifier.FieldEquals(() => _amount))
            {
                if (_amount < 70)
                {
                    _validationMessageStore.Add(fieldIdentifier, LocalizationService.GetLocalizedStringValue(
                        typeof(SteamPaymentIntentDialog), "GIZ_STEAM_PAYMENT_INTENT_MINIMUM_AMOUNT_ERROR"));
                    _isValid = false;
                }
                else if (_amount > 14999)
                {
                    _validationMessageStore.Add(fieldIdentifier, LocalizationService.GetLocalizedStringValue(
                        typeof(SteamPaymentIntentDialog), "GIZ_STEAM_PAYMENT_INTENT_MAXIMUM_AMOUNT_ERROR"));
                    _isValid = false;
                }
                else _isValid = true;
            }
            _editContext.NotifyValidationStateChanged();
        }
    }
}

