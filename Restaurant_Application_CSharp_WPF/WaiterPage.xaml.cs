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
    /// Interaction logic for WaiterPage.xaml
    /// </summary>
    public partial class WaiterPage : Window
    {
        public UserLoginState User { get; set; }
        public WaiterPage(UserLoginState user)
        {
            this.User = user;

            InitializeComponent();
            
            //MessageBox.Show($"empid: {user.empId}, name: {user.FullName}, userTypE: {user.EmployeeType}, isuserlogin: {user.IsLoggedIn}");

            lblEmpName.Content = $"Employee Name: { user.FullName}";
            lblEmpNo.Content = $"Employee No: {user.empId}";
            tbPageTitle.Text = user.EmployeeType;

            // populate order table
            dgOrders.ItemsSource = Services.GetOrderDetails(user.empId);

            // Populata restaurant table
            dgTables.ItemsSource = Services.GetrestaurantTables();


        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic od = dgOrders.SelectedItem;
            int orderId = od.OrderNo;
            int tableId = od.TableNo;
            DateTime time = od.CreationTime;

            //MessageBox.Show($"orderid: {orderId}, table no: {tableId}");

            OrderInfo ord = new OrderInfo(User,orderId, tableId, time);

            ord.Show();
            
        }

        private void btnCloseWP_Click(object sender, RoutedEventArgs e)
        {
            User.Logout();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            NewOrder newOrder = new NewOrder(User);
            newOrder.Show();

            this.Hide();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            dynamic orderSelected = dgOrders.SelectedItem;


            if(orderSelected != null)   // Datagrid selected
            {
                int orderId = orderSelected.OrderNo;
                int tableId = orderSelected.TableNo;

                UpdateOrder updateOrder = new UpdateOrder(User, orderId, tableId);
                updateOrder.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Select Order Detail Before Proceed!", "Order Selection Required");
            }

            
        }
    }
}
