using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.Data;

namespace StockAnalyzer
{

   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      // STEP 1: GET USER INPUT ---------------------------------------------------------
      // - Ticker
      // - StartingDate
      // - EndingDate
      // - Frequency

      // Instantiate aStockTracker object for use during events
      // Default constructor 
      // Consider way to incorporate 4 param constructor instead
      // Can this be moved inside button event?
      public static aCandlestick tracker = new aCandlestick();
      //public aStringSplitter splitter;  
      public static aStringSplitter splitter = new aStringSplitter();

      // Set starting date on calender pick event
      private void datePicker_StartingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
      {
         tracker.StartingDate = datePicker_StartingDate.SelectedDate.Value;
      }

      // Set ending date on calendar pick event
      private void datePicker_EndingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
      {
         tracker.EndingDate = datePicker_EndingDate.SelectedDate.Value;
      }

      // Radio buttons for frequency. Defaults to "d" 
      private void radioButton_daily_Checked(object sender, RoutedEventArgs e)
      {
         tracker.Frequency = "d";
      }

      private void radioButton_weekly_Checked(object sender, RoutedEventArgs e)
      {
         tracker.Frequency = "w";
      }

      private void radioButton_monthly_Checked(object sender, RoutedEventArgs e)
      {
         tracker.Frequency = "m";
      }
      // --------------------------------------------------------------------------------

      // Run button click event. This is the main event in the program
      private void button_Run_Click(object sender, RoutedEventArgs e)
      {
         // Set ticker on tracker object
         try
         {
            if (textBox_Ticker.Text != "")
            {
               tracker.Ticker = textBox_Ticker.Text;
            }
            else
            {
               throw new Exception("Invalid ticker");
            }
         }//end try
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
            return;
         }//end catch

         // STEP 2: GET WEB DATA --------------------------------------------------------
         // Read RTN stock data from Yahoo finance website into string
         try
         {
            // Call buildURL() method from aStockTracker class
            tracker.getWebData();
         }//end try
         catch (WebException ex)
         {
            MessageBox.Show("Web Exception: Please check all inputs and \n" +
            "and make sure you have an internet connection!");
            return;
         }//end catch

         // -----------------------------------------------------------------------------

         // STEP 3: SPLIT THE STRING ----------------------------------------------------
         // Call aStringSplitter to parse raw sting and return DataTable object

         DataTable table = splitter.SplitToDataTable(tracker.RawString);

         // -----------------------------------------------------------------------------

         // STEP 4: PERFORM ANALYSIS ----------------------------------------------------
         // Calculate the high and low prices for the period 

         try
         {
            tracker.calcwithLINQ(table);
         }
         catch
         {
            // Reset values so old ones don't display on form
            tracker.HighValue = 0.0;
            tracker.LowValue = 0.0;
         }

         // -----------------------------------------------------------------------------

         // Write results
         // If statements required if exception is thrown to prevent
         // this code from populating form with zeros in results
         if (tracker.HighValue != 0.0)
         {
            textBoxHighPrice.Text = tracker.HighValue.ToString("$0.00");
         }

         if (tracker.LowValue != 0.0)
         {
            textBoxLowPrice.Text = tracker.LowValue.ToString("$0.00");
         }

         if (tracker.AvgClose != 0.0)
         {
            textBox_avgClose.Text = tracker.AvgClose.ToString("$0.00");
         }

         if (tracker.SumVol != 0) 
         {
            textBox_sumVol.Text = tracker.SumVol.ToString("#,##0"); 
         }

      }//end button run click

      

      /// <summary>
      /// Show new window with DataGrid of downloaded historical stock data
      /// </summary>
      private void button_ShowTable_Click(object sender, RoutedEventArgs e)
      {
         // Pop up new window showing DataGrid
         Window_DataGridView dataGridWindow = new Window_DataGridView();
         dataGridWindow.Show();
      }

      private void button_ShowCloseHigherDataGrid_Click(object sender, RoutedEventArgs e)
      {
         // Pop up new window showing Close Higher DataGrid
         Window_CloseHigherDataGridView closeHigherDataGridWindow = new Window_CloseHigherDataGridView();
         closeHigherDataGridWindow.Show();
      }

      /// <summary>
      /// Show new window with chart of historical stock data
      /// </summary>
      private void button_ShowChart_Click(object sender, RoutedEventArgs e)
      {
         // Pop up the new widow holding the chart
         Form_Candlestick_Chart chartWindow = new Form_Candlestick_Chart();
         chartWindow.Show(); 
      }


      // This method clears the form but does not yet reset the date pickers
      // I can't figure out how to do that yet.
      private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         // Consider restarting the whole program from here instead

         textBoxHighPrice.Text = "";
         textBoxLowPrice.Text = "";
         textBox_Ticker.Text = "";
         textBox_avgClose.Text = "";
         textBox_sumVol.Text = ""; 

         // How do I reset this value??? 
         // datePicker_StartingDate 

         // Reset the radio buttons
         radioButton_daily.IsChecked = true;
         radioButton_weekly.IsChecked = false;
         radioButton_monthly.IsChecked = false;
      }      
   }
}
