using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Globalization;

namespace TestWriteExistingExcelFile
{

    public class ExcelWriter
    {

        //FIELD MEMBERS
        private Excel.Application _myExcelApplication;
        private Excel.Workbook _myExcelWorkbook;
        private Excel.Worksheet _myExcelWorkSheet;

        //PROPERTIES
        public string ExcelFilePath { get; set; }
        public string TargetSheet { get; set; }
        public bool LoopUntilLastRow { get; set; }
        public ExcelCondition Condition { get; set; }
        public System.Data.DataTable SourceTable { get; set; }


        //CONSTRUCTOR
        public ExcelWriter(string excelFilePath)
        {
            this.ExcelFilePath = excelFilePath;
            this.openExcel();
        }

        private void openExcel()
        {

            try
            {
                _myExcelApplication = null;

                _myExcelApplication = new Excel.Application();
                _myExcelApplication.DisplayAlerts = false;

                _myExcelWorkbook = (Excel.Workbook)(_myExcelApplication.Workbooks._Open(this.ExcelFilePath, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value));
            }
            catch (Exception)
            {
                this._myExcelApplication = null;
                this._myExcelWorkbook = null;
                this._myExcelWorkSheet = null;
            }

        }

        public string PerformWrite(string productionType)
        {
            string result = String.Empty;
            try
            {
                _myExcelWorkSheet = (Excel.Worksheet)_myExcelWorkbook.Worksheets[this.TargetSheet];
                int colQty = _myExcelWorkSheet.Rows.Count;

                int numberOfSheets = _myExcelWorkbook.Worksheets.Count;

                if (this.LoopUntilLastRow)
                {
                    int counter = 0;
                    switch (productionType)
                    {
                        case "HOZ":
                            counter = 10;
                            break;
                        case "VER":
                            counter = 12;
                            break;
                        default:
                            break;
                    }


                    string[] dataToCheck = this.SourceTable.AsEnumerable().Select(r => r.Field<string>(this.Condition.ConditionColumn)).ToArray();

                    foreach (string data in dataToCheck)
                    {
                        bool isWrite = false;
                        if (!data.Equals(String.Empty))
                        {
                            switch (this.Condition.ConditionOperator)
                            {
                                case ConditionOperator.Equal:
                                    isWrite = true;
                                    break;
                                case ConditionOperator.NotEqual:

                                    break;
                                case ConditionOperator.StartWith:

                                    break;
                                case ConditionOperator.EndWith:
                                    break;
                                case ConditionOperator.Contain:
                                    break;
                                case ConditionOperator.ExistsIn:
                                    string type = this.Condition.ConditionValue.GetType().Name;
                                    if (type.Contains("List"))
                                    {
                                        List<string> strList = (List<string>)this.Condition.ConditionValue;
                                        if (data.ToString().Trim().In(strList.ToArray()))
                                        {
                                            isWrite = true;
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }

                            if (isWrite)
                            {
                                _myExcelWorkSheet.Cells[counter, this.Condition.ColumnToBeWrite] = this.Condition.ValueToBeWrite;
                            }
                        }
                        counter++;
                    }
                }

                _myExcelWorkbook.SaveAs(this.ExcelFilePath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value);


                _myExcelWorkbook.Close(true, this.ExcelFilePath, System.Reflection.Missing.Value);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                if (_myExcelApplication != null)
                {
                    _myExcelApplication.Quit();
                    result = "OK";
                }
            }

            return string.Empty;
        }
    }



    public enum ConditionOperator
    {
        Equal,
        NotEqual,
        StartWith,
        EndWith,
        Contain,
        ExistsIn
    }

    public class ExcelCondition
    {

        public ExcelCondition(string conditionColumn, ConditionOperator conditionOperator, object conditionValue, string columnToBeWrite, string valueToBeWrite)
        {
            this.ConditionColumn = conditionColumn;
            this.ConditionOperator = conditionOperator;
            this.ConditionValue = conditionValue;
            this.ColumnToBeWrite = columnToBeWrite;
            this.ValueToBeWrite = valueToBeWrite;
        }

        public string ConditionColumn { get; set; }
        public ConditionOperator ConditionOperator { get; set; }
        public object ConditionValue { get; set; }
        public string ColumnToBeWrite { get; set; }
        public string ValueToBeWrite { get; set; }

    }

    public static class ExtensionMethods
    {
        public static bool In<T>(this T source, params T[] list) //Extension method can be use in LINQ to determine which object in list is exist in another list. 
        {
            return list.Contains(source);
        }

        public static IEnumerable<T> SortRandomly<T>(this IEnumerable<T> source)  //Extension method for sort object in list randomly.
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }
    }

}
