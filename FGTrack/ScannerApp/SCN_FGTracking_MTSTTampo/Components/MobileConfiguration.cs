using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Xml;

namespace HTN.BITS.FGTRACK.MTSTTAMPO.Components
{
    public sealed class MobileConfiguration
    {
        static MobileConfiguration instance = null;

        static readonly object locker = new object();

        NameValueCollection settings = null;

        MobileConfiguration()
        {
            string codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
            string appPath = Path.GetDirectoryName(codeBase);
            int index = codeBase.LastIndexOf(@"/") + 1;

            string configFile = Path.Combine(appPath, string.Format("{0}.config", codeBase.Substring(index, codeBase.Length - index)));

            if (!File.Exists(configFile))
            {

                throw new FileNotFoundException(string.Format("Application configuration file '{0}' not found.", configFile));

            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(configFile);

            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("appSettings");

            settings = new NameValueCollection();
            foreach (XmlNode node in nodeList)
            {
                foreach (XmlNode key in node.ChildNodes)
                {

                    settings.Add(key.Attributes["key"].Value, key.Attributes["value"].Value);

                }
            }
        }

        public static MobileConfiguration Configuration
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new MobileConfiguration();
                    }

                    return instance;
                }
            }
        }

        public NameValueCollection Settings
        {
            get
            {
                return settings;
            }
        }
    }
}
