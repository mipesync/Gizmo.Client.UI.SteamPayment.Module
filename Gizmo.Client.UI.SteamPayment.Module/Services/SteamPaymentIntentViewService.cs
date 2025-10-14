using Gizmo.Client.UI.SteamPayment.Module.ViewStates;
using Gizmo.Shared.Client.Options;
using Gizmo.UI;
using Gizmo.UI.Services;
using Gizmo.UI.View.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gizmo.Client.UI.Services.View.Services
{
    [Register]
    public class SteamPaymentIntentViewService : ValidatingViewStateServiceBase<SteamPaymentIntentViewState>
    {
        
        #region CONSTRUCTOR
        
        public SteamPaymentIntentViewService(SteamPaymentIntentViewState viewState,
            ILogger<SteamPaymentIntentViewService> logger,
            IServiceProvider serviceProvider,
            ILocalizationService localizationService,
            IGizmoClient gizmoClient,
            IOptions<SteamPaymentIntentOptions> steamPaymentIntentOptions) : base(viewState, logger, serviceProvider)
        {
            _localizationService = localizationService;
            _gizmoClient = gizmoClient;
            _steamPaymentIntentOptions = steamPaymentIntentOptions;
        }
        
        #endregion

        #region FIELDS
        
        private readonly ILocalizationService _localizationService;
        private readonly IGizmoClient _gizmoClient;
        private readonly IOptions<SteamPaymentIntentOptions> _steamPaymentIntentOptions;
        
        #endregion

        #region FUNCTIONS

        public void SetAmount(decimal? value)
        {
            if (!value.HasValue)
            {
                ViewState.Amount = null;
            }

            ViewState.Amount = value;
            ValidateProperty(() => ViewState.Amount);
        }

        #endregion

        #region OVERRIDES

        protected override Task OnInitializing(CancellationToken ct)
        {
            _gizmoClient.LoginStateChange += OnLoginStateChange;
            return base.OnInitializing(ct);
        }

        protected override void OnDisposing(bool isDisposing)
        {
            _gizmoClient.LoginStateChange -= OnLoginStateChange;
            base.OnDisposing(isDisposing);
        }

        private void OnLoginStateChange(object? sender, UserLoginStateChangeEventArgs e)
        {
            if (e.State == LoginState.LoggedIn)
            {
                ViewState.IsEnabled = !_steamPaymentIntentOptions.Value.Disabled;
            }
        }

        protected override void OnValidate(FieldIdentifier fieldIdentifier, ValidationTrigger validationTrigger)
        {
            if (fieldIdentifier.FieldEquals(() => ViewState.Amount))
            {
                if (ViewState.Amount < _steamPaymentIntentOptions.Value.MinimumAmount)
                {
                    AddError(() => ViewState.Amount, _localizationService.GetString("GIZ_STEAM_PAYMENT_INTENT_MINIMUM_AMOUNT_ERROR", _steamPaymentIntentOptions.Value.MinimumAmount));
                }
                else if (ViewState.Amount > _steamPaymentIntentOptions.Value.MaximumAmount)
                {
                    AddError(() => ViewState.Amount, _localizationService.GetString("GIZ_STEAM_PAYMENT_INTENT_MAXIMUM_AMOUNT_ERROR", _steamPaymentIntentOptions.Value.MaximumAmount));
                }
            }
        }

        #endregion
    }
}
