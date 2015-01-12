using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Windows;

namespace StockAnalyzer
{
   public class aStringSplitter
   {
      /// <summary>
      /// DataTable that is returned
      /// </summary>
      public DataTable Table { get; set; }
      /// <summary>
      /// Single column array to store results of string splitter
      /// </summary>
      public string[] RawArray { get; set; }

      /// <summary>
      /// Returns a DataTable object that represents the Yahoo Finance data
      /// </summary>
      public DataTable SplitToDataTable(string RawString)
      {
         // Split the string into an single column raw array
         try
         {
            RawArray = RawString.Split(',', '\n');
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message + "\nYou are in SplitToDataTable");
         }

         // Build the DataTable from the array of strings
         Table = new DataTable();

         // Column names:
         // "Date", "Open", "High", "Low", "Close", "Volume", "Adj Close"
         try
         {
            // Set the table headers and types
            Table.Columns.Add("Date", typeof(string)); //[i]
            Table.Columns.Add("Open", typeof(double)); //[i+1]
            Table.Columns.Add("High", typeof(double)); //[i+2]
            Table.Columns.Add("Low", typeof(double)); //[i+3]
            Table.Columns.Add("Close", typeof(double)); //[i+4]
            Table.Columns.Add("Volume", typeof(decimal)); //[i+5]
            Table.Columns.Add("Adj Close", typeof(double)); //[i+6]

            for (int i = 7; i < RawArray.Length; i++)
            {
               // Add the rows to the table
               if ((i % 7 == 0) && (i < (RawArray.Length - 7)))
               {
                  Table.Rows.Add(RawArray[i], double.Parse(RawArray[i + 1]), double.Parse(RawArray[i + 2]),
                     double.Parse(RawArray[i + 3]), double.Parse(RawArray[i + 4]), 
                     decimal.Parse(RawArray[i + 5]), double.Parse(RawArray[i + 6]));
               }
            }//end for
         }
         catch
         {

         }

         return Table;

      }

   }
}
