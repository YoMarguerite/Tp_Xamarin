using System.IO;
using SQLite;
using TpXamarin;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace TpXamarin
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbname = "tpxamarin.db";
            var path = Path.Combine(System.Environment.
                GetFolderPath(System.Environment.
                SpecialFolder.Personal), dbname);
            return new SQLiteConnection(path);
        }
    }
}
