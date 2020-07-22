using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace HTN.BITS.SQLITE.BLL
{
    public class UtilityBLL
    {
        public static DataTable ListToDataTable<T>(IList<T> data, string tableName)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable(tableName);
            Type columnType;
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                columnType = prop.PropertyType;
                if (columnType.IsGenericType)
                {
                    columnType = columnType.GetGenericArguments()[0];
                }

                if (columnType.Namespace.Equals("System") || columnType.Namespace.Equals("System.Drawing")) //!System.Collections.Generic
                {
                    table.Columns.Add(prop.Name, columnType);
                }
            }

            if (data != null)
            {
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in props)
                    {
                        columnType = prop.PropertyType;
                        if (columnType.Namespace.Equals("System") || columnType.Namespace.Equals("System.Drawing")) //!System.Collections.Generic
                        {
                            object obj = prop.GetValue(item);
                            row[prop.Name] = (obj == null ? DBNull.Value : obj);
                        }
                    }

                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}
