using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Aspose.Cells;
using Aspose.Cells.Charts;
using Aspose.Cells.Rendering;

namespace ISM.Lib
{
    public class Excel
    {
        public static Bitmap GetImageChart(byte[] filedata)
        {
            Bitmap bitmap = null;
            try
            {
                if (filedata == null) return null;
                using (MemoryStream ms = new MemoryStream(filedata))
                {
                    Aspose.Cells.Workbook book = new Aspose.Cells.Workbook(ms);
                    if (book.Worksheets.Count > 0)
                    {
                        Aspose.Cells.Worksheet sheet = book.Worksheets[0];
                        if (sheet.Charts.Count > 0)
                        {
                            Chart chart = sheet.Charts[0];
                            bitmap = chart.ToImage();
                        }
                    }
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return bitmap;
        }
        public static Bitmap GetImageChart(string filename)
        {
            Bitmap bitmap = null;
            try
            {
                if (string.IsNullOrEmpty(filename)) return null;
                Aspose.Cells.Workbook book = new Aspose.Cells.Workbook(filename);
                if (book.Worksheets.Count > 0)
                {
                    Aspose.Cells.Worksheet sheet = book.Worksheets[0];
                    if (sheet.Charts.Count > 0)
                    {
                        Chart chart = sheet.Charts[0];
                        bitmap = chart.ToImage();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return bitmap;
        }
        public static Bitmap GetImageSheet(byte[] filedata)
        {
            Bitmap bitmap = null;
            try
            {
                if (filedata == null) return null;
                using (MemoryStream ms = new MemoryStream(filedata))
                {
                    Aspose.Cells.Workbook book = new Aspose.Cells.Workbook(ms);
                    if (book.Worksheets.Count > 0)
                    {
                        Aspose.Cells.Worksheet sheet = book.Worksheets[0];
                        ImageOrPrintOptions imgOptions = new ImageOrPrintOptions();
                        imgOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                        imgOptions.OnePagePerSheet = true;
                        imgOptions.IsCellAutoFit = false;

                        SheetRender sr = new SheetRender(sheet, imgOptions);
                        bitmap = sr.ToImage(0);
                    }
                    ms.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return bitmap;
        }
        public static Bitmap GetImageSheet(string filename)
        {
            Bitmap bitmap = null;
            try
            {
                if (string.IsNullOrEmpty(filename)) return null;
                Aspose.Cells.Workbook book = new Aspose.Cells.Workbook(filename);
                if (book.Worksheets.Count > 0)
                {
                    Aspose.Cells.Worksheet sheet = book.Worksheets[0];
                    ImageOrPrintOptions imgOptions = new ImageOrPrintOptions();
                    imgOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    imgOptions.OnePagePerSheet = true;
                    imgOptions.IsCellAutoFit = false;

                    SheetRender sr = new SheetRender(sheet, imgOptions);
                    bitmap = sr.ToImage(0);
                }
            }
            catch (Exception ex)
            {
            }
            return bitmap;
        }
    }
}
