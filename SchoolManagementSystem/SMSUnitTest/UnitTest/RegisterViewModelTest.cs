using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.ViewModels;

namespace SMSUnitTest.UnitTest
{
    [TestClass]
    public class RegisterViewModelTest
    {
        [TestMethod]
        public void TestIfClassImplementsBaseViewModel()
        {
            Assert.IsTrue(typeof(RegisterViewModel).BaseType == typeof(BaseViewModel));
        }

        [TestMethod]
        public void TestRegisterCommandWhenUsernameInvalid()
        {
            
        }
    }
}
