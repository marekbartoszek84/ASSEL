using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Assel.Contacts.WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result, Func<IActionResult> onSuccess, Func<Result, IActionResult> onFailure)
        {
            return result.Finally((Result res) => (!res.IsSuccess) ? onFailure(res) : onSuccess());
        }

        public static IActionResult ToActionResult<T>(this Result<T> result, Func<T, IActionResult> onSuccess, Func<Result, IActionResult> onFailure)
        {
            return result.Finally((Result<T> res) => (!res.IsSuccess) ? onFailure(result) : onSuccess(result.Value));
        }
    }
}
