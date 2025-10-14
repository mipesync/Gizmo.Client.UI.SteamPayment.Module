using System.ComponentModel.DataAnnotations;
using Gizmo.UI;
using Gizmo.UI.View.States;
using Microsoft.Extensions.DependencyInjection;

namespace Gizmo.Client.UI.SteamPayment.Module.ViewStates
{
    [Register()]
    public sealed class SteamPaymentIntentViewState : ValidatingViewStateBase
    {
        #region PROPERTIES

        public bool IsEnabled { get; internal set; }

        public decimal MinimumAmount { get; internal set; }
        public decimal MaximumAmount { get; internal set; }

        [ValidatingProperty()]
        [Required(ErrorMessageResourceType = typeof(Resources.Properties.Resources), ErrorMessageResourceName = "GIZ_GEN_VE_REQUIRED_FIELD")]
        public decimal? Amount { get; internal set; }

        public bool IsLoading { get; internal set; }

        public bool HasError { get; internal set; }

        public string ErrorMessage { get; internal set; } = string.Empty;

        #endregion
    }
}
