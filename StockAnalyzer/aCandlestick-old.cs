using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockAnalyzer
{
   /// <summary>
   /// aCandlestick class is used to build technical stock
   /// candlesticks for use in varius data analysis.
   /// </summary>
   class aCandlestickOld
   {
      // Properties ----------------------------------
      // These are the properties that encapsulate the 
      // private fields.  By convention, public identifiers
      // begin with capital letter 

      /// <summary>
      /// Date property for the private date field
      /// </summary>
      public string Date {get; set;}

      /// <summary>
      /// Open property for the private open field
      /// </summary>
      public double Open { get; set; }

      /// <summary>
      /// High property for the private high field
      /// </summary>
      public double High { get; set; }

      /// <summary>
      /// Low property for the private low field
      /// </summary>
      public double Low { get; set; }

      /// <summary>
      /// Close property for the private close field
      /// </summary>
      public double Close { get; set; }

      /// <summary>
      /// Volume property for the private volume field
      /// </summary>
      public int Volume { get; set; }

      /// <summary>
      /// Color property for the private color field
      /// </summary>
      public string Color { get; set; } 
      // --------------------------------------------------

      
      // Constructors -------------------------------------
      /// <summary>
      /// Default constructor takes no arguments
      /// </summary>
      public aCandlestick()
      {
         //default constructor
      }

      /// <summary>
      /// Constructor taking key financial fields
      /// </summary>
      /// <param name="argDate">Candlestick date</param>
      /// <param name="argOpen">Opening price on date</param>
      /// <param name="argHigh">High price reached on date</param>
      /// <param name="argLow">Low price reached on date</param>
      /// <param name="argClose">Closing price on date</param>
      public aCandlestick(string argDate, double argOpen, double argHigh,
         double argLow, double argClose)
      {
         Date = argDate;
         Open = argOpen;
         High = argHigh;
         Low = argLow;
         Close = argClose; 
      }

      /// <summary>
      /// Constructor takes all fields
      /// </summary>
      /// <param name="argDate">Candlestick date</param>
      /// <param name="argOpen">Opening price on date</param>
      /// <param name="argHigh">High price reached on date</param>
      /// <param name="argLow">Low price reached on date</param>
      /// <param name="argClose">Closing price on date</param>
      /// <param name="argVolume">Trading volume for date</param>
      /// <param name="argColor">Candlestick color indicating gain or loss. 
      /// Green is gain, Red is loss for date</param>
      public aCandlestick(string argDate, double argOpen, double argHigh,
         double argLow, double argClose, int argVolume, string argColor)
      {
         Date = argDate;
         Open = argOpen;
         High = argHigh;
         Low = argLow;
         Close = argClose;
         Volume = argVolume;
         Color = argColor; 
      }
      // --------------------------------------------------


      // Methods ------------------------------------------
      // TBD
      // --------------------------------------------------

      
   }//end class
}//end namespace
