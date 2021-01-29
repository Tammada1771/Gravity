using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ART.Gravity.BL.Test
{
    [TestClass]
    public class utGravity
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(1, GravityManager.Load().Count());
        }

        [TestMethod]
        public void CalcForceTest()
        {
            double expected = 8.008799734504635E-10;
            Models.Gravity gravity = new Models.Gravity();
            gravity.Mass2 = 4;
            gravity.Mass1 = 3;
            gravity.Distance = 1;
            GravityManager.CalcForce(gravity);

            Assert.AreEqual(expected, gravity.Force);
        }

        [TestMethod]
        public void InsertTest()
        {
            int results = GravityManager.Insert(new Models.Gravity
            {
                Id = Guid.Empty,
                Mass1 = 10,
                Mass2 = 12,
                Distance = 2
            }, true);
            Assert.AreEqual(1, results);
        }
    }
}
