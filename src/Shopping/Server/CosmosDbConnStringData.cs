using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server
{
    public class CosmosDbConnStringData
    {
        public string Endpoint { get; set; }
        public string Key { get; set; }

        public CosmosDbConnStringData(string connectionString)
        {
            var splitted = connectionString.Split(";");
            Endpoint = splitted[0].Substring(splitted[0].IndexOf("=")+1);
            Key = splitted[1].Substring(splitted[1].IndexOf("=")+1);
        }
    }
}
