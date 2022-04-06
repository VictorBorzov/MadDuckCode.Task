namespace StoreChain;

public abstract class Product
{
    public string Name { get; }
    public float Price { get; }
}

public class ParkingTickets : Product
{
    public ParkingTickets(int serialNumber)
    {
        SerialNumber = serialNumber;
    }

    public int SerialNumber { get; }
}

public class Toys : Product
{
}

public class Cigarettes : Product
{
}

public class Medicine : Product
{
    public Medicine(int serialNumber)
    {
        SerialNumber = serialNumber;
    }

    public int SerialNumber { get; }
}

public class Drink : Product
{
}

public class Food : Product
{
}

