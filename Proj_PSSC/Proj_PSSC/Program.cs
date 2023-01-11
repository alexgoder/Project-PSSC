using System;
using System.Text.Json;
using Proj_PSSC;
using Proj_PSSC.Models;
using Proj_PSSC.Services;
using Proj_PSSC.Workflows;

namespace Proj_PSSC
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceGetData service = new ServiceGetData(@"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/Products.json");
            ServiceGetOrder orders = new ServiceGetOrder(@"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/Orders.json");
            Product prod1 = new Product("ZPVYP", "17197", 3);
            Product[] prods = { prod1 };
            //CreateOrderWorkflow workflow = new CreateOrderWorkflow(prods, service, orders);
            //var res=workflow.Execute();
            //if (res.IsNone)
            //File.WriteAllText("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/text.txt", res.ToString());
            //else
            //{
            //    var result = (Order)res;
            //    File.WriteAllText("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/text.txt", res.ToString());//+ "\n"+result.GetProductsAsString());
            //}
                


        }

       
    }
    
}

