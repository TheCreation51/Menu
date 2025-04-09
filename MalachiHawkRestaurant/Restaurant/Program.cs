
internal class Program
{
    private static void Main(string[] args)
    {
        MenuItem item1 = new MenuItem("Deep Fried Pickles", 5.99, "Appetizer", false);
        MenuItem item2 = new MenuItem("6oz Sirloin", 15.99, "Main Course", false);
        MenuItem item3 = new MenuItem("Chocolate Cake", 6.99, "Dessert", true);

        Menu menu = new Menu();
        menu.AddMenuItem(item1);
        menu.AddMenuItem(item2);
        menu.AddMenuItem(item3);

        menu.DisplayMenu();

        Console.WriteLine("\nEnter the amount of money in your wallet:");
        double wallet = double.Parse(Console.ReadLine());

        while (true)
        {
            Console.WriteLine("\nEnter the name of the item you want to buy (or type 'I'm all good' to exit):");
            string itemName = Console.ReadLine();

            if (itemName.Equals("I'm all good", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Thank you for visiting! Enjoy your meal!");
                break;
            }

            menu.BuyItem(itemName, ref wallet);

            if (wallet <= 0)
            {
                Console.WriteLine("You have no more money left. Thank you for visiting!");
                break;
            }
        }
    }
}

public class MenuItem
{
    public string Description { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public bool IsNew { get; set; }

    public MenuItem(string description, double price, string category, bool isNew)
    {
        Description = description;
        Price = price;
        Category = category;
        IsNew = isNew;
    }

    public override string ToString()
    {
        return $"{Description} - ${Price} ({Category}) {(IsNew ? "[NEW]" : "")}";
    }
}

public class Menu
{
    private List<MenuItem> Items { get; set; }
    public DateTime LastUpdated { get; private set; }

    public Menu()
    {
        Items = new List<MenuItem>();
        LastUpdated = DateTime.Now;
    }

    public void AddMenuItem(MenuItem item)
    {
        Items.Add(item);
        LastUpdated = DateTime.Now;
    }

    public void DisplayMenu()
    {
        Console.WriteLine("Menu:");
        foreach (var item in Items)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine($"\nLast Updated: {LastUpdated}");
    }

    public void BuyItem(string itemName, ref double wallet)
    {
        MenuItem item = Items.Find(i => i.Description.Equals(itemName, StringComparison.OrdinalIgnoreCase));

        if (item == null)
        {
            Console.WriteLine("Item not found on the menu.");
            return;
        }

        if (wallet >= item.Price)
        {
            wallet -= item.Price;
            Console.WriteLine($"You bought {item.Description} for ${item.Price}. Remaining wallet balance: ${wallet:F2}");
        }
        else
        {
            Console.WriteLine($"You don't have enough money to buy {item.Description}. You need ${item.Price - wallet:F2} more.");
        }
    }
}
