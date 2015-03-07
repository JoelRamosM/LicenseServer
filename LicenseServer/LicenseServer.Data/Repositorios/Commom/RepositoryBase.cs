﻿using System.Linq;
using LicenseServer.Core.Interfaces;
using LicenseServer.Core.Modelos.Commom;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace LicenseServer.Data.Repositorios.Commom
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entidade
    {
        private readonly MongoDatabase dataBase;
        private MongoCollection<TEntity> collection;

        public RepositoryBase(MongoDatabase dataBase)
        {
            collection = dataBase.GetCollection<TEntity>(typeof(TEntity).Name);
        }


        public TEntity Find(string id)
        {
            var query = Query.EQ("Id", id);
            return collection.Find(query).FirstOrDefault();
        }

        public TEntity Include(TEntity entidade)
        {
            entidade.Id = ObjectId.GenerateNewId().ToString();
            collection.Insert(entidade);
            return entidade;
        }

        public void Delete(string id)
        {
            var query = Query.EQ("Id", id);
            collection.Remove(query);
        }

        public IQueryable<TEntity> All()
        {
            return collection.FindAll().AsQueryable();
        }
    }
}
