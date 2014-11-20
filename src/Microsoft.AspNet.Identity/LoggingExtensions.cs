using Microsoft.Framework.Logging;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public static class LoggingExtensions
    {
        public static async Task<IdentityResult> LogResultAsync<TUser>(this ILogger logger, IdentityResult result, TUser user, UserManager<TUser> userManager,
            LogLevel logLevel = LogLevel.Information, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
            where TUser : class
        {
            if (result.Succeeded)
            {
                InvokeLoggerMethod(logger, logLevel, string.Format(Resources.LogIdentityResultSuccess, methodName, await userManager.GetUserIdAsync(user)));
            }
            else
            {
                InvokeLoggerMethod(logger, logLevel, string.Format(Resources.LogIdentityResultFailure, methodName, await userManager.GetUserIdAsync(user), string.Join(",", result.Errors)));
            }

            return result;
        }

        public static async Task<SignInStatus> LogResultAsync<TUser>(this ILogger logger, SignInStatus signStatus, TUser user, UserManager<TUser> userManager, 
            LogLevel logLevel = LogLevel.Information, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "")
           where TUser : class
        {
            InvokeLoggerMethod(logger, logLevel, string.Format(Resources.LoggingSigninStatus, methodName, await userManager.GetUserIdAsync(user), signStatus.ToString()));

            return signStatus;
        }

        private static void InvokeLoggerMethod(ILogger logger, LogLevel logLevel, string log)
        {
            switch (logLevel)
            {
                case LogLevel.Warning:
                    logger.WriteWarning(log);
                    break;
                case LogLevel.Error:
                    logger.WriteError(log);
                    break;
                case LogLevel.Critical:
                    logger.WriteCritical(log);
                    break;
                case LogLevel.Verbose:
                    logger.WriteVerbose(log);
                    break;
                case LogLevel.Information:
                default:
                    logger.WriteInformation(log);
                    break;
            }
        }
    }
}
