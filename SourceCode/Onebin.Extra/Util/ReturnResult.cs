using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Onebin.Extra.Dic;

namespace Onebin.Extra.Util
{
    public class ReturnResult
    {
        public ReturnResult() { }

        public ReturnResult(ResultStatus status)
        {
            this.Status = status;
        }

        public ReturnResult(ResultStatus status, string message)
            : this(status)
        {
            this.Message = message;
        }

        public ReturnResult(ResultStatus status, string message, object data)
            :this(status, message)
        {
            this.Data = data;
        }

        public ReturnResult SetData<T>(T data) where T : class
        {
            this.Data = new DataConverter<T>().ToDictionary(data);
            return this;
        }

        public ReturnResult SetExtras(object extras)
        {
            this.Extras = extras;
            return this;
        }

        public ResultStatus Status { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public object Extras { get; set; }
    }
}
