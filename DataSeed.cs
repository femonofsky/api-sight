using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;
using System;

namespace Advantage.API
{
    public class DataSeed
    {
        private readonly  ApiContext _ctx;

        public DataSeed(ApiContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders)
        {
            if(!_ctx.Customers.Any())
            {
                SeedCustomers(nCustomers);
                _ctx.SaveChanges();
            }
            if(!_ctx.Orders.Any())
            {
                SeedOrders(nOrders);
                _ctx.SaveChanges();
            }
            if(!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }
        }

        public void SeedCustomers(int nCustomers)
        {
            List<Customer> customers = BuildCustomerList(nCustomers);
            foreach (var Customer in customers)
            {
                _ctx.Customers.Add(Customer);
            }
        }

        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customers = new List<Customer>();
            var customerNames = new List<string>();


            for (int i = 1; i <= nCustomers; i++)
            {
                var name = Helpers.MakeUniqueCustomerName(customerNames);
                customerNames.Add(name);

                customers.Add( new Customer{
                    Id = i,
                    Name = name, 
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState(),
                });
            }
            return customers; 

        }

        public void SeedOrders(int nOrders)
        {
            List<Order> orders = BuildOrderList(nOrders);
            foreach (var order in orders)
            {
                _ctx.Orders.Add(order);
            }
        }

        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();
            var rand = new Random();


            for (int i = 1; i <= nOrders; i++)
            {
                var randCustomerId = rand.Next(1,_ctx.Customers.Count());
                Console.WriteLine("randCustomerID: "+ randCustomerId);
                var placed = Helpers.getRandomOrderPlaced();
                Console.WriteLine("placed: "+ placed.Day);

                var _completed = Helpers.getRandomOrderCompleted(placed);
                Console.WriteLine("_completed: "+ placed.Day);


                orders.Add(new Order{
                    Id = i,
                    Customer = _ctx.Customers.First(c => c.Id == randCustomerId), 
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    completed = _completed
                });
            }
            return orders; 
            
        }


        public void SeedServers()
        {
            List<Server> servers = BuildServerList();
            foreach (var server in servers)
            {
                _ctx.Servers.Add(server);
            }
        }

        private List<Server> BuildServerList()
        {
            return  new List<Server>()
            {
                new Server{
                    Id = 1,
                    Name = "Dev-Web",
                    isOnline = true
                },
                new Server{
                    Id = 2,
                    Name = "Dev-Mail",
                    isOnline = false
                },
                new Server{
                    Id = 3,
                    Name = "Dev-Services",
                    isOnline = true
                },
                new Server{
                    Id = 4,
                    Name = "QA-Web",
                    isOnline = true
                },
                new Server{
                    Id = 5,
                    Name = "QA-Mail",
                    isOnline = false
                },
                new Server{
                    Id = 6,
                    Name = "QA-Services",
                    isOnline = true
                },
                 new Server{
                    Id = 7,
                    Name = "Prod-Web",
                    isOnline = true
                },
                new Server{
                    Id = 8,
                    Name = "Prod-Mail",
                    isOnline = true
                },
                new Server{
                    Id = 9,
                    Name = "Prod-Services",
                    isOnline = true
                },


            };
            

        }


    }
}