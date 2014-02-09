using System;
using System.IO;
using System.Xml.Serialization;


namespace PNUnit.Agent
{
    [Serializable]
    public class AgentConfig
    {
        public int Port;
        public string PathToAssemblies;
        public bool UseDomainPool;
        public bool NoTimeout;
    }

    public class AgentConfigLoader
    {
        public static AgentConfig LoadFromFile(string file)
        {
            FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read);
            try
            {
                XmlSerializer ser= new XmlSerializer(typeof(AgentConfig));
                return (AgentConfig)ser.Deserialize(reader);
            }
            finally
            {
                reader.Close();
            }
        }
    }

}
