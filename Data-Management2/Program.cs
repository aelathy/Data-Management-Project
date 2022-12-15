
// Data Management Project
#nullable disable
using System.Text.Json;
Console.Clear();

// Read user-data file
string jsonString = File.ReadAllText("user-data.json");

// Convert Back -- > Data
List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);

// List of users
users.Add(new User("Aly", "hi"));
users.Add(new User("Mr. Veldkamp", "CS"));
users.Add(new User("Bob", "Password"));

// List of products (including ones not in shopping list)
List<Product> products = new List<Product>();

// All Products
products.Add(new Product("Laptop", "Acer", 800));
products.Add(new Product("Phone", "Samsung", 1000));
products.Add(new Product("Phone", "Apple", 1000));
products.Add(new Product("Monitor", "Asus", 300));
products.Add(new Product("Telivision", "Samsung", 700));
products.Add(new Product("Tablet", "Samsung", 100));
products.Add(new Product("Laptop", "Apple", 2000));
// Temp Shopping List
List<Product> ShoppingList = new List<Product>();

// Boolean based on signing up or logging in (log is true and sign is false)
bool signOrLog = true;

Console.WriteLine("1. Log in with existing account.");
Console.WriteLine("2. Sign up using new account.");
string enterInput = Console.ReadLine();
if (enterInput == "2")
{
    signOrLog = false;
}
else if (enterInput != "1")
{
    Environment.Exit(0);
}
//User Login
bool usernameMatch = false;
if (!signOrLog)
    Console.WriteLine("Please enter the username and password of the account you would like to register.");
Console.Write("Username: ");
string usernameInput = Console.ReadLine();
Console.Write("Password: ");
string passwordInput = Console.ReadLine();

// If signing up, check if username doesn't already exist. If so, kick out
if (!signOrLog)
{
    foreach (User user in users)
    {
        if (usernameInput == user.Username)
        {
            usernameMatch = true;
        }
    }
    if (!usernameMatch)
    {
        users.Add(new User(usernameInput, passwordInput));
    }
    else
    {
        Console.WriteLine("Username already exists.");
        Environment.Exit(0);
    }
}

// var shopList = getShopList(usernameInput);
if (findUser(usernameInput, passwordInput))
{
    bool menuLoop = true;
    while (menuLoop)
    {
        var shopList = getShopList(usernameInput);
        // Main Menu Loop
        Console.WriteLine("\n Main Menu");
        Console.WriteLine("1. Display All Products");
        Console.WriteLine("2. Look for Product");
        Console.WriteLine("3. Sort & Show Lowest to Highest");
        Console.WriteLine("4. Add Product to Shopping Cart");
        Console.WriteLine("5. Remove Product from Shopping Cart");
        Console.WriteLine("6. Display Shopping Cart");
        Console.WriteLine("7. Register New Account");
        Console.WriteLine("8. Delete Account");
        Console.WriteLine("9. Exit");
        string menuOption = Console.ReadLine().ToLower();
        Console.WriteLine();


        if (menuOption == "1")
        {
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{products[i].Type} {products[i].Brand} ${products[i].Price}");
            }
        }
        else if (menuOption == "2")
        {
            bool result = false;
            // Implement Search Program
            Console.Write("Search by Brand: ");
            string brandSearch = Console.ReadLine().ToLower();
            // First letters of the product brand match the letters type (left to right)
            for (int i = 0; i < products.Count; i++)
            {
                if (brandSearch == products[i].Brand.ToLower())
                {
                    result = true;
                    Console.WriteLine($"{products[i].Type} {products[i].Brand} ${products[i].Price}");
                }
            }
            if (!result)
            {
                Console.WriteLine("Product not found.");
            }
        }
        else if (menuOption == "3")
        {
            // Do Some Type of Sort to Organize Products by Lowest to Highest Price
            for (int i = 0; i < products.Count; i++)
            {
                for (int j = 0; j < products.Count - (i + 1); j++)
                {
                    int compare = products[j].Price.CompareTo(products[j + 1].Price);
                    if (compare == 1)
                    {
                        int chng = products[j + 1].Price;
                        products[j + 1].Price = products[j].Price;
                        products[j].Price = chng;
                    }
                }
            }

        }
        else if (menuOption == "4")
        {
            bool result = false;
            // Add Product to Shopping List
            Console.WriteLine("Enter the type & brand of the product you want to ADD:");
            Console.Write("Type: ");
            string addType = Console.ReadLine().ToLower();
            Console.Write("Brand: ");
            string addBrand = Console.ReadLine().ToLower();
            for (int i = 0; i < products.Count; i++)
            {
                if (addType == products[i].Type.ToLower() && addBrand == products[i].Brand.ToLower())
                {
                    // Product Found
                    result = true;
                    if (ShoppingList.Count > 0)
                    {
                        foreach (Product item in shopList)
                        {
                            if (addType == item.Type.ToLower() && addBrand == item.Brand.ToLower())
                            {
                                Console.WriteLine("Item already in shopping cart.");
                            }
                            else
                            {
                                shopList.Add(products[i]);
                                Console.WriteLine("Item added to shopping cart.");
                                break;
                            }
                        }
                    }
                    else
                    {
                        shopList.Add(products[i]);
                        Console.WriteLine("Item addded to shopping cart.");
                    }
                }
            }
            if (!result)
            {
                Console.WriteLine("Product not found.");
            }
        }
        else if (menuOption == "5")
        {
            // Remove Product from Shopping List
            if (shopList.Count > 0)
            {
                bool result = false;
                Console.WriteLine("Enter the type & brand of the product you want to REMOVE:");
                Console.Write("Type: ");
                string addType = Console.ReadLine().ToLower();
                Console.Write("Brand: ");
                string addBrand = Console.ReadLine().ToLower();
                for (int j = 0; j < shopList.Count; j++)
                {
                    if (addType == shopList[j].Type.ToLower() && addBrand == shopList[j].Brand.ToLower())
                    {
                        result = true;
                        shopList.Remove(shopList[j]);
                        Console.WriteLine("Item removed from shopping cart.");
                    }
                }
                if (!result)
                {
                    Console.WriteLine("Item not found in shopping cart.");
                }
            }
            else
            {
                Console.WriteLine("Shopping cart is already empty.");
            }
        }
        else if (menuOption == "6")
        {
            // Display Shopping Cart
            if (shopList.Count > 0)
            {
                for (int i = 0; i < shopList.Count; i++)
                {
                    Console.WriteLine($"{shopList[i].Type} {shopList[i].Brand} ${shopList[i].Price}");
                }
            }
            else
            {
                Console.WriteLine("Shopping cart empty.");
            }
        }
        else if (menuOption == "7")
        {
            usernameMatch = false;
            Console.WriteLine("Please enter the username & password of the account you want to register.");
            Console.WriteLine();
            Console.WriteLine("Username: ");
            string newUsernameInput = Console.ReadLine();
            Console.WriteLine("Password: ");
            string newPasswordInput = Console.ReadLine();
            foreach (User user in users)
            {
                if (user.Username == newUsernameInput)
                {
                    usernameMatch = true;
                }
            }
            if (!usernameMatch)
            {
                users.Add(new User(newUsernameInput, newPasswordInput));
            }
            else
            {
                Console.WriteLine("Username is already in use.");
            }
        }
        else if (menuOption == "8")
        {
            usernameMatch = false;
            Console.WriteLine("Please enter the username & password of the accont you want to delete.");
            Console.WriteLine();
            Console.WriteLine("Username: ");
            string dltUsernameInput = Console.ReadLine();
            Console.WriteLine("Password: ");
            string dltPasswordInput = Console.ReadLine();
            // Check if valid valid username and passwordto output message
            foreach (User user in users)
            {
                if (user.Username == dltUsernameInput && user.Password == dltPasswordInput)
                    usernameMatch = true;
            }
            // Remove all users with this username and password
            users.RemoveAll(users => users.Username == dltUsernameInput && users.Password == dltPasswordInput);
            // Output based on foreach loop result
            if (usernameMatch)
            {
                Console.WriteLine($"Account of {dltUsernameInput} deleted.");
            }
            else
            {
                Console.WriteLine("Username and/or password do not match/exist.");
            }
        }
        else if (menuOption == "9")
        {
            break;
        }
    }
}

bool findUser(string username, string password)
{
    foreach (User user in users)
    {
        if (user.Username == username && user.Password == password)
        {
            Console.WriteLine($"Welcome, {username}");
            return true;
        }
    }
    Console.WriteLine("Username and/or password not found.");
    return false;
}

List<Product> getShopList(string username)
{
    foreach (User user in users)
    {
        if (user.Username == username)
        {
            return user.ShopList;
        }
    }
    return null;
}

// Convert --> JSON string
jsonString = JsonSerializer.Serialize(users);

// Store in user-data file
File.WriteAllText("user-data.json", jsonString);

class Product
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }

    public Product(string type, string brand, int price)
    {
        this.Type = type;
        this.Brand = brand;
        this.Price = price;
    }
}

class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Product> ShopList { get; set; }

    public User(string username, string password)
    {
        this.Username = username;
        this.Password = password;
        this.ShopList = new List<Product>();
    }

    public override string ToString()
    {
        return $"({this.Username},{this.Password}, {this.ShopList})";
    }
}