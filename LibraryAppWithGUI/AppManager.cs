using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryAppWithGUI
{
    public static class AppManager
    {
        public static string dbFile = "LibraryDB.db";
        public static string solutionFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../../Resources/"));
        public static string dbFilePath = Path.Combine(solutionFolder, dbFile);

        public static string connectionString;

        public static UserDatabase userDatabase = new UserDatabase();

        static AppManager()
        {
            if (File.Exists(dbFilePath))
            {
                connectionString = $"Data Source={dbFilePath}";
            }
        }
    }
}
