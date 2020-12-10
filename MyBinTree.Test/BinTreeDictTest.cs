using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyBinTree;
using System.Collections;
using System.Collections.Generic;

namespace MyBinTree.Test
{
    [TestClass]
    public class BinTreeDictTest
    {
        [TestMethod]
        public void EmptyDict()
        {            
            var dict = new BinTreeDict<int, int>();            

            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void SetAndGetInt()
        {
            int value = 5;
            int key = 4;
            var dict = new BinTreeDict<int, int>() { { key, value} };

            Assert.AreEqual(5, dict[4]);
        }

        [TestMethod]
        public void SetAndGetDouble()
        {
            double value = 5.56;
            double key = 4.9;
            var dict = new BinTreeDict<double, double>() { { key, value } };

            Assert.AreEqual(5.56, dict[4.9]);
        }

        [TestMethod]
        public void SetAndGetFloat()
        {
            float value = 5.2425f;
            float key = 4.5f;
            var dict = new BinTreeDict<float, float>() { { key, value } };

            Assert.AreEqual(5.2425f, dict[4.5f]);
        }

        [TestMethod]
        public void SetAndGetDecimal()
        {
            decimal value = 5.2m;
            decimal key = 3.7m;
            var dict = new BinTreeDict<decimal, decimal>() { { key, value } };

            Assert.AreEqual(5.2m, dict[3.7m]);
        }

        [TestMethod]
        public void SetAndGetChar()
        {
            char value = 'H';
            char key = 'A';
            var dict = new BinTreeDict<char, char>() { { key, value } };

            Assert.AreEqual('H', dict['A']);
        }

        [TestMethod]
        public void SetAndGetString()
        {
            string value = "Hello";
            string key = "First";
            var dict = new BinTreeDict<string, string>() { { key, value } };

            Assert.AreEqual("Hello", dict["First"]);
        }

        [TestMethod]
        public void GetKeys()
        {
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };
            var keys = new List<int>() {-100, 0, 100};            

            Assert.AreEqual(keys.ToString(), dict.Keys.ToString());
        }

        [TestMethod]
        public void GetValues()
        {
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };
            var values = new List<int>() { 100, 0, -100 };

            Assert.AreEqual(values.ToString(), dict.Values.ToString());
        }

        [TestMethod]
        public void GetCount()
        {
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };
            var values = new List<int>() { 100, 0, -100 };

            Assert.AreEqual(values.Count, dict.Count);
        }

        [TestMethod]
        public void Add1()
        {
            var dict = new BinTreeDict<int, int>();            

            dict.Add(3, 2);

            Assert.AreEqual(2, dict[3]);
        }

        [TestMethod]
        public void Add2()
        {
            var dict = new BinTreeDict<int, int>();
            var kv = new KeyValuePair<int, int>(3, 2);

            dict.Add(kv);

            Assert.AreEqual(2, dict[3]);
        }

        [TestMethod]
        public void Add3()
        {
            var dict = new BinTreeDict<int, int>();
            dict[-100] = 100;
            dict[0] = 0;
            dict[100] = -100;            

            Assert.AreEqual(0, dict[0]);
            Assert.AreEqual(-100, dict[100]);
            Assert.AreEqual(100, dict[-100]);
        }

        [TestMethod]
        public void Clear()
        {
            var dictForClear = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };            

            dictForClear.Clear();
            
            Assert.AreEqual(0, dictForClear.Count);
        }

        [TestMethod]
        public void Contains()
        {
            var kvTrue = new KeyValuePair<int, int>(100, -100);
            var kvFalse = new KeyValuePair<int, int>(0, -100);
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };

            Assert.IsTrue(dict.Contains(kvTrue));
            Assert.IsFalse(dict.Contains(kvFalse));
        }

        [TestMethod]
        public void ContainsKey()
        {            
            int keyTrue = -100;
            int keyFalse = 200;
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };

            Assert.IsTrue(dict.ContainsKey(keyTrue));
            Assert.IsFalse(dict.ContainsKey(keyFalse));
        }

        [TestMethod]
        public void TryGetValue()
        {
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };            
            int value;

            Assert.IsTrue(dict.TryGetValue(0, out value));
            Assert.IsFalse(dict.TryGetValue(26, out value));
        }

        [TestMethod]
        public void SerializeAndFile()
        {
            var ser = new Serializer();
            var fm = new FileManager();
            var dict1 = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };

            var serDict = ser.DataFromDict(dict1, ';');

            Assert.AreEqual("-100:100;0:0;100:-100", serDict);

            fm.FileFromData("Test.txt", serDict);

            var dataFromFile = fm.DataFromFile("Test.txt");

            Assert.AreEqual("-100:100;0:0;100:-100", dataFromFile);

            var dict2 = new BinTreeDict<int, int>();
            ser.DictFromData(dict2, dataFromFile, ';');

            Assert.AreEqual(dict1.Keys.ToString(), dict2.Keys.ToString());
            Assert.AreEqual(dict1.Values.ToString(), dict2.Values.ToString());
        }

        [TestMethod]
        public void Remove()
        {
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };
            var keys = new List<int> 
            {
                {-100},
                {0},
                {100}
            };
            var values = new List<int>
            {
                {100},
                {0},
                {-100}
            };

            dict.Remove(0);

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual(keys.ToString(), dict.Keys.ToString());
            Assert.AreEqual(values.ToString(), dict.Values.ToString());
        }

        [TestMethod]
        public void CopyTo()
        {
            var array = new KeyValuePair<int, int>[5];
            var dict = new BinTreeDict<int, int>()
            {
                {-100,100},
                {0,0},
                {100,-100}
            };
            var keyValue = new KeyValuePair<int, int>(100, -100);

            dict.CopyTo(array, 2);

            Assert.AreEqual(keyValue.ToString(), array[4].ToString());
            //Assert.AreEqual(keys.ToString(), dict.Keys.ToString());
            //Assert.AreEqual(values.ToString(), dict.Values.ToString());
        }
    }
}
