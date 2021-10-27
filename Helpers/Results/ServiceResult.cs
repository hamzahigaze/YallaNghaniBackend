using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.Helpers.Results
{
    public class ServiceResult<TOutcome> : ServiceResult
    {
        public TOutcome Outcome { get; set; }

        public new static ServiceResult<TOutcome> Success(TOutcome outcome)
        {
            return new ServiceResult<TOutcome> { Successful = true, StatusCode = 200, Outcome = outcome };
        }

        public static ServiceResult<TOutcome> BadRequest(Object error = null)
        {
            return new ServiceResult<TOutcome> { StatusCode = 400, Error = error, Successful = false };
        }

        public static ServiceResult<TOutcome> NotFound(Object error = null)
        {
            return new ServiceResult<TOutcome> { StatusCode = 404, Error = error, Successful = false };
        }

        public static ServiceResult<TOutcome> Forbidden(Object error = null)
        {
            return new ServiceResult<TOutcome> { StatusCode = 403, Error = error, Successful = false };
        }

        public static ServiceResult<TOutcome> InternalServerError(Object error = null)
        {
            return new ServiceResult<TOutcome> { StatusCode = 500, Error = error, Successful = false };
        }
    }

    public class ServiceResult 
    {
        public bool Successful { get; set; } = true;

        public Object Error { get; set; }

        public int StatusCode { get; set; }

        public new static ServiceResult Success()
        {
            return new ServiceResult { Successful = true, StatusCode = 200 };
        }

        public static ServiceResult BadRequest(Object error = null)
        {
            return new ServiceResult { StatusCode = 400, Error = error, Successful = false };
        }

        public static ServiceResult NotFound(Object error = null)
        {
            return new ServiceResult { StatusCode = 404, Error = error, Successful = false };
        }

        public static ServiceResult Forbidden(Object error = null)
        {
            return new ServiceResult { StatusCode = 403, Error = error, Successful = false };
        }

        public static ServiceResult InternalServerError(Object error = null)
        {
            return new ServiceResult { StatusCode = 500, Error = error, Successful = false };
        }
    }
}
