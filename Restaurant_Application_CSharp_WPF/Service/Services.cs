using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Application_CSharp_WPF.Service;

namespace Restaurant_Application_CSharp_WPF.Service
{
    public static class Services
    {
        public static List<Employee> GetEmployee(string pin)
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var empDetails = (from e in restaurantEntities.Employees
                                  select e).Where(e => e.Password == pin);

                return empDetails.ToList();
            }

                
        }

        // GET Orders Table
        public static dynamic GetOrderDetails(int empid)
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var orderDetailsJoin = (from oh in restaurantEntities.OrderHeaders
                                        join e in restaurantEntities.Employees
                                        on oh.EmpID equals e.EmpID
                                        select new
                                        {
                                            EmpId = oh.EmpID,
                                            TableNo = oh.TableID,
                                            OrderNo = oh.OrderID,
                                            CreationTime = oh.CreationTime,
                                            Employee = e.EmployeType
                                        }).Where(e => e.EmpId == empid);

                return orderDetailsJoin.ToList();
            }
                
        }

        // Get Restaurant Availability Tables
        public static List<RestaurantTable> GetrestaurantTables()
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var restaurantTables = (from rt in restaurantEntities.RestaurantTables
                                        select rt);

                return restaurantTables.ToList();
            }
        }

        // Get Order Detail based on given order id given
        public static dynamic GetOrderDetailByOrderId(int orderId)
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var orderDetailByOrderId = (from od in restaurantEntities.OrderDetails
                                            join p in restaurantEntities.Products
                                            on od.ProductID equals p.ProductID
                                            join oh in restaurantEntities.OrderHeaders
                                            on od.OrderID equals oh.OrderID
                                            select new
                                            {
                                                OrderNo = od.OrderID,
                                                TableNo = oh.TableID,
                                                ProductName = p.ProductName,
                                                Quantity = od.Quantity,
                                                Price = p.Price * od.Quantity,
                                                Ready = od.IsReady
                                            }).Where(x => x.OrderNo == orderId);

                return orderDetailByOrderId.ToList();
            }
        }
    }
}
