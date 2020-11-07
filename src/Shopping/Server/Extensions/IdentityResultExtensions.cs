using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string GetCompleteErrorMessage(this IdentityResult result)
        {
            List<string> errorList = result.GetErrorMessagesAsList();
            string resultError = errorList.Aggregate((sum, next) => sum += (next + Environment.NewLine));
            return resultError;
        }
        public static List<string> GetErrorMessagesAsList(this IdentityResult result)
        {
            List<string> errors = new List<string>();
            foreach (IdentityError error in result.Errors)
            {
                errors.Add($"[{error.Code}]: {error.Description}");
            }
            return errors;
        }
    }
}
