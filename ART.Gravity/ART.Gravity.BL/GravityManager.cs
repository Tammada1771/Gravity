using ART.Gravity.PL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ART.Gravity.BL
{
    public static class GravityManager
    {
        public static int Insert(BL.Models.Gravity gravity, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (GravityEntities dc = new GravityEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGravity tblgravity = new tblGravity();
                    tblgravity.Id = Guid.NewGuid();
                    tblgravity.MassOne = (double)gravity.Mass1;
                    tblgravity.MassTwo = (double)gravity.Mass2;
                    tblgravity.Force = (double)gravity.Force;
                    tblgravity.Distance = (double)gravity.Distance;
                    tblgravity.ChangeDate = DateTime.Now;

                    //backfill the transport class id
                    gravity.Id = tblgravity.Id;

                    dc.TblGravities.Add(tblgravity);
                    int results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                    return results;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Update(BL.Models.Gravity gravity, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (GravityEntities dc = new GravityEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGravity tblgravity = dc.TblGravities.FirstOrDefault(t => t.Id == gravity.Id);

                    if (tblgravity != null)
                    {

                        tblgravity.MassOne = (double)gravity.Mass1;
                        tblgravity.MassTwo = (double)gravity.Mass2;
                        tblgravity.Distance = (double)gravity.Distance;
                        tblgravity.ChangeDate = DateTime.Now;

                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();

                        return results;
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (GravityEntities dc = new GravityEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGravity tblgravity = dc.TblGravities.FirstOrDefault(t => t.Id == id);

                    if (tblgravity != null)
                    {
                        dc.TblGravities.Remove(tblgravity);
                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();

                        return results;
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<BL.Models.Gravity> Load()
        {
            try
            {
                List<BL.Models.Gravity> gravities = new List<Models.Gravity>();

                using (GravityEntities dc = new GravityEntities())
                {
                    dc.TblGravities.ToList().ForEach(g => gravities.Add(new BL.Models.Gravity
                    {
                        Id = g.Id,
                        Mass1 = (float)g.MassOne,
                        Mass2 = (float)g.MassTwo,
                        Distance = (float)g.Distance,
                        ChangeDate = g.ChangeDate,
                        
                    }));
                    return gravities;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<BL.Models.Gravity> LoadForDataBox()
        {
            try
            {
                List<BL.Models.Gravity> gravities = new List<Models.Gravity>();

                using (GravityEntities dc = new GravityEntities())
                {
                    dc.TblGravities.ToList().ForEach(g => gravities.Add(new BL.Models.Gravity
                    {
                        Mass1 = (float)g.MassOne,
                        Mass2 = (float)g.MassTwo,
                        Distance = (float)g.Distance,
                        Force = (float)g.Force
                    }));

                    return gravities;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static BL.Models.Gravity LoadById(Guid id)
        {
            try
            {
                BL.Models.Gravity gravity = new BL.Models.Gravity();

                using (GravityEntities dc = new GravityEntities())
                {
                    var tblgravity = dc.TblGravities.FirstOrDefault(g => g.Id == id);


                    gravity.Id = tblgravity.Id;
                    gravity.Mass1 = (float)tblgravity.MassOne;
                    gravity.Mass2 = (float)tblgravity.MassTwo;
                    gravity.Distance = (float)tblgravity.Distance;
                    gravity.ChangeDate = tblgravity.ChangeDate;

                    return gravity;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CalcForce(BL.Models.Gravity gravity)
        {
            try
            {
                using (GravityEntities dc = new GravityEntities())
                {
                    var parameterMass1 = new SqlParameter
                    {
                        ParameterName = "MassOne",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Value = gravity.Mass1
                    };

                    var parameterMass2 = new SqlParameter
                    {
                        ParameterName = "MassTwo",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Value = gravity.Mass2
                    };

                    var parameterDistance = new SqlParameter
                    {
                        ParameterName = "Distance",
                        SqlDbType = System.Data.SqlDbType.Decimal,
                        Value = gravity.Distance
                    };


                    var results = dc.Set<spCalcForceResult>().FromSqlRaw("exec spCalcForce @MassOne, @MassTwo, @Distance", parameterMass1, parameterMass2, parameterDistance);

                    foreach (var r in results)
                    {
                        gravity.Force = (float)r.Force;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
