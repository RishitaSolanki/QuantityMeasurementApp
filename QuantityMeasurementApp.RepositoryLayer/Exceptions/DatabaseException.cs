
using System;

namespace QuantityMeasurementApp.RepositoryLayer.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
