using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // بررسی ModelState
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Any())
                .Select(x => new
                {
                    field = x.Key,
                    messages = x.Value.Errors.Select(e => e.ErrorMessage)
                });

            context.Result = new JsonResult(new
            {
                status = 400,
                message = "خطای اعتبارسنجی ورودی",
                errors
            })
            {
                StatusCode = 400
            };
        }
        else
        {
            // بررسی ورودی‌هایی که اصلاً Bind نشدن (مثلاً Guid نامعتبر)
            foreach (var kvp in context.ActionArguments)
            {
                if (kvp.Value == null)
                {
                    context.Result = new JsonResult(new
                    {
                        status = 400,
                        message = $"پارامتر '{kvp.Key}' مقدار نامعتبری دارد. لطفاً مقدار صحیح ارسال کنید."
                    })
                    {
                        StatusCode = 400
                    };
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
