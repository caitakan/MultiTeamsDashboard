using Dashboard.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Dashboard.DashboardDataReader
{
    public class ServiceUpTimeKustoDashboardDataReader : KustoDashboardDataReader
    {
        public ServiceUpTimeKustoDashboardDataReader(string name, string kustoAccessKey, string kustoApiEndpoint) :
                base(name, kustoAccessKey, kustoApiEndpoint)
        {
        }       
    }
}