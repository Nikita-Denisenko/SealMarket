using SealMarket.Core.Entities;

public class Cart
{
    public int Id { get; private set; }
    public int AccountId { get; private set; }
    public Account? Account { get; private set; }
    public string Name { get; private set; }
    public List<Product> Products { get; private set; } = [];

    private Cart() { }

    public Cart(string name, int accountId)
    {
        Name = name;
        AccountId = accountId;
    }

    public void AddProduct(Product product)
    {
        if (!Products.Any(p => p.Id == product.Id))
            Products.Add(product);
    }

    public void RemoveProduct(int productId)
    {
        var product = Products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
            Products.Remove(product);
    }

    public void Clear() => Products.Clear();
}