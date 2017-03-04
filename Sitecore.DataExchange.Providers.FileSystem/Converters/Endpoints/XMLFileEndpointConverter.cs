using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.DataExchange.Repositories;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Providers.FileSystem.Plugins;
using Sitecore.DataExchange.Providers.FileSystem.Models.ItemModels.Endpoints;
using Sitecore.DataExchange.Converters;

namespace Sitecore.DataExchange.Providers.FileSystem.Converters.Endpoints
{
    public class XMLFileEndpointConverter : BaseEndpointConverter<ItemModel>
    {
        private static readonly Guid TemplateId = Guid.Parse("{9BBC1EB0-6CE8-4942-93F7-2B33C9709B8B}");
        public XMLFileEndpointConverter(IItemModelRepository repository) : base(repository)
        {
            this.SupportedTemplateIds.Add(TemplateId);
        }
        protected override void AddPlugins(ItemModel source, Endpoint endpoint)
        {
            var settings = new XMLFileSettings();
            settings.Path = base.GetStringValue(source, XMLFileEndpointItemModel.Path);
            //
            endpoint.Plugins.Add(settings);
        }
    }
}
