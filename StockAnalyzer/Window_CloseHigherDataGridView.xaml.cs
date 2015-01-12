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
   /// Interaction logic for Window_CloseHigherDataGridView.xaml
   /// </summary>
   public partial class Window_CloseHigherDataGridView : Window
   {
      public Window_CloseHigherDataGridView()
      {
         InitializeComponent();

         try
         {
            // Access Table from object in Main Window
            DataTable dt = MainWindow.tracker.CloseHigher;

            // Need DefaultView to populate ItemSource in WPF
            dataGrid_CloseHigher.ItemsSource = dt.DefaultView;
         }

         catch { }   
      }
   }
}
