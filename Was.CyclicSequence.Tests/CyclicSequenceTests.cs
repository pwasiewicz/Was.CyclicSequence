using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Was.CyclicSequence.Tests
{
    [TestClass]
    public class CyclicSequenceTests
    {
        [TestMethod]
        public void Next_Sample3Elements1Occureneces_Generates6Elements()
        {
            var cyclicSequence = new CyclicSequence<int>(new[] {1, 2, 3});

            var elem1 = cyclicSequence.Next();
            var elem2 = cyclicSequence.Next();
            var elem3 = cyclicSequence.Next();
            var elem4 = cyclicSequence.Next();
            var elem5 = cyclicSequence.Next();
            var elem6 = cyclicSequence.Next();

            Assert.AreEqual(1, elem1);
            Assert.AreEqual(2, elem2);
            Assert.AreEqual(3, elem3);
            Assert.AreEqual(1, elem4);
            Assert.AreEqual(2, elem5);
            Assert.AreEqual(3, elem6);
        }

        [TestMethod]
        public void GetEnumerator_Sample3Elements1Occureneces_GeneratesElements()
        {
            var cyclicSequence = new CyclicSequence<int>(new[] { 1, 2, 3 });

            var elements = cyclicSequence.Take(6).ToList();

            Assert.AreEqual(1, elements[0]);
            Assert.AreEqual(2, elements[1]);
            Assert.AreEqual(3, elements[2]);
            Assert.AreEqual(1, elements[3]);
            Assert.AreEqual(2, elements[4]);
            Assert.AreEqual(3, elements[5]);
        }
    }
}
