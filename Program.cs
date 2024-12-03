public class Computers
{
    public string Name;
    public string Model;
    public double Price;
    public string PurchaseDate;
    public Computers(string name, string model, double price, string purchaseDate)
    {
        Name = name;
        Model = model;
        Price = price;
        PurchaseDate = purchaseDate;
    }
    public string Show()
    {
        Console.WriteLine("Asset".PadRight(30) + "Brand".PadRight(30) + "Price".PadRight(30) + "Purchase Date".PadRight(30));
        return Name.PadRight(30) + Model.PadRight(30) + Price.ToString().PadRight(30) + PurchaseDate.PadRight(30);
    }
}
public class Phones : Computers
{
    public Phones(string name, string model, double price, string purchaseDate) : base(name, model, price, purchaseDate)
    {

    }
}
public class Program
{
    List<Computers> assets = new List<Computers>();
    int i = 0;
    public void AddAssets()
    {
        while (true)
        {
            Console.WriteLine("Enter the asset name or enter Q to end the program!");
            Console.Write("Enter your asset name: ");
            string assetName = Console.ReadLine();
            if (assetName.ToLower() == "q")
            {
                break;
            }
            Console.Write("Enter your asset model: ");
            string assetModel = Console.ReadLine();

            Console.Write("Enter the asset price: ");
            double.TryParse(Console.ReadLine(), out double assetPrice);

            Console.Write("Enter the purchase date(YYYY-MM-DD): ");
            string purchaseDate = Console.ReadLine();

            Console.Write("Is this asset a computer? (yes/no): ");
            string isComputer = Console.ReadLine().ToLower();

            if (isComputer == "yes")
            {
                assets.Add(new Computers(assetName, assetModel, assetPrice, purchaseDate));
            }
            else
            {
                assets.Add(new Phones(assetName, assetModel, assetPrice, purchaseDate));
            }


        }

        SortByClass();
        SortByDate();
        PrintAssets();
    }
    public void SortByClass()
    {
        assets = assets.OrderBy(asset => asset.GetType().Name).ToList();
    }
    public void SortByDate()
    {
        assets = assets.OrderBy(assets => DateTime.Parse(assets.PurchaseDate)).ToList();
    }
    public void PrintAssets()
    {
        foreach (var asset in assets)
        {
            Console.WriteLine(asset.Show());
        }
    }
    public static void Main()
    {
        Program program = new Program();
        program.AddAssets();

    }
}