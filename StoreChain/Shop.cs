namespace StoreChain;

public class ShopData
{
    public ShopData(string name, Dictionary<Product, (int Price, int Amount)> products)
    {
        Name = name;
        Products = products;
    }

    public string Name { get; }
    public Dictionary<Product, (int Price, int Amount)> Products { get; }
}

public abstract class Shop
{
    protected Shop(ShopData shopData)
    {
        Name = shopData.Name;
        Products = shopData.Products;
    }

    public string Name { get; }

    private Dictionary<Product, (int Price, int Amount)> Products { get; }

    private int _billsNumber = 1;

    private DateTime? _lastBillDate;

    protected abstract bool CanSell(Product product);

    private readonly List<Bill> _soldProducts = new();

    public List<Product> GetReport(DateTime startDateTime, DateTime endDateTime)
    {
        return _soldProducts
            .Where(bill => bill.Date >= startDateTime && bill.Date <= endDateTime)
            .SelectMany(bill => bill.BoughtProducts)
            .Select(productAndAmount => productAndAmount.Key)
            .ToList();
    }

    public ILogger? Logger;

    public Bill? Sell(Customer customer, Dictionary<Product, int> productsAndAmount, DateTime date)
    {
        var boughtProducts = new Dictionary<Product, int>();
        foreach (var (product, requiredAmount) in productsAndAmount)
        {
            if (!Products.TryGetValue(product, out var productInfo) || productInfo.Amount < requiredAmount)
                return null;

            if (CanSell(product) && requiredAmount != 0)
            {
                var amountAfterPurchase = productInfo.Amount - requiredAmount;
                var amountBeforePurchase = productInfo.Amount;
                Products[product] = (productInfo.Price, amountAfterPurchase);
                boughtProducts.Add(product, requiredAmount);
                Logger?.Log(new LogData
                {
                    ProductPrice = productInfo.Price,
                    ProductType = product.GetType(),
                    StoreType = GetType(),
                    AmountAfterPurchase = amountAfterPurchase,
                    AmountBeforePurchase = amountBeforePurchase,
                    BillDateTime = date,
                });
            }
        }

        if (date.Year > _lastBillDate?.Year)
            _billsNumber = 1;
        _lastBillDate = date;

        var bill = new Bill(_billsNumber++, date, customer, boughtProducts);
        _soldProducts.Add(bill);
        return bill;
    }
}

public class Pharmacy : Shop
{
    protected override bool CanSell(Product product)
    {
        return product switch
        {
            Cigarettes => false,
            _ => true,
        };
    }

    public Pharmacy(ShopData shopData) : base(shopData)
    {
    }
}

public class Supermarket : Shop
{
    protected override bool CanSell(Product product)
    {
        return product switch
        {
            Cigarettes or Medicine => false,
            _ => true,
        };
    }

    public Supermarket(ShopData shopData) : base(shopData)
    {
    }
}

public class CornerShop : Shop
{
    protected override bool CanSell(Product product)
    {
        return product switch
        {
            Medicine => false,
            _ => true,
        };
    }

    public CornerShop(ShopData shopData) : base(shopData)
    {
    }
}