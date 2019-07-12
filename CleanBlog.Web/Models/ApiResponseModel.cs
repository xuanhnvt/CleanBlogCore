using System;
using System.Collections.Generic;
using System.Text;

namespace CleanBlog.Models
{
    /// <summary>
    /// Represent API response structure format
    /// </summary>
    public class ApiResponseModel
    {
        /// <summary>
        /// Response status. It can be (success / fail / error)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Response data.
        /// </summary>
        public Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; }

        public ApiResponseModel()
        {
            //Data = new Dictionary<string, object>();
        }

    }

    public static class ResponseStatus
    {
        public static string Success = "success";
        public static string Fail = "fail";
        public static string Error = "error";
    }
}
