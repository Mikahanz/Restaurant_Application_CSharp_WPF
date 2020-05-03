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

namespace Restaurant_Application_CSharp_WPF
{
    /// <summary>
    /// Interaction logic for WaiterPage.xaml
    /// </summary>
    public partial class WaiterPage : Window
    {
        public WaiterPage()
        {
            InitializeComponent();

            Student student = new Student();
            List<Student> students = student.generateStudents();

            PhoneNumbers phoneNumber = new PhoneNumbers();
            List<PhoneNumbers> phoneNumbers = phoneNumber.generatePhoneNumbers();

            //Query syntax
            var studentsList = from stu in students
                               where stu.Gender == Gender.Male
                               select stu;

            dgOrders.ItemsSource = studentsList;
            dgTables.ItemsSource = studentsList;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
