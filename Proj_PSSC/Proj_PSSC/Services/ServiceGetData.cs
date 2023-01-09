using System;
using System.Text.Json;
using System.Collections;

using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.ClassInstances;
using Proj_PSSC.Models;

namespace Proj_PSSC.Services
{
	public class ServiceGetData:IServiceGetData
	{
		readonly string path;
		public ServiceGetData(string path)
		{
			this.path = path;
		}

		public IServiceReturnType[] GetData()
		{
			string path1 = @"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/Products.json";
			string str = File.ReadAllText(path1);

            Console.WriteLine(str);
			
			Product[] objects= (Product[])JsonSerializer.Deserialize<Product[]>(str);// "/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Proj_PSSC/TestFiles/Products.json");

			
                File.WriteAllText("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/text.txt", objects[0].ToString() + "\n" + objects[1].ToString());

            return objects;
		}
	}
}

