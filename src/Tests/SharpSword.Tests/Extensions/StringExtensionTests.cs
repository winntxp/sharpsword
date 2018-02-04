using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace SharpSword.Tests
{
    [TestClass()]
    public class StringExtensionTests
    {
        [TestMethod()]
        public void ContainsTest()
        {
            "SharpSword".Contains("sword", StringComparison.OrdinalIgnoreCase).ShouldBe(true);
        }

        [TestMethod()]
        public void UrlEncodeTest()
        {
            "SharpSword".UrlEncode().ShouldBe("SharpSword");
        }

        [TestMethod()]
        public void HtmlEncodeTest()
        {


            "SharpSword".HtmlEncode().ShouldNotBeNull();
        }

        [TestMethod()]
        public void ToArrayTest()
        {
            "1,2,3,S，吧".ToArray<int>(new char[] { ',' }).ShouldContain(1);
            "1,2,3,S".ToArray<int>(new char[] { ',' }).ShouldContain(2);
            "1,2,3,S".ToArray<int>(new char[] { ',' }).ShouldContain(3);
            "1,2,3,S".ToArray<int>(new char[] { ',' }).Length.ShouldBe(3);
        }
    }
}