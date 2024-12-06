public class Computers : Assets
{
    public Computers(string type, string brand, string model, string office, DateOnly purchaseDate, double priceUSD, string currency, double localPrice) : base(type, brand, model, office, purchaseDate, priceUSD, currency, localPrice)
    {
    }
}
public class Phones : Assets
{
    public Phones(string type, string brand, string model, string office, DateOnly purchaseDate, double priceUSD, string currency, double localPrice) : base(type, brand, model, office, purchaseDate, priceUSD, currency, localPrice)
    {

    }
}
public class ManageAssets
{
    List<Assets> assets = new List<Assets>();
    public void AddAssets()
    {
        CurrencyConverter.FetchRates();
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Enter the asset type or enter Q to end the program!");
            Console.ResetColor();
            Console.Write("Enter the asset type, computer or phone: ");
            string assetType = Console.ReadLine().Trim().ToLower();
            if (assetType.ToLower() == "q") break;
            if (assetType != "computer" && assetType != "phone")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid asset type. Please enter 'computer' or 'phone'.");
                Console.ResetColor();
                continue;
            }
            string assetBrand;
            while (true)
            {
                Console.Write("Enter the asset brand: ");
                assetBrand = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(assetBrand))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Brand cannot be empty. Try again.");
                    Console.ResetColor();
                }
                else break;
            }
            string assetModel;
            while (true)
            {
                Console.Write("Enter the asset model: ");
                assetModel = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(assetModel))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Brand cannot be empty. Try again.");
                    Console.ResetColor();

                }
                else break;
            }
            string office;
            while (true)
            {
                Console.Write("Enter the office, Sweden, Spain or USA: ");
                office = Console.ReadLine().Trim().ToLower();
                if (office != "sweden" && office != "spain" && office != "usa")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid office. Please enter 'Sweden' or 'Spain' or 'USA'.");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }
            string currency = office switch
            {
                "sweden" => "SEK",
                "spain" => "EUR",
                _ => "USD"
            };
            double priceUSD;
            while (true)
            {
                Console.Write("Enter the asset price in USD: ");
                if (!double.TryParse(Console.ReadLine(), out priceUSD) || priceUSD <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price. Please enter a positive number.");
                    Console.ResetColor();

                }
                else
                {
                    break;
                }
            }
            double localPrice = CurrencyConverter.Convert(priceUSD, "USD", currency);
            DateTime purchaseTime;
            while (true)
            {
                Console.Write("Enter the purchase date(MM-DD-YYYY): ");
                if (!DateTime.TryParse(Console.ReadLine(), out purchaseTime))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Please enter a valid date");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }
            DateOnly purchaseDate = DateOnly.FromDateTime(purchaseTime);

            if (string.Equals(assetType, "computer", StringComparison.OrdinalIgnoreCase))
            {
                assets.Add(new Computers(assetType, assetBrand, assetModel, office, purchaseDate, priceUSD, currency, localPrice));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Asset added successfully!");
                Console.ResetColor();
            }
            else if (string.Equals(assetType, "phone", StringComparison.OrdinalIgnoreCase))
            {
                assets.Add(new Phones(assetType, assetBrand, assetModel, office, purchaseDate, priceUSD, currency, localPrice));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Asset added successfully!");
                Console.ResetColor();
            }
        }
    }
    public void ShowAssets()
    {
        Sort();
        Console.WriteLine("Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Office".PadRight(20) + "Purchase Date".PadRight(20) + "Price in USD".PadRight(20) + "Currency".PadRight(20) + "Local price".PadRight(20));
        int threeYears = 365 * 3;
        int threeMonths = 30 * 3;
        int sixMonths = 30 * 6;

        foreach (var a in assets)
        {
            int validDays = DateOnly.FromDateTime(DateTime.Now).DayNumber - a.PurchaseDate.DayNumber;
            ConsoleColor color = ConsoleColor.White;

            if (validDays > threeYears || threeYears - validDays <= threeMonths)
            {
                color = ConsoleColor.Red;
            }
            else if (threeMonths <= threeYears - validDays && threeYears - validDays <= sixMonths)
            {
                color = ConsoleColor.Yellow;
            }
            else { color = ConsoleColor.White; };
            PrintAssets(a, color);
        }
    }
    public void Sort()
    {
        assets = assets.OrderBy(asset => asset.Office).ThenBy(asset => asset.GetType().Name).ThenByDescending(assets => assets.PurchaseDate).ToList();
    }
    private string Capitalize(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToUpper(input[0]) + input.Substring(1).ToLower();
    }
    public void PrintAssets(Assets a, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(Capitalize(a.Type).PadRight(20) + Capitalize(a.Brand).PadRight(20) + Capitalize(a.Model).PadRight(20) + Capitalize(a.Office).PadRight(20) + a.PurchaseDate.ToString().PadRight(20) + a.PriceUSD.ToString("0.00").PadRight(20) + a.Currency.PadRight(20) + a.LocalPrice.ToString("0.00").PadRight(20));
        Console.ResetColor();
    }
}