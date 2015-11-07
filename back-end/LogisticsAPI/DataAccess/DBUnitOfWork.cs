using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using LogisticsAPI.Models;
using MySql.Data.MySqlClient;

namespace LogisticsAPI.DataAccess
{
    public class DBUnitOfWork : IDisposable
    {
        private readonly DBContextLogistics _context;
        private readonly MySqlConnection _connection;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public DBUnitOfWork(DBContextLogistics context)
        {
            this._context = context;
        }

        public DBUnitOfWork()
        {
            _connection =new MySqlConnection(ConfigurationManager.ConnectionStrings["DBContextLogistics"].ConnectionString);
            _connection.Open();
            _context = new DBContextLogistics(_connection, false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            _disposed = true;
        }

        public Repository<T> Repository<T>() where T : BaseModel
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context, _connection);
                _repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)_repositories[type];
        }
    }
}