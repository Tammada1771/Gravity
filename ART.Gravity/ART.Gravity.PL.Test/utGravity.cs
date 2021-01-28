using Microsoft.VisualStudio.TestTools.UnitTesting;
using ART.Gravity.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ART.Gravity.PL.Test
{
    [TestClass]
    public class utGravity
    {
        protected GravityEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new GravityEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            int expected = 1;
            int actual = 0;

            actual = dc.TblGravities.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalcForceTest()
        {
            double expected = 8.008E-10;
            double? actual = 0;

            var parameterMass1 = new SqlParameter
            {
                ParameterName = "MassOne",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = 3
            };

            var parameterMass2 = new SqlParameter
            {
                ParameterName = "MassTwo",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = 4
            };

            var parameterDistance = new SqlParameter
            {
                ParameterName = "Distance",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = 1
            };

            // get an error because it cant convert a double to single. tried just about everything i could look up
            var results = dc.Set<spCalcForceResult>().FromSqlRaw("exec spCalcForce @MassOne, @MassTwo, @Distance", parameterMass1, parameterMass2, parameterDistance).ToList();

            foreach(var r in results)
            {
                actual = r.Force;
            }
          
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertTest()
        {
            int expected = 1;
            int actual = 0;

            tblGravity newrow = new tblGravity();
            newrow.Id = Guid.NewGuid();
            newrow.MassOne = 10;
            newrow.MassTwo = 12;
            newrow.Distance = 2;
            newrow.ChangeDate = DateTime.Now;
            newrow.Force = 1;

            dc.TblGravities.Add(newrow);
            actual = dc.SaveChanges();
            Assert.AreEqual(expected, actual);
        }
    }
}
