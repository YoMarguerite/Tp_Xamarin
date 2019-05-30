using System;
using System.Collections.Generic;
using System.Text;

namespace TpXamarin
{
    interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
