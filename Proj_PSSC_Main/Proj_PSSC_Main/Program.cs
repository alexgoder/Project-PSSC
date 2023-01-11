
using Proj_PSSC.Services;
using Proj_PSSC.Workflows;
using Proj_PSSC_2.Workflows;
using Proj_PSSC_1.Workflows;
using Proj_PSSC_2.Models;
using LanguageExt.TypeClasses;

public class Program
{
    public static void Main(string[] args)
    {
        string productsPath = @"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/Products.json";
        string ordersPath = @"/Users/alexa/Desktop/FACULTA/Anul4/PSSC/Project-PSSC/Proj_PSSC/Proj_PSSC/TestFiles/Orders.json";

        Proj_PSSC.Services.ServiceGetData getData = new Proj_PSSC.Services.ServiceGetData(productsPath);
        Proj_PSSC.Services.ServiceGetOrder getOrder = new Proj_PSSC.Services.ServiceGetOrder(ordersPath);
        Proj_PSSC_1.Services.ServiceGetOrder getOrder1 = new Proj_PSSC_1.Services.ServiceGetOrder(ordersPath);
        Proj_PSSC_2.Services.ServiceGetOrder getOrder2 = new Proj_PSSC_2.Services.ServiceGetOrder(ordersPath);


        int opt;
        string options;

        do
        {
            Console.WriteLine("Introduceti optiunea:\n" +
                "1. Creare comanda\n" +
                "2. Modificare comanda\n" +
                "3. Stergere comanda\n" +
                "Optiunea dvs este: "
                 );
            options = Console.ReadLine();
            opt = int.Parse(options);
            switch (opt)
            {
                case 1:
                    int nrProds;
                    List<string> prods = new List<string> { };
                    Console.WriteLine("Introduceti nr de produse pe care doriti sa le comandati: ");
                    nrProds = int.Parse(Console.ReadLine());
                    for (int i = 0; i < nrProds; i++)
                    {
                        Console.WriteLine("Intr prod Id: ");
                        prods.Add(Console.ReadLine());
                        Console.WriteLine("Intr cantitatea: ");
                        prods.Add(Console.ReadLine());
                    }

                    CreateOrderWorkflow createOrder = new CreateOrderWorkflow(prods.ToArray(), getData, getOrder);

                    var result = createOrder.Execute();

                    if (result.IsSome)
                        Console.WriteLine(result);
                    else
                        Console.WriteLine("Comanda nu a fost creata cu succes");

                    break;

                case 2:
                    List<string> prodsToBeDeleted = new List<string> { };
                    string orderId;
                    Console.WriteLine("Introduceti Id-ul comenzii: ");
                    orderId = Console.ReadLine();
                    Console.WriteLine("Introduceti nr de produse pe care doriti sa le eliminati din comanda: ");
                    nrProds = int.Parse(Console.ReadLine());
                    for (int i = 0; i < nrProds; i++)
                    {
                        Console.WriteLine("Intr prod Id: ");
                        prodsToBeDeleted.Add(Console.ReadLine());

                    }

                    ModifyOrderWorkflow modifyOrder = new ModifyOrderWorkflow(orderId, prodsToBeDeleted.ToArray(), getOrder2);

                    var result1 = modifyOrder.Execute();

                    if (result1.IsSome)
                    {
                        var ord = (Order)result1;
                        if (ord.products.Length < 1)
                        {
                            DeleteOrderWorkflow deleteOrderIfNeeded = new DeleteOrderWorkflow(ord.id, getOrder1);
                            deleteOrderIfNeeded.Execute();
                            Console.WriteLine("Nu au mai ramas produse in comanda, de aceea aceasta a fost stearsa");
                        }

                        else
                            Console.WriteLine("Comanda modificata cu succes");
                    }
                    else
                        Console.WriteLine("Comanda nu a putu fi gasita sau nu a fost modificata cu succes");

                    break;

                case 3:
                    string orderToBeDeleted;
                    Console.WriteLine("Introduceti id-ul comenzii pe care doriti sa o stergeti");
                    orderToBeDeleted = Console.ReadLine();
                    DeleteOrderWorkflow deleteOrder = new DeleteOrderWorkflow(orderToBeDeleted, getOrder1);
                    var result2 = deleteOrder.Execute();

                    if (result2.IsSome)
                        Console.WriteLine("Comanda a fost stearsa cu succes");
                    else
                        Console.WriteLine("Comanda nu a fost stearsa cu succes");

                    break;

                
                default:
                    Console.WriteLine("Optiunea introdusa e gresita!");
                    break;
            }
        } while (true);

    }
}

