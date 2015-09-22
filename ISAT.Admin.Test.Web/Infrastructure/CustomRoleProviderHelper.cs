using System.Configuration;
using System.Reflection;
using ISAT.Admin.Test.Web.Data;

namespace ISAT.Admin.Test.Web.Infrastructure
{
    public class CustomRoleProviderHelper
    {
        public static void SetCustomRoleProviderConnectionString(string path)
        {
            var config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = path }, ConfigurationUserLevel.None);
            //RoleManagerSection r = (RoleManagerSection)config.GetSection("system.web/roleManager");
            //NameValueCollection pc = (NameValueCollection)r.Providers[0].Parameters;        //Get default provider in Providers collection
            string connectionString = ApplicationDbContext.conStr;
            //pc["connectionStringName"] = connectionString;

            //ConnectionStringsSection c = (ConnectionStringsSection) config.ConnectionStrings;
            //System.Configuration.ConfigurationManager.ConnectionStrings["userContext"].ConnectionString = connectionString;
            var settings = ConfigurationManager.ConnectionStrings["userContext"];
            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            fi?.SetValue(settings, false);
            settings.ConnectionString = connectionString;
        }
    }
}