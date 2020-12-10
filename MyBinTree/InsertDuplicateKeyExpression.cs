using System;
using System.Collections.Generic;
using System.Text;

namespace MyBinTree
{
    public class InsertDuplicateKeyException : Exception
    {
        public InsertDuplicateKeyException() { }
        public InsertDuplicateKeyException(string key)
            : base($"Attempt insert to binary search tree a duplicate key: {key}") { }
    }
}
