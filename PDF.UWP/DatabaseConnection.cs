using GeoWorks.Mobile.Services;
using GeoWorks.Mobile.UWP;
using SQLite;
using System.IO;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]

namespace GeoWorks.Mobile.UWP
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "GeoWorksDb.db3");
            return new SQLiteConnection(path);
        }
    }
}
