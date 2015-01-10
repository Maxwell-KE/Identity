// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.Logging;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Microsoft.AspNet.Identity.Test
{
    public static class IdentityResultAssert
    {
        public static void IsSuccess(IdentityResult result)
        {
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
        }

        public static void IsFailure(IdentityResult result)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
        }

        public static void IsFailure(IdentityResult result, string error)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.Equal(error, result.Errors.First().Description);
        }

        public static void IsFailure(IdentityResult result, IdentityError error)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.Equal(error.Description, result.Errors.First().Description);
            Assert.Equal(error.Code, result.Errors.First().Code);
        }

        public static void VerifyUserManagerFailureLog(ILogger logger, string methodName, string userId, params IdentityError[] errors)
        {
            VerifyFailureLog(logger, "UserManager", methodName, userId, errors);
        }

        public static void VerifyUserManagerSuccessLog(ILogger logger, string methodName, string userId)
        {
            VerifySuccessLog(logger, "UserManager", methodName, userId);

        }
        private static void VerifySuccessLog(ILogger logger, string className, string methodName, string userId)
        {
            if (logger is TestFileLogger)
            {
                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), (className + "log.txt"));
                string expected = string.Format("{0} for user: {1} : Success", methodName, userId);

                Assert.True(File.ReadAllText(filename).Contains(expected));
            }
            else
            {
                Assert.True(true, "No logger registered");
            }
        }

        private static void VerifyFailureLog(ILogger logger, string className, string methodName, string userId, params IdentityError[] errors)
        {
            if (logger is TestFileLogger)
            {
                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), (className + "log.txt"));
                errors = errors ?? new IdentityError[] { new IdentityError() };
                string expected = string.Format("{0} for user: {1} : Failed : {2}", methodName, userId, string.Join(",", errors.Select(x => x.Code).ToList()));

                Assert.True(File.ReadAllText(filename).Contains(expected));
            }
            else
            {
                Assert.True(true, "No logger registered");
            }
        }
    }
}