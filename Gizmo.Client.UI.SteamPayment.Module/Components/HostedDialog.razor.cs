using Gizmo.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Gizmo.Client.UI.SteamPayment.Module.Components
{
    public partial class HostedDialog : CustomDOMComponentBase
    {
        #region PROPERTIES

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        #endregion

        protected void OnClickDialogHandler(MouseEventArgs args)
        {
        }
    }
}
