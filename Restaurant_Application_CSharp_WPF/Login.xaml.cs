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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant_Application_CSharp_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }


        String strPin = "";
        private void btnPin_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            strPin += bt.Content.ToString();
            MessageBox.Show(strPin);
            tbxPin.Password = strPin;
                                            
        }
    }
}
