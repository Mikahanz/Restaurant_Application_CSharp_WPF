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
        public UserLoginState user { get; set; }
        public WaiterPage(UserLoginState user)
        {
            this.user = user;

            InitializeComponent();
            
            //MessageBox.Show($"empid: {user.empId}, name: {user.FullName}, userTypE: {user.EmployeeType}, isuserlogin: {user.IsLoggedIn}");

            lblEmpName.Content = user.FullName;
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

            //MessageBox.Show($"orderid: {orderId}, table no: {tableId}");

            OrderDetail ord = new OrderDetail(orderId, tableId);

            ord.Show();
            
        }
    }
}
