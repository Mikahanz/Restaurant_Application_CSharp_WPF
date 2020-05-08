using Restaurant_Application_CSharp_WPF.Model;
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
using Restaurant_Application_CSharp_WPF.Model;
using System.Data.Entity.Infrastructure;
using Restaurant_Application_CSharp_WPF.Service;

namespace Restaurant_Application_CSharp_WPF.View
{
    /// <summary>
    /// Interaction logic for NewUpdateEmployee.xaml
    /// </summary>
    public partial class NewUpdateEmployee : Window
    {
        public UserLoginState User { get; set; }
        public Employee Emp { get; set; }
        public NewUpdateEmployee(UserLoginState user, Employee emp, string operationType)
        {
            this.User = user;
            this.Emp = emp;

            InitializeComponent();

            if(operationType == "New")
            {
                btnSave.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Collapsed;
            }
            else if(operationType == "Update")
            {
                btnSave.Visibility = Visibility.Collapsed;
                btnUpdate.Visibility = Visibility.Visible;

                // Get Value from selected row in the datagrid and put them on textboxes field
                txbFullName.Text = emp.FullName;
                txbUserName.Text = emp.UserName;
                pwdBox.Password = emp.Password;

                string empType = emp.EmployeType;

                switch (empType)
                {
                    case "Waiter":
                        cbEmpType.SelectedIndex = 0;
                        break;
                    case "Manager":
                        cbEmpType.SelectedIndex = 1;
                        break;
                    case "Chef":
                        cbEmpType.SelectedIndex = 2;
                        break;
                    default:
                        MessageBox.Show("Pleace Select Emplyee Type");
                        break;
                }

            }

            tbPageTitleNU.Text = $"{operationType} Employee";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Textboxes validation (Can not be empty)
            if (txbFullName.Text.Equals(""))
            {
                lblFNError.Visibility = Visibility.Visible;
                txbFullName.Focus();
            }
            else if (txbUserName.Text.Equals(""))
            {
                lblUNError.Visibility = Visibility.Visible;
                txbUserName.Focus();
            }
            else if (pwdBox.Password.Equals(""))
            {
                lblPWError.Visibility = Visibility.Visible;
                pwdBox.Focus();
            }
            else
            {
                // validation passed
                lblFNError.Visibility = Visibility.Collapsed;
                lblUNError.Visibility = Visibility.Collapsed;
                lblPWError.Visibility = Visibility.Collapsed;

                // Save employee to database
                string fullName = txbFullName.Text;
                string userName = txbUserName.Text;
                string passWord = pwdBox.Password;
                string empType = cbEmpType.Text;


                using(RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    Employee employee = new Employee() { FullName = fullName, UserName = userName, Password = passWord, EmployeType = empType };

                    restaurantEntities.Employees.Add(employee);

                    MessageBoxResult result = MessageBox.Show($"Are you sure to add new employee {fullName}, As {empType}?", "New Employee", MessageBoxButton.YesNo);

                    if(result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            restaurantEntities.SaveChanges();
                            MessageBox.Show($"Employee {fullName}, As {empType} Has Been Created!", "New Employee Created");
                            this.User.refreshingWaiterPage($"Employee {fullName}, As {empType} Has Been Created!");
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Textboxes validation (Can not be empty)
            if (txbFullName.Text.Equals(""))
            {
                lblFNError.Visibility = Visibility.Visible;
                txbFullName.Focus();
            }
            else if (txbUserName.Text.Equals(""))
            {
                lblUNError.Visibility = Visibility.Visible;
                txbUserName.Focus();
            }
            else if (pwdBox.Password.Equals(""))
            {
                lblPWError.Visibility = Visibility.Visible;
                pwdBox.Focus();
            }
            else
            {
                // validation passed
                lblFNError.Visibility = Visibility.Collapsed;
                lblUNError.Visibility = Visibility.Collapsed;
                lblPWError.Visibility = Visibility.Collapsed;

                // Save employee to database
                string fullName = txbFullName.Text;
                string userName = txbUserName.Text;
                string passWord = pwdBox.Password;
                string empType = cbEmpType.Text;

                //MessageBox.Show($"{fullName},{userName},{passWord} -- {Emp.EmpID}");

                using (RestaurantEntities restaurantEntities = new RestaurantEntities())
                {
                    Employee employee = new Employee();
                    employee = restaurantEntities.Employees.Find(Emp.EmpID);

                    employee.FullName = fullName;
                    employee.UserName = userName;
                    employee.Password = passWord;
                    employee.EmployeType = empType;
                                       
                    MessageBoxResult result = MessageBox.Show($"Are you sure to Change employee from {Emp.FullName} to {fullName}?", "Edit Employee", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            restaurantEntities.SaveChanges();
                            MessageBox.Show($"Employee {Emp.FullName} Has Been Changed To {fullName}!", "Employee Edited");
                            this.User.refreshingWaiterPage($"Employee {Emp.FullName} Has Been Changed To {fullName}!");
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
    }
}
