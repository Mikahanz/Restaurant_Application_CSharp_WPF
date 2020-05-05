using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        public UserLoginState User { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubPrice { get; set; }
        public int TableId { get; set; }
        public int OrderId { get; set; }
        public DateTime Time { get; set; }
        public Invoice(UserLoginState user, decimal totalPrice, decimal subPrice, int tableId, int orderId, DateTime time)
        {
            this.User = user;
            this.TotalPrice = totalPrice;
            this.SubPrice = subPrice;
            this.TableId = tableId;
            this.OrderId = orderId;
            this.Time = time;
            InitializeComponent();

            txblkDate.Text = $"{time:MMMM dd yyyy}";
            txblkInvoiceNo.Text = $"HZR-I{orderId}";
            txblkOrderNo.Text = $"{orderId}";
            txblkTableNo.Text = $"{tableId}";
            txblkTotal.Text = $"${totalPrice}";
            txblkSubTotal.Text = $"${subPrice}";
            txblkEmpName.Text = $"{this.User.FullName}";

            dgOrderDetail.ItemsSource = Services.GetOrderDetailByOrderId(orderId);

        }

        private void btnPrintInvoice_Click(object sender, RoutedEventArgs e)
        {
            
            PrintDialog printDialog = new PrintDialog();            

            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(print, "Restaurant_Application_CSharp_WPF");

            }
            
            
        }

        private void btnCloseInvoice_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
