using SealMarket.Core.Entities;

public class Cart
{
    public int Id { get; private set; }
    public int AccountId { get; private set; }
    public Account? Account { get; private set; }
    public string Name { get; private set; } = "AccountCart";
    public List<CartItem> CartItems { get; private set; } = [];

    private Cart() { }

    public Cart(string name, int accountId)
    {
        Name = name;
        AccountId = accountId;
    }

    public void AddProduct(CartItem newItem)
    {
        if (!CartItems.Any(item => item.Id == newItem.Id))
            CartItems.Add(newItem);
    }

    public void RemoveProduct(int newItemId)
    {
        var item = CartItems.FirstOrDefault(item => item.Id == newItemId);
        if (item != null)
            CartItems.Remove(item);
    }

    public void Clear() => CartItems.Clear();
}