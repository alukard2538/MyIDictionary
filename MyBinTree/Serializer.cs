using System;
using System.Collections.Generic;
using System.Text;

namespace MyBinTree
{
    public class Serializer
    {
        public string DataFromDict<TKey, TValue>(IDictionary<TKey, TValue> dictionary, char sep)
        {
            var result = "";
            char[] charsToTrim = { ';', '\r', '\n' };
            foreach (var key in dictionary.Keys)
                result += $"{key}:{dictionary[key]}{sep}";
            return result.TrimEnd(charsToTrim);
        }

        public void DictFromData<TKey, TValue>(IDictionary<TKey, TValue> dictionary, string data, char sep)
        {
            foreach (var keyValue in data.Split(sep))
            {
                var Key = (TKey)Convert.ChangeType(keyValue.Split(":")[0], typeof(TKey));
                var Value = (TValue)Convert.ChangeType(keyValue.Split(":")[1], typeof(TValue));
                dictionary.Add(Key, Value);
            }
        }
    }
}
