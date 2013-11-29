﻿using System.Data.Entity;
using DataObjects.EntityFramework;
using DataObjects.EntityFramework.Implementation;
using DataObjects.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Entity;


namespace DataObjects.Tests
{
    [TestClass]
    public class EntityCurrencyDaoTest
    {
        [TestMethod]
        public void Test()
        {
            IDao<Currency> currencyDao = new EntityCurrencyDao(new LMateEntities());
            //var list = currencyDao.Get(includeProperties: "Currency, Currency.Id");
            var list = currencyDao.GetAsync(null, x => x.OrderBy(y => y.Name));

        }
    }
}