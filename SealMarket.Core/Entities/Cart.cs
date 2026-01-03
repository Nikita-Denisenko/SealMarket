using SealMarket.Core.Entities;

public class Cart
{
    public int Id { get; private set; }
    public int AccountId { get; private set; }
    public Account? Account { get; private set; }
    public string Name { get; private set; } = "MyCart";
    public List<CartItem> CartItems { get; private set; } = [];
    public decimal? TotalPrice => CartItems.Sum(ci => ci.TotalPrice);

    private Cart() { }

    public Cart(int accountId)
    {
        AccountId = accountId;
    }

    public void AddItem(CartItem newItem)
    {
        var item = CartItems.FirstOrDefault(item => item.Product.Id == newItem.Product.Id);

        if (item != null)
        {
            item.IncreaseQuantity();
            return;
        }

        CartItems.Add(newItem);
    }

    public void RemoveItem(CartItem item, bool removeAll = true)
    {
        if (!(removeAll || item.Quantity <= 1))
        {
            item.DecreaseQuantity();
            return;
        }

        CartItems.Remove(item);
    }

    public void Clear() => CartItems.Clear();
}