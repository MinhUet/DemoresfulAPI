using System;
using System.Collections.Generic;
using System.Text;

namespace Nhom9.Docker_Kubernet.Demo.Entity.DTO
{
    public class ResponeDTO
    {
        public Status StatusCode { get; set; }
        public string Message { get; set; }
    }
    public enum Status
    {
        LoginSuccess = 10200,
        LoginFail = 10404,
        Unauthorized = 10401,
        ValidToken = 11200,
        UnvalidToken = 11404
    };
}
