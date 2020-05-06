using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Application_CSharp_WPF.Service
{
    public class UserLoginState
    {
        public event EventHandler<string> RefreshWaiterPageEvent; // event variable
        public event EventHandler<string> RefreshOrderInfoPageEvent; // event variable
        public int empId { get; set; }
        public string FullName { get; set; }
        public string EmployeeType { get; set; }

        public bool IsLoggedIn { get; private set; }

        public void Login()
        {
            this.IsLoggedIn = true;
        }

        public void Logout()
        {
            this.IsLoggedIn = false;
        }

        // methods for delegate event
        public void refreshingWaiterPage(string str)
        {
            RefreshWaiterPageEvent?.Invoke(this, str);
        }

        public void refreshingOrderDetailPage(string str)
        {
            RefreshOrderInfoPageEvent?.Invoke(this, str);
        }
    }
}
