using Proj_PSSC_2.Services;
using Proj_PSSC_2.Workflows;
using Proj_PSSC_2.Services;

public class Program
{
    static void Main(string[] args)
    {
        ServiceGetData service = new ServiceGetData(@"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/Products.json");
        ServiceGetOrder orders = new ServiceGetOrder(@"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC_2/Proj_PSSC_2/TestFiles/Orders.json");

        string[] prodIds = { "17197" };

        ModifyOrderWorkflow workflow = new ModifyOrderWorkflow("29698",prodIds, orders);
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

