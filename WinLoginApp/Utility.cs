using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WinLoginApp
{
    class Utility
    {
        //GEt the connection string from the App config file
        internal static string GetConnectionString()
        {
            //Assume failure
            string returnValue = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["WinLoginApp.Properties.Settings.LoginAppDBConnectionString"];

            //If found - return the connectionsstring
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
