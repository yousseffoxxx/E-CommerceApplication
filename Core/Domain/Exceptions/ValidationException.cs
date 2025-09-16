﻿namespace Domain.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidationException(IEnumerable<string> errors)
            : base("Validation Failed")
        {
            Errors = errors;
        }
    }
}
