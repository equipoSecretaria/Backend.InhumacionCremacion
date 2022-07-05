using System;
using System.Collections.Generic;
using System.Net;

namespace Backend.InhumacionCremacion.Entities.Responses
{
    public class ResponseBase<T>
    {
        public ResponseBase()
        {
            ResponseTime = DateTime.UtcNow.AddHours(-5);
        }

        public ResponseBase(HttpStatusCode code = HttpStatusCode.OK, string message = null, T data = default, int count = 0)
        {
            ResponseTime = DateTime.UtcNow.AddHours(-5);
            Code = (int)code;
            Message = message;
            Data = data;
            Count = count;
        }

        public ResponseBase(int code = (int)HttpStatusCode.OK, string message = null, T data = default, int count = 0)
        {
            ResponseTime = DateTime.UtcNow.AddHours(-5);
            Code = code;
            Message = message;
            Data = data;
            Count = count;
        }

        public ResponseBase(T data, string message = "Solicitud OK.", int count = 0)
        {
            Data = data;
            Code = (int)HttpStatusCode.OK;
            Message = message;
            ResponseTime = DateTime.UtcNow.AddHours(-5);
            if (data is List<T> && count == 0)
            {
                Count = (data as List<T>).Count;
            }
        }

        public ResponseBase(HttpStatusCode code = HttpStatusCode.InternalServerError, string message = "Error en el servidor!")
        {
            Code = (int)code;
            Message = message;
            ResponseTime = DateTime.UtcNow.AddHours(-5);
        }

        [Newtonsoft.Json.JsonProperty("message")]
        public string Message { get; set; }

        [Newtonsoft.Json.JsonProperty("count")]
        public int Count { get; set; }

        [Newtonsoft.Json.JsonProperty("responseTime")]
        public DateTime ResponseTime { get; set; }

        [Newtonsoft.Json.JsonProperty("data")]
        public T Data { get; set; }

        [Newtonsoft.Json.JsonProperty("code")]
        public int Code { get; set; }
    }
}
