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
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        public UserLoginState user { get; set; }
        public NewOrder(UserLoginState user)
        {
            this.user = user;

            InitializeComponent();

            lblEmpName.Content = user.FullName;

            dgTablesNO.ItemsSource = Services.GetAvailableTables();

            cbCategory.ItemsSource = Services.GetFoodCategory();

        }

        private void btnCloseNO_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(cbCategory.SelectedItem.ToString());
            string prodCategory = cbCategory.SelectedItem.ToString();   // food category from combobox

            dgProductsNO.ItemsSource = Services.GetProductByCategory(prodCategory);     // Populate Product Table
        }

        bool isTableSelected = false;
        int tableNo = 0;
        private void btnConfirmTable_Click(object sender, RoutedEventArgs e)
        {
            dynamic table = dgTablesNO.SelectedItem;    // Annonymous data type

            if(table != null)
            {
                isTableSelected = true;
                tableNo = table.TableNo;
                //MessageBox.Show(table.TableNo.ToString());
                string strTableSelection = $"Table No: {table.TableNo.ToString()}";
                lblTableSelection.Content = strTableSelection;
                lblTableSelection.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Please Select Table Before Proceed", "Table Not Selected");
            }
            
        }

        List<NewProduct> theNewOrder = new List<NewProduct>();
        private void btnAddNO_Click(object sender, RoutedEventArgs e)
        {
            int prodQuantity = 0;

            if (cbQuantity.SelectedIndex > -1)   // comboBox selected
            {
                prodQuantity = int.Parse(cbQuantity.Text);  // Product Quatity in (int32)
                //MessageBox.Show(prodQuantity.GetType().ToString());
            }
            else
            {
                MessageBox.Show("Please Provide Quantity Before Proceed!", "Product Quatity Required");
            }

            dynamic product = dgProductsNO.SelectedItem;

            

            if(product != null)
            {
                string productName = product.ProductName;
                int productId = product.ProductId;
                theNewOrder.Add(new NewProduct() { ProdId = productId, ProdName = productName, ProdQuantity = prodQuantity });
                RefreshProductList();


                //MessageBox.Show(product.ProductId.ToString());
            }
            else
            {
                MessageBox.Show("Please Select Product Before Proceed!", "Product Selection Required");
            }


        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {           

            NewProduct newProduct = new NewProduct();
            newProduct = lvProductAdded.SelectedItem as NewProduct;

            theNewOrder.Remove(newProduct);
            RefreshProductList();
            //MessageBox.Show(newProduct.ProdName.ToString());

        }

        // Refresh Product list Method
        public void RefreshProductList()
        {
            lvProductAdded.ItemsSource = null;
            lvProductAdded.ItemsSource = theNewOrder;
        }

        
    }

    public class NewProduct
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public int ProdQuantity { get; set; }
    }
}
