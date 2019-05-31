using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem;

namespace SMSUnitTest.UnitTest
{
    /*
     * References:
     * WPF Enterprise MVVM Session 4 - Implementing CRUD operations (Create, Read, Update, Delete)
     * Source: https://www.youtube.com/watch?v=7SwgqLJCLI8
     * Author: DCOM Engineering, LLC 
     * 
     */

    [TestClass]  
    public class RelayCommandTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsExceptionIfActionParameterIsNull()
        {
            var command = new RelayCommand(null);
        }

        [TestMethod]
        public void ExecuteInvokesAction()
        {
            var invoked = false;

            Action<Object> action = obj => invoked = true;

            var command = new RelayCommand(action);

            command.Execute();

            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void ExecuteOverloadInvokesActionWithParameter()
        {
            var invoked = false;

            Action<Object> action = obj =>
            {
                Assert.IsNotNull(obj);
                invoked = true;
            };

            var command = new RelayCommand(action);

            command.Execute(new object());

            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void CanExecuteIsTrueByDefault()
        {
            var command = new RelayCommand(obj => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        [TestMethod]
        public void CanExecuteOverloadExecutesTruePredicate()
        {
            var command = new RelayCommand(obj => { }, obj => (int)obj == 1);
            Assert.IsTrue(command.CanExecute(1));
        }

        [TestMethod]
        public void CanExecuteOverloadExecutesFalsePredicate()
        {
            var command = new RelayCommand(obj => { }, obj => (int)obj == 1);
            Assert.IsFalse(command.CanExecute(0));
        }
    }
}
