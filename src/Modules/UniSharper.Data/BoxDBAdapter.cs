/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2018 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using iBoxDB.LocalServer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniSharper.Data
{
    /// <summary>
    /// The multi-condition operator of iBoxDB database.
    /// </summary>
    public enum BoxDBMultiConditionOperator
    {
        And,
        Or
    }

    /// <summary>
    /// The operator enumeration of iBoxDB database.
    /// </summary>
    public enum BoxDBQueryOperator
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }

    /// <summary>
    /// The query condition for iBoxDB database.
    /// </summary>
    public struct BoxDBQueryCondition
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxDBQueryCondition"/> struct.
        /// </summary>
        /// <param name="propertyName">The name of property.</param>
        /// <param name="value">The value.</param>
        /// <param name="queryOperator">The query operator.</param>
        public BoxDBQueryCondition(string propertyName, object value, BoxDBQueryOperator queryOperator = BoxDBQueryOperator.Equal)
        {
            PropertyName = propertyName;
            Value = value;
            QueryOperator = queryOperator;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The name of property.
        /// </summary>
        public string PropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// The query operator.
        /// </summary>
        public BoxDBQueryOperator QueryOperator
        {
            get;
            set;
        }

        /// <summary>
        /// The value.
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        #endregion Properties
    }

    /// <summary>
    /// The database adapter for iBoxDB.
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    public class BoxDBAdapter : IDisposable
    {
        #region Fields

        private const BoxDBMultiConditionOperator defaultMultiConditionOperator = BoxDBMultiConditionOperator.And;

        private static readonly Dictionary<BoxDBMultiConditionOperator, string> multiConditionOperators =
            new Dictionary<BoxDBMultiConditionOperator, string>()
        {
                { BoxDBMultiConditionOperator.And, " &" },
                { BoxDBMultiConditionOperator.Or, " |" }
        };

        private static readonly Dictionary<BoxDBQueryOperator, string> queryOperators =
            new Dictionary<BoxDBQueryOperator, string>()
{
            { BoxDBQueryOperator.Equal, "==" },
            { BoxDBQueryOperator.NotEqual, "!=" },
            { BoxDBQueryOperator.GreaterThan, ">" },
            { BoxDBQueryOperator.LessThan, "<" },
            { BoxDBQueryOperator.GreaterThanOrEqual, ">=" },
            { BoxDBQueryOperator.LessThanOrEqual, "<=" }
};

        private AutoBox database;
        private DB dbServer;
        private bool disposed = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxDBAdapter"/> class.
        /// </summary>
        /// <param name="dbPath">The database path.</param>
        /// <param name="bin">The binary data of database file.</param>
        public BoxDBAdapter(string dbPath, byte[] bin)
        {
            DB.Root(dbPath);
            dbServer = new DB(bin);
            dbServer.MinConfig();
            dbServer.GetConfig().DBConfig.FileIncSize = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxDBAdapter"/> class.
        /// </summary>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbDestAddr">The database destnation address.</param>
        public BoxDBAdapter(string dbPath, long dbDestAddr = 1)
        {
            DB.Root(dbPath);
            dbServer = new DB(dbDestAddr, dbPath);
            dbServer.MinConfig();
            dbServer.GetConfig().DBConfig.FileIncSize = 1;
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="BoxDBAdapter"/> class.
        /// </summary>
        ~BoxDBAdapter()
        {
            Dispose(false);
        }

        #endregion Destructors

        #region Properties

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        public AutoBox Database
        {
            get { return database; }
        }

        /// <summary>
        /// Gets the database server.
        /// </summary>
        /// <value>The database server.</value>
        public DB DbServer
        {
            get { return dbServer; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public void Close()
        {
            if (dbServer != null)
            {
                try
                {
                    if (!dbServer.IsClosed())
                    {
                        dbServer.Close();
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }

        /// <summary>
        /// Deletes data by the given primary key value.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="primaryKeyValues">The primary key values.</param>
        /// <returns><c>true</c> if delete data success, <c>false</c> otherwise.</returns>
        public bool Delete(string tableName, params object[] primaryKeyValues)
        {
            CheckDisposed();

            try
            {
                if (database != null && primaryKeyValues != null)
                {
                    using (IBox box = database.Cube())
                    {
                        for (int i = 0, length = primaryKeyValues.Length; i < length; ++i)
                        {
                            object primaryKeyValue = primaryKeyValues[i];
                            box.Bind(tableName, primaryKeyValue).Delete();
                        }

                        CommitResult result = box.Commit();

                        if (result.Equals(CommitResult.OK))
                        {
                            return true;
                        }
                        else
                        {
                            Debug.LogError(result.GetErrorMsg(box));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return false;
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="BoxDBAdapter"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Ensures the table.
        /// </summary>
        /// <typeparam name="T">The type definition of data in table.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="names">The names.</param>
        /// <returns>The config of database.</returns>
        public DatabaseConfig.Config EnsureTable<T>(string tableName, params string[] names) where T : class
        {
            CheckDisposed();

            try
            {
                if (dbServer != null)
                {
                    return dbServer.GetConfig().EnsureTable<T>(tableName, names);
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return null;
        }

        /// <summary>
        /// Inserts data.
        /// </summary>
        /// <typeparam name="T">The type definition of data.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="values">The list of data.</param>
        /// <returns><c>true</c> if insert data success, <c>false</c> otherwise.</returns>
        public bool Insert<T>(string tableName, params T[] values) where T : class
        {
            CheckDisposed();

            try
            {
                if (database != null && values != null)
                {
                    using (IBox box = database.Cube())
                    {
                        for (int i = 0, length = values.Length; i < length; ++i)
                        {
                            T data = values[i];
                            box.Bind(tableName).Insert(data);
                        }

                        CommitResult result = box.Commit();

                        if (result.Equals(CommitResult.OK))
                        {
                            return true;
                        }
                        else
                        {
                            Debug.LogError(result.GetErrorMsg(box));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return false;
        }

        /// <summary>
        /// Makes the new identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="step">The step.</param>
        /// <returns>The new identifier.</returns>
        public long MakeNewId(byte name = 0, long step = 1)
        {
            if (database != null)
            {
                return database.NewId(name, step);
            }

            return 0;
        }

        /// <summary>
        /// Opens the database connection.
        /// </summary>
        public void Open()
        {
            if (dbServer != null)
            {
                try
                {
                    database = dbServer.Open();
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }

        /// <summary>
        /// Retrieve data by primary key value.
        /// </summary>
        /// <typeparam name="T">The type of data return.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="primaryKeyValue">The primary key value.</param>
        /// <returns>The data return.</returns>
        public T Select<T>(string tableName, object primaryKeyValue) where T : class, new()
        {
            CheckDisposed();

            try
            {
                if (database != null)
                {
                    return database.SelectKey<T>(tableName, primaryKeyValue);
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return default(T);
        }

        /// <summary>
        /// Retrieve the data items by some conditions.
        /// </summary>
        /// <typeparam name="T">The tyoe of data return.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="conditions">The query conditions.</param>
        /// <param name="multiConditionOperators">The multi-condition operators.</param>
        /// <returns>The data items.</returns>
        public List<T> Select<T>(string tableName, List<BoxDBQueryCondition> conditions,
            List<BoxDBMultiConditionOperator> multiConditionOperators = null) where T : class, new()
        {
            CheckDisposed();

            try
            {
                if (database != null)
                {
                    object[] values;
                    string sql = GenerateMultiConditionQuerySQL(out values, tableName, conditions, multiConditionOperators);

                    if (!string.IsNullOrEmpty(sql))
                    {
                        return database.Select<T>(sql, values);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return null;
        }

        /// <summary>
        /// Retrieve all data items of the table.
        /// </summary>
        /// <typeparam name="T">The tyoe of data return.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>All data items of the table.</returns>
        public List<T> SelectAll<T>(string tableName) where T : class, new()
        {
            CheckDisposed();

            try
            {
                if (database != null)
                {
                    object[] values;
                    string sql = GenerateMultiConditionQuerySQL(out values, tableName);

                    if (!string.IsNullOrEmpty(sql))
                    {
                        return database.Select<T>(sql, values);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return null;
        }

        /// <summary>
        /// Get the item count of the table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>The item count of the table.</returns>
        public long SelectCount(string tableName)
        {
            try
            {
                if (database != null)
                {
                    return database.SelectCount(string.Format("from {0}", tableName));
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return 0;
        }

        /// <summary>
        /// Get the item count by multi-condition.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="multiConditionOperators">The multi condition operators.</param>
        /// <returns>The item count.</returns>
        public long SelectCount(string tableName, List<BoxDBQueryCondition> conditions,
            List<BoxDBMultiConditionOperator> multiConditionOperators = null)
        {
            try
            {
                if (database != null)
                {
                    object[] values;
                    string sql = GenerateMultiConditionQuerySQL(out values, tableName, conditions, multiConditionOperators);

                    if (!string.IsNullOrEmpty(sql))
                    {
                        return database.SelectCount(sql, values);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return 0;
        }

        /// <summary>
        /// Updates data.
        /// </summary>
        /// <typeparam name="T">The type definition of data.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="values">The list of data.</param>
        /// <returns><c>true</c> if update the list of data success, <c>false</c> otherwise.</returns>
        public bool Update<T>(string tableName, params T[] values) where T : class
        {
            CheckDisposed();

            try
            {
                if (database != null && values != null)
                {
                    using (IBox box = database.Cube())
                    {
                        for (int i = 0, length = values.Length; i < length; ++i)
                        {
                            T data = values[i];
                            box.Bind(tableName).Update<T>(data);
                        }

                        CommitResult result = box.Commit();

                        if (result.Equals(CommitResult.OK))
                        {
                            return true;
                        }
                        else
                        {
                            Debug.LogError(result.GetErrorMsg(box));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }

            return false;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="BoxDBAdapter"/> and optionally
        /// disposes of the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Close();

                if (dbServer != null)
                {
                    dbServer.Dispose();
                }
            }
        }

        /// <summary>
        /// Generates the multi-condition query SQL statement.
        /// </summary>
        /// <param name="values">The values need to query.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="conditions">The conditions.</param>
        /// <param name="multiConditionOperators">The multi condition operators.</param>
        /// <returns>The SQL statement.</returns>
        protected string GenerateMultiConditionQuerySQL(out object[] values, string tableName,
            List<BoxDBQueryCondition> conditions = null, List<BoxDBMultiConditionOperator> multiConditionOperators = null)
        {
            string sql = string.Format("from {0}", tableName);
            values = null;

            if (conditions != null && conditions.Count > 0)
            {
                sql += " where";
                values = new object[conditions.Count];

                for (int i = 0, length = conditions.Count; i < length; i++)
                {
                    BoxDBQueryCondition condition = conditions[i];
                    string queryOpString = queryOperators[condition.QueryOperator];
                    sql += string.Format(" {0}{1}?", condition.PropertyName, queryOpString);

                    if (i < length - 1)
                    {
                        BoxDBMultiConditionOperator multiConditionOpString = defaultMultiConditionOperator;

                        if (multiConditionOperators != null && i < multiConditionOperators.Count)
                        {
                            multiConditionOpString = multiConditionOperators[i];
                        }

                        sql += BoxDBAdapter.multiConditionOperators[multiConditionOpString];
                    }

                    if (values != null)
                    {
                        values[i] = condition.Value;
                    }
                }
            }

            return sql;
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if the <see cref="BoxDBAdapter"/> is in
        /// the disposed state.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Cannot access a disposed object. Object name: 'UniSharper.Data.BoxDBAdapter'.
        /// </exception>
        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        #endregion Methods
    }
}