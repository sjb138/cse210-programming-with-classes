using System;
using System.Collections.Generic;

public class Product
{
    public string Name { get; private set; }
    public int ProductId { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }

    public Product(string name, int productId, double price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    // Calculate the total cost of this product
    public double TotalCost()
    {
        return Price * Quantity;
    }
}

public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    // Check if the address is in the USA
    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    // Return the full address as a string
    public string GetFullAddress()
    {
        return $"{Street}\n{City}, {State}\n{Country}";
    }
}

public class Customer
{
    public string Name { get; private set; }
    public Address CustomerAddress { get; private set; }

    public Customer(string name, Address address)
    {
        Name = name;
        CustomerAddress = address;
    }

    // Check if the customer lives in the USA
    public bool LivesInUSA()
    {
        return CustomerAddress.IsInUSA();
    }
}

public class Order
{
    public List<Product> Products { get; private set; }
    public Customer Customer { get; private set; }

    public Order(Customer customer)
    {
        Products = new List<Product>();
        Customer = customer;
    }

    // Add a product to the order
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    // Calculate the total cost of the order (products + shipping)
    public double TotalCost()
    {
        double totalProductCost = 0;
        foreach (var product in Products)
        {
            totalProductCost += product.TotalCost();
        }
        double shippingCost = Customer.LivesInUSA() ? 5.0 : 35.0;
        return totalProductCost + shippingCost;
    }

    // Generate the packing label
    public string GetPackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (var product in Products)
        {
            packingLabel += $"{product.Name} (ID: {product.ProductId})\n";
        }
        return packingLabel;
    }

    // Generate the shipping label
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.Name}\n{Customer.CustomerAddress.GetFullAddress()}";
    }
}

public class Program
{
    public static void Main()
    {
        // Create some products
        Product product1 = new Product("Helecarrier", 101, 799.99, 1);
        Product product2 = new Product("Eyepatch", 102, 49.99, 2);

        // Create an address and customer
        Address address1 = new Address("616 PinkPonySt.", "Quahog", "NY", "USA");
        Customer customer1 = new Customer("Nick Fury", address1);

        // Create an order and add products
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        // Output the order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Order Cost: ${order1.TotalCost()}");

        // You can add more orders or products here if needed
    }
}
