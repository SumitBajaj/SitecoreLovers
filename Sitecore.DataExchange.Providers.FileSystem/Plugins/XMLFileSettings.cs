using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.DataExchange.Providers.FileSystem.Plugins
{
    public class XMLFileSettings : Sitecore.DataExchange.IPlugin
    {
        public XMLFileSettings()
        {
        }
        public string Path { get; set; }
        public string RootNode { get; set; }
        public string ChildNodes { get; set; }
    }
}
