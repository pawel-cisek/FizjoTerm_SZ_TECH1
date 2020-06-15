using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabMenu2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizjoTerm;

namespace TabMenu2.Models.Tests
{
    [TestClass()]
    public class PatientTests
    {
        [TestMethod()]
        public void PeselValidationTestNeg()
        {
            if (Patient.PeselValidation("12345678912"))
            Assert.Fail();
        }
        [TestMethod()]
        public void PeselValidationTestNeg2()
        {
            if (Patient.PeselValidation("123456789123456789"))
                Assert.Fail();
        }
        [TestMethod()]
        public void PeselValidationTestNeg3()
        {
            if (Patient.PeselValidation("teutyurtyutyuyiuuyi"))
                Assert.Fail();
        }
        [TestMethod()]
        public void PeselValidationTestNeg4()
        {
            if (Patient.PeselValidation("123"))
                Assert.Fail();
        }
        [TestMethod()]
        public void PeselValidationTestNeg5()
        {
            if (Patient.PeselValidation(""))
                Assert.Fail();
        }

        [TestMethod()]
        public void PeselValidationTestPos()
        {
            if (!Patient.PeselValidation("88110512118"))
                Assert.Fail();
        }
        [TestMethod()]
        public void PeselValidationTestPos2()
        {
            if (!Patient.PeselValidation("89062714364"))
                Assert.Fail();
        }

    }
}