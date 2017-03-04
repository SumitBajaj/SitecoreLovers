using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Providers.FileSystem.Extensions;
using Sitecore.DataExchange.Providers.FileSystem.Plugins;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections;

namespace Sitecore.DataExchange.Providers.FileSystem.Processors.PipelineSteps
{
    [RequiredEndpointPlugins(typeof(XMLFileSettings))]
    public class ReadXMLFileStepProcessor : BaseReadDataStepProcessor
    {
        public ReadXMLFileStepProcessor()
        {
        }
        public override bool CanProcess(PipelineStep pipelineStep, PipelineContext pipelineContext)
        {
            return base.CanProcess(pipelineStep, pipelineContext);
        }
        public override void Process(PipelineStep pipelineStep, PipelineContext pipelineContext)
        {
            base.Process(pipelineStep, pipelineContext);
        }
        protected override void ReadData(Endpoint endpoint, PipelineStep pipelineStep, PipelineContext pipelineContext)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException();
            }
            if (pipelineStep == null)
            {
                throw new ArgumentNullException();
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException();
            }
            var logger = pipelineContext.PipelineBatchContext.Logger;
            //
            //get the file path from the plugin on the endpoint
            var settings = endpoint.GetXMLFileSettings();
            if (settings == null)
            {
                logger.Error("No xml file settings are specified on the endpoint. (pipeline step: {0}, endpoint: {1})", pipelineStep.Name, endpoint.Name);
                return;
            }
            if (string.IsNullOrWhiteSpace(settings.Path))
            {
                logger.Error("No path is specified on the endpoint. (pipeline step: {0}, endpoint: {1})", pipelineStep.Name, endpoint.Name);
                return;
            }
            //
            //if the path is relative, the base directory is used to build an 
            //absolute path so that when this code runs on the Sitecore server, 
            //relative paths will be based on the webroot
            var path = settings.Path;
            if (!Path.IsPathRooted(path))
            {
                path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, path);
            }
            //
            if (!File.Exists(path))
            {
                logger.Error("The path specified on the endpoint does not exist. (pipeline step: {0}, endpoint: {1}, path: {2})", pipelineStep.Name, endpoint.Name, path);
                return;
            }
            //
            //read the file, one line at a time
            List<string[]> result = new List<string[]>();
            List<XElement> XMLChildNodes = new List<XElement>();
            var ChildNodes = settings.ChildNodes.Split(',').ToList();
            foreach(string childnode in ChildNodes)
            {
                XMLChildNodes.Add(new XElement(childnode));
            }

            foreach (XElement level1Element in XElement.Load(settings.Path).Elements(settings.RootNode))
            {
                //result.AppendLine(level1Element.Attribute("name").Value);
                foreach (XElement level2Element in XMLChildNodes)
                {
                    var arr = new string[] { level2Element.Value };
                    result.Add(arr);
                }
            }

            //add the data that was read from the file to a plugin
            var dataSettings = new IterableDataSettings(result);
            logger.Info("{0} rows were read from the file. (pipeline step: {1}, endpoint: {2})", result.Count, pipelineStep.Name, endpoint.Name);
            //
            //add the plugin to the pipeline context
            pipelineContext.Plugins.Add(dataSettings);
        }
    }
}
