using Gizmo.Client.UI.SteamPayment.Module.Components;
using Gizmo.UI;
using Gizmo.Web.Components;

namespace Gizmo.Client.UI.SteamPayment.Module;

[ModuleGuid(KnownModules.MODULE_STEAM_PAYMENT)]
[UserMenuUIModule(TitleLocalizationKey = "GIZ_STEAM_PAYMENT_INTENT_TITLE")]
[ModuleDisplayOrder(1)]
[DialogItem(typeof(SteamPaymentIntentDialog))]
public class SteamPaymentModule : CustomDOMComponentBase { }