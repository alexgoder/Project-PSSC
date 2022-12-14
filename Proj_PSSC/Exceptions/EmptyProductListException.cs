namespace Proj_PSSC.Exceptions;

public class EmptyProductListException : Exception
{
    private string message = "Empty Product List/Cart";
    public EmptyProductListException():base()
    {
        Console.WriteLine(this.message);
    }
}