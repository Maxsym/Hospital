using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.API.Extensions;
using Hospital.BL.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Infrastructure
{
    public static class InvalidModelStateHandler
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}
