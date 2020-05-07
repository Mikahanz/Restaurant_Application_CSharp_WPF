using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Application_CSharp_WPF.Service;
using Restaurant_Application_CSharp_WPF.Model;

namespace Restaurant_Application_CSharp_WPF.Service
{
    public static class Services
    {
        // Get Employee using password
        public static List<Employee> GetEmployee(string pin)
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var empDetails = (from e in restaurantEntities.Employees
                                  select e).Where(e => e.Password == pin);

                return empDetails.ToList();
            }

                
        }

        // QUERY ALL RECORDS OF ORDERS THAT THAT ARE NOT READY - FOR KITCHEN STAFF
        public static dynamic GetallOrdersThatNotReady()
        {
            using(RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var allOrdersThatNotReady = from od in restaurantEntities.OrderDetails
                                            join oh in restaurantEntities.OrderHeaders
                                            on od.OrderID equals oh.OrderID
                                            join e in restaurantEntities.Employees
                                            on oh.EmpID equals e.EmpID
                                            join p in restaurantEntities.Products
                                            on od.ProductID equals p.ProductID
                                            where od.IsReady == false
                                            orderby oh.CreationTime descending
                                            select new
                                            {
                                                OrderDetailNo = od.OrderDetailID,
                                                OrderNo = od.OrderID,
                                                ProductNo = p.ProductID,
                                                ProductName = p.ProductName,
                                                Quantity = od.Quantity,
                                                UserName = e.UserName,
                                                EmpNo = e.EmpID,
                                                IsReady = od.IsReady,
                                                CreationTime = oh.CreationTime
                                            };

                return allOrdersThatNotReady.ToList();
            }
            
        }

        // QUERY ALL RECORDS OF ORDERS THAT THAT ARE READY - FOR KITCHEN STAFF
        public static dynamic GetallOrdersThatReady()
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var allOrdersThatNotReady = from od in restaurantEntities.OrderDetails
                                            join oh in restaurantEntities.OrderHeaders
                                            on od.OrderID equals oh.OrderID
                                            join e in restaurantEntities.Employees
                                            on oh.EmpID equals e.EmpID
                                            join p in restaurantEntities.Products
                                            on od.ProductID equals p.ProductID
                                            where od.IsReady == true
                                            orderby oh.CreationTime descending
                                            select new
                                            {
                                                OrderDetailNo = od.OrderDetailID,
                                                OrderNo = od.OrderID,
                                                ProductNo = p.ProductID,
                                                ProductName = p.ProductName,
                                                Quantity = od.Quantity,
                                                UserName = e.UserName,
                                                EmpNo = e.EmpID,
                                                IsReady = od.IsReady,
                                                CreationTime = oh.CreationTime
                                            };

                return allOrdersThatNotReady.ToList();
            }

        }

        // Get orders that are active
        public static dynamic GetAllOrdersActive()
        {
            using(RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var orderDetailsJoin = (from oh in restaurantEntities.OrderHeaders
                                        join e in restaurantEntities.Employees
                                        on oh.EmpID equals e.EmpID
                                        select new
                                        {
                                            EmpId = oh.EmpID,
                                            EmpUserName = e.UserName,
                                            TableNo = oh.TableID,
                                            OrderNo = oh.OrderID,
                                            CreationTime = oh.CreationTime,
                                            Employee = e.EmployeType,
                                            IsServing = oh.IsServing
                                        }).Where((e) => e.IsServing == true);

                return orderDetailsJoin.ToList();
            }
        }

        // Get orders that are not active
        public static dynamic GetAllOrdersNotActive()
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var orderDetailsJoin = (from oh in restaurantEntities.OrderHeaders
                                        join e in restaurantEntities.Employees
                                        on oh.EmpID equals e.EmpID
                                        select new
                                        {
                                            EmpId = oh.EmpID,
                                            EmpUserName = e.UserName,
                                            TableNo = oh.TableID,
                                            OrderNo = oh.OrderID,
                                            CreationTime = oh.CreationTime,
                                            Employee = e.EmployeType,
                                            IsServing = oh.IsServing
                                        }).Where((e) => e.IsServing == false);

                return orderDetailsJoin.ToList();
            }
        }

        // GET Orders That Are Active
        public static dynamic GetOrderDetailsActive(int empid)
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
                                            Employee = e.EmployeType,
                                            IsServing = oh.IsServing
                                        }).Where((e) => e.EmpId == empid && e.IsServing == true);

                return orderDetailsJoin.ToList();
            }
                
        }

        // GET Orders That Are Not Close
        public static dynamic GetOrderDetailsNotActive(int empid)
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
                                            Employee = e.EmployeType,
                                            IsServing = oh.IsServing
                                        }).Where((e) => e.EmpId == empid && e.IsServing == false);

                return orderDetailsJoin.ToList();
            }

        }

        // Get Restaurant Availability Tables
        public static dynamic GetrestaurantTables()
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var restaurantTables = (from rt in restaurantEntities.RestaurantTables
                                        select new 
                                        {
                                            TableNo = rt.TableID,
                                            Capacity = rt.Capacity,
                                            Availability = rt.Availability
                                        });

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
                                                OrderDetailNo = od.OrderDetailID,
                                                OrderNo = od.OrderID,
                                                TableNo = oh.TableID,
                                                ProductNo = p.ProductID,
                                                ProductName = p.ProductName,
                                                Quantity = od.Quantity,
                                                EachPrice = p.Price,
                                                Price = p.Price * od.Quantity,
                                                Ready = od.IsReady,
                                                IsServing = oh.IsServing
                                            }).Where(x => x.OrderNo == orderId);

                return orderDetailByOrderId.ToList();
            }
        }

        // Get OrderHeader Single Record by OrderId
        public static OrderHeader GetOrderHeaderByOrderId(int orderId)
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                OrderHeader oh = new OrderHeader();
                oh = restaurantEntities.OrderHeaders.Find(orderId);

                return oh;
            }
        }

        // Get Order Total Price
        public static decimal GetOrderTotalPrice(int orderId)
        {
            using (RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var OrderTotalPrice = (from od in restaurantEntities.OrderDetails
                                       join p in restaurantEntities.Products
                                       on od.ProductID equals p.ProductID
                                       select new
                                       {
                                           OrderNo = od.OrderID,
                                           Price = p.Price * od.Quantity
                                       }).Where(x => x.OrderNo == orderId).Sum(p => p.Price);

                return OrderTotalPrice; // return a decimal
            }
        }

        // Get only available tables
        public static dynamic GetAvailableTables()
        {
            using(RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var restaurantTables = (from rt in restaurantEntities.RestaurantTables
                                        select new
                                        {
                                            TableNo = rt.TableID,
                                            Capacity = rt.Capacity,
                                            Availability = rt.Availability
                                        }).Where(t => t.Availability == true);

                return restaurantTables.ToList();
            }
        }

        // Get all food category
        public static List<string> GetFoodCategory()
        {
            using(RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                
                var productCategory = restaurantEntities.Products.GroupBy(c => c.ProductType).Select(s => s.Key);

                return productCategory.ToList();
            }
        }

        // Get Product by Category
        public static dynamic GetProductByCategory(string type)
        {
            using(RestaurantEntities restaurantEntities = new RestaurantEntities())
            {
                var productsByCategory = (from p in restaurantEntities.Products
                                          select new
                                          {
                                            ProductNo = p.ProductID,
                                            ProductName = p.ProductName,
                                            ProductType = p.ProductType,
                                            Price = p.Price,
                                            Availability = p.Availability
                                          }).Where(p => p.ProductType == type);

                return productsByCategory.ToList();

            }
        }
    }
}
