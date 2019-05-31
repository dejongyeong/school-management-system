using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.ViewModels;
using System.ComponentModel;
using SchoolManagementSystem;

namespace SMSUnitTest.UnitTest
{
    /*
     * References: 
     * WPF Enterprise MVVM Session 4 - Implementing CRUD operations (Create, Read, Update, Delete)
     * Source: https://www.youtube.com/watch?v=7SwgqLJCLI8
     * Author: DCOM Engineering, LLC
     * 
     * As references to unit test ViewModel class. Changed of attributes name.
     * 
     */

    [TestClass]
    public class BaseViewModelTest
    {
        [TestMethod]
        public void BaseViewModelIsAbstractClass()
        {
            Type type = typeof(BaseViewModel);

            Assert.IsTrue(type.IsAbstract);
        }

        [TestMethod]
        public void BaseViewModelIsINotifyDataErrorInfo()
        {
            Assert.IsFalse(typeof(INotifyDataErrorInfo).IsAssignableFrom(typeof(BaseViewModel)));
        }

        [TestMethod]
        public void BaseViewModelIsObservableObject()
        {
            Assert.IsTrue(typeof(BaseViewModel).BaseType == typeof(ObservableObject));
        }


    }
}
