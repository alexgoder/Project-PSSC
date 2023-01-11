using System;
using System.Text.Json;
using System.Collections;

using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.ClassInstances;
using Proj_PSSC_1.Models;

namespace Proj_PSSC_1.Services
{
    public class ServiceGetData : IServiceGetData
    {
        readonly string path;
        public ServiceGetData(string path)
        {
            this.path = path;
        }

        public IServiceReturnType[] GetData()
        {

            string str = File.ReadAllText(path);



            Product[] objects = (Product[])JsonSerializer.Deserialize<Product[]>(str);
            return objects;
        }

        public void SetData(IServiceReturnType[] input)
        {
            string str = File.ReadAllText(this.path);
            Product[] inputData = (Product[])input;
            Product[] objects = (Product[])JsonSerializer.Deserialize<Product[]>(str);
            List<Product> newProds = new List<Product> { };
            for (int i = 0; i < objects.Length; i++)
            {
                int newQty = -1;
                for (int j = 0; j < inputData.Length; j++)
                {
                    if (inputData[j].id == objects[i].id)
                    {
                        newQty = objects[i].qty - inputData[j].qty;
                    }
                }
                if (newQty > 0)
                {
                    newProds.Add(new Product(objects[i].name, objects[i].id, newQty, objects[i].price));
                }
                else
                {
                    newProds.Add(objects[i]);
                }
            }
            var options = new JsonSerializerOptions { WriteIndented = true };
            string textToBeSent = JsonSerializer.Serialize(newProds.ToArray(), options);
            File.WriteAllText(this.path, textToBeSent);
        }
    }
}

