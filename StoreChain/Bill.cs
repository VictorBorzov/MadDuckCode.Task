namespace StoreChain;

public class Bill
{
    public Bill(int numberOfBill, DateTime date, Customer customer, Dictionary<Product, int> boughtProducts)
    {
        NumberOfBill = numberOfBill;
        Date = date;
        Customer = customer;
        BoughtProducts = boughtProducts;
    }

    public int NumberOfBill { get; }

    public DateTime Date { get; }

    public Customer Customer { get; }

    private void AddProducts(List<Product> products)
    {
        
    }

    public Dictionary<Product, int> BoughtProducts { get; }

}