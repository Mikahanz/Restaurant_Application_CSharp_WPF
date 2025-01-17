﻿using System;
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

namespace Restaurant_Application_CSharp_WPF.View
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public UserLoginState User { get; set; }
        public Manager(UserLoginState user)
        {
            this.User = user;

            InitializeComponent();

            lblEmpName.Content = $"Employee Name: { user.FullName}";
            lblEmpNo.Content = $"Employee No: {user.empId}";
            tbPageTitle.Text = user.EmployeeType;

            // populate order table
            dgOrders.ItemsSource = Services.GetAllOrdersActive();

            // Populata restaurant table
            dgTables.ItemsSource = Services.GetrestaurantTables();

            // Populate Employees Table
            dgEmployees.ItemsSource = Services.GetAllEmployees();

            // Populate Products Table
            dgProducts.ItemsSource = Services.GetAllProducts();

            User.RefreshWaiterPageEvent += User_RefreshWaiterPageEvent;
        }

        private void User_RefreshWaiterPageEvent(object sender, string str)
        {
            // refresh orders table
            dgOrders.ItemsSource = null; ;
            dgOrders.ItemsSource = Services.GetAllOrdersActive();

            // refresh unactive orders table
            dgOrdersNotActive.ItemsSource = null;
            dgOrdersNotActive.ItemsSource = Services.GetAllOrdersNotActive(); 

            // refresh Rest Table table
            dgTables.ItemsSource = null;
            dgTables.ItemsSource = Services.GetrestaurantTables();

            // Refresh Employees Table
            dgEmployees.ItemsSource = null;
            dgEmployees.ItemsSource = Services.GetAllEmployees();

            // Refresh Products Table
            dgProducts.ItemsSource = null;
            dgProducts.ItemsSource = Services.GetAllProducts();

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

            OrderInfo ord = new OrderInfo(User, orderId, tableId, time);

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


            if (orderSelected != null)   // Datagrid selected
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
            dgOrdersNotActive.ItemsSource = Services.GetAllOrdersNotActive();
            tabClosedOrders.Visibility = Visibility.Visible;
            tabControl.SelectedIndex = 1;
        }

        private void btnShowClosedOrders_Unchecked(object sender, RoutedEventArgs e)
        {
            btnShowClosedOrders.Content = $"Inactive Orders";
            dgOrdersNotActive.ItemsSource = Services.GetAllOrdersNotActive();
            tabClosedOrders.Visibility = Visibility.Collapsed;
            tabControl.SelectedIndex = 0;
        }

        // Create New Employee
        private void btnNewEmployees_Click(object sender, RoutedEventArgs e)
        {
            string operationType = "New";

            NewUpdateEmployee newUpdateEmployee = new NewUpdateEmployee(User, new Employee(), operationType);
            newUpdateEmployee.Show();

        }

        // Update Employee
        private void btnUpdateEmployees_Click(object sender, RoutedEventArgs e)
        {
            if(dgEmployees.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Any Row That You Would Like To Update!", "Row Selection Required");
            }
            else
            {
                Employee emp = dgEmployees.SelectedItem as Employee;
                string operationType = "Update";

                NewUpdateEmployee newUpdateEmployee = new NewUpdateEmployee(User, emp, operationType);
                newUpdateEmployee.Show();
            }
        }

        // Delete Employee
        private void btnDeleteEmployees_Checked(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Any Row That You Would Like To Delete!", "Row Selection Required");
            }
            else
            {
                Employee employee = dgEmployees.SelectedItem as Employee;
                int empid = employee.EmpID;

                MessageBoxResult result = MessageBox.Show($"Are You Sure To Delete Employee {employee.FullName}?", "Delete Employee", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // Delete Employee
                    using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                    {
                        Employee emp = new Employee();
                        emp = restaurantEntities.Employees.Find(empid);

                        try
                        {
                            restaurantEntities.Employees.Remove(emp);
                            restaurantEntities.SaveChanges();
                            MessageBox.Show($"Employee {employee.FullName} Has Been Deleted");
                            User.refreshingWaiterPage($"Employee {employee.FullName} Has Been Deleted");
                        }
                        catch (DbUpdateException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            
        }

        // Create New Product
        private void btnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            string operationType = "New";

            NewUpdateProduct newUpdateProduct = new NewUpdateProduct(User, new Product(), operationType);
            newUpdateProduct.Show();
        }

        // Update Product
        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Any Row That You Would Like To Update!", "Row Selection Required");
            }
            else
            {
                Product product = dgProducts.SelectedItem as Product;
                string operationType = "Update";

                NewUpdateProduct newUpdateProduct = new NewUpdateProduct(User, product, operationType);
                newUpdateProduct.Show();
            }
        }

        // Delete Product
        private void btnDeleteProduct_Checked(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Any Row That You Would Like To Delete!", "Row Selection Required");
            }
            else
            {
                Product product = dgProducts.SelectedItem as Product;
                int prodId = product.ProductID;

                MessageBoxResult result = MessageBox.Show($"Are You Sure To Delete Product {product.ProductName}?", "Delete Product", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // Delete Product
                    using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                    {
                        Product product1 = new Product();
                        product1 = restaurantEntities.Products.Find(prodId);

                        try
                        {
                            restaurantEntities.Products.Remove(product1);
                            restaurantEntities.SaveChanges();
                            MessageBox.Show($"Product {product.ProductName} Has Been Deleted");
                            User.refreshingWaiterPage($"Product {product.ProductName} Has Been Deleted");
                        }
                        catch (DbUpdateException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            

        }
    }


}
