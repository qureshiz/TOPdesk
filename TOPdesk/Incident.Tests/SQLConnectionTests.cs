using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TOPdesk.Tests
{
    [TestClass]
    public class SQLConnectionTests
    {
        [TestMethod]
        public void Can_get_connectionString_from_appConfig()
        {
            var assembly = Assembly.GetAssembly(typeof(IncidentConfigurationManager));

            Assert.IsNotNull(assembly, "Could not load Incident Project Assembly");
            var path = assembly.Location;
            Assert.IsFalse(string.IsNullOrEmpty(path), "Could not get Incident Assembly Location");
            path = path.Replace(".dll", "");

            var connectionStrings = ConfigurationManager.ConnectionStrings;
            Assert.IsNotNull(connectionStrings, "ConnectionStrings is null");
            //Assert.IsTrue(connectionStrings.Any(), "ConnectionStrings doesn't contain any connectionStrings");

        }

        [TestMethod]
        public void Can_get_connectionString_from_appConfig_of_known_assembly()
        {
            var assembly = Assembly.GetAssembly(typeof(IncidentConfigurationManager));

            Assert.IsNotNull(assembly, "Could not load Incident Project Assembly");
            var path = assembly.Location;
            Assert.IsFalse(string.IsNullOrEmpty(path), "Could not get Incident Assembly Location");
            path = path.Replace(".dll", ".dll.config");
            Assert.IsTrue(File.Exists(path), "Cannot find Config File");

            var configuration = ConfigurationManager.OpenExeConfiguration(path);
                //.ConnectionStrings;
            Assert.IsNotNull(configuration, "ConnectionStrings is null");
            //Assert.IsTrue(connectionStrings.Any(), "ConnectionStrings doesn't contain any connectionStrings");

        }

        [TestMethod]
        public void Can_get_connectionString_from_SqlConnection()
        {
            var sqlConnection = new IncidentConfigurationManager();
            Assert.IsNotNull(sqlConnection, "SqlConnnection is null");

            var connectionManager = sqlConnection.GetConfigManager();
            Assert.IsNotNull(connectionManager, "Connection Strings Collection is null");
            Assert.IsTrue(connectionManager.Count > 0, "Connection String Collection doesn't contain any Connection Strings");
        }

        [TestMethod]
        public void Can_get_connectionString_from_SqlConnection_Connection()
        {
            var connectionStringName = "TOPdesk577";
            var sqlConnection = new IncidentConfigurationManager();
            Assert.IsNotNull(sqlConnection, "SqlConnnection is null");

            var connectionManager = sqlConnection.GetConfigManager();
            Assert.IsNotNull(connectionManager, "Connection Strings Collection is null");
            Assert.IsTrue(connectionManager.Count > 0, "Connection String Collection doesn't contain any Connection Strings");

            var connectionString = sqlConnection.GetConnectionString(connectionStringName);
            Assert.IsFalse(string.IsNullOrEmpty(connectionString), "Could not get Named connection string from Connection Manager");
        }

        [TestMethod]
        public void Can_create_SqlConnection()
        {
            // Assign
            var connectionStringName = "TOPdesk577";
            var sqlConnection = new IncidentConfigurationManager();

            var connectionString = sqlConnection.GetConnectionString(connectionStringName);
            Assert.IsFalse(string.IsNullOrEmpty(connectionString), "Could not get Named connection string from Connection Manager");

            // Act
            SqlConnection connection = new SqlConnection(connectionString);
            Assert.IsNotNull(connection, "SQL Connection is null.");
            // Assert
        }

        [TestMethod]
        public void Can_create_SqlConnection_and_open()
        {
            // Assign
            var connectionStringName = "TOPdesk577";
            var configManager = new IncidentConfigurationManager();

            var connectionString = configManager.GetConnectionString(connectionStringName);
            Assert.IsFalse(string.IsNullOrEmpty(connectionString), "Could not get Named connection string from Connection Manager");

            // Act
            using (var connection = new SqlConnection(connectionString))
            {
                Assert.IsNotNull(connection, "SQL Connection is null.");
                connection.Open();

                // Assert
                Assert.IsTrue(connection.State == System.Data.ConnectionState.Open, "Could not open SQL connection. : " + connectionString);
                connection.Close();
            }
        }

        [TestMethod]
        public void Can_execute_simple_SQL_statement()
        {
            // Assign
            string sqlQuery = "SELECT TOP 10 [incident].[naam] AS [Incident] FROM incident";
            var connectionStringName = "TOPdesk577";

            var configManager = new IncidentConfigurationManager();
            Assert.IsNotNull(configManager, "SQL Connection is null.");
            

            // Act
            // Use the configManager to get a connection string.
            var configString = configManager.GetConnectionString(connectionStringName);
            
            // Using the connection string, get a connection to the database inside a Using statement.
            using (var sqlConnection = new SqlConnection(configString))
            {
            // Assert
                Assert.IsNotNull(sqlConnection, "SQL connection is null");

                //try
                //{
                sqlConnection.Open();

                    // set the SQL connection's SQL query text.
                    using (var command = new SqlCommand(sqlQuery, sqlConnection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var readText = reader["Incident"].ToString();
                                Assert.IsFalse(string.IsNullOrEmpty(readText), "Could not read text from Query " + sqlQuery);
                            }
                        }
                    }
                //}
                //catch (Exception exception)
                //{
                //}
                //finally
                //{
                //    sqlConnection.Close();
                //}
            }
        }

        [TestMethod]
        public void Can_execute_simple_SQL_statement_with_object()
        {
            // Assign
            string sqlQuery = "SELECT TOP 10 [incident].[naam] AS [Incident] FROM incident";
            var connectionStringName = "TOPdesk577";

            var configManager = new IncidentConfigurationManager();
            Assert.IsNotNull(configManager, "SQL Connection is null.");


            // Act
            // Use the configManager to get a connection string.
            var configString = configManager.GetConnectionString(connectionStringName);
            var incidentList = new List<Incident>();

            // Using the connection string, get a connection to the database inside a Using statement.
            using (var sqlConnection = new SqlConnection(configString))
            {
                // Assert
                Assert.IsNotNull(sqlConnection, "SQL connection is null");

                //try
                //{
                sqlConnection.Open();

                // set the SQL connection's SQL query text.
                using (var command = new SqlCommand(sqlQuery, sqlConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            incidentList.Add(new Incident { IncidentName = reader["Incident"].ToString() });
                        }
                    }
                }
                //}
                //catch (Exception exception)
                //{
                //}
                //finally
                //{
                //    sqlConnection.Close();
                //}
            }

            foreach (var item in incidentList)
            {
                Assert.IsFalse(string.IsNullOrEmpty(item.IncidentName));
            }

            incidentList.ForEach(x => Assert.IsFalse(string.IsNullOrEmpty(x.IncidentName)));
        }
    }

    public class Incident
    {
        public string IncidentName { get; set; }
    }
}
