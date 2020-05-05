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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        public UserLoginState user { get; set; }
        public UpdateOrder(UserLoginState user, int orderId, int tableId)
        {
            this.user = user;

            InitializeComponent();

            lblEmpNameUpd.Content = $"Employee Name: {user.FullName}";
            lblEmpNo.Content =      $"Employee No  : {user.empId}";
            lblOrderNo.Content = $"Order No  : {orderId}";
            lblTableNo.Content = $"Table No  : {tableId}";

            cbCategoryUpd.ItemsSource = Services.GetFoodCategory();

            dgOrderDetailUpd.ItemsSource = Services.GetOrderDetailByOrderId(orderId);
        }

        private void btnCloseUO_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbCategoryUpd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string prodCategory = cbCategoryUpd.SelectedItem.ToString();   // food category from combobox

            dgProductsUpd.ItemsSource = Services.GetProductByCategory(prodCategory);     // Populate Product Table
        }
    }
}
