using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Filters
{
    //这是一个action filter!!!

    public class Ticket_EnsureEnteredDate:ActionFilterAttribute
    {
        //只是示例额，没写完整的. don't be so hard
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var player=context.ActionArguments["player"] as Player;
            if(player!=null&&player.Id==null)
            {
                context.ModelState.AddModelError("EnteredId", "The ID is required");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }

    }
}
