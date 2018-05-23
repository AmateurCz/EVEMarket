using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace EVEMarket.WPF.Data
{
    public static class StaticDataSerializer
    {
        public static T Deserialize<T>(Stream stream) where T : class
        {
            var deserializer = new DeserializerBuilder()
                                     .WithNamingConvention(new CamelCaseNamingConvention())  
                                     .WithNodeDeserializer(new VectorDeserializer())
                                     .Build();
            StreamReader reader = new StreamReader(stream);
            return deserializer.Deserialize<T>(reader);
        } 
    }
}
