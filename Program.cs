
using System.Text;

Product beans = new Product("beans", 2, "A12");
VendingMachine myVendo = new VendingMachine("11");

myVendo.StockItem(beans, -2);
myVendo.VendItem("A12", new List<int>() { 5, -1 });
myVendo.VendItem("", new List<int>() { 5, 1 });


class VendingMachine
{
    public string BarCode { get; }
    public static int SerialNumber { get; set; } = 0;
    private Dictionary<int, int> MoneyFloat { get; set; }
    private Dictionary<Product, int> Inventory { get; set; }

    public VendingMachine(string barcode)
    {
        try
        {
            SerialNumber++;
            BarCode = CheckBarcode(barcode);
            MoneyFloat = new Dictionary<int, int>();
            Inventory = new Dictionary<Product, int>();
        } catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public string CheckBarcode(string code)
    {
        if(!string.IsNullOrEmpty(code))
        {
            return code;
        }

        throw new Exception("Invalid Code");
    }

    public int CheckInt(int qty)
    {
        if(qty > 0)
        {
            return qty;
        }

        throw new Exception("Not a valid QTY");
    }

    public void StockItem(Product product, int quantity)
    {

        try
        {
            int prodQty = CheckInt(quantity);
            if (Inventory.ContainsKey(product))
            {
                prodQty++;
                Console.WriteLine($"Product {product.Name} with code {product.Code} and price of {product.Price} new quantity is {quantity}");
            }
            else
            {
                Inventory.Add(product, prodQty);
                Console.WriteLine($"Product {product.Name} with code {product.Code} and price of {product.Price} has been added");
            }
        } catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

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

    public List<int> CheckUserMoney(List<int> moneyList)
    {
        List<int> checkedMoney = new List<int>();
        foreach(int money in moneyList)
        {
            if(money > 0)
            {
                checkedMoney.Add(money);
            } else
            {
                throw new Exception("Invalid Cash");
            }
        }

        return checkedMoney;
    }

    public void VendItem(string code, List<int> userMoney)
    {
        try
        {
            List<int> checkedMoney = CheckUserMoney(userMoney);
            string checkedCode = CheckBarcode(code);
            int totalUserMoney = checkedMoney.Sum();
            int change = 0;

            if (totalUserMoney > 0)
            {
                for (int itemIndex = 0; itemIndex < Inventory.Count; itemIndex++)
                {
                    KeyValuePair<Product, int> item = Inventory.ElementAt(itemIndex);

                    if (item.Key.Code == checkedCode && item.Key.Price < totalUserMoney)
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
                    }
                    else
                    {
                        Console.WriteLine("Invalid code or insufficient fund");
                    }
                }

            }
            else
            {
                Console.WriteLine("Insert some cash");
            }

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
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
        try
        {
            Name = CheckString(name);
            Price = CheckPrice(price);
            Code = CheckString(code);
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public string CheckString(string value)
    {
        if(!string.IsNullOrEmpty(value))
        {
            return value;
        }
        throw new Exception("Not a valid name");
    }

    public int CheckPrice(int price)
    {
        if(price > 0)
        {
            return price;
        }
        throw new Exception("Not a valid price");
    }
}

