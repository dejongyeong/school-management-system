using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolManagementSystem.Helpers;
using System.Data.SqlClient;
using System.Configuration;

namespace SMSUnitTest.FunctionalTest
{
    [TestClass]
    public class DatabaseConnectionTest
    {
        /* 
         * References:
         * Coding for Unit Testing
         * Source: http://drunkcoding.net/coding-for-unit-testing/
         * Author: Duy Hoang
         * 
         */

        [TestMethod]
        public void CheckDatabaseConnectionOpenState()
        {
            // Get connection string from App.config
            var connectionString = ConfigurationManager.ConnectionStrings["SchoolManagementSystem.Properties.Settings.dejongDBConnectionString"].ConnectionString;

            // Create instance of SQLHelper by passing in connection string
            var connection = new SqlHelper(connectionString);

            // Open Connection
            connection.Open();

            // Verify State Value
            Assert.IsTrue(connection.State == System.Data.ConnectionState.Open);

            // Close Connection
            connection.Close();
        }

        [TestMethod]
        public void CheckDatabaseConnectionCloseState()
        {
            // Get connection string from App.config
            var connectionString = ConfigurationManager.ConnectionStrings["SchoolManagementSystem.Properties.Settings.dejongDBConnectionString"].ConnectionString;

            // Create instance of SQLHelper by passing in connection string
            var connection = new SqlHelper(connectionString);

            // Open Connection
            connection.Open();

            // Close Connection
            connection.Close();

            // Verify State Value
            Assert.IsTrue(connection.State == System.Data.ConnectionState.Closed);
        }

        [TestMethod]
        public void CheckDatabaseConnectionStringIsEqual()
        {
            // Get connection string from App.config in SchoolManangementSystem
            var actual = SqlHelper.GetConnectionString();

            // Get connection string from App.config
            var expected = ConfigurationManager.ConnectionStrings["SchoolManagementSystem.Properties.Settings.dejongDBConnectionString"].ConnectionString;

            Assert.AreEqual(expected, actual);
        }
    }
}
