using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Service.Infrastructure
{
    internal interface IHttpClient
    {
        HttpClient HttpClient { get; }
    }
}
