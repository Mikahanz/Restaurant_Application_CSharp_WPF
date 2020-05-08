using Restaurant_Application_CSharp_WPF.Model;
using Restaurant_Application_CSharp_WPF.Service;
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

namespace Restaurant_Application_CSharp_WPF.View
{
    /// <summary>
    /// Interaction logic for NewUpdateProduct.xaml
    /// </summary>
    public partial class NewUpdateProduct : Window
    {
        public UserLoginState User { get; set; }
        public Product Prod { get; set; }
        public NewUpdateProduct(UserLoginState user, Product prod, string operationType)
        {
            this.User = user;
            this.Prod = prod;

            InitializeComponent();

            if (operationType == "New")
            {
                btnSaveProduct.Visibility = Visibility.Visible;
                btnUpdateProduct.Visibility = Visibility.Collapsed;
            }
            else if (operationType == "Update")
            {
                btnSaveProduct.Visibility = Visibility.Collapsed;
                btnUpdateProduct.Visibility = Visibility.Visible;

                // Get Value from selected row in the datagrid and put them on textboxes field
                txbProductName.Text = prod.ProductName;
                txbPrice.Text = prod.Price.ToString();

                if (prod.Availability.Equals(true))
                {
                    cbAvailability.SelectedIndex = 0;
                }
                else
                {
                    cbAvailability.SelectedIndex = 1;
                }

                string prodType = prod.ProductType;

                switch (prodType)
                {
                    case "Appetizer":
                        cbProductType.SelectedIndex = 0;
                        break;
                    case "Main Course":
                        cbProductType.SelectedIndex = 1;
                        break;
                    case "Dessert":
                        cbProductType.SelectedIndex = 2;
                        break;
                    case "Beverage":
                        cbProductType.SelectedIndex = 3;
                        break;
                    default:
                        MessageBox.Show("Please Select Product Type!");
                        break;
                }

            }

            tbPageTitleNU.Text = $"{operationType} Employee";
        }

        // Create New Product
        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            // Textboxes validation (Can not be empty)
            if (txbProductName.Text.Equals(""))
            {
                lblProdNameError.Visibility = Visibility.Visible;
                txbProductName.Focus();
            }
            else if (txbPrice.Text.Equals(""))
            {
                lblPriceError.Visibility = Visibility.Visible;
                txbPrice.Focus();
            }
            else
            {
                // validation passed
                lblProdNameError.Visibility = Visibility.Collapsed;
                lblPriceError.Visibility = Visibility.Collapsed;

                // Save product to database
                string prodName = txbProductName.Text;
                decimal price = decimal.Parse(txbPrice.Text);
                bool availability = cbAvailability.Text.Equals("Yes")? true : false;
                string prodType = cbProductType.Text;

               
                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    restaurantEntities.Database.Log = Console.WriteLine;

                    // The new next ProductID
                    int newProdId = restaurantEntities.Products.Max(x => x.ProductID) + 1;

                    //MessageBox.Show($"{newProdId},{prodName}, {price}, {availability}, {prodType}");

                    Product product = new Product() { ProductID = newProdId, ProductName = prodName, Price = price, Availability = availability, ProductType = prodType };

                    MessageBoxResult result = MessageBox.Show($"Are you sure to add new Product {prodName}, As {prodType}?", "New Product", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            

                            restaurantEntities.Products.Add(product);
                            restaurantEntities.SaveChanges();

                            

                            MessageBox.Show($"Product {prodName}, As {prodType} Has Been Created!", "New Product Created");
                            this.User.refreshingWaiterPage($"Product {prodName}, As {prodType} Has Been Created!");
                        }
                        catch (DbUpdateException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    this.Close();
                }
            }
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            /// Textboxes validation (Can not be empty)
            if (txbProductName.Text.Equals(""))
            {
                lblProdNameError.Visibility = Visibility.Visible;
                txbProductName.Focus();
            }
            else if (txbPrice.Text.Equals(""))
            {
                lblPriceError.Visibility = Visibility.Visible;
                txbPrice.Focus();
            }
            else
            {
                // validation passed
                lblProdNameError.Visibility = Visibility.Collapsed;
                lblPriceError.Visibility = Visibility.Collapsed;


                string prodName = txbProductName.Text;
                decimal price = decimal.Parse(txbPrice.Text);
                bool availability = cbAvailability.Text.Equals("Yes") ? true : false;
                string prodType = cbProductType.Text;

                //MessageBox.Show($"{fullName},{userName},{passWord} -- {Emp.EmpID}");

                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    Product product = new Product();
                    product = restaurantEntities.Products.Find(Prod.ProductID);

                    product.ProductName = prodName;
                    product.Price = price;
                    product.Availability = availability;
                    product.ProductType = prodType;

                    MessageBoxResult result = MessageBox.Show($"Are you sure to Change Product from {Prod.ProductName} to {prodName}?", "Edit Product", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            restaurantEntities.SaveChanges();
                            MessageBox.Show($"Employee {Prod.ProductName} Has Been Changed To {prodName}!", "Employee Edited");
                            this.User.refreshingWaiterPage($"Employee {Prod.ProductName} Has Been Changed To {prodName}!");
                        }
                        catch (DbUpdateException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    this.Close();
                }
            }
        }

        private void btnCloseNU_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
