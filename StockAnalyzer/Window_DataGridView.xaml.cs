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
   /// <summary>
   /// Interaction logic for Window_DataGridView.xaml
   /// </summary>
   public partial class Window_DataGridView : Window
   {
      public Window_DataGridView()
      {
         InitializeComponent();

         try
         {
            // Access Table from object in Main Window
            DataTable dt = MainWindow.splitter.Table;

            // Need DefaultView to populate ItemSource in WPF
            dataGrid_StockData.ItemsSource = dt.DefaultView;
         }

         catch { }                  

      }
   }
}
