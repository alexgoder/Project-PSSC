using Proj_PSSC_1.Models;
using Proj_PSSC_1.Services;
using Proj_PSSC_1.Workflows;

public class Program
{
    static void Main(string[] args)
    {
        ServiceGetData service = new ServiceGetData(@"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/Products.json");
        ServiceGetOrder orders = new ServiceGetOrder(@"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC_1/Proj_PSSC_1/TestFiles/Orders.json");


        DeleteOrderWorkflow workflow = new DeleteOrderWorkflow("83473", orders);
        var res = workflow.Execute();
        //if (res.IsNone)
        //    File.WriteAllText("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/text.txt", res.ToString());
        //else
        //{
        //    var result = (Order)res;
        //    File.WriteAllText("/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/text.txt", res.ToString());//+ "\n"+result.GetProductsAsString());
        //}

        Console.WriteLine(res);

    }


}

