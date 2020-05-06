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
   
    public partial class WaiterPage : Window
    {
        public UserLoginState User { get; set; }
        public WaiterPage(UserLoginState user)
        {
            this.User = user;

            InitializeComponent();
            
            lblEmpName.Content = $"Employee Name: { user.FullName}";
            lblEmpNo.Content = $"Employee No: {user.empId}";
            tbPageTitle.Text = user.EmployeeType;

            // populate order table
            dgOrders.ItemsSource = Services.GetOrderDetailsActive(user.empId);

            // Populata restaurant table
            dgTables.ItemsSource = Services.GetrestaurantTables();

            User.RefreshWaiterPageEvent += User_RefreshWaiterPageEvent;
        }

        private void User_RefreshWaiterPageEvent(object sender, string str)
        {
            // refresh orders table
            dgOrders.ItemsSource = null; ;
            dgOrders.ItemsSource = Services.GetOrderDetailsActive(this.User.empId);

            // refresh Rest Table table
            dgTables.ItemsSource = null;
            dgTables.ItemsSource = Services.GetrestaurantTables();

            // show notification
            lblNotification.Content = str;
            lblNotification.Visibility = Visibility.Visible;
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
            }
            else
            {
                MessageBox.Show("Please Select Order Detail Before Proceed!", "Order Selection Required");
            }
        }

        private void lblNotification_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lblNotification.Visibility = Visibility.Collapsed;
        }

        private void dgOrdersNotActive_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic od = dgOrdersNotActive.SelectedItem;
            int orderId = od.OrderNo;
            int tableId = od.TableNo;
            DateTime time = od.CreationTime;

            OrderInfo ord = new OrderInfo(User, orderId, tableId, time);

            ord.Show();
        }

       

        private void btnShowClosedOrders_Checked(object sender, RoutedEventArgs e)
        {
            btnShowClosedOrders.Content = $"Close Inactive";
            dgOrdersNotActive.ItemsSource = Services.GetOrderDetailsNotActive(this.User.empId);
            tabClosedOrders.Visibility = Visibility.Visible;
            tabControl.SelectedIndex = 1;
        }

        private void btnShowClosedOrders_Unchecked(object sender, RoutedEventArgs e)
        {
            btnShowClosedOrders.Content = $"Inactive Orders";
            dgOrdersNotActive.ItemsSource = null;
            tabClosedOrders.Visibility = Visibility.Collapsed;
            tabControl.SelectedIndex = 0;
        }
    }
}
