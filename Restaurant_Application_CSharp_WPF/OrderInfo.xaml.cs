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
using System.Data.Entity.Infrastructure;

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

            User.RefreshOrderInfoPageEvent += User_RefreshOrderInfoPageEvent;
        }

        private void User_RefreshOrderInfoPageEvent(object sender, string str)
        {
            dgOrderDetail.ItemsSource = null;
            dgOrderDetail.ItemsSource = Services.GetOrderDetailByOrderId(this.OrderId);

            // show notification
            lblNotification.Content = str;
            lblNotification.Visibility = Visibility.Visible;
        }

        private void btnCloseOD_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // BUTTON CHECKOUT
        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to Checkout? Checkout will close the order!", "Check Out", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var wantPrint = MessageBox.Show("Would You Like To Print Receipt? Yes, Will Close The Order And Print Receipt! No, Will Only Close The Order", "Print Receipt", MessageBoxButton.YesNo);
                if(wantPrint == MessageBoxResult.Yes)
                {
                    Invoice invoice = new Invoice(this.User, this.TotalPrice, this.SubPrice, this.TableId, this.OrderId, this.Time);
                    invoice.Show();
                }

                // Check Out

                    // Change IsServing Status To False and Enter TotalPrice 
                using(RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    OrderHeader orderHeader = new OrderHeader();
                    orderHeader = restaurantEntities.OrderHeaders.Find(this.OrderId);

                    orderHeader.IsServing = false;  //IsServing Status To False
                    orderHeader.TotalPrice = this.SubPrice; //Enter TotalPrice (Total bofore taxes)

                    try
                    {
                        restaurantEntities.SaveChanges();
                    }
                    catch(DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                // Change RestaurantTable Availability to True
                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    RestaurantTable restaurantTable = new RestaurantTable();
                    restaurantTable = restaurantEntities.RestaurantTables.Find(this.TableId);

                    restaurantTable.Availability = true;

                    try
                    {
                        restaurantEntities.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                // Message 
                MessageBox.Show($"Order No: {this.OrderId} Has Now Been Closed, and Table No: {this.TableId} Is Now Available", "Order Closed");

                // Refresh WaiterPage Orders Table and Table Availability
                this.User.refreshingWaiterPage($"Order No: {this.OrderId} Has Now Been Closed, and Table No: {this.TableId} Is Now Available");
                
                // Close This window and open
                this.Close();
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

        private void lblNotification_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lblNotification.Visibility = Visibility.Collapsed;
        }
    }
}
