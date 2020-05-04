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
    }
}
