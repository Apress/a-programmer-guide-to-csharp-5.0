// 08 - Other Class Details\Creation, Initialization, Destruction\Initialization
// copyright 2000 Eric Gunnerson
public class Parser        // Support class
{
    public Parser(int number)
    {
        this.number = number;
    }
    int number;
}
class MyClass
{
    public int counter = 100;
    public string heading = "Top";
    private Parser parser = new Parser(100);
}