using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Domain.Entities.ViewModel
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public T ResponseData { get; set; }

        public bool IsSuccess { get; set; } = true;
    }
}
