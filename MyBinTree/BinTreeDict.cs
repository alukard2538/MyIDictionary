using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace MyBinTree
{
    public class BinTreeDict<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Node Root;       
        
        public TValue this[TKey key]
        {
            get
            {
                var node = SearchKey(Root, key);
                if (node == null)
                    throw new KeyNotFoundException();
                return node.KeyValue.Value;
            }
            set
            {
                var node = SearchKey(Root, key);
                if (node != null)
                    throw new KeyNotFoundException();
                this.Add(key, value);
            }
        }

        public ICollection<TKey> Keys => this.Select(x => x.Key).ToList();

        public ICollection<TValue> Values => this.Select(x => x.Value).ToList();

        public int Count => Keys.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            var keyValue = new KeyValuePair<TKey, TValue>(key, value);
            var node = new Node() 
            { 
                KeyValue = keyValue 
            };
            if (Root == null)
            {
                Root = node;
                return;
            }
            Insert(Root, node);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Root = null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var desiredNode = SearchKey(Root, item.Key);
            if (desiredNode == null)
                return false;
            else if (Comparer<TValue>.Default.Compare(desiredNode.KeyValue.Value, item.Value) != 0)
                return false;
            else
                return true;            
        }

        public bool ContainsKey(TKey key)
        {
            if (SearchKey(Root, key) == null)
                return false;
            else
                return true;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException($"Array is null");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException($"Index is less than zero");

            if (Keys.Count > (array.Length - arrayIndex))
                throw new ArgumentException("The number of elements in the source collection is greater than " +
                                             "the available space from index to the end of the destination array");

            foreach (var keyValuePair in this)
            {
                array[arrayIndex] = keyValuePair;
                arrayIndex++;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var result = new List<KeyValuePair<TKey, TValue>>();
            PreOrderTraversal(result, Root);
            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(TKey key)
        {
            var node = SearchKey(Root, key);
            if (node == null)
            {
                return false;
            }
            if (node.LeftNode == null && node.RightNode == null)
            {
                MakeNewParentForChildOfDeleted(node.ParentNode, node, null);
            }
            if (node.LeftNode == null && node.RightNode != null)
            {
                MakeNewParentForChildOfDeleted(node.ParentNode, node, node.RightNode);
            }
            if (node.LeftNode != null & node.RightNode == null)
            {
                MakeNewParentForChildOfDeleted(node.ParentNode, node, node.LeftNode);
            }
            if (node.LeftNode != null && node.RightNode != null)
            {
                var successor = NextNode(key);
                node.KeyValue = successor.KeyValue;
                MakeNewParentForChildOfDeleted(successor.ParentNode, successor, successor.RightNode);
                MakeNewParentForChildOfDeleted(successor.ParentNode, successor, successor.LeftNode);
            }
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var node = SearchKey(Root, key);
            value = node == null ? default(TValue) : this[key];
            return node != null;
        }

        private Node Insert(Node parent, Node node)
        {
            if (parent == null)
                return node;

            if (Comparer<TKey>.Default.Compare(node.KeyValue.Key, parent.KeyValue.Key) == -1)
            {
                parent.LeftNode = Insert(parent.LeftNode, node);
                parent.LeftNode.ParentNode = parent;
            }
            else if (Comparer<TKey>.Default.Compare(node.KeyValue.Key, parent.KeyValue.Key) == 1)
            {
                parent.RightNode = Insert(parent.RightNode, node);
                parent.RightNode.ParentNode = parent;
            }
            else
                throw new InsertDuplicateKeyException(node.KeyValue.Key.ToString());

            return parent;
        }

        private void PreOrderTraversal(List<KeyValuePair<TKey, TValue>> list, Node node)
        {
            if (node == null)
                return;

            list.Add(new KeyValuePair<TKey, TValue>(node.KeyValue.Key, node.KeyValue.Value));
            PreOrderTraversal(list, node.LeftNode);
            PreOrderTraversal(list, node.RightNode);
        }

        private Node SearchKey(Node parent, TKey searchedKey)
        {
            if (parent == null || Comparer<TKey>.Default.Compare(searchedKey, parent.KeyValue.Key) == 0)
                return parent;

            if (Comparer<TKey>.Default.Compare(searchedKey, parent.KeyValue.Key) == -1)
                return SearchKey(parent.LeftNode, searchedKey);
            else
                return SearchKey(parent.RightNode, searchedKey);
        }

        private Node NextNode(TKey key)
        {
            Node currNode = Root;
            Node nextNode = null;
            while (currNode != null)
            {
                if (Comparer<TKey>.Default.Compare(currNode.KeyValue.Key, key) > 0)
                {
                    nextNode = currNode;
                    currNode = currNode.LeftNode;
                }
                else
                {
                    currNode = currNode.RightNode;
                }
            }
            return nextNode;
        }

        private void MakeNewParentForChildOfDeleted(Node parent, Node deleted, Node child)
        {
            if (parent?.LeftNode == deleted)
                parent.LeftNode = child;
            if (parent?.RightNode == deleted)
                parent.RightNode = child;
            if (child != null && deleted != null)
                child.ParentNode = deleted.ParentNode;
        }        

        private class Node
        {
            public KeyValuePair<TKey, TValue> KeyValue { get; set; }
            public Node LeftNode { get; set; }
            public Node RightNode { get; set; }
            public Node ParentNode { get; set; }
        }

    }    
}
