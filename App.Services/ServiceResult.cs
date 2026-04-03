using System.Net;
using System.Net.Http.Headers;

namespace App.Services
{
    public class ServiceResult<T>
    {
         //bu 2 property de nullable olmalı. Çünkü ya başarılı ya başarısız değer döndüreceğiz. İkisini de döndürmeyeceğiz.
        public T?Data { get; set; }
        public List<string>? ErrorMessage { get; set; }//Birden fazla hata mesajımız olabilir
        public bool IsSuccess=> ErrorMessage == null || ErrorMessage.Count == 0; //sadece geti olan bir property.

        public bool IsFail => !IsSuccess; //Successin tersi 

        public HttpStatusCode Status {  get; set; }

        public static ServiceResult<T> Success(T data,HttpStatusCode status=HttpStatusCode.OK)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                Status = status
            };
        }

        //static factory method bunlar
        public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                 ErrorMessage = errorMessage,
                 Status=status
            };
        }
        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = new List<string>() { errorMessage },//bunu dotnet 8.0 ile ErrorMessage=[errorMessage]
                Status=status                               //şeklinde kullanabiliriz
            };
        }
    }

    public class ServiceResult
    {
       
        public List<string>? ErrorMessage { get; set; }
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

        public bool IsFail => !IsSuccess; 

        public HttpStatusCode Status { get; set; }

        public static ServiceResult Success( HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                Status = status
            };
        }

        //static factory method bunlar
        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = errorMessage,
                Status = status
            };
        }
        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = new List<string>() { errorMessage },//bunu dotnet 8.0 ile ErrorMessage=[errorMessage]
                Status = status                               //şeklinde kullanabiliriz
            };
        }

    }
}
