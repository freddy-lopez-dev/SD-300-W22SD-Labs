
using System.Text;

Product beans = new Product("Chocolate-covered Beans", 2, "A12");
Product lays = new Product("Lays", 1, "A13");

VendingMachine myVendo = new VendingMachine("00212");

Console.WriteLine(myVendo.StockItem(beans, 10));
Console.WriteLine(myVendo.StockItem(lays, 10));
myVendo.StockFloat(20, 10);
myVendo.StockFloat(10, 10);
myVendo.StockFloat(5, 10);
myVendo.StockFloat(2, 10);
Console.WriteLine($"Your bills are {myVendo.StockFloat(1, 10)}"); 
List<int> myMoney = new List<int>() { 1, 5};

myVendo.VendItem("A12", myMoney);
VendingMachine myVendo2 = new VendingMachine("00213");
Console.WriteLine(VendingMachine.SerialNumber);

class VendingMachine
{
    public string BarCode { get; }
    public static int SerialNumber { get; set; } = 0;
    private Dictionary<int, int> MoneyFloat { get; set; }
    private Dictionary<Product, int> Inventory { get; set; }

    public VendingMachine(string barcode)
    {
        SerialNumber++;
        BarCode = barcode;
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
            informUser.Append(' ');
        }

        return informUser.ToString();
        
    }

    public void VendItem(string code, List<int> userMoney)
    {
        int totalUserMoney = userMoney.Sum();
        int change = 0;
        
        if(totalUserMoney > 0)
        {
            for(int itemIndex = 0; itemIndex < Inventory.Count; itemIndex++)
            {
                KeyValuePair<Product, int> item = Inventory.ElementAt(itemIndex);
            
                if (item.Key.Code == code && item.Key.Price < totalUserMoney)
                {

                        change = totalUserMoney - item.Key.Price;
                        // deduct the qty item
                        Inventory[item.Key]--;
                        // give some change
                        Dictionary<int, int> userChange = new Dictionary<int, int>();
                        for (int coinIndex = 0; coinIndex < MoneyFloat.Count; coinIndex++)
                        {
                            KeyValuePair<int, int> coin = MoneyFloat.ElementAt(coinIndex);

                            //Check if there is enough qty for the given coin(coin.Key) in the vending machine.

                            if (coin.Value > 0)
                            {
                                if (change > 0 && change >= coin.Key)
                                {
                                    change -= coin.Key;
                                    coinIndex--;
                                    MoneyFloat[coin.Key] = coin.Value - 1;
                                    if (userChange.ContainsKey(coin.Key))
                                    {
                                        userChange[coin.Key]++;
                                    }
                                    else
                                    {
                                        userChange.Add(coin.Key, 1);
                                    }
                                }
                            }

                        }
                        // print change
                        Console.WriteLine("Your change is:");
                        foreach (KeyValuePair<int, int> c in userChange)
                        {
                            Console.WriteLine(c);
                        }
                    } else
                {
                    Console.WriteLine("Invalid code or insufficient fund");
                }
                }
            
        } else
        {
            Console.WriteLine("Insert some cash");
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

