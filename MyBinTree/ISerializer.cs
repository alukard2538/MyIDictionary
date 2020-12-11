using System;
using System.Collections.Generic;
using System.Text;

namespace MyBinTree
{
    public interface ISerializer
    {
        string DataFromDict<TKey, TValue>(IDictionary<TKey, TValue> dictionary, char sep);
        void DictFromData<TKey, TValue>(IDictionary<TKey, TValue> dictionary, string data, char sep);
    }
}
