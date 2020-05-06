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
using Restaurant_Application_CSharp_WPF.Service;

namespace Restaurant_Application_CSharp_WPF
{
    /// <summary>
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        public UserLoginState user { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public UpdateOrder(UserLoginState user, int orderId, int tableId)
        {
            this.user = user;
            this.OrderId = orderId;
            this.TableId = tableId;

            InitializeComponent();

            lblEmpNameUpd.Content = $"Employee Name: {user.FullName}";
            lblEmpNo.Content = $"Employee No  : {user.empId}";
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

        private void btnAddUpd_Click(object sender, RoutedEventArgs e)
        {
            dynamic product = dgProductsUpd.SelectedItem;
            int prodQuantity = int.Parse(cbQuantityUpd.Text);
            //MessageBox.Show($"{this.OrderId},{product.ProductId},{product.ProductName},{prodQuantity}");

            MessageBoxResult result = MessageBox.Show($"Do You Sure You Want To Add Item no {product.ProductNo} ({product.ProductName})", "Add New Item To Existing Cart", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderID = this.OrderId,
                        ProductID = product.ProductNo,
                        Quantity = prodQuantity,
                        IsReady = false
                    };

                    restaurantEntities.OrderDetails.Add(orderDetail);

                    try
                    {
                        restaurantEntities.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                RefreshDGOrderDetail(); // refreshing the orderdetail table on UpdateOrder Page
                this.user.refreshingWaiterPage($"Item No {product.ProductNo} ({product.ProductName}) Has Been Added To Order No {this.OrderId}");

            }
        }

        public void RefreshDGOrderDetail()
        {
            dgOrderDetailUpd.ItemsSource = null;
            dgOrderDetailUpd.ItemsSource = Services.GetOrderDetailByOrderId(this.OrderId);
        }

        private void btnRemoveUpd_Click(object sender, RoutedEventArgs e)
        {
            dynamic product = dgOrderDetailUpd.SelectedItem;

            MessageBoxResult result = MessageBox.Show($"Are You Sure You Want To Remove Item No {product.ProductNo} {product.ProductName}?", "Remove Item From Existing Cart", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail = restaurantEntities.OrderDetails.Find(product.OrderDetailNo);

                    restaurantEntities.OrderDetails.Remove(orderDetail);

                    try
                    {
                        restaurantEntities.SaveChanges();
                        MessageBox.Show($"Item: {product.ProductNo} {product.ProductName}, has been removed from order No: {this.OrderId}");
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                RefreshDGOrderDetail();
                this.user.refreshingWaiterPage($"Item No {product.ProductNo} {product.ProductName} Has Been Removed From Order No {this.OrderId}");
            }

            //MessageBox.Show($"{product.OrderDetailNo}");
        }

        // Update
        //Employee employee = new Employee();
        //employee = restaurantEntities.Employees.Find(106);

        //employee.FullName = "Nolan Hanzel";           
        //restaurantEntities.SaveChanges();
        private void btnEditUpd_Click(object sender, RoutedEventArgs e)
        {
            dynamic productMenu = dgProductsUpd.SelectedItem;
            dynamic productOrder = dgOrderDetailUpd.SelectedItem;
            int prodQuantity = int.Parse(cbQuantityUpd.Text);

            MessageBoxResult result = MessageBox.Show($"Are You Sure You Want To Change Item {productOrder.ProductNo} ({productOrder.ProductName}) with Item {productMenu.ProductNo} ({productMenu.ProductName})?", 
                "Change Order Item From Existing Cart", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail = restaurantEntities.OrderDetails.Find(productOrder.OrderDetailNo);

                    orderDetail.ProductID = productMenu.ProductNo;
                    orderDetail.Quantity = prodQuantity;

                    try
                    {
                        restaurantEntities.SaveChanges();
                    }
                    catch(DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                RefreshDGOrderDetail();
                this.user.refreshingWaiterPage($"Item {productOrder.ProductNo} ({productOrder.ProductName}) Has Been Change with Item {productMenu.ProductNo} ({productMenu.ProductName})");
            }

                

        }
    }
}
