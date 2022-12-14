using System;
using System.Text.Json;
using Proj_PSSC;
using Proj_PSSC.Models;

namespace Proj_PSSC
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }
//for each product that the client wants, there is a async task that puts it into the order
        public static Task<Order> InputData()
        {
            return Task.Run(() => new Order(1,null));
        }
    }
    
}

