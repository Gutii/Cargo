using Cargo.Contract.Common;
using IDeal.Common.Components;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace Cargo.ServiceHost
{
    public class MessageInjectorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            try
            {
                string authHeader = context.HttpContext.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Bearer"))
                {
                    var authenticatedMessageObj = context
                        .ActionArguments
                        .Values
                        .SingleOrDefault(a => a.GetType().GetInterfaces().Any(t => t == typeof(IAuthenticatedMessage) || t == typeof(IContragentSpecific)));

                    int? contragentId = null;
                    var identity = (ClaimsIdentity)context.HttpContext.User.Identity;

                    Claim claim = identity.Claims.FirstOrDefault(c => c.Type == "contragentId");

                    if (claim != null && !string.IsNullOrEmpty(claim.Value))
                    {
                        int agentId = 0;
                        if (int.TryParse(claim.Value, out agentId))
                        {
                            contragentId ??= agentId;
                        }
                    }

                    int customerId = 55;
                    claim = identity.Claims.FirstOrDefault(c => c.Type == "customerId");
                    if (claim != null && !string.IsNullOrEmpty(claim.Value))
                    {
                        var t = 0;
                        if (int.TryParse(claim.Value, out t))
                        {
                            customerId = t;
                        }
                    }

                    string lang = "EN";
                    claim = identity.Claims.FirstOrDefault(c => c.Type == "lang");
                    if (claim != null && !string.IsNullOrEmpty(claim.Value)) lang = claim.Value;


                    var role = identity.Claims.FirstOrDefault(c => c.Type == "selectedRoleNameEn");

                    if (authenticatedMessageObj is IAuthenticatedMessage authenticatedMessage)
                    {
                        authenticatedMessage.SelectedRoleNameEn = role?.Value;
                        authenticatedMessage.AgentId = role?.Value != Role.Carrier.Value ? contragentId : null;
                        authenticatedMessage.CustomerId = customerId;
                        authenticatedMessage.CarrierId = customerId;
                        authenticatedMessage.Language = lang;
                    }

                    if (authenticatedMessageObj is IContragentSpecific contragentSpecific)
                    {
                        if (contragentSpecific.ContragentId == 0 && contragentId.HasValue)
                            contragentSpecific.ContragentId = contragentId.Value;
                    }

                }


            }
            catch
            { }
        }
    }
}