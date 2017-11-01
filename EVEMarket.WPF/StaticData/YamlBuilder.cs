using EVEMarket.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EVEMarket.StaticData
{
    public static class YamlBuilder
    {
        public static Model.Type CreateType(KeyValuePair<object, object> arg)
        {
            return CreateClass<Model.Type>(arg);
        }

        public static Group CreateGroup(KeyValuePair<object, object> arg)
        {
            return CreateClass<Group>(arg);
        }

        public static Model.Category CreateCategory(KeyValuePair<object, object> arg)
        {
            return CreateClass<Model.Category>(arg);
        }

        public static Model.Icon CreateIcon(KeyValuePair<object, object> arg)
        {
            return CreateClass<Model.Icon>(arg);
        }

        public static T CreateClass<T>(KeyValuePair<object, object> x)
        {
            var properties = x.Value as Dictionary<object, object>;
            if (properties == null)
                throw new Exception();

            var type = typeof(T);
            var c = GetClassSafe((Dictionary<object, object>)x.Value, type);
            var idProp = type.GetProperty("Id");
            idProp.SetValue(c, Convert.ToInt32(x.Key));
            return (T)c;
        }

        public static object GetClassSafe(Dictionary<object, object> content, System.Type t)
        {
            object instance = Activator.CreateInstance(t);
            var writableProperties = t.GetProperties().Where(i => i.CanWrite);
            foreach (var prop in writableProperties)
            {
                var attributes = prop.GetCustomAttributes(true);
                string propKey = null;
                foreach (var attribute in attributes)
                {
                    var jsonAtt = attribute as JsonPropertyAttribute;
                    if (jsonAtt != null)
                    {
                        propKey = jsonAtt.PropertyName;
                        break;
                    }
                }
                if (propKey == "ID" || string.IsNullOrEmpty(propKey))
                    continue;
                if (prop.PropertyType == typeof(int))
                {
                    prop.SetValue(instance, GetIntSafe(content, propKey));
                }
                else if (prop.PropertyType == typeof(long))
                {
                    prop.SetValue(instance, GetLongSafe(content, propKey));
                }
                else if (prop.PropertyType == typeof(double))
                {
                    prop.SetValue(instance, GetDoubleSafe(content, propKey));
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    prop.SetValue(instance, GetBoolSafe(content, propKey));
                }
                else if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(instance, GetStringSafe(content, propKey));
                }
                else if (prop.PropertyType.IsClass)
                {
                    prop.SetValue(instance, GetClassSafe(content, propKey, prop.PropertyType));
                }
            }

            return instance;
        }

        public static object GetClassSafe(Dictionary<object, object> content, string key, System.Type t)
        {
            object value;
            if (content.TryGetValue(key, out value))
            {
                return GetClassSafe((Dictionary<object, object>)value, t);
            }

            return Activator.CreateInstance(t);
        }

        public static long GetLongSafe(Dictionary<object, object> properties, string key)
        {
            object value;
            if (properties.TryGetValue(key, out value))
                return Convert.ToInt64(value);
            return 0;
        }

        public static int GetIntSafe(Dictionary<object, object> properties, string key)
        {
            object value;
            if (properties.TryGetValue(key, out value))
                return Convert.ToInt32(value);
            return 0;
        }

        public static double GetDoubleSafe(Dictionary<object, object> properties, string key)
        {
            object value;
            if (properties.TryGetValue(key, out value))
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            return 0.0;
        }

        public static bool GetBoolSafe(Dictionary<object, object> properties, string key)
        {
            object value;
            if (properties.TryGetValue(key, out value))
                return "true".Equals(value);
            return false;
        }

        public static Text GetTextSafe(Dictionary<object, object> properties, string key)
        {
            object value;
            if (properties.TryGetValue(key, out value))
            {
                var prop = value as Dictionary<object, object>;
                return new Text
                {
                    De = GetStringSafe(prop, "de"),
                    En = GetStringSafe(prop, "en"),
                    Fr = GetStringSafe(prop, "fr"),
                    Ja = GetStringSafe(prop, "ja"),
                    Ru = GetStringSafe(prop, "ru"),
                    Zh = GetStringSafe(prop, "de")
                };
            }
            return new Text();
        }

        public static string GetStringSafe(Dictionary<object, object> properties, string key)
        {
            object value;
            if (properties.TryGetValue(key, out value))
                return value.ToString();
            return string.Empty;
        }
    }
}