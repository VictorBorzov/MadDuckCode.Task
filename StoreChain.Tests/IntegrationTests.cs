using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StoreChain.Tests;

public class IntegrationTests
{
    private static readonly ShopData Products = new("TestShop", new Dictionary<Product, (int Price, int Amount)>
    {
        { new Medicine(1), (1, 1) },
        { new Drink(), (2, 2) },
        { new Toys(), (1, 1) },
        { new Cigarettes(), (1, 1) },
        { new ParkingTickets(1), (1, 1) },
    });

    private readonly StoreChain _storeChain = new()
    {
        Shops = new List<Shop>
        {
            new Pharmacy(Products),
            new Supermarket(Products),
            new CornerShop(Products),
        },
    };

    [Test]
    public void T01_GetAllAvailableProducts()
    {
        var date = DateTime.Now;
        var expectedLog = $"StoreType:StoreChain.Pharmacy, ProductType:StoreChain.Medicine, ProductPrice:1, AmountBeforePurchase:1, AmountAfterPurchase:0, BillDateTime:{date}\nStoreType:StoreChain.Pharmacy, ProductType:StoreChain.Drink, ProductPrice:2, AmountBeforePurchase:2, AmountAfterPurchase:0, BillDateTime:{date}\nStoreType:StoreChain.Pharmacy, ProductType:StoreChain.Toys, ProductPrice:1, AmountBeforePurchase:1, AmountAfterPurchase:0, BillDateTime:{date}\nStoreType:StoreChain.Pharmacy, ProductType:StoreChain.ParkingTickets, ProductPrice:1, AmountBeforePurchase:1, AmountAfterPurchase:0, BillDateTime:{date}\nStoreType:StoreChain.CornerShop, ProductType:StoreChain.Cigarettes, ProductPrice:1, AmountBeforePurchase:1, AmountAfterPurchase:0, BillDateTime:{date}";
        var logger = new Logger();
        _storeChain.Logger = logger;
        var customer = new Customer("Mary", "Smith", "44-777-777");
        foreach (var shop in _storeChain.Shops)
        {
            shop.Sell(customer, Products.Products.ToDictionary(pair => pair.Key, pair => pair.Value.Amount), date);
        }

        var log = logger.GetLog();
        Assert.AreEqual(log, expectedLog);
    }

    [Test]
    public void T02_GetMoreThanAvailable()
    {
        var logger = new Logger();
        _storeChain.Logger = logger;
        var customer = new Customer("Mary", "Smith", "44-777-777");
        foreach (var shop in _storeChain.Shops)
        {
            shop.Sell(customer, Products.Products.ToDictionary(pair => pair.Key, pair => pair.Value.Amount + 1), DateTime.Now);
        }

        const string expectedLog = "";
        var log = logger.GetLog();
        Assert.AreEqual(log, expectedLog);
    }
}