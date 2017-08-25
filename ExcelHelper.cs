using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.Cells;
using System.Data;
using System.Reflection;

using System.IO;
using System.Text;
using System.Web.ModelBinding;

namespace MDM.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 导出Excel，新建SheetName
        /// </summary>
        /// <param name="dtData">数据List，</param>
        /// <param name="SheetName">SheetName，名称少于40个字符，如果为空默认Sheet1,条数超过65535,自动新建SheetName1</param>
        /// <param name="Exportpro">Excel列字段，不设置全部导出</param>
        /// <param name="ExcelName">下载Excel显示名称</param>
        /// <param name="HttpContext">本页面HttpContext变量</param>
        public static void DataToExcel<T>(List<T> dtData, String SheetName, List<string> Exportpro, string ExcelName, System.Web.HttpContextBase HttpContext) where T : new()
        {
            string sheetName = SheetName;
            if (sheetName.Trim() == string.Empty)
            {
                sheetName = "Sheet1";
            }
            if (Exportpro == null)
            {
                Exportpro = new List<string>();
            }
            if (sheetName.Length > 40)
            {
                sheetName = sheetName.Substring(0, 40);
            }

            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();//定义导出的Excel对象
            workbook.Worksheets.Clear();

            for (int i = 0; i <= dtData.Count / 65536; i++)//根据记录条数，创建不同的Sheet，以便兼容 excel 2003。
            {
                if (i == 0)
                {
                    workbook.Worksheets.Add(sheetName);
                    workbook.Worksheets[0].AutoFitColumns();

                }
                else
                {
                    workbook.Worksheets.Add(sheetName + i.ToString());
                    workbook.Worksheets[i].AutoFitColumns();
                }
            }
            #region 标题样式

            //为单元格添加样式    
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
            //设置居中
            style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
            //设置背景颜色
            style.ForegroundColor = System.Drawing.Color.FromArgb(192, 192, 192);
            style.Pattern = BackgroundType.Solid;
            style.Font.IsBold = true;
            style.Font.Name = "黑体";
            style.Borders[BorderType.BottomBorder].LineStyle = (CellBorderType.Thin);
            style.Borders[BorderType.TopBorder].LineStyle = (CellBorderType.Thin);
            style.Borders[BorderType.RightBorder].LineStyle = (CellBorderType.Thin);
            style.Borders[BorderType.LeftBorder].LineStyle = (CellBorderType.Thin);
            style.Borders.SetColor(System.Drawing.Color.Black);
            #endregion

            #region 内容样式

            //为单元格添加样式    
            Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
            //设置居中
            style1.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
            style1.Pattern = BackgroundType.Solid;
            style1.Font.Name = "宋体";
            style1.Borders[BorderType.BottomBorder].LineStyle = (CellBorderType.Thin);
            style1.Borders[BorderType.TopBorder].LineStyle = (CellBorderType.Thin);
            style1.Borders[BorderType.RightBorder].LineStyle = (CellBorderType.Thin);
            style1.Borders[BorderType.LeftBorder].LineStyle = (CellBorderType.Thin);
            style1.Borders.SetColor(System.Drawing.Color.Black);

            #endregion
            T t0 = new T();
            int i0 = 0;
            int j = 0;
            int sheetindex = 0;
            int rowid = 1;
            PropertyInfo[] propertyInfos = t0.GetType().GetProperties();//
            if (Exportpro.Count > 0)
            {
                foreach (string n in Exportpro)
                {
                    string headname = GetModelMetadata<T>(n).DisplayName;

                    foreach (Aspose.Cells.Worksheet sheet in workbook.Worksheets)
                    {
                        sheet.Cells[0, i0].PutValue(headname);
                        sheet.Cells[0, i0].SetStyle(style);
                        sheet.Cells.SetRowHeight(0, 30);
                        sheet.Cells.SetColumnWidth(i0, 30);
                    }
                    i0++;
                }
                foreach (T t in dtData)
                {
                    i0 = 0;

                    foreach (string n in Exportpro)
                    {
                        object obj = t.GetType().GetProperties().SingleOrDefault(model => model.Name == n).GetValue(t, null);
                        if (obj == null)
                        {
                            obj = "";
                        }
                        sheetindex = j / 65535;
                        rowid = j - sheetindex * 65535 + 1;
                        workbook.Worksheets[sheetindex].Cells[rowid, i0].PutValue(obj.ToString());
                        workbook.Worksheets[sheetindex].Cells[rowid, i0].SetStyle(style1);
                        workbook.Worksheets[sheetindex].Cells.SetRowHeight(rowid, 30);
                        i0++;
                    }
                    workbook.Worksheets[sheetindex].AutoFitColumns();
                    j++;
                }
            }
            else
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string headname = GetModelMetadata<T>(propertyInfo.Name).DisplayName;

                    foreach (Aspose.Cells.Worksheet sheet in workbook.Worksheets)
                    {
                        sheet.Cells[0, i0].PutValue(headname);
                        sheet.Cells[0, i0].SetStyle(style);
                        sheet.Cells.SetColumnWidth(i0, 30);
                        sheet.Cells.SetRowHeight(0, 30);
                    }
                    i0++;
                }
                foreach (T t in dtData)
                {
                    i0 = 0;

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        object obj = t.GetType().GetProperties().SingleOrDefault(model => model.Name == propertyInfo.Name).GetValue(t, null);
                        if (obj == null)
                        {
                            obj = "";
                        }
                        sheetindex = j / 65535;
                        rowid = j - sheetindex * 65535 + 1;
                        workbook.Worksheets[sheetindex].Cells[rowid, i0].PutValue(obj.ToString());
                        workbook.Worksheets[sheetindex].Cells[rowid, i0].SetStyle(style1);
                        workbook.Worksheets[sheetindex].Cells.SetRowHeight(rowid, 30);
                        i0++;
                    }
                    workbook.Worksheets[sheetindex].AutoFitColumns();
                    j++;
                }
            }
            MemoryStream fileStream = workbook.SaveToStream();

            if (HttpContext.Request.UserAgent.ToLower().IndexOf("msie") > -1)
            {
                ExcelName = ToHexString(ExcelName);
            }
            if (HttpContext.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ExcelName + ".xls\"");
            }
            else
            {
                HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + ExcelName + ".xls");
            }

            HttpContext.Response.AddHeader("Content-length", fileStream.Length.ToString());
            HttpContext.Response.BinaryWrite(fileStream.GetBuffer());
            HttpContext.Response.Flush();
            fileStream.Dispose();
            HttpContext.Response.End();

        }


      //  public static void DataTo2Excels<T1, T2>(List<T1> dtData1, String sheetName1, List<string> Exportpro1, List<T2> dtData2, String sheetName2, List<string> Exportpro2, string ExcelName, System.Web.HttpContextBase HttpContext)
      //where T1 : new()
      //where T2 : new()
      //  {
      //      if (string.IsNullOrWhiteSpace(sheetName1) || string.IsNullOrWhiteSpace(sheetName2))
      //      {
      //          throw new Exception("sheetName参数不能为空");
      //      }
      //      if (sheetName1 == sheetName2)
      //      {
      //          throw new Exception("sheetName1和sheetName2不能相同");
      //      }
      //      if (sheetName1.Length > 40 || sheetName2.Length > 40)
      //      {
      //          throw new Exception("sheetName参数长度不能超过40");
      //      }
      //      if (Exportpro1 == null || Exportpro1.Count == 0)
      //      {
      //          throw new Exception("Exportpro1导出列名不能为空");
      //      }

      //      if (Exportpro2 == null || Exportpro2.Count == 0)
      //      {
      //          throw new Exception("Exportpro2导出列名不能为空");
      //      }
      //      if (dtData1 == null || dtData2 == null)
      //      {
      //          throw new Exception("数据源不能为null");
      //      }
      //      Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();//定义导出的Excel对象
      //      workbook.Worksheets.Clear();
      //      int maxRows = 65535;
      //      InitialSheet(dtData1, sheetName1, workbook, maxRows);
      //      InitialSheet(dtData2, sheetName2, workbook, maxRows);
      //      #region 标题样式

      //      //为单元格添加样式    
      //      Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
      //      //设置居中
      //      style.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
      //      //设置背景颜色
      //      style.ForegroundColor = System.Drawing.Color.FromArgb(192, 192, 192);
      //      style.Pattern = BackgroundType.Solid;
      //      style.Font.IsBold = true;
      //      style.Font.Name = "黑体";
      //      style.Borders[BorderType.BottomBorder].LineStyle = (CellBorderType.Thin);
      //      style.Borders[BorderType.TopBorder].LineStyle = (CellBorderType.Thin);
      //      style.Borders[BorderType.RightBorder].LineStyle = (CellBorderType.Thin);
      //      style.Borders[BorderType.LeftBorder].LineStyle = (CellBorderType.Thin);
      //      style.Borders.SetColor(System.Drawing.Color.Black);

      //      #endregion

      //      #region 内容样式

      //      //为单元格添加样式    
      //      Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()];
      //      //设置居中
      //      style1.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;
      //      style1.Pattern = BackgroundType.Solid;
      //      style1.Font.Name = "宋体";
      //      style1.Borders[BorderType.BottomBorder].LineStyle = (CellBorderType.Thin);
      //      style1.Borders[BorderType.TopBorder].LineStyle = (CellBorderType.Thin);
      //      style1.Borders[BorderType.RightBorder].LineStyle = (CellBorderType.Thin);
      //      style1.Borders[BorderType.LeftBorder].LineStyle = (CellBorderType.Thin);
      //      style1.Borders.SetColor(System.Drawing.Color.Black);

      //      #endregion
      //      T t0 = new T();
      //      int i0 = 0;
      //      int j = 0;
      //      int sheetindex = 0;
      //      int rowid = 1;
      //      PropertyInfo[] propertyInfos = t0.GetType().GetProperties();//
      //      if (Exportpro.Count > 0)
      //      {
      //          foreach (string n in Exportpro)
      //          {
      //              string headname = GetModelMetadata<T>(n).DisplayName;

      //              foreach (Aspose.Cells.Worksheet sheet in workbook.Worksheets)
      //              {
      //                  sheet.Cells[0, i0].PutValue(headname);
      //                  sheet.Cells[0, i0].SetStyle(style);
      //                  sheet.Cells.SetRowHeight(0, 30);
      //                  sheet.Cells.SetColumnWidth(i0, 30);
      //              }
      //              i0++;
      //          }
      //          foreach (T t in dtData)
      //          {
      //              i0 = 0;

      //              foreach (string n in Exportpro)
      //              {
      //                  object obj = t.GetType().GetProperties().SingleOrDefault(model => model.Name == n).GetValue(t, null);
      //                  if (obj == null)
      //                  {
      //                      obj = "";
      //                  }
      //                  sheetindex = j / 65535;
      //                  rowid = j - sheetindex * 65535 + 1;
      //                  workbook.Worksheets[sheetindex].Cells[rowid, i0].PutValue(obj.ToString());
      //                  workbook.Worksheets[sheetindex].Cells[rowid, i0].SetStyle(style1);
      //                  workbook.Worksheets[sheetindex].Cells.SetRowHeight(rowid, 30);
      //                  i0++;
      //              }
      //              workbook.Worksheets[sheetindex].AutoFitColumns();
      //              j++;
      //          }
      //      }
      //      else
      //      {
      //          foreach (PropertyInfo propertyInfo in propertyInfos)
      //          {
      //              string headname = GetModelMetadata<T>(propertyInfo.Name).DisplayName;

      //              foreach (Aspose.Cells.Worksheet sheet in workbook.Worksheets)
      //              {
      //                  sheet.Cells[0, i0].PutValue(headname);
      //                  sheet.Cells[0, i0].SetStyle(style);
      //                  sheet.Cells.SetColumnWidth(i0, 30);
      //                  sheet.Cells.SetRowHeight(0, 30);
      //              }
      //              i0++;
      //          }
      //          foreach (T t in dtData)
      //          {
      //              i0 = 0;

      //              foreach (PropertyInfo propertyInfo in propertyInfos)
      //              {
      //                  object obj = t.GetType().GetProperties().SingleOrDefault(model => model.Name == propertyInfo.Name).GetValue(t, null);
      //                  if (obj == null)
      //                  {
      //                      obj = "";
      //                  }
      //                  sheetindex = j / 65535;
      //                  rowid = j - sheetindex * 65535 + 1;
      //                  workbook.Worksheets[sheetindex].Cells[rowid, i0].PutValue(obj.ToString());
      //                  workbook.Worksheets[sheetindex].Cells[rowid, i0].SetStyle(style1);
      //                  workbook.Worksheets[sheetindex].Cells.SetRowHeight(rowid, 30);
      //                  i0++;
      //              }
      //              workbook.Worksheets[sheetindex].AutoFitColumns();
      //              j++;
      //          }
      //      }
      //      MemoryStream fileStream = workbook.SaveToStream();

      //      if (HttpContext.Request.UserAgent.ToLower().IndexOf("msie") > -1)
      //      {
      //          ExcelName = ToHexString(ExcelName);
      //      }
      //      if (HttpContext.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
      //      {
      //          HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ExcelName + ".xls\"");
      //      }
      //      else
      //      {
      //          HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + ExcelName + ".xls");
      //      }

      //      HttpContext.Response.AddHeader("Content-length", fileStream.Length.ToString());
      //      HttpContext.Response.BinaryWrite(fileStream.GetBuffer());
      //      HttpContext.Response.Flush();
      //      fileStream.Dispose();
      //      HttpContext.Response.End();

      //  }

        private static void InitialSheet<T>(List<T> dtData, string sheetName, Workbook workbook, int maxRows) where T : new()
        {
            if (dtData.Count <= maxRows)
            {
                workbook.Worksheets.Add(sheetName);
                workbook.Worksheets[0].AutoFitColumns();
            }
            else
            {
                for (int i = 1; i <= Math.Ceiling(dtData.Count * 1.0 / maxRows); i++)//根据记录条数，创建不同的Sheet，以便兼容 excel 2003。
                {
                    workbook.Worksheets.Add(sheetName + i.ToString());
                    workbook.Worksheets[i].AutoFitColumns();
                }
            }
        }

        /// <summary>
        /// 取得属性 的注释信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public static ModelMetadata GetModelMetadata<TModel>(string PropertyName)
        {
            ModelMetadataProvider Provider = ModelMetadataProviders.Current;
            ModelMetadata modelMetadata = new ModelMetadata(Provider, null, () => null, typeof(TModel), PropertyName);
            return modelMetadata.Properties.FirstOrDefault(t => t.PropertyName == PropertyName);
        }


        #region 字符串编码
        /// <summary>
        /// 对字符串进行编码，防止下载文件名出现乱码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }

            return builder.ToString();
        }
        /// <summary>
        /// 对字符串进行编码，防止下载文件名出现乱码
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }
		
		 /// <summary>
        /// 测试方法
        /// </summary>
		public static void Test(){
		 Console.WriteLine("Test");
		}
		
        #endregion
    }

    public class SheetInfo
    {
        public List<string> ColumnNames { get; set; }
        public string SheetName { get; set; }
    }
}