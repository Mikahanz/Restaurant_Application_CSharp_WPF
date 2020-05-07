using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
using Restaurant_Application_CSharp_WPF.Model;
using Restaurant_Application_CSharp_WPF.Service;

namespace Restaurant_Application_CSharp_WPF
{
    /// <summary>
    /// Interaction logic for KitchenStaff.xaml
    /// </summary>
    public partial class KitchenStaff : Window
    {
        private UserLoginState User { get; set; }
        public KitchenStaff(UserLoginState user)
        {
            this.User = user;
            InitializeComponent();

            dgOrdersToBeServed.ItemsSource = Services.GetallOrdersThatNotReady();
            dgHadBeenServed.ItemsSource = Services.GetallOrdersThatReady();

            lblEmpName.Content = $"Chef Name: {this.User.FullName}";
            lblEmpNo.Content = $"Emp No: {this.User.empId}";
            tbPageTitle.Text = this.User.EmployeeType;
        }

        private void btnChangeOrderReady_Click(object sender, RoutedEventArgs e)
        {
            dynamic product = dgOrdersToBeServed.SelectedItem;

            if(product != null)
            {
                using(RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail = restaurantEntities.OrderDetails.Find(product.OrderDetailNo);

                    orderDetail.IsReady = true;

                    try
                    {
                        restaurantEntities.SaveChanges();
                        User.refreshingWaiterPage($"Order No: {product.OrderNo}({product.ProductName}) Is Ready To Be Served!");
                        RefreshDGOnKitchenPage();
                        lblNotification1.Content = $"Order No: {product.OrderNo}({product.ProductName}) Is Ready To Be Served!";
                        lblNotification1.Visibility = Visibility.Visible;
                    }
                    catch(DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //MessageBox.Show($"{},{product.IsReady}");
            }
            else
            {
                MessageBox.Show($"Please Select Item To Be Updated!", "Item Selection Required");
            }
        }
            
            

        private void lblNotification1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lblNotification1.Visibility = Visibility.Collapsed;
        }

        private void btnCloseWP_Click(object sender, RoutedEventArgs e)
        {
            User.Logout();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tabControl.SelectedIndex == 1)
            {
                btnChangeOrderReady.Visibility = Visibility.Hidden;
                btnChangeOrderNotReady.Visibility = Visibility.Visible;
            }
            else
            {
                btnChangeOrderReady.Visibility = Visibility.Visible;
                btnChangeOrderNotReady.Visibility = Visibility.Hidden;
            }
        }

        private void btnChangeOrderNotReady_Click(object sender, RoutedEventArgs e)
        {

        }

        public void RefreshDGOnKitchenPage()
        {
            dgHadBeenServed.ItemsSource = null;
            dgOrdersToBeServed.ItemsSource = null;

            dgOrdersToBeServed.ItemsSource = Services.GetallOrdersThatNotReady();
            dgHadBeenServed.ItemsSource = Services.GetallOrdersThatReady();
        }
    }
}
