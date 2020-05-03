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
using Restaurant_Application_CSharp_WPF.Service;

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
            
            tbxPin.Password = strPin;
                                            
        }

        private void btnCloseLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClearLogin_Click(object sender, RoutedEventArgs e)
        {
            strPin = "";
            tbxPin.Clear();
        }

        private void btnEnterLogin_Click(object sender, RoutedEventArgs e)
        {
            var emp = Services.GetEmployee(strPin);
            if(emp.Count() == 1)
            {
                UserLoginState user = new UserLoginState();
                user.empId = emp[0].EmpID;
                user.FullName = emp[0].FullName;
                user.EmployeeType = emp[0].EmployeType;
                user.Login();

                WaiterPage waiterPage = new WaiterPage(user);
                waiterPage.Show();

            }
            else
            {
                MessageBox.Show("Pin Entered Is Not In Record. Please Try Again!", "Login Failed");
                strPin = "";
                tbxPin.Clear();
            }
        }
    }
}
