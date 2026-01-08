# 🦭SealMarket API

## О проекте
**SealMarket** — это REST API для интернет-магазина мужской одежды и обуви, построенный на ASP.NET Core с применением Clean Architecture и современных практик разработки. Проект демонстрирует полный цикл разработки enterprise-приложения: от проектирования доменной модели и реализации бизнес-логики до настройки аутентификации, пагинации, фильтрации и структурированного логирования. API предоставляет все необходимые эндпоинты для управления товарами, пользователями, корзинами, уведомлениями и транзакциями, следуя принципам RESTful дизайна.

## Технологический стек
- **.NET 8.0** (ASP.NET Core Web API)
- **MySQL** с Entity Framework Core 8
- **JWT Bearer Authentication**
- **Repository Pattern**
- **Serilog** для структурированного логирования
- **Swagger/OpenAPI** для автоматической документации

## Архитектура
Проект реализован с использованием Clean Architecture, что обеспечивает высокую поддерживаемость и тестируемость:

<img width="350" height="350" alt="image (3)" src="https://github.com/user-attachments/assets/018aa966-07f1-47f1-ab42-835ecd528364" />


## Диаграмма связей сущностей
```mermaid
erDiagram
    User ||--o| Account : has
    Account ||--o| Cart : "owns one"
    Account ||--o{ Notification : receives
    Account ||--o{ Transaction : performs
    Cart ||--o{ CartItem : contains
    CartItem }o--|| Product : refers_to
    Product }o--|| Brand : manufactured_by
    Product }o--|| Category : belongs_to
    Brand ||--o{ Product : produces
    Category ||--o{ Product : includes
```

## Доменные сущности
### User
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK, Auto Increment |
| `Name` | `string` | Имя |
| `BirthDate` | `DateOnly` | Дата рождения |
| `City` | `string` | Город проживания |
| `AvatarUrl` | `string` | URL аватарки |
| `Account` | `Account` | Навигация → Account (1:1) |

### Account  
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `UserId` | `int` | FK → User.Id |
| `Login` | `string` | Уникальный логин |
| `Password` | `string` | Хэш пароля (BCrypt) |
| `Email` | `string` | Уникальный email |
| `PhoneNumber` | `string` | Номер телефона |
| `Balance` | `decimal` | Баланс (default: 0) |
| `Role` | `string` | "User" или "Admin" |
| `CreatedAt` | `DateTime` | Дата создания |
| `Cart` | `Cart` | Навигация → Cart (1:1) |
| `Transactions` | `List<Transaction>` | Навигация → Transaction (1:N) |
| `Notifications` | `List<Notification>` | Навигация → Notification (1:N) |

### Cart
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `AccountId` | `int` | FK → Account.Id |
| `Name` | `string` | Название (default: "MyCart") |
| `CartItems` | `List<CartItem>` | Навигация → CartItem (1:N) |

### CartItem
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `ProductId` | `int` | FK → Product.Id |
| `CartId` | `int` | FK → Cart.Id |
| `Quantity` | `int` | Количество (min: 1) |
| `AddedAt` | `DateTime` | Дата добавления |
| `Product` | `Product` | Навигация → Product (N:1) |
| `Cart` | `Cart` | Навигация → Cart (N:1) |

### Product
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `Name` | `string` | Название товара |
| `BrandId` | `int` | FK → Brand.Id |
| `CategoryId` | `int` | FK → Category.Id |
| `Description` | `string` | Описание товара |
| `ImageUrl` | `string` | URL изображения |
| `Quantity` | `int` | Кол-во на складе (≥0) |
| `Price` | `decimal` | Цена (>0) |
| `IsActive` | `bool` | Активен ли (soft delete) |
| `CreatedAt` | `DateTime` | Дата добавления |
| `Brand` | `Brand` | Навигация → Brand (N:1) |
| `Category` | `Category` | Навигация → Category (N:1) |

### Brand
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `Name` | `string` | Уникальное название |
| `LogoUrl` | `string` | URL логотипа |
| `Description` | `string` | Описание бренда |
| `Products` | `List<Product>` | Навигация → Product (1:N) |

### Category
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `Name` | `string` | Уникальное название |
| `Description` | `string` | Описание категории |
| `ImageUrl` | `string` | URL изображения |
| `Products` | `List<Product>` | Навигация → Product (1:N) |

### Transaction
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `AccountId` | `int` | FK → Account.Id |
| `TotalSum` | `decimal` | Сумма операции (>0) |
| `IsSuccessful` | `bool` | Успешно ли выполнена |
| `Message` | `string` | Сообщение/причина |
| `CreatedAt` | `DateTime` | Дата операции |
| `Account` | `Account` | Навигация → Account (N:1) |

### Notification
| Свойство | Тип | Описание |
|----------|-----|----------|
| `Id` | `int` | PK |
| `AccountId` | `int` | FK → Account.Id |
| `Name` | `string` | Заголовок уведомления |
| `Message` | `string` | Текст уведомления |
| `DateTime` | `DateTime` | Дата отправки |
| `HasBeenRead` | `bool` | Прочитано ли (default: false) |
| `Account` | `Account` | Навигация → Account (N:1) |

## API Endpoints

### Аутентификация (/api/auth)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| POST | `/api/auth/register` | Регистрация | - | RegisterDto<br>• UserName<br>• BirthDate<br>• City<br>• Login<br>• Email<br>• Password<br>• PhoneNumber | AuthResultDto<br>• Token<br>• AccountId<br>• Email<br>• FullName<br>• Role |
| POST | `/api/auth/login` | Вход | - | LoginDto<br>• Login<br>• Password | AuthResultDto<br>• Token<br>• AccountId<br>• Email<br>• FullName<br>• Role |

### Пользователи (/api/users)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/users` | Список пользователей | - | UsersFilterDto<br>• Page<br>• Size<br>• SearchText<br>• OrderBy | List\<ShortUserDto\><br>• Id<br>• Name |
| GET | `/api/users/my-profile` | Мой профиль | Customer | - | UserProfileDto<br>• Id<br>• Name<br>• BirthDate<br>• City<br>• AccountId<br>• Login<br>• Email<br>• PhoneNumber<br>• Balance<br>• RegisteredAt<br>• AvatarUrl |
| GET | `/api/users/{id}` | Публичный профиль | - | - | PublicUserDto<br>• Id<br>• Name<br>• BirthDate<br>• City<br>• AvatarUrl |
| PUT | `/api/users/my-profile` | Обновить профиль | Customer | UpdateUserDto<br>• UserName?<br>• City?<br>• AvatarUrl? | - |
| DELETE | `/api/users/{id}` | Удалить пользователя | Admin | - | - |
| DELETE | `/api/users/my-account` | Удалить свой аккаунт | Customer | - | - |

### Аккаунты (/api/accounts)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/accounts` | Все аккаунты | Admin | AccountsFilterDto<br>• Page<br>• Size<br>• Role<br>• MinBalance | List\<ShortAccountDto\><br>• Id<br>• Login<br>• UserId |
| GET | `/api/accounts/my-account` | Мой аккаунт | Customer | - | AccountDashboardDto<br>• Id<br>• Balance<br>• CreatedAt<br>• CartId<br>• CartItemsQuantity<br>• NoReadNotificationsQuantity |
| GET | `/api/accounts/{id}` | Аккаунт по ID | Admin | - | AccountDashboardDto<br>• Id<br>• Balance<br>• CreatedAt<br>• CartId<br>• CartItemsQuantity<br>• NoReadNotificationsQuantity |
| PUT | `/api/accounts/my-account` | Обновить аккаунт | Customer | UpdateAccountDto<br>• Login?<br>• Email?<br>• PhoneNumber? | - |

### Товары (/api/products)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/products` | Список товаров | - | ProductsFilterDto<br>• Page<br>• Size<br>• MinPrice<br>• MaxPrice<br>• SearchText<br>• CategoryName | List\<ShortProductDto\><br>• Id<br>• Name<br>• ImageUrl<br>• Price<br>• BrandId<br>• CategoryId |
| GET | `/api/products/{id}` | Товар по ID | - | - | ProductDto<br>• Id<br>• Name<br>• Description<br>• ImageUrl<br>• Quantity<br>• Price<br>• CreatedAt<br>• IsActive<br>• BrandId<br>• BrandName<br>• CategoryId<br>• CategoryName |
| POST | `/api/products` | Создать товар | Admin | CreateProductDto<br>• Name<br>• BrandId<br>• CategoryId<br>• Description<br>• ImageUrl<br>• Quantity<br>• Price<br>• IsActive | ProductDto |
| PUT | `/api/products/{id}` | Обновить товар | Admin | UpdateProductDto<br>• Name?<br>• Description?<br>• ImageUrl?<br>• Quantity?<br>• Price?<br>• IsActive? | - |
| DELETE | `/api/products/{id}` | Удалить товар | Admin | - | - |

### Корзина (/api/carts)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/carts/my-cart` | Моя корзина | Customer | - | CartDto<br>• Id<br>• Name<br>• TotalPrice<br>• CartItems |
| GET | `/api/carts/{id}` | Корзина по ID | Admin | - | CartDto |
| GET | `/api/carts` | Все корзины | Admin | CartsFilterDto<br>• Page<br>• Size<br>• MinTotalPrice<br>• MaxTotalPrice | List\<ShortCartDto\><br>• Id<br>• AccountId |
| POST | `/api/carts/my-cart/add-item` | Добавить товар | Customer | CreateCartItemDto<br>• ProductId<br>• Quantity | CartItemDto<br>• Id<br>• ProductId<br>• Quantity<br>• AddedAt<br>• ProductName<br>• ProductImageUrl<br>• ProductPrice<br>• TotalPrice |
| DELETE | `/api/carts/my-cart/remove-item/{itemId}` | Удалить товар | Customer | Query: removeAll | - |

### Бренды (/api/brands)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/brands` | Список брендов | - | BrandsFilterDto<br>• Page<br>• Size<br>• SearchText | List\<ShortBrandDto\><br>• Id<br>• Name<br>• LogoUrl |
| GET | `/api/brands/{id}` | Бренд по ID | - | - | BrandDto<br>• Id<br>• Name<br>• LogoUrl<br>• Description<br>• ProductQuantity |
| POST | `/api/brands` | Создать бренд | Admin | CreateBrandDto<br>• Name<br>• LogoUrl<br>• Description | BrandDto |
| PUT | `/api/brands/{id}` | Обновить бренд | Admin | UpdateBrandDto<br>• Name?<br>• LogoUrl?<br>• Description? | - |
| DELETE | `/api/brands/{id}` | Удалить бренд | Admin | - | - |

### Категории (/api/categories)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/categories` | Список категорий | - | CategoriesFilterDto<br>• Page<br>• Size<br>• SearchText | List\<ShortCategoryDto\><br>• Id<br>• Name<br>• ImageUrl |
| GET | `/api/categories/{id}` | Категория по ID | - | - | CategoryDto<br>• Id<br>• Name<br>• ImageUrl<br>• Description<br>• ProductQuantity |
| POST | `/api/categories` | Создать категорию | Admin | CreateCategoryDto<br>• Name<br>• Description<br>• ImageUrl | CategoryDto |
| PUT | `/api/categories/{id}` | Обновить категорию | Admin | UpdateCategoryDto<br>• Name?<br>• Description?<br>• ImageUrl? | - |
| DELETE | `/api/categories/{id}` | Удалить категорию | Admin | - | - |

### Уведомления (/api/notifications)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/notifications/my-notifications` | Мои уведомления | Customer | NotificationsFilterDto<br>• Page<br>• Size<br>• HasBeenRead | List\<ShortNotificationDto\><br>• Id<br>• Name<br>• DateTime<br>• HasBeenRead |
| GET | `/api/notifications` | Все уведомления | Admin | NotificationsFilterDto<br>• Page<br>• Size<br>• HasBeenRead | List\<ShortNotificationDto\> |
| GET | `/api/notifications/{id}` | Уведомление по ID | Admin/Customer* | - | NotificationDto<br>• Id<br>• Name<br>• Message<br>• DateTime<br>• HasBeenRead<br>• AccountId |
| POST | `/api/notifications` | Создать уведомление | Admin | CreateNotificationDto<br>• AccountId<br>• Name<br>• Message | NotificationDto |
| DELETE | `/api/notifications/{id}` | Удалить уведомление | Admin | - | - |

### Транзакции (/api/transactions)
| Метод | Endpoint | Описание | Роли | Request DTO | Response DTO |
|-------|----------|----------|------|-------------|--------------|
| GET | `/api/transactions/{id}` | Транзакция по ID | Admin/Customer* | - | TransactionDto<br>• Id<br>• AccountId<br>• Login<br>• Email<br>• Balance<br>• TotalSum<br>• IsSuccessful<br>• Message<br>• CreatedAt |
| GET | `/api/transactions/my-transactions` | Мои транзакции | Customer | TransactionsFilterDto<br>• Page<br>• Size<br>• MinAmount<br>• MaxAmount<br>• IsSuccessful | List\<ShortTransactionDto\><br>• Id<br>• AccountId<br>• TotalSum<br>• IsSuccessful<br>• Message<br>• CreatedAt |
| POST | `/api/transactions` | Создать транзакцию | Admin | CreateTransactionDto<br>• AccountId<br>• TotalSum<br>• IsSuccessful<br>• Message | TransactionDto |

## Планы развития

### Запланировано к реализации:
1. **Фронтенд на React** с TypeScript и Vite
2. **Интеграция платежных систем** (Stripe/YooKassa)
3. **Email-уведомления** для пользователей
4. **Real-time обновления** через WebSockets
5. **Docker-контейнеризация** для легкого развертывания
6. **Расширенная аналитика** продаж и пользовательской активности

## Заключение

SealMarket API демонстрирует современный подход к разработке бэкенд-систем на .NET с использованием лучших практик: чистая архитектура, DDD элементы, репозиторный паттерн, JWT аутентификация и структурированное логирование.

Проект готов к интеграции с фронтенд-приложением и масштабированию для production-использования.

## Спасибо за внимание!
