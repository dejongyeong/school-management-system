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
     * As references to unit test ObservableObject class. Changed of attributes name.
     * 
     */


    [TestClass]
    public class ObservableObjectTest
    {
        [TestMethod]
        public void PropertyChangedEventHandlerIsRaise()
        {
            var obj = new StubObservableObject();

            bool raised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            obj.ChangedProperty = "Some value";

            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void PropertyChangedEventHandlerIsNotRaise()
        {
            var obj = new StubObservableObject();

            bool raised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = false;
            };

            obj.ChangedProperty = "ChangedProperty";

            Assert.IsFalse(raised);
        }

        private class StubObservableObject : ObservableObject
        {
            private string _changedProperty;

            public string ChangedProperty
            {
                get { return this._changedProperty; }
                set
                {
                    this._changedProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
