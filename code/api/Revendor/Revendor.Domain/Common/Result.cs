using System;
using System.Collections.Generic;
using System.Linq;
using Revendor.Domain.Dto;

namespace Revendor.Domain.Common
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors, object responseObject = null)
        {
            Succeeded = succeeded;
            Errors = errors?.ToArray();
            ResponseObject = responseObject;
        }

        public bool Succeeded { get; }

        public string[] Errors { get; }
        public object ResponseObject { get; private set; }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Success(object responseObject)
        {
            return new Result(true, null, responseObject);
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Failure(string error)
        {
            return new Result(false, new[] { error });
        }
    }
}