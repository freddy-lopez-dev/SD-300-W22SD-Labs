
using System.Text;

Product beans = new Product("Chocolate-covered Beans", 2, "A12");
Product lays = new Product("Lays", 1, "002");

VendingMachine myVendo = new VendingMachine(143);

myVendo.StockItem(beans, 10);
myVendo.StockItem(lays, 10);
myVendo.StockFloat(20, 10);
myVendo.StockFloat(10, 10);
myVendo.StockFloat(5, 10);
myVendo.StockFloat(2, 10);
myVendo.StockFloat(1, 10);

Console.WriteLine(myVendo.SerialNumber);
foreach(KeyValuePair<int, int> money in myVendo.MoneyFloat)
{
    Console.WriteLine(money);
}

class VendingMachine
{
    public int SerialNumber { get; set; }
    public Dictionary<int, int> MoneyFloat { get; set; }
    public Dictionary<Product, int> Inventory { get; set; }

    public VendingMachine()
    {
        MoneyFloat = new Dictionary<int, int>();
        Inventory = new Dictionary<Product, int>();
    }

    public VendingMachine(int srlno)
    {
        SerialNumber = srlno;
        MoneyFloat = new Dictionary<int, int>();
        Inventory = new Dictionary<Product, int>();
    }

    public VendingMachine(int serialNumber, Dictionary<int, int> moneyFloat, Dictionary<Product, int> inventory)
    {
        SerialNumber = serialNumber;
        MoneyFloat = moneyFloat;
        Inventory = inventory;
        MoneyFloat = new Dictionary<int, int>();
        Inventory = new Dictionary<Product, int>();
    }

    public string StockItem(Product product, int quantity)
    {
        string informStock;

        if (Inventory.ContainsKey(product))
        {
            quantity++;
            informStock = $"Product {product.Name} with code {product.Code} and price of {product.Price} new quantity is {quantity}";
        } else
        {
            Inventory.Add(product, quantity);
            informStock = $"Product {product.Name} with code {product.Code} and price of {product.Price} has been added";
        }

        return informStock;
    }

    public string StockFloat(int moneyDenomination, int quantity)
    {
        StringBuilder informUser = new StringBuilder();

        if(!MoneyFloat.ContainsKey(moneyDenomination))
        {
            MoneyFloat.Add(moneyDenomination, quantity);
        }
        

        foreach(KeyValuePair<int, int> money in MoneyFloat)
        {
            informUser.Append(money.Key);
        }

        return informUser.ToString();
        
    }

    public void VendItem(string code, List<int> userMoney)
    {
        int totalUserMoney = userMoney.Sum();

        foreach(KeyValuePair<Product, int> product in Inventory)
        {
            //check if user has money -- throw Error: insufficient money provided
            //check if code exist -- throw Error, no item with code “E17”
            //check if product has stock -- throw Error: Item is out of stock
            //vend
            if (product.Key.Code == code && totalUserMoney >= product.Key.Price)
            {
                //Vend
            } 
            if(product.Key.Code == code && product.Value <= 0)
            {
                // no stock
            }
            if(product.Key.Code != code)
            {
                // no item with code {code}
            }
        }
    }
}

class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Code { get; set; }

    public Product(string name, int price, string code)
    {
        Name = name;
        Price = price;
        Code = code;
    }
}

