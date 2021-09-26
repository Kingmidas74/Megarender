using System.Collections;
using System.Collections.Generic;

namespace Megarender.Features.UnitTests.Models
{
    public abstract class TheoryData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new();

        protected void AddRow(params object[] values)
        {
            _data.Add(values);
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class TheoryData<T1, T2> : TheoryData
    {
        private string _testName = string.Empty;
        protected void Add(T1 p1, T2 p2, string testName)
        {
            _testName = testName;
            AddRow(p1,p2);
        }
    }
}