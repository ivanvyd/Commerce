using System;
using System.Runtime.Serialization;

namespace Commerce.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("One or more entities were not found.")
        { }
    }
}
