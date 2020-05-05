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
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {

        public UserLoginState User { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal SubPrice { get; set; }
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public DateTime Time { get; set; }



        public OrderInfo(UserLoginState user, int orderId, int tableId, DateTime time)
        {

            InitializeComponent();

            this.User = user;
            this.TableId = tableId;
            this.OrderId = orderId;
            this.Time = time;

            lblOrderNoText.Content = orderId;   // OrderId label
            lblTableText.Content = tableId;     // TableId label

            dgOrderDetail.ItemsSource = Services.GetOrderDetailByOrderId(orderId);      // Populate table


            this.SubPrice = Services.GetOrderTotalPrice(orderId); // Price
            lblSubTotalText.Content = this.SubPrice;               // SubTotalPrice Label

            this.TotalPrice = Math.Round((SubPrice + (SubPrice * 0.15m)), 2);
            lblTotalText.Content = this.TotalPrice; // Total Price Label

            lblTimeText.Content = this.Time;


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


            Invoice invoice = new Invoice(this.User, this.TotalPrice, this.SubPrice, this.TableId, this.OrderId, this.Time);
            invoice.Show();

        }
    }
}
