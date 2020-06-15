using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabMenu2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabMenu2.Models.Tests
{
    [TestClass()]
    public class PhysiotherapistTests
    {
        [TestMethod()]
        public void NwpzValidationTestNeg()
        {
            if(Physiotherapist.NwpzValidation("abc"))
            Assert.Fail();
        }
        [TestMethod()]
        public void NwpzValidationTestNeg2()
        {
            if (Physiotherapist.NwpzValidation("123abc"))
                Assert.Fail();
        }
        [TestMethod()]
        public void NwpzValidationTestNeg3()
        {
            if (Physiotherapist.NwpzValidation("123 345"))
                Assert.Fail();
        }
        [TestMethod()]
        public void NwpzValidationTestNeg4()
        {
            if (Physiotherapist.NwpzValidation(""))
                Assert.Fail();
        }

        [TestMethod()]
        public void NwpzValidationTestPos()
        {
            if (!Physiotherapist.NwpzValidation("1234"))
                Assert.Fail();
        }
    }
}