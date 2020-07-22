using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Data;
using DevExpress.XtraGrid.Columns;
using System.ComponentModel;
using System.Threading;
using DevExpress.XtraEditors;
using System.Collections;
using HTN.BITS.BLL.PLASESS;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.Win32;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraReports.UI;

namespace HTN.BITS.UIL.PLASESS.Component
{
    public class UiUtility
    {
        private static Configuration configInExcel = null;
        private static string languageUse = "en-GB";//"en-GB"; //"th-TH"

        public static string LanguageUse
        {
            get
            {
                return languageUse;
            }

            set
            {
                languageUse = value;
            }
        }

        #region System Configuration

        public static string AppCheckVersionPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AppCheckVersionPath"];
            }
        }

        public static string ApplicationStyle
        {
            get
            {
                return ConfigurationManager.AppSettings["AppStyle"];
            }
        }

        public static bool IsAppIdleTime
        {
            get
            {
                return (ConfigurationManager.AppSettings["IsAppIdleTime"].ToLower().Equals("true"));
            }
        }

        public static string ApplicationIdleTime
        {
            get
            {
                return ConfigurationManager.AppSettings["AppIdleTime"];
            }
        }

        public static string LogInIdleTime
        {
            get
            {
                return ConfigurationManager.AppSettings["LogInIdleTime"];
            }
        }

        public static string DialogIdleTime
        {
            get
            {
                return ConfigurationManager.AppSettings["DialogIdleTime"];
            }
        }

        public static string RPTViewerIdleTime
        {
            get
            {
                return ConfigurationManager.AppSettings["RPTViewerIdleTime"];
            }
        }

        public static int TimeOutCheckVersion
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["TimeOutCheckVersion"], NumberFormatInfo.CurrentInfo);
            }
        }

        public static bool IsAutoCheckVersion
        {
            get
            {
                return (ConfigurationManager.AppSettings["IsAutoCheckVersion"].ToLower().Equals("true"));
            }
        }

        public static string CheckVersionTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CheckVersionTime"];
            }
        }

        public static string StateConfigPath
        {
            get
            {
                return ConfigurationManager.AppSettings["StateConfigPath"];
            }
        }

        public static string ExcelTemplatePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ExcelTemplatePath"];
            }
        }

        public static string ExcelHeaderRange
        {
            get
            {
                return ConfigurationManager.AppSettings["ExcelHeaderRange"];
            }
        }

        public static string FixProductCardReport
        {
            get
            {
                return ConfigurationManager.AppSettings["FixProductCardReport"];
            }
        }

        public static string HistoryCSVPath
        {
            get
            {
                return ConfigurationManager.AppSettings["HistoryCSVPath"];
            }
        }

        public static string HistoryXLSPath
        {
            get
            {
                return ConfigurationManager.AppSettings["HistoryXLSPath"];
            }
        }

        public static int MinusFormHeight
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["MinusFormHeight"], NumberFormatInfo.CurrentInfo);
            }
        }

        public static string UPLOAD_SO_QUERY
        {
            get
            {
                return ConfigurationManager.AppSettings["UPLOAD_SO_QUERY"];
            }
        }

        #endregion

        #region Converting

        public static DataTable BuildDataTable<T>(IList<T> lst)
        {
            //create DataTable Structure
            DataTable tbl = CreateTable<T>();
            Type entType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            //get the list item and add into the list
            if (lst != null)
            {
                foreach (T item in lst)
                {
                    DataRow row = tbl.NewRow();
                    Type columnType;
                    foreach (PropertyDescriptor prop in properties)
                    {
                        columnType = prop.PropertyType;
                        if (!columnType.Namespace.Equals("System.Collections.Generic"))
                        {
                            object cValue = prop.GetValue(item);
                            if (cValue == null)
                            {
                                row[prop.Name] = DBNull.Value;
                            }
                            else
                            {
                                row[prop.Name] = cValue;
                            }
                            //row[prop.Name] = prop.GetValue(item);
                        }
                    }

                    tbl.Rows.Add(row);
                }
            }
            return tbl;
        }

        private static DataTable CreateTable<T>()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new DataTable(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            Type columnType;
            foreach (PropertyDescriptor prop in properties)
            {
                columnType = prop.PropertyType;
                if (columnType.IsGenericType)
                {
                    columnType = columnType.GetGenericArguments()[0];
                }
                //add property as column
                if (!columnType.Namespace.Equals("System.Collections.Generic"))
                {
                    tbl.Columns.Add(prop.Name, columnType);
                }
            }
            return tbl;
        }

        #endregion

        #region ODP.NET Assembly

        public static string GetOracleDataAccessPath()
        {
            RegistryKey rgkLM = Registry.LocalMachine;
            string rgkPath = string.Format(@"SOFTWARE\ORACLE\ODP.NET\{0}", ConfigurationManager.AppSettings["ODPNetVersion"]);
            RegistryKey odpnet = rgkLM.OpenSubKey(rgkPath);

            string oracleDataAccPath = odpnet.GetValue("DllPath").ToString().Replace("bin", @"odp.net\bin\2.x\Oracle.DataAccess.dll");

            if (File.Exists(oracleDataAccPath))
            {
                return oracleDataAccPath;
            }
            else
            {
                return "Oracle.DataAccess.dll";
            }
        }

        #endregion



        private static bool isLogOut = false;
        public static bool IsLogOut
        {
            get
            {
                return isLogOut;
            }

            set
            {
                if (isLogOut == value)
                    return;
                isLogOut = value;
            }



        }

        public static Form GetFromByName(string strFormName)
        {
            Form form = null;

            try
            {
                string fullname = string.Format("{0}.{1}", Assembly.GetExecutingAssembly().GetName().Name
                                                         , strFormName);

                Type t = Type.GetType(fullname, true, true);
                if (t == null)
                {
                    t = Type.GetType(fullname, true, true);
                }


                form = (Form)Activator.CreateInstance(t);

                return form;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + "\n Please Contract Administrator");
                return null;
            }
        }

        public static Font MenuGroupFont()
        {
            Font defFont = new Font("Tahoma", 8.0f, FontStyle.Bold, GraphicsUnit.Point, 1);
            return defFont;
        }

        public static Font MenuItemFont()
        {
            Font defFont = new Font("Tahoma", 7.0f, FontStyle.Bold, GraphicsUnit.Point, 1);
            return defFont;
        }

        public static object IsNullValue(object expression, object resultIsNull)
        {
            try
            {
                if (expression.Equals(System.DBNull.Value))
                    return resultIsNull;
                else
                    return expression;
            }
            catch (System.NullReferenceException e)
            {
                return resultIsNull;
            }
        }

        public static DialogResult ShowPopupForm(Form popupForm, Form parentForm, bool isCenter)
        {
            if (isCenter)
            {
                popupForm.StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                popupForm.StartPosition = FormStartPosition.Manual;
                int locationPointX = parentForm.ParentForm.Location.X + 182;
                int locationPointY = parentForm.ParentForm.Location.Y + 70;

                popupForm.Location = new Point(locationPointX, locationPointY);
            }

            return popupForm.ShowDialog(parentForm);
        }

        public static XtraReport GetReportByName(string reportName)
        {
            XtraReport report = null;

            try
            {
                string assemblyReport = string.Format("{0}.Reports.{1}", Assembly.GetExecutingAssembly().GetName().Name
                                                         , reportName);

                Type t = Type.GetType(assemblyReport, true, true);
                if (t == null)
                {
                    t = Type.GetType(assemblyReport, true, true);
                }


                report = (XtraReport)Activator.CreateInstance(t);

                return report;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #region "Check Version"

        public static Version CheckVersion(bool isShowStatus, out bool isClearGrid)
        {
            isClearGrid = false;
            Version newVersion = null;

            string url = "";
            XmlTextReader reader = null;

            try
            {
                string xmlURL = AppCheckVersionPath + string.Format("?Number={0}", GetInfiniteRandomNumbers());

                if (ExistUrl(xmlURL))
                {
                    reader = new XmlTextReader(xmlURL);

                    reader.MoveToContent();
                    string elementName = "";

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "ourfancyapp"))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                elementName = reader.Name;
                            }
                            else
                            {
                                if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                                {
                                    switch (elementName)
                                    {
                                        case "version":
                                            newVersion = new Version(reader.Value);
                                            break;
                                        case "cleargrid":
                                            isClearGrid = reader.Value.Equals("True");
                                            break;
                                        case "url":
                                            url = reader.Value;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (isShowStatus)
                    {
                        MessageBox.Show("THE SYSTEM CAN'T CHECK UPDATE VERSION!!", "INFORMATION", MessageBoxButtons.OK);
                    }

                    newVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                newVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return newVersion;
        }

        private static int GetInfiniteRandomNumbers()
        {
            var rand = new Random();
            return rand.Next(9999);
        }


        private static bool ExistUrl(string url)
        {
            bool isExist = false;

            Uri urlCheck = new Uri(url);
            WebRequest request = WebRequest.Create(urlCheck);

            #region Proxy with Credential

            //CredentialCache cc = new CredentialCache();
            //NetworkCredential nc = new NetworkCredential("Administrator",
            //"ashishisadmin",
            //"htn");
            //cc.Add("10.211.101.40", 8080, "Basic", nc);

            //WebProxy wProxy = new WebProxy();
            //wProxy.Credentials = cc;

            #endregion

            request.Proxy = null;
            request.Timeout = TimeOutCheckVersion;

            WebResponse response;
            try
            {
                response = request.GetResponse();
                isExist = true;
            }
            catch (Exception)
            {
                isExist = false; //url does not exist
            }
            finally
            {
                request.Abort();
            }

            return isExist;
        }

        #endregion

        public static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);
            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn, ImageFormat imgFormat)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imgFormat);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] BitmapToByteArray(System.Drawing.Bitmap bitmapIn)
        {
            MemoryStream ms = new MemoryStream();
            bitmapIn.Save(ms, ImageFormat.Bmp);

            return ms.ToArray();
        }

        public static Bitmap ByteArrayToBitmap(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Bitmap returnBitmap = (Bitmap)Bitmap.FromStream(ms);
            return returnBitmap;
        }

        //public static DataTable ListToDataTable<T>(IList<T> data)
        //{
        //    PropertyDescriptorCollection props =
        //        TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, prop.PropertyType);
        //    }
        //    object[] values = new object[props.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        table.Rows.Add(values);
        //    }
        //    return table;
        //}


        #region Convert List to Table and Table to List

        //List to DataTable
        public static DataTable ConvertToDataTable<T>(List<T> list)
        {
            var entityType = typeof(T);

            // Lists of type System.String and System.Enum (which includes enumerations and structs) must be handled differently 
            // than primitives and custom objects (e.g. an object that is not type System.Object).
            if (entityType == typeof(String))
            {
                var dataTable = new DataTable(entityType.Name);
                dataTable.Columns.Add(entityType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();
                    row[0] = item;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
            else if (entityType.BaseType == typeof(Enum))
            {
                var dataTable = new DataTable(entityType.Name);
                dataTable.Columns.Add(entityType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (string namedConstant in Enum.GetNames(entityType))
                {
                    var row = dataTable.NewRow();
                    row[0] = namedConstant;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }

            // Check if the type of the list is a primitive type or not. Note that if the type of the list is a custom 
            // object (e.g. an object that is not type System.Object), the underlying type will be null.
            var underlyingType = Nullable.GetUnderlyingType(entityType);
            var primitiveTypes = new List<Type>
            {
                typeof (Byte),
                typeof (Char),
                typeof (Decimal),
                typeof (Double),
                typeof (Int16),
                typeof (Int32),
                typeof (Int64),
                typeof (SByte),
                typeof (Single),
                typeof (UInt16),
                typeof (UInt32),
                typeof (UInt64),
            };

            var typeIsPrimitive = primitiveTypes.Contains(underlyingType);

            // If the type of the list is a primitive, perform a simple conversion.
            // Otherwise, map the object's properties to columns and fill the cells with the properties' values.
            if (typeIsPrimitive)
            {
                var dataTable = new DataTable(underlyingType.Name);
                dataTable.Columns.Add(underlyingType.Name);

                // Iterate through each item in the list. There is only one cell, so use index 0 to set the value.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();
                    row[0] = item;
                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
            else
            {
                // TODO:
                // 1. Convert lists of type System.Object to a data table.
                // 2. Handle objects with nested objects (make the column name the name of the object and print "system.object" as the value).

                var dataTable = new DataTable(entityType.Name);
                var propertyDescriptorCollection = TypeDescriptor.GetProperties(entityType);

                // Iterate through each property in the object and add that property name as a new column in the data table.
                foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorCollection)
                {
                    // Data tables cannot have nullable columns. The cells can have null values, but the actual columns themselves cannot be nullable.
                    // Therefore, if the current property type is nullable, use the underlying type (e.g. if the type is a nullable int, use int).
                    var propertyType = Nullable.GetUnderlyingType(propertyDescriptor.PropertyType) ?? propertyDescriptor.PropertyType;
                    dataTable.Columns.Add(propertyDescriptor.Name, propertyType);
                }

                // Iterate through each object in the list adn add a new row in the data table.
                // Then iterate through each property in the object and add the property's value to the current cell.
                // Once all properties in the current object have been used, add the row to the data table.
                foreach (T item in list)
                {
                    var row = dataTable.NewRow();

                    foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorCollection)
                    {
                        var value = propertyDescriptor.GetValue(item);
                        row[propertyDescriptor.Name] = value ?? DBNull.Value;
                    }

                    dataTable.Rows.Add(row);
                }

                return dataTable;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------


        //DataTable To List
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------

        #endregion


        #region "GridView"

        public static int GetRowHandleByColumnValue(GridView view, string columnFieldName, object value)
        {
            //string columnName = view.Columns.ColumnByFieldName(columnFieldName).Name;
            int result = GridControl.InvalidRowHandle;
            for (int i = 0; i < view.RowCount; i++)
            {
                if (view.GetRowCellDisplayText(i, columnFieldName).Equals(value))
                {
                    return i;
                }
            }

            return result;
        }

        public static int GetRowHandleByColumnValue(GridView view, object value)
        {
            int result = GridControl.InvalidRowHandle;

            for (int i = 0; i < view.RowCount; i++)
            {
                foreach (GridColumn gridcolumn in view.Columns)
                {
                    if (Match(value.ToString(), view.GetDataRow(i)[gridcolumn.FieldName].ToString()))
                    {
                        return i;
                    }
                }
            }

            return result;
        }

        private static bool Match(string userString, string cellValue)
        {

            userString = userString.ToUpper();
            cellValue = cellValue.ToUpper();
            //   If the user string is larger than the cell value, it is by definition
            //   a mismatch, so return false
            if (userString.Length > cellValue.Length)
                return false;
            else if (userString.Length == cellValue.Length)
            {
                if (userString == cellValue)
                    return true;
                else
                    return false;
            }
            else
            {
                //   There is probably an easier way to do this
                for (int i = 0; i <= (cellValue.Length - userString.Length); i++)
                {
                    if (userString == cellValue.Substring(i, userString.Length))
                        return true;
                }

                return false;

            }

            //return false;
        }

        public static void SetGridFocused(GridView view, DrawFocusRectStyle focustyle, bool isGridFocused)
        {
            view.FocusRectStyle = isGridFocused ? focustyle : DrawFocusRectStyle.None;
            view.OptionsSelection.EnableAppearanceFocusedCell = isGridFocused;
            view.OptionsSelection.EnableAppearanceFocusedRow = isGridFocused;
            view.OptionsSelection.EnableAppearanceHideSelection = isGridFocused;
        }

        public static void SetGridFocused(BandedGridView view, DrawFocusRectStyle focustyle, bool isGridFocused)
        {
            view.FocusRectStyle = isGridFocused ? focustyle : DrawFocusRectStyle.None;
            view.OptionsSelection.EnableAppearanceFocusedCell = isGridFocused;
            view.OptionsSelection.EnableAppearanceFocusedRow = isGridFocused;
            view.OptionsSelection.EnableAppearanceHideSelection = isGridFocused;
        }

        public static void SetGridReadOnly(GridView view, bool status)
        {
            view.OptionsBehavior.Editable = !status;

            if (status)
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;

                view.FocusedRowHandle = GridControl.NewItemRowHandle;
            }
        }

        public static void SetGridEditOnly(GridView view, bool status, int readOnlyField)
        {
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;

            view.OptionsBehavior.Editable = status;

            view.Columns[readOnlyField].OptionsColumn.ReadOnly = true;
            view.Columns[readOnlyField].OptionsColumn.AllowEdit = false;
            view.Columns[readOnlyField].OptionsColumn.AllowFocus = false;
        }

        public static void SetGridEditOnly(GridView view, bool status, string readOnlyFieldName)
        {
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;

            view.OptionsBehavior.Editable = true;

            view.Columns[readOnlyFieldName].OptionsColumn.ReadOnly = status;
            view.Columns[readOnlyFieldName].OptionsColumn.AllowEdit = !status;
            view.Columns[readOnlyFieldName].OptionsColumn.AllowFocus = !status;
        }

        public static void RemoveActiveRow(GridView view)
        {
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            view.FocusedRowHandle = GridControl.NewItemRowHandle;

            //Thread.Sleep(200);

            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;

        }

        public static void RemoveActiveRowTop(GridView view)
        {
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            view.FocusedRowHandle = GridControl.NewItemRowHandle;

            //Thread.Sleep(200);

            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;

        }

        public static void RemoveActiveRow(BandedGridView view)
        {
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            view.FocusedRowHandle = GridControl.NewItemRowHandle;

            //Thread.Sleep(200);

            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;

        }

        public static void ClearSelection(BaseView baseView)
        {
            switch (baseView.GetType().Name)
            {
                case "BandedGridView":

                    BandedGridView bView = baseView as BandedGridView;
                    bView.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                    bView.FocusedRowHandle = GridControl.NewItemRowHandle;
                    bView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    //bView.OptionsSelection.EnableAppearanceFocusedRow = false;
                    bView.ClearSelection();

                    break;
                case "GridView":
                    GridView gView = baseView as GridView;
                    gView.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                    gView.FocusedRowHandle = GridControl.NewItemRowHandle;
                    gView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    //gView.OptionsSelection.EnableAppearanceFocusedRow = false;
                    gView.ClearSelection();
                    break;
                default:
                    break;
            }
            
        }

        public static int FindRowInGrid(GridView view, DataTable dt, string key)
        {
            int index = dt.Rows.IndexOf(dt.Rows.Find(key));
            view.FocusedRowHandle = view.GetRowHandle(index);

            return index;
        }

        public static bool IsDuplicated(GridView view, string columnCheck, string valueCheck)
        {
            for (int i = 0; i < view.RowCount; i++)
            {
                if (view.GetRowCellDisplayText(i, columnCheck).Equals(valueCheck))
                {
                    return true;
                }
            }

            return false;
        }

        public static void UpdateGroupSummaryEnh(ref GridView view)
        {
            if (!view.IsFocusedView)
                view.UpdateGroupSummary();
            else
            {
                // If editor is active, we need to work around the fact that UpdateGroupSummary cancels
                // the current active editor
                BaseEdit editor = view.ActiveEditor;
                if (editor != null)
                {
                    TextEdit textEditor = editor as TextEdit;
                    object value = editor.EditValue;
                    int selectionStart = 0;
                    int selectionLength = 0;
                    if (textEditor != null)
                    {
                        selectionStart = textEditor.SelectionStart;
                        selectionLength = textEditor.SelectionLength;
                    }

                    view.UpdateGroupSummary();
                    view.ShowEditor();
                    editor.EditValue = value;
                    if (textEditor != null)
                    {
                        textEditor.SelectionStart = selectionStart;
                        textEditor.SelectionLength = selectionLength;
                    }
                }
                else
                {
                    view.UpdateGroupSummary();
                }
            }
        }

        #endregion

        #region "Processing"

        public static void BeginProcessing(string messageStatus, DevExpress.XtraEditors.XtraForm formParent)
        {
            //Splasher.SplashLoading.Show(typeof(processingForm), formParent);
            //Splasher.SplashLoading.Status = messageStatus;
        }

        public static void EndProcessing()
        {
            //Thread.Sleep(10);
            //Splasher.SplashLoading.Close();
        }

        #endregion

        #region "Threading"


        public static void UpdatePrintTime(int SeqNo, string userid)
        {
            Hashtable objValue = new Hashtable();
            objValue.Add("SEQ_NO", SeqNo);
            objValue.Add("USER_ID", userid);

            Thread thr = new Thread(UpdatePrintTime_Thread);
            thr.Start(objValue);
        }
        private static void UpdatePrintTime_Thread(object objValue)
        {
            Hashtable objResult = (Hashtable)objValue;
            try
            {
                if (objResult != null)
                {
                    int seq = (int)objResult["SEQ_NO"];
                    string userid = (string)objResult["USER_ID"];

                    using (JobOrderBLL jobOrdBll = new JobOrderBLL())
                    {
                        jobOrdBll.UpdatePrintTime(seq, userid);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
        }


        public static void CopyFilesToServer(ICollection<string> files)
        {
            Thread thr = new Thread(CopyFiles_ToServer_Thread);
            thr.Start(files);
        }
        private static void CopyFiles_ToServer_Thread(object objValue)
        {
            ICollection<string> vFiles = (ICollection<string>)objValue;
            try
            {
                if (vFiles != null)
                {
                    string sourceFile = string.Empty;
                    string destination = string.Empty;

                    foreach (string file in vFiles)
                    {
                        destination = string.Format("{0}\\{1}", HistoryXLSPath, Path.GetFileName(file));
                        File.Copy(file, destination, true);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        #endregion

       

        public static void SaveExcelInputLastPath(string path)
        {
            try
            {

                configInExcel = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                configInExcel.AppSettings.Settings["ExcelPath"].Value = path;

                configInExcel.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                configInExcel = null;
            }
        }
        public static string ExcelInput
        {
            get
            {
                return ConfigurationManager.AppSettings["ExcelPath"];
            }
        }

        public static string TimeSpanInWords(TimeSpan aTimeSpan)
        {
            List<string> timeStrings = new List<string>();

            int[] timeParts = new[] { aTimeSpan.Days, aTimeSpan.Hours, aTimeSpan.Minutes, aTimeSpan.Seconds };
            string[] timeUnits = new[] { "day", "hour", "min", "sec" };

            for (int i = 0; i < timeParts.Length; i++)
            {
                if (timeParts[i] > 0)
                {
                    timeStrings.Add(string.Format("{0} {1}", timeParts[i], timeUnits[i])); //Pluralize(timeParts[i], timeUnits[i])
                }
            }

            return timeStrings.Count != 0 ? string.Join(", ", timeStrings.ToArray()) : "0 sec";
        }

        public static DateTime FirstDayofMonth()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, 1);
        }

        #region XML Serialization

        public static void SerializeObject<T>(T obj, string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    xs.Serialize(stream, obj);

                    stream.Flush();
                    stream.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static T DeserializeObject<T>(string filename)
        {
            T result;
            try
            {
                using (TextReader textReader = new StreamReader(filename))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(T));
                    result = (T)deserializer.Deserialize(textReader);

                    
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsValidXML(string filename)
        {
            System.Xml.Linq.XElement element;

            try
            {
                element = System.Xml.Linq.XElement.Load(filename);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        //private static string _USER_NAME = string.Empty;
        //public static string USER_NAME
        //{
        //    get
        //    {
        //        return _USER_NAME;
        //    }

        //    set
        //    {
        //        if (_USER_NAME == value)
        //            return;
        //        _USER_NAME = value;
        //    }



        //}

    }
}
