﻿using System;
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
                                            Employee = e.EmployeType,
                                            IsServing = oh.IsServing
                                        }).Where((e) => e.EmpId == empid && e.IsServing == true);

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
                                                OrderNo = od.OrderID,
                                                TableNo = oh.TableID,
                                                ProductNo = p.ProductID,
                                                ProductName = p.ProductName,
                                                Quantity = od.Quantity,
                                                EachPrice = p.Price,
                                                Price = p.Price * od.Quantity,
                                                Ready = od.IsReady
                                            }).Where(x => x.OrderNo == orderId);

                return orderDetailByOrderId.ToList();
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
                                            ProductId = p.ProductID,
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