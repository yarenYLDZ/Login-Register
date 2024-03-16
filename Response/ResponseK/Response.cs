using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Response.Response
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public static Response<T> Success(T data)
        {
            return new Response<T> { IsSuccess = true, Data = data };
        }

        public static Response<T> Fail(IEnumerable<string> errors)
        {
            return new Response<T> 
            {
                IsSuccess = false,
                Errors = errors.ToList(),
                Data = default(T)
            };
        }
    }
}
