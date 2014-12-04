using System;

namespace Wey.Common
{
    public class Throw
    {
        /// <summary>
        ///     Ifs the null.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="thisObject">The this object.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IfNull<TException>(object thisObject, string parameterName) where TException : Exception
        {
            if (thisObject == null)
            {
                PrivateThrow<TException>(parameterName);
            }
        }

        /// <summary>
        ///     Ifs the null.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="thisObject">The this object.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void IfNullOrEmpty<TException>(string thisObject, string parameterName) where TException : Exception
        {
            if (string.IsNullOrEmpty(thisObject))
            {
                PrivateThrow<TException>(parameterName);
            }
        }

        /// <summary>
        ///     Privates the throw.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        private static void PrivateThrow<TException>(string parameterName) where TException : Exception
        {
            var exception = (TException) Activator.CreateInstance(typeof (TException), new[] {parameterName});
            throw exception;
        }
    }
}