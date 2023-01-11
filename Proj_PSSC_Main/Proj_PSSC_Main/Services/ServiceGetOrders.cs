using System;
using System.Text.Json;
using Proj_PSSC_Main.Models;

namespace Proj_PSSC_Main.Services
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

            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            string str = File.ReadAllText(this.path);
			Order[] objects;
			
			if (str.Length > 8)
				objects = (Order[])JsonSerializer.Deserialize<Order[]>(str,options);
			else
				objects = new List<Order> { }.ToArray();
			return objects;
        }

		private string Strigify(Order[] orders)
		{
            var options = new JsonSerializerOptions { WriteIndented = true };
            string text = "[";
			for(int i=0;i<orders.Length;i++)
			{
				text += "{\""  + "id\":\"" + orders[i].id.ToString() + "\",\n";
				text += "\"products\":[\n";
				for (int j = 0; j < orders[i].products.Length;j++)
				{
					text+= JsonSerializer.Serialize(orders[i].products[j], options);
					if (j < orders[i].products.Length - 1)
						text += ",\n";
                }
				text += "]\n}\n";
				if (i < orders.Length - 1)
					text += ",";
            }
			return text + "]";
		}

        public void SetData(IServiceReturnType[] input)
        {
            List<Order> newOrders = new List<Order> { };
            string str = File.ReadAllText(this.path);
			if (str.Length > 9)
			{
                Order[] objects = (Order[])JsonSerializer.Deserialize<Order[]>(str);
                

                newOrders.AddRange(objects);
            }
            
			newOrders.AddRange((Order[])input);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string textToBeSent = JsonSerializer.Serialize<Order[]>(newOrders.ToArray(),options);
			Console.WriteLine(textToBeSent);
            File.WriteAllText(this.path, textToBeSent);
        }
    }
}

