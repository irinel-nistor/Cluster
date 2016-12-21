using Bytescout.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster2
{
    class XslWriter : IDisposable
    {
        private string filePath;
        private Spreadsheet document;
        private Worksheet Sheet;

        public XslWriter(string filePath, int T, int n, string namePrefix = "")
        {
            this.filePath = filePath;
            // Create new Spreadsheet
            this.document = new Spreadsheet();
            if(File.Exists(this.filePath)){
                document.LoadFromFile(this.filePath);
            }
            
            // add new worksheet
            var sheetName = "T"+ T + "n" + n + namePrefix;
            if (document.Workbook.Worksheets.ByName(sheetName) != null)
            {
                Sheet = document.Workbook.Worksheets.ByName(sheetName);
            }
            else
            {
                Sheet = document.Workbook.Worksheets.Add(sheetName);
            }   
        }

        public void write(string cell, int value)
        {
            // headers to indicate purpose of the column
            Sheet.Cell(cell).ValueAsInteger = value;

            // Save document
            document.SaveAs(filePath);     
        }

        public void write(string cell, DateTime value)
        {
            // headers to indicate purpose of the column
            Sheet.Cell(cell).ValueAsDateTime = value;

            // Save document
            document.SaveAs(filePath);
        }

        public void write(string cell, object value)
        {
            // headers to indicate purpose of the column
            Sheet.Cell(cell).Value = value;

            // Save document
            document.SaveAs(filePath);
        }

        public void Dispose(){
            // Close Spreadsheet
            document.Close();
        }
    }
}
