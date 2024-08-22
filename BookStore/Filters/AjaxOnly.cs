using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BookStore.Filters;

public class AjaxOnly : ActionMethodSelectorAttribute
{
    public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
    {
        var request = routeContext.HttpContext.Request;
        bool isAjax = request.Headers.XRequestedWith == "XMLHttpRequest";
        return isAjax;
    }
}
