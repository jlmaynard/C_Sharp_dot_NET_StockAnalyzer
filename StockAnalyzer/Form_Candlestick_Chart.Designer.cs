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
using System.Windows.Shapes;
using System.Data;

namespace StockAnalyzer
{
   partial class Form_Candlestick_Chart
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         //
         // Set up data from datatable
         //

         // Access Table from object in Main Window
         aStringSplitter localSplitter = MainWindow.splitter;

         // Createa a local DataTable object to access DefaultView
         DataTable dt = localSplitter.Table;
         // ----------------------------------------------------------------

         // Create list of high values
         List<double> highValueList = new List<double>();

         // Loop through each row and populate list of high values
         foreach (DataRow row in dt.Rows)
         {
            highValueList.Add(Convert.ToDouble(row["High"]));
         }
         
         // Create list of low values
         List<double> lowValueList = new List<double>();

         // Loop through each row and populate list of low values
         foreach (DataRow row in dt.Rows)
         {
            lowValueList.Add(Convert.ToDouble(row["Low"]));
         }

         // Create list of open values
         List<double> openValueList = new List<double>();

         // Loop through each row and populate list of open values
         foreach (DataRow row in dt.Rows)
         {
            openValueList.Add(Convert.ToDouble(row["Open"]));
         }

         // Create list of close values
         List<double> closeValueList = new List<double>();

         // Loop through each row and populate list of close values
         foreach (DataRow row in dt.Rows)
         {
            closeValueList.Add(Convert.ToDouble(row["Close"]));
         }

         // Create list of date values
         List<string> dateValueList = new List<string>();

          // Loop through each row and populate list of open values
         foreach (DataRow row in dt.Rows)
         {
            dateValueList.Add(Convert.ToString(row["Date"]));
         }

         // Reverse lists to dispaly from left to right in chart
         // This could be improved by accessing data table object directly
         highValueList.Reverse();
         lowValueList.Reverse();
         openValueList.Reverse();
         closeValueList.Reverse();
         dateValueList.Reverse(); 

         

         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();

         System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Candlestick_Chart));
         this.CandleStick = new System.Windows.Forms.DataVisualization.Charting.Chart();
         ((System.ComponentModel.ISupportInitialize)(this.CandleStick)).BeginInit();
         this.SuspendLayout();
         // 
         // CandleStick
         // 
         chartArea1.Name = "ChartArea1";
         this.CandleStick.ChartAreas.Add(chartArea1);
         this.CandleStick.Dock = System.Windows.Forms.DockStyle.Fill;
         this.CandleStick.Location = new System.Drawing.Point(0, 0);
         this.CandleStick.Name = "CandleStick";
         series1.ChartArea = "ChartArea1";
         series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
         series1.CustomProperties = "PriceUpColor=Green, PriceDownColor=Red";
         series1.Name = "myCandlestickSeries";

         // Set the minimum value of Y axis to 90% of low value of stock price to scale chart
         // Otherwise defaults to zero (ie. Minimum = NaN)
         chartArea1.AxisY.Minimum = 0.95 * MainWindow.tracker.LowValue;

         // Set the maximum value of Y axis to 110% of the high price to scale chart
         chartArea1.AxisY.Maximum = 1.05 * MainWindow.tracker.HighValue; 

         // Add data points or series here
         // bind the lists to the X and Y values of data points in the "ByArray" series
         // series1.Points.DataBindXY(dateValueList, highValueList, lowValueList, openValueList, closeValueList); 
         series1.YValuesPerPoint = 4;
         series1.Points.DataBindXY(dateValueList, highValueList, lowValueList, openValueList, closeValueList); 
         
         this.CandleStick.Series.Add(series1);
         this.CandleStick.Size = new System.Drawing.Size(464, 378);
         this.CandleStick.TabIndex = 0;
         this.CandleStick.Text = "chart1";
         title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
         title1.Name = "Title1";

         // Add ticker and frequency to chart title

         // local variable to convert frequency to full name
         // i.e., "d" = "Daily"
         string freqText = null;

         switch (MainWindow.tracker.Frequency)
         {
            case "d":
               freqText = " - Daily";
               break;
            case "w":
               freqText = " - Weekly";
               break; 
            case "m":
               freqText = " - Monthly";
               break; 
         }
         title1.Text = MainWindow.tracker.Ticker.ToUpper() + 
            " " + freqText + " " + "CandleStick Chart"; 

         this.CandleStick.Titles.Add(title1);
         // 
         // Form_Candlestick_Chart
         // 
         
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(464, 378);
         this.Controls.Add(this.CandleStick);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "Form_Candlestick_Chart";
         this.Text = "Historical Stock Data";
         ((System.ComponentModel.ISupportInitialize)(this.CandleStick)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataVisualization.Charting.Chart CandleStick;
   }
}