using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTXI1.DML
{
    public class DMLGenerator
    {

        public static string CreateDeleteStatement(object Entity, Dictionary<string, string> whereClause)
        {
            var deleteStatement = "";
            var sbProperties = new StringBuilder();
            var sbWhere = new StringBuilder();
            var entityName = Entity.GetType().Name;
            var sbColumns = new StringBuilder();
            sbColumns.Append("DELETE " + entityName + " WHERE ");

            foreach (var whereInfo in whereClause)
            {
                sbWhere.Append(whereInfo.Key);
                sbWhere.Append(" = '" + whereInfo.Value + "'");
                sbWhere.Append(" AND");
            }
            deleteStatement = sbColumns.ToString();
            deleteStatement += sbWhere.ToString().Substring(0, sbWhere.ToString().Length - 3);
            return deleteStatement;
        }

        public static string CreateUpdateStatement(object Entity, Dictionary<string, string> whereClause)
        {
            var updateStatement = "";
            var sbProperties = new StringBuilder();
            var sbWhere = new StringBuilder();
            var entityName = Entity.GetType().Name;
            var sbColumns = new StringBuilder();
            sbColumns.Append("UPDATE " + entityName + " SET ");
            var columnId = 0;
            foreach(var propertyInfo in Entity.GetType().GetProperties())
            {
                if (columnId != 0)
                {
                    sbColumns.Append(propertyInfo.Name + " =");
                    sbColumns.Append("@" + propertyInfo.Name + ",");
                }
                columnId++;
            }
            foreach (var whereInfo in whereClause) {
                sbWhere.Append(whereInfo.Key);
                sbWhere.Append(" = '" + whereInfo.Value + "'");
                sbWhere.Append(" AND");
            }
            updateStatement = sbColumns.ToString().Substring(0, sbColumns.ToString().Length - 1) + " WHERE ";
            updateStatement += sbWhere.ToString().Substring(0, sbWhere.ToString().Length - 3);
            return updateStatement;
        }

        public static string CreateInsertStatement(object Entity, bool skipPrimaryKey = false)
        {
            var insertStatement = "";
            var sbProperties = new StringBuilder();
            var entityName = Entity.GetType().Name;
            var sbColumns = new StringBuilder();
            sbColumns.Append("INSERT INTO " + entityName + "  (");
            sbProperties.Append(" VALUES (");
            var columnId = 0;
            foreach (var propertyInfo in Entity.GetType().GetProperties())
            {
                if (columnId != 0)
                {
                    sbColumns.Append(propertyInfo.Name + ",");
                    sbProperties.Append("@" + propertyInfo.Name + ",");
                }
                columnId++;
            }

            insertStatement = sbColumns.ToString().Substring(0, sbColumns.ToString().Length - 1) + ")" + sbProperties.ToString().Substring(0, sbProperties.ToString().Length - 1) + ")";
            return insertStatement;
        }
    }
}
