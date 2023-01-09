using System;
using System.Text.Json;
using Proj_PSSC.Models;

namespace Proj_PSSC.Services
{
	public class ServiceGetOrder : IServiceGetData
	{
		string path;

		public ServiceGetOrder(string path)
		{
			this.path = path;
		}

		public IServiceReturnType[] GetData()
		{
            string path1 = @"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/TestFiles/Products.json";
            string str = File.ReadAllText(this.path);

            //Console.WriteLine(str);
            //File.WriteAllText("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/TestFiles/text.txt", "pub");
            Order[] objects = (Order[])JsonSerializer.Deserialize<Order[]>(str);// "/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Proj_PSSC/TestFiles/Products.json");
            return objects;
        }
    }
}

