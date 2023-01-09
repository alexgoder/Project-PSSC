using System;
using System.Text.Json;
using Proj_PSSC;
using Proj_PSSC.Models;
using Proj_PSSC.Services;

namespace Proj_PSSC
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceGetData service = new ServiceGetData("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/TestFiles");
            Console.WriteLine(service.GetData());
        }

       
    }
    
}

