using System;
using System.Collections.Generic;

namespace SampleProject.Common.Utilities
{
    public static class ExceptionUtility
    {
        public static IEnumerable<Exception> GetExceptions(Exception exception)
        {
            Exception ex = exception;

            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
        }
    }
}
