using System;
using System.Data;

namespace SmnHelpDesk.Repository.Infra.Extension
{
    public static class ExtensionRepository
    {
        public static T GetValue<T>(this IDataReader reader, string columnName)
        {
            var coluna = reader[columnName];

            if (coluna is DBNull)
                return default(T);

            return (T)coluna;
        }
    }
}
