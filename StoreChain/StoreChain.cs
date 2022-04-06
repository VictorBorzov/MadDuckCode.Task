namespace StoreChain;

public class StoreChain
{
    public List<Shop> Shops { get; init; }

    public ILogger Logger
    {
        set
        {
            foreach (var shop in Shops)
                shop.Logger = value;
        }
    }
}

public class Customer
{
    public readonly string FirstName;

    public readonly string SecondName;

    public readonly string Tel;

    public Customer(string firstName, string secondName, string tel)
    {
        FirstName = firstName;
        SecondName = secondName;
        Tel = tel;
    }
}