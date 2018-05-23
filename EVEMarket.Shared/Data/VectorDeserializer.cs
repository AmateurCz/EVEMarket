using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace EVEMarket.WPF.Data
{
    internal class VectorDeserializer : INodeDeserializer
    {
        public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
        {
            if (expectedType == typeof(Model.Vector3))
            {
                var vector = new Model.Vector3();
                vector.X = ParseDouble(reader);
                vector.Y = ParseDouble(reader);
                vector.Z = ParseDouble(reader);
                value = vector;
                reader.MoveNext();
                reader.MoveNext();
                return true;
            }

            value = null;
            return false;
        }

        private double ParseDouble(IParser reader)
        {
            reader.MoveNext();
            var value = (reader.Current as Scalar)?.Value;

            if (double.TryParse(value, out var result))
                return result;
            return 0.0;
        }
    }
}