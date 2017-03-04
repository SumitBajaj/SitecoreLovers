using Sitecore.DataExchange.Providers.FileSystem.Plugins;
using Sitecore.DataExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.DataExchange.Providers.FileSystem.Extensions
{
    public static class EndpointExtensions
    {
        public static XMLFileSettings GetXMLFileSettings(this Endpoint endpoint)
        {
            return endpoint.GetPlugin<XMLFileSettings>();
        }
        public static bool HasXMLtFileSettings(this Endpoint endpoint)
        {
            return (GetXMLFileSettings(endpoint) != null);
        }
    }
}
