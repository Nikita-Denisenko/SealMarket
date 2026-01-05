using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Entities;

namespace SealMarket.Infrastructure.Data
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Brands.AnyAsync())
                return;

            try
            {
                // 1. ПОЛЬЗОВАТЕЛИ
                var users = new List<User>
                {
                    new("Иван Петров", new DateOnly(1995, 5, 15), "Москва"),
                    new("Анна Сидорова", new DateOnly(1998, 8, 22), "Санкт-Петербург"),
                    new("Елена Смирнова", new DateOnly(2000, 11, 30), "Новосибирск"),
                    new("Админ Системы", new DateOnly(1990, 1, 1), "Москва")
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                // 2. АККАУНТЫ 
                var accounts = new List<Account>
                {
                    new(users[0].Id, "ivan95", "Password123!", "ivan@mail.com", "+79161234567", "Customer"),
                    new(users[1].Id, "anna98", "AnnaPass123!", "anna@mail.com", "+79162345678", "Customer"),
                    new(users[2].Id, "elena00", "Elena2000!", "elena@mail.com", "+79164567890", "Customer"),
                    new(users[3].Id, "admin", "Admin123!", "admin@sealmarket.com", "+79165678901", "Admin")
                };

                accounts.ForEach(a => a.Deposit(50000));
                accounts[3].Deposit(1000000);

                await context.Accounts.AddRangeAsync(accounts);
                await context.SaveChangesAsync();

                // 3. КОРЗИНЫ
                accounts.ForEach(a => a.CreateCart());
                await context.SaveChangesAsync();

                // 4. БРЕНДЫ
                var brands = new List<Brand>
                {
                    new("Nike", "https://cdn.worldvectorlogo.com/logos/nike-4.svg",
                         "Just do it - спорт и стиль"),
                    new("Adidas", "https://cdn.worldvectorlogo.com/logos/adidas-2021.svg",
                         "Impossible is nothing"),
                    new("Puma", "https://cdn.worldvectorlogo.com/logos/puma-logo.svg",
                         "Forever Faster"),
                    new("New Balance", "https://cdn.worldvectorlogo.com/logos/new-balance-1.svg",
                         "Fearlessly Independent"),
                    new("Levi's", "https://cdn.worldvectorlogo.com/logos/levis.svg",
                         "Quality never goes out of style"),
                    new("Zara", "https://cdn.worldvectorlogo.com/logos/zara-1.svg",
                         "Модная одежда по доступным ценам")
                };

                await context.Brands.AddRangeAsync(brands);
                await context.SaveChangesAsync();

                // 5. КАТЕГОРИИ
                var categories = new List<Category>
                {
                    new("Кроссовки", "Спортивная обувь"),
                    new("Кеды", "Повседневная обувь"),
                    new("Футболки", "Базовые и принтованные"),
                    new("Худи и свитшоты", "Теплая одежда"),
                    new("Джинсы", "Классические и современные"),
                    new("Куртки", "Верхняя одежда"),
                    new("Аксессуары", "Сумки, шапки, ремни")
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();

                // 6. ПРОДУКТЫ
                var products = new List<Product>
                {
                    // NIKE - Кроссовки
                    new("Nike Air Force 1", brands[0].Id, categories[0].Id,
                        "Классические белые кроссовки",
                        "https://images.unsplash.com/photo-1600269452121-4f2416e55c28?w=800&h=800&fit=crop",
                        45, 11990, true),

                    new("Nike Dunk Low", brands[0].Id, categories[0].Id,
                        "Ретро-кроссовки для повседневной носки",
                        "https://images.unsplash.com/photo-1605348532760-6753d2c43329?w=800&h=800&fit=crop",
                        32, 12990, true),
                    
                    // NIKE - Футболки
                    new("Nike Sportswear T-Shirt", brands[0].Id, categories[2].Id,
                        "Базовая футболка с логотипом",
                        "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=800&h=800&fit=crop",
                        120, 2990, true),
                    
                    // ADIDAS
                    new("Adidas Ultraboost", brands[1].Id, categories[0].Id,
                        "Беговые кроссовки с технологией Boost",
                        "https://images.unsplash.com/photo-1606107557195-0e29a4b5b4aa?w=800&h=800&fit=crop",
                        38, 15990, true),

                    new("Adidas Superstar", brands[1].Id, categories[1].Id,
                        "Классические кеды с ракушкой",
                        "https://images.unsplash.com/photo-1600185365483-26d7a4cc7519?w=800&h=800&fit=crop",
                        55, 8990, true),
                    
                    // PUMA
                    new("Puma RS-X", brands[2].Id, categories[0].Id,
                        "Кроссовки в ретро-стиле",
                        "https://images.unsplash.com/photo-1549298916-b41d501d3772?w=800&h=800&fit=crop",
                        40, 10990, true),
                    
                    // NEW BALANCE
                    new("New Balance 574", brands[3].Id, categories[0].Id,
                        "Классические кроссовки для повседневной носки",
                        "https://images.unsplash.com/photo-1608231387042-66d1773070a5?w=800&h=800&fit=crop",
                        42, 9990, true),
                    
                    // LEVI'S - Джинсы
                    new("Levi's 501 Original", brands[4].Id, categories[4].Id,
                        "Классические джинсы прямого кроя",
                        "https://images.unsplash.com/photo-1542272604-787c3835535d?w=800&h=800&fit=crop",
                        70, 8990, true),
                    
                    // LEVI'S - Куртки
                    new("Levi's Trucker Jacket", brands[4].Id, categories[5].Id,
                        "Джинсовая куртка в стиле вестерн",
                        "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=800&h=800&fit=crop",
                        45, 11990, true),
                    
                    // ZARA
                    new("Zara Basic Hoodie", brands[5].Id, categories[3].Id,
                        "Базовый худи оверсайз",
                        "https://images.unsplash.com/photo-1556821840-3a63f95609a7?w=800&h=800&fit=crop",
                        85, 4990, true),

                    new("Zara Denim Shorts", brands[5].Id, categories[4].Id,
                        "Джинсовые шорты для лета",
                        "https://images.unsplash.com/photo-1594633312681-425c7b97ccd1?w=800&h=800&fit=crop",
                        60, 3490, true),
                    
                    // АКСЕССУАРЫ
                    new("Nike Backpack", brands[0].Id, categories[6].Id,
                        "Рюкзак для спорта и путешествий",
                        "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=800&h=800&fit=crop",
                        35, 5990, true),

                    new("Adidas Cap", brands[1].Id, categories[6].Id,
                        "Бейсболка с логотипом",
                        "https://images.unsplash.com/photo-1588850561407-ed78c282e89b?w=800&h=800&fit=crop",
                        150, 1990, true)
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();

                // 7. ДОБАВЛЯЕМ В КОРЗИНЫ
                var cart1 = await context.Carts
                    .Include(c => c.CartItems)
                    .FirstAsync(c => c.AccountId == accounts[0].Id);

                cart1.AddItem(new CartItem(products[0].Id, cart1.Id, 1)); // Nike Air Force
                cart1.AddItem(new CartItem(products[7].Id, cart1.Id, 1)); // Levi's 501
                cart1.AddItem(new CartItem(products[12].Id, cart1.Id, 2)); // Adidas Cap (2 шт)

                var cart2 = await context.Carts
                    .Include(c => c.CartItems)
                    .FirstAsync(c => c.AccountId == accounts[1].Id);

                cart2.AddItem(new CartItem(products[3].Id, cart2.Id, 1)); // Adidas Ultraboost
                cart2.AddItem(new CartItem(products[9].Id, cart2.Id, 1)); // Zara Hoodie

                await context.SaveChangesAsync();

                // 8. УВЕДОМЛЕНИЯ
                var notifications = new List<Notification>
                {
                    new(accounts[0].Id, "Добро пожаловать в SealMarket!", "Приветствие"),
                    new(accounts[0].Id, "На Nike скидка 15% до конца недели", "Акция"),
                    new(accounts[3].Id, "Новые заказы ожидают обработки", "Админ-уведомление")
                };

                await context.Notifications.AddRangeAsync(notifications);
                await context.SaveChangesAsync();

                Console.WriteLine("Seed создан успешно!");
                Console.WriteLine($"• Пользователей: {users.Count}");
                Console.WriteLine($"• Брендов: {brands.Count}");
                Console.WriteLine($"• Товаров: {products.Count}");
                Console.WriteLine($"• Товаров в корзинах: {cart1.CartItems.Count + cart2.CartItems.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка seed: {ex.Message}");
                throw;
            }
        }
    }
}