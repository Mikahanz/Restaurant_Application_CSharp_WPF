using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Restaurant_Application_CSharp_WPF.Service;

namespace Restaurant_Application_CSharp_WPF
{
    /// <summary>
    /// Interaction logic for OrderDetail.xaml
    /// </summary>
    public partial class OrderDetail : Window
    {
        
        public OrderDetail(int orderId, int tableId, DateTime time)
        {
            
            InitializeComponent();

            lblOrderNoText.Content = orderId;   // OrderId label
            lblTableText.Content = tableId;     // TableId label
                        
            dgOrderDetail.ItemsSource = Services.GetOrderDetailByOrderId(orderId);      // Populate table


            decimal SubPrice = Services.GetOrderTotalPrice(orderId); // Price
            lblSubTotalText.Content = SubPrice ;               // SubTotalPrice Label

            decimal TotalPrice = Math.Round((SubPrice + (SubPrice * 0.15m)), 2);
            lblTotalText.Content = TotalPrice; // Total Price Label

            lblTimeText.Content = time;
        }

        private void btnCloseOD_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to Checkout? Checkout will close the order!", "Check Out", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

                //MessageBox.Show("YESSSS");
            }
            else
            {
                //MessageBox.Show("NOOOO");
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            

            //Directory.CreateDirectory(newPath);

            

            Invoice invoice = new Invoice();
            invoice.Show();

        }
    }
}
