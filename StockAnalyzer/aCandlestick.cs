using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.Data;

namespace StockAnalyzer
{
   /// <summary>
   /// This class accesses Yahoo Finance data and provides methods to analyze the data
   /// </summary>
   /// <remarks>Includes webClient and provides results of various analysis</remarks>
   public class aCandlestick
   {

      // Constructors -------------------------------------------------------------------

      /// <summary>
      /// Default constructor
      /// </summary>
      public aCandlestick()
      {
         // Default values
         // This is highly dependent on Yahoo Finance
         URLBase = "http://ichart.finance.yahoo.com/table.csv?";
      }

      /// <summary>
      /// Constructor with too many parameters
      /// </summary>
      public aCandlestick(string Ticker, string StartingMonth, string StartingDay,
         string StartingYear, string EndingMonth, string EndingDay,
         string EndingYear, string Frequency)
      {
         this.Ticker = Ticker;

         // Starting date
         this.StartingMonth = StartingMonth;
         this.StartingDay = StartingDay;
         this.StartingYear = StartingYear;

         // EndingDate date
         this.EndingMonth = EndingMonth;
         this.EndingDay = EndingDay;
         this.EndingYear = EndingYear;

         this.Frequency = Frequency;
      }

      /// <summary>
      /// Constructor with DateTime params
      /// </summary>
      /// <param name="Ticker">Stock ticker</param>
      /// <param name="StartingDate">Starting date of historical stock analysis</param>
      /// <param name="EndingDate">Ending date of historical stock analysis</param>
      /// <param name="Frequency">Month, day, or year</param>
      public aCandlestick(string Ticker, DateTime StartingDate, DateTime EndingDate, string Frequency)
      {
         this.Ticker = Ticker;
         this.StartingDate = StartingDate;
         this.EndingDate = EndingDate;
         this.Frequency = Frequency;
      }
      // End Constructors ---------------------------------------------------------------

      // Properties ---------------------------------------------------------------------

      /// <summary>
      /// argTicker symbol for stock
      /// </summary>
      public String Ticker { get; set; }

      /// <summary>
      /// Starting month for analysis
      /// </summary>
      public String StartingMonth { get; set; }

      /// <summary>
      /// Starting day for analysis
      /// </summary>
      public string StartingDay { get; set; }

      /// <summary>
      /// Starting year for analysis
      /// </summary>
      public string StartingYear { get; set; }

      /// <summary>
      /// EndingDate month for analysis
      /// </summary>
      public string EndingMonth { get; set; }

      /// <summary>
      /// EndingDate day for analysis
      /// </summary>
      public string EndingDay { get; set; }

      /// <summary>
      /// EndingDate year for analysis
      /// </summary>
      public string EndingYear { get; set; }

      /// <summary>
      /// Daily, Weekly, or Monthly
      /// </summary>
      public string Frequency { get; set; }

      /// <summary>
      /// Beginning part of URL to access stock data from Yahoo Finance
      /// </summary>
      public string URLBase { get; set; }

      /// <summary>
      /// The full URL to download CSV file for analysis
      /// </summary>
      public StringBuilder URL { get; set; }

      /// <summary>
      /// The string returned from WebClient
      /// </summary>
      public string RawString { get; set; }

      /// <summary>
      /// The high value for the period of analysis
      /// </summary>
      public double HighValue { get; set; }

      /// <summary>
      /// The low value for the period of analysis
      /// </summary>
      public double LowValue { get; set; }

      /// <summary>
      /// The average closing level for the period of analysis
      /// </summary>
      public double AvgClose { get; set; }

      /// <summary>
      /// The sum of the volume for the period of analysis
      /// </summary>
      public decimal SumVol { get; set; }

      /// <summary>
      /// Starting date of analysis
      /// </summary>
      public DateTime StartingDate { get; set; }

      /// <summary>
      /// Ending date of analysis
      /// </summary>
      public DateTime EndingDate { get; set; }

      /// <summary>
      /// DataTable to store candlesticks that close higher than open
      /// </summary>
      public DataTable CloseHigher { get; set; }

      /// <summary>
      /// Array used for initial split of CSV data
      /// </summary>
      public string[] SingleColumnArray { get; set; }

      // End properties -----------------------------------------------------------------

      // Methods ------------------------------------------------------------------------

      // Build the string for the URL using StringBuilder based on documentation
      // url is where we will store the built string
      // string url_base = "http://ichart.finance.yahoo.com/table.csv?";
      /// <summary>
      /// Build URL and get WebClient data string
      /// </summary>
      public void getWebData()
      {
         StartingMonth = (StartingDate.Month - 1).ToString();
         StartingDay = StartingDate.Day.ToString();
         StartingYear = StartingDate.Year.ToString();

         EndingMonth = (EndingDate.Month - 1).ToString();
         EndingDay = EndingDate.Day.ToString();
         EndingYear = EndingDate.Year.ToString();

         URL = new StringBuilder(URLBase);
         URL.AppendFormat("s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}&g={7}&ignore=.csv",
            Ticker, StartingMonth, StartingDay, StartingYear, EndingMonth, EndingDay, EndingYear, Frequency);

         // Get the data from Yahoo Finance in the form of a string
         WebClient myClient = new WebClient();
         //need to recast url as string from StringBuilder object
         RawString = myClient.DownloadString(URL.ToString());

         // Write the RawString out to a text file for later processing
         using (StreamWriter sw = new StreamWriter("RawStream.txt"))
         {
            sw.Write(RawString);
         }
      }

      /// <summary>
      /// Get the high and low stock price for the period
      /// </summary>
      public void calcHighLow(DataTable table)
      {
         // Create list of high values
         List<double> highValueList = new List<double>();

         // Create list of low values
         List<double> lowValueList = new List<double>();

         // Loop through each row and populate list of high values
         foreach (DataRow row in table.Rows)
         {
            highValueList.Add(Convert.ToDouble(row["High"]));
         }
         HighValue = highValueList.Max();

         // Loop through each row and populate list of low values
         foreach (DataRow row in table.Rows)
         {
            lowValueList.Add(Convert.ToDouble(row["Low"]));
         }
         LowValue = lowValueList.Min();
      }

      /// <summary>
      /// This method finds stock values with LINQ and lamda expressions 
      /// Min, Max, Average closing price, Sum of volumes
      /// </summary>
      /// <param name="table">DataTable</param>
      public void calcwithLINQ(DataTable table)
      {
         try
         {
            // Find min value
            List<double> lowLevels = table.AsEnumerable().Select(low => (low.Field<double>("Low"))).ToList();
            LowValue = lowLevels.Min();
            
            // Find max value
            List<double> highLevels = table.AsEnumerable().Select(high => (high.Field<double>("High"))).ToList();            
            HighValue = highLevels.Max();

            // Find avg close
            List<double> closingLevels = table.AsEnumerable().Select(close => (close.Field<double>("Close"))).ToList();
            AvgClose = closingLevels.Average();

            // Find sum of volumes
            List<decimal> sumVol = table.AsEnumerable().Select(sum => (sum.Field<decimal>("Volume"))).ToList();
            SumVol = sumVol.Sum();

            // Find candlesticks where closing is greater than opening 
            // i.e., green candlesticks
            var closeHigherValues = from row in table.AsEnumerable()
                                    where row.Field<double>("Open") < row.Field<double>("Close")
                                    select row;

            // Convert enumerable var to DataTable type
            CloseHigher = closeHigherValues.CopyToDataTable(); 

         }
         catch (Exception ex)
         {
            Console.WriteLine("This is in calcWithLINQ method\n" + ex.Message); 
         }
      }

      // End methods --------------------------------------------------------------------

   }//end class
}//end namespace

