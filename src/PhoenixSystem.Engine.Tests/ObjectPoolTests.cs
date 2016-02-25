using System;
using PhoenixSystem.Engine.Collections;
using Xunit;

namespace PhoenixSystem.Engine.Tests
{
    public class ObjectPoolTests
    {
        private readonly ObjectPool _items;

        public ObjectPoolTests()
        {
            _items = new ObjectPool(() => new TestData(), td => ((TestData)td).Data = "Reset at " + DateTime.Now.ToShortDateString());
        }

        [Fact]
        public void Should_Get_New_Instance_Of_Object()
        {
            Assert.Equal(0, _items.Count);
            var item = _items.Get<TestData>();
            Assert.IsType<TestData>(item);
            Assert.True(string.IsNullOrEmpty(item.Data));
        }

        [Fact]
        public void Should_Get_Pooled_Instance_Of_Object()
        {
            var item = _items.Get<TestData>();
            _items.Put(item);
            item = null;
            item = _items.Get<TestData>();
            Assert.StartsWith("Reset at ", item.Data);
        }

        [Fact]
        public void Should_Put_Item_In_Pool()
        {
            var item = _items.Get<TestData>();
            Assert.Equal(0, _items.Count);
            _items.Put(item);
            Assert.Equal(1, _items.Count);
        }

        public class TestData
        {
            public TestData(string data = "")
            {
                Data = data;
            }

            public string Data { get; set; }
        }
    }
}