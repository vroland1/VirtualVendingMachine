using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<VendingMachineItem> items = new List<VendingMachineItem>();
        private string FilePath = @"C:\VendingMachine\";
        private string FileName = @"vendingmachine.csv";
        public decimal Balance { get; set; }

        public List<VendingMachineItem> clone // for testing only
        {
            get
            {
                return items;
            }
            set
            {

            }
        }

        public string CurrentDateTime
        {
            get
            {
                return DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            }
        }
        private List<string[]> AuditData = new List<string[]>();

        public VendingMachine()
        {
            GenerateInfo(FilePath + FileName);
        }

        public VendingMachine(string filePath) //created for testing with own filePath
        {
            FilePath = filePath;
            GenerateInfo(FilePath);
        }

        public bool FeedMoney(string input)
        {
            decimal feedInput = 0;
            bool valid = false;
            try
            {
                feedInput = Convert.ToDecimal(input);
                feedInput = Decimal.Parse(feedInput.ToString("0.00"));
            }
            catch
            {
                valid = false;
            }

            if (feedInput == 1 || feedInput == 2 || feedInput == 5 || feedInput == 10)
            {
                valid = true;
                Balance += feedInput;
                string[] feedAudit = new string[] { CurrentDateTime, "FEED MONEY", feedInput.ToString(), Balance.ToString() };
                AuditData.Add(feedAudit);
            }

            return valid;
        }

        public string ReturnChange(decimal balance)
        {
            string change = "";

            int returnAmount = (int)(balance * 100);

            int quarters = returnAmount / 25;
            returnAmount = returnAmount % 25;

            int dimes = returnAmount / 10;
            returnAmount = returnAmount % 10;

            int nickels = returnAmount / 5;
            returnAmount = returnAmount % 5;

            int pennies = returnAmount / 1;

            change = $"Your change is {quarters} quarters, {dimes} dimes, {nickels} nickels, and {pennies} pennies, for a total of ${balance}";

            string[] feedAudit = new string[] { CurrentDateTime, "GIVE CHANGE", Balance.ToString(), "0.00" };
            AuditData.Add(feedAudit);

            Balance = 0;

            return change;
        }

        public string Purchase(string input)
        {
            string oldBalance = Balance.ToString();
            string nameAndSlot = "";
            string output = "Item not found!";
            decimal getPrice = 0;
            bool canPurchase = false;

            foreach (VendingMachineItem x in items)
            {
                if (x.Slot.ToLower().Equals(input))
                {
                    getPrice = Convert.ToDecimal(x.Price);

                    if (getPrice > Balance)
                    {
                        output = "Not enough funds!";
                    }
                    else if (x.Quantity == 0)
                    {
                        output = "Item is sold out!";
                    }
                    else
                    {
                        canPurchase = true;
                        nameAndSlot = x.Name + " " + x.Slot;
                    }

                    if (canPurchase)
                    {
                        Balance -= getPrice;
                        x.Quantity -= 1;
                        string[] feedAudit = new string[] { CurrentDateTime, nameAndSlot, oldBalance, Balance.ToString() };
                        AuditData.Add(feedAudit);

                        output = "You have successfully purchased " + x.Name;
                    }
                }
            }

            return output;
        }

        public string ConsumptionMessage(string input)
        {
            string message = "";

            message = (input.ToLower().Contains("a")) ? "Crunch Crunch, Yum!" : message;

            message = (input.ToLower().Contains("b")) ? "Munch Munch, Yum!" : message;

            message = (input.ToLower().Contains("c")) ? "Glug Glug, Yum!" : message;

            message = (input.ToLower().Contains("d")) ? "Chew Chew, Yum!" : message;

            return message;
        }

        public void GenerateInfo(string filePath)
        {
            string line;

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] item = line.Split('|');
                    int index = items.Count;

                    items.Add(new VendingMachineItem());
                    items[index].Slot = item[0];
                    items[index].Name = item[1];
                    items[index].Price = item[2];
                    items[index].Quantity = 5;
                }
            }
        }

        public string Display()
        {
            string displayString = String.Format("   {0,-5}  {1,-20} {2,-10} {3,-10}", "Slot", "Item Name", "Price", "Amount");
            displayString += "\n";

            for (int i = 0; i < 45; i++)
            {
                displayString += "=";
            }

            displayString += "\n";

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Quantity == 0)
                {
                    displayString += String.Format("    {0,-5} {1,-20} {2,-7}{3,-10}", items[i].Slot, items[i].Name, items[i].Price, "Sold Out!");
                    displayString += "\n";
                }
                else
                {
                    displayString += String.Format("    {0,-5} {1,-20} {2,-10} {3,-10}", items[i].Slot, items[i].Name, items[i].Price, items[i].Quantity);
                    displayString += "\n";
                }
            }

            return displayString;
        }

        public void PrintAudit()
        {
            using (StreamWriter sr = new StreamWriter($@"{FilePath}Log.txt", true))
            {
                foreach (string[] entry in AuditData)
                {
                    string line = String.Format("{0,-20}     {1,-25}     {2,-5} {3,-5}", entry[0], entry[1], entry[2], entry[3]);
                    sr.WriteLine(line);
                }
            }
        }

        public void PrintSalesReport()
        {
            decimal totalSales = 0;
            string salesDateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm tt");
            salesDateTime = salesDateTime.Replace('/', '_').Replace(':', '-').Replace(' ', '_');

            string saleReportName = FilePath + salesDateTime + "_SalesReport.txt";

            using (StreamWriter sw = new StreamWriter(saleReportName))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    sw.WriteLine(String.Format("    {0,-20} {1,-5}", items[i].Name, "| " + Math.Abs(items[i].Quantity - 5)));
                    totalSales += (Convert.ToDecimal(items[i].Price) * (Math.Abs(items[i].Quantity - 5)));
                    totalSales = Math.Round(totalSales, 2);
                }

                sw.WriteLine();
                sw.WriteLine(String.Format("    {0,-20} {1,-5}", "** TOTAL SALES **", totalSales.ToString("0.00")));
            }
        }
    }
}














