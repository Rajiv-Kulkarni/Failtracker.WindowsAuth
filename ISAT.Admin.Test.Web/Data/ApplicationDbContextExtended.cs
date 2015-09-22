using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ISAT.Admin.Test.Web.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public static string conStr => getConnectionString();

        public static string getConnectionString()
        {
            if (System.Web.HttpContext.Current != null &&
                System.Web.HttpContext.Current.Items["ConnectionString"] != null &&
                System.Web.HttpContext.Current.Items["ConnectionString"].ToString() != "")
            {
                return System.Web.HttpContext.Current.Items["ConnectionString"].ToString();
            }

            string Environment = "dev";
            //string strServerName = System.Environment.MachineName.ToString();

            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            if (Environment == "dev")
            {
                conn.DataSource = @".\sqlexpress";
                //conn.AttachDBFilename = @"|DataDirectory|\NORTHWND.MDF";
                conn.InitialCatalog = "Test.Web";
                conn.IntegratedSecurity = true;
                conn.PersistSecurityInfo = true;
                conn.MultipleActiveResultSets = true;
            }

            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.Items["ConnectionString"] = conn.ConnectionString;
            }

            return conn.ConnectionString;
        }

        public ApplicationDbContext(string connectionString)
            : base(conStr)
        {
            //Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }
    }
}
