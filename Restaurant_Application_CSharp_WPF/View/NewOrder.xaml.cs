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
using Restaurant_Application_CSharp_WPF.Model;

namespace Restaurant_Application_CSharp_WPF
{
    
    public partial class NewOrder : Window
    {
        public UserLoginState user { get; set; }
        public int TableNo { get; set; }
        public NewOrder(UserLoginState user)
        {
            this.user = user;

            InitializeComponent();

            lblEmpName.Content = $"Employee Name: {user.FullName}";
            lblEmpNo.Content = $"Employee No: {user.empId}";

            dgTablesNO.ItemsSource = Services.GetAvailableTables();

            cbCategory.ItemsSource = Services.GetFoodCategory();

            btnRemove.Visibility = Visibility.Hidden;
            

        }

        private void btnCloseNO_Click(object sender, RoutedEventArgs e)
        {
            WaiterPage waiterPage = new WaiterPage(user);
            waiterPage.Show();

            this.Close();
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string prodCategory = cbCategory.SelectedItem.ToString();   // food category from combobox

            dgProductsNO.ItemsSource = Services.GetProductByCategory(prodCategory);     // Populate Product Table
        }

        bool isTableSelected = false;
        
        private void btnConfirmTable_Click(object sender, RoutedEventArgs e)
        {
            dynamic table = dgTablesNO.SelectedItem;    // Annonymous data type

            if(table != null)
            {
                isTableSelected = true;
                this.TableNo = table.TableNo;
                lblTableSelection.Content = $"Table No: {this.TableNo.ToString()}";
                lblTableSelection.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Please Select Table Before Proceed!", "Table Not Selected");
            }
            
        }

        List<NewProduct> theNewOrder = new List<NewProduct>();
        private void btnAddNO_Click(object sender, RoutedEventArgs e)
        {
            int prodQuantity = 0;

            if (cbQuantity.SelectedIndex > -1)   // comboBox selected
            {
                prodQuantity = int.Parse(cbQuantity.Text);  // Product Quatity in (int32)
            }
            else
            {
                MessageBox.Show("Please Provide Quantity Before Proceed!", "Product Quatity Required");
            }

            dynamic product = dgProductsNO.SelectedItem;

            if(product != null)
            {
                // Check if Tabel Selected
                if (isTableSelected == false)
                {
                    MessageBox.Show("Please Select Table No Before Proceed!", "Table Not Selected");
                }
                else
                {
                    string productName = product.ProductName;
                    int productId = product.ProductNo;
                    theNewOrder.Add(new NewProduct() { ProdId = productId, ProdName = productName, ProdQuantity = prodQuantity });
                    RefreshProductList();
                }

            }
            else
            {
                MessageBox.Show("Please Select Product Before Proceed!", "Product Selection Required");
            }

            if (lvProductAdded.Items.Count < 1)
            {
                btnRemove.Visibility = Visibility.Hidden;
            }
            else
            {
                btnRemove.Visibility = Visibility.Visible;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvProductAdded.Items.Count < 1)
            {
                MessageBox.Show("Please Add Item To Cart","Order Card Is Empty!");
                btnRemove.Visibility = Visibility.Hidden;
            }
            else if(lvProductAdded.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Product To Be Deleted!", "Product Selection Required");
            }
            else
            {
                NewProduct newProduct = new NewProduct();
                newProduct = lvProductAdded.SelectedItem as NewProduct;

                theNewOrder.Remove(newProduct);
                RefreshProductList();
                //MessageBox.Show(newProduct.ProdName.ToString());
            }
        }

        // Refresh Product list Method
        public void RefreshProductList()
        {
            lvProductAdded.ItemsSource = null;
            lvProductAdded.ItemsSource = theNewOrder;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            // Check if list is empty
            if (lvProductAdded.Items.Count < 1)
            {
                MessageBox.Show("Please Add Item To Cart", "Order Card Is Empty!");
            }
            else
            {
                // Insert to OrderHeader
                OrderHeader orderHeader = new OrderHeader()
                {
                    EmpID = this.user.empId,
                    TableID = this.TableNo,
                    CreationTime = DateTime.Now,
                    TotalPrice = null,
                    IsServing = true,
                    DiningIn = true
                };

                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    restaurantEntities.OrderHeaders.Add(orderHeader);
                    try
                    {
                        restaurantEntities.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                // Insert to OrderDetail
                for (int i = 0; i < theNewOrder.Count(); i++)
                {
                    using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            OrderID = orderHeader.OrderID,
                            ProductID = theNewOrder[i].ProdId,
                            Quantity = theNewOrder[i].ProdQuantity,
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
                }

                // Update RestaurantTable Availability to false
                using(RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    RestaurantTable restaurantTable = new RestaurantTable();
                    restaurantTable = restaurantEntities.RestaurantTables.Find(this.TableNo);

                    restaurantTable.Availability = false;

                    try
                    {
                        restaurantEntities.SaveChanges();
                    }
                    catch(DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                // Show Insertion Sucessful
                MessageBox.Show($"New Order {orderHeader.OrderID} Has Been Successfully Created!", "New Order Created Successfully");

                // Open waiter page
                //WaiterPage waiterPage = new WaiterPage(user);
                //waiterPage.Show();

                this.user.refreshingWaiterPage($"New Order {orderHeader.OrderID} Has Been Created");

                // close this page
                this.Close();
            }
        }
    }

    public class NewProduct
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public int ProdQuantity { get; set; }
    }
}
