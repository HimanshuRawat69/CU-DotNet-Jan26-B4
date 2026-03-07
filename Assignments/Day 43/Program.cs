using System.Diagnostics.Metrics;

namespace FinancialPortfolioManagementReportingSystem
{

    interface IRiskAssessable
    {
        string GetRiskCategory();
    }
    interface IReportable
    {
        string GenerateReportLine();
    }
    abstract class FinancialInstrument
    {
        private int instrumentId;

        private string name;

        private string currency;

        private int quantity;

        private decimal purchasePrice;

        private decimal marketPrice;

        public int InstrumentId
        {
            get
            {
                return instrumentId;
            }
            set
            {
                instrumentId = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Currency
        {
            get
            {
                return currency;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
                    throw new InvalidFinancialDataException
                        ("Invalid currency format, Must contain 3-letter code.");
                currency = value.ToUpper();
            }
        }
        public DateOnly PurchaseDate { get; set; }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value < 0)
                    throw new InvalidFinancialDataException("Qunatity can't be negative.");
                quantity = value;
            }
        }
        public decimal PurchasePrice
        {
            get
            {
                return purchasePrice;
            }
            set
            {
                if (value < 0)
                    throw new InvalidFinancialDataException("Purchase price can't be negative.");
                purchasePrice = value;
            }
        }
        public decimal MarketPrice
        {
            get
            {
                return marketPrice;
            }
            set
            {
                if (value < 0)
                    throw new InvalidFinancialDataException("Market price can't be negative.");
                marketPrice = value;
            }
        }
        public abstract decimal CalculateCurrentValue();

        public virtual string GetInstrumentSummary()
        {
            return $"ID-{InstrumentId}, Name-{Name}, Value-{CalculateCurrentValue().ToString("C")}";
        }

    }
    class InvalidFinancialDataException : Exception
    {
        public InvalidFinancialDataException(string message) : base(message) { }
    }

    class Equity : FinancialInstrument, IRiskAssessable, IReportable
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }
        public string GetRiskCategory()
        {
            return $"High";
        }

        public string GenerateReportLine()
        {
            return $"Equity | {Name} | {Quantity} Units | Value-{CalculateCurrentValue().ToString("C")} ";
        }
    }

    class Bond : FinancialInstrument, IRiskAssessable, IReportable
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }
        public string GetRiskCategory()
        {
            return $"Low";
        }

        public string GenerateReportLine()
        {
            return $"Bond | {Name} | {Quantity} Units | Value-{CalculateCurrentValue().ToString("C")} ";
        }
    }
    class MutualFund : FinancialInstrument, IRiskAssessable, IReportable
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }
        public string GetRiskCategory()
        {
            return $"Medium";
        }

        public string GenerateReportLine()
        {
            return $"MutualFund | {Name} | {Quantity} Units | Value-{CalculateCurrentValue().ToString("C")} ";
        }
    }
    class FixedDeposit : FinancialInstrument, IRiskAssessable
    {
        public override decimal CalculateCurrentValue()
        {
            return Quantity * MarketPrice;
        }
        public string GetRiskCategory()
        {
            return $"Low";
        }
    }

    class Portfolio
    {
        private List<FinancialInstrument> instruments = new List<FinancialInstrument>();
        private Dictionary<string, FinancialInstrument> instrumentMapper = new Dictionary<string, FinancialInstrument>();
        
        public void AddInstrument(FinancialInstrument instrument)
        {
            string id = instrument.InstrumentId.ToString();
            if (instruments.Count > 0 && instruments[0].Currency != instrument.Currency)
            {
                throw new InvalidFinancialDataException("Currency Mismatch: Portfolio already contains " + instruments[0].Currency);
            }

            if (instrumentMapper.ContainsKey(id))
            {
                throw new InvalidFinancialDataException($"Duplicate instrument ID-{id}");
            }
            instruments.Add(instrument);
            instrumentMapper[id] = instrument;
        }

        public void RemoveInstrument(int id)
        {
            string idString = id.ToString();
            if (instrumentMapper.ContainsKey(idString))
            {
                FinancialInstrument i = instrumentMapper[idString];
                instruments.Remove(i);
                instrumentMapper.Remove(idString);
            }
        }
        public decimal GetTotalPortfolioValue()
        {
            var result = instruments.Sum(i => i.CalculateCurrentValue());
            return result;
        }

        public FinancialInstrument GetInstrumentById(int id)
        {
            var res = instrumentMapper.GetValueOrDefault(id.ToString());
            return res;
        }

        public IEnumerable<FinancialInstrument> GetInstrumentsByRisk(string risk)
        {
            var r = instruments.Where(i => i is IRiskAssessable ra && ra.GetRiskCategory() == risk);
            return r;
        }

        public List<FinancialInstrument> GetAll()
        {
            return instruments;
        }
    }

    public enum TransactionType { Buy, Sell }
    class Transaction
    {
        public int TransactionId { get; set; }
        public int InstrumentId { get; set; }

        public TransactionType Type { get; set; }

        public int Units { get; set; }

        public DateOnly Date { get; set; }

        public void Execute(Portfolio portfolio)
        {
            var instrument = portfolio.GetInstrumentById(InstrumentId);
            if (instrument == null) throw new InvalidFinancialDataException("Instrument ID " + InstrumentId + " not found.");
            if (Type == TransactionType.Buy)
                instrument.Quantity += Units;
            else if(Type == TransactionType.Sell)
            {
                if (instrument.Quantity < Units)
                    throw new InvalidFinancialDataException("Insufficient units for sell. Owned: " + instrument.Quantity);
                instrument.Quantity -= Units;
            }
        }
    }


    class ReportGenerator
    {

        public static FinancialInstrument ParseCsv(string csv)
        {
            string[] p = csv.Split(',');
            if (p.Length < 7) 
                throw new InvalidFinancialDataException("Invalid CSV Format");
            FinancialInstrument inst;
            if (p[1] == "Equity")
            {
                inst = new Equity();
            }
            else if (p[1] == "Bond")
            {
                inst = new Bond();
            }
            else if (p[1] == "MutualFund")
            {
                inst = new MutualFund();
            }
            else
            {
                inst = new FixedDeposit();
            }

            string idPart = new string(p[0].Where(char.IsDigit).ToArray());
            inst.InstrumentId = int.Parse(idPart);
            inst.Name = p[2];
            inst.Currency = p[3];
            inst.Quantity = int.Parse(p[4]);
            inst.PurchasePrice = decimal.Parse(p[5]);
            inst.MarketPrice = decimal.Parse(p[6]);
            return inst;
        }
        public void GenerateConsoleReport(Portfolio p)
        {
            Console.WriteLine("\n===== PORTFOLIO SUMMARY =====");

            var summary = p.GetAll().GroupBy(i => i.GetType().Name).Select(group => new
                {
                    Type = group.Key,
                    TotalInvestment = group.Sum(i => i.Quantity * i.PurchasePrice),
                    CurrentValue = group.Sum(i => i.CalculateCurrentValue())
                }).OrderByDescending(x => x.CurrentValue);
            foreach (var info in summary)
            {
                Console.WriteLine("\nInstrument Type: " + info.Type);
                Console.WriteLine("Total Investment: " + info.TotalInvestment.ToString("C"));
                Console.WriteLine("Current Value: " + info.CurrentValue.ToString("C"));
                Console.WriteLine("Profit/Loss: " + (info.CurrentValue - info.TotalInvestment).ToString("C"));
            }
            Console.WriteLine("\nOverall Portfolio Value: " + p.GetTotalPortfolioValue().ToString("C"));

            Console.WriteLine("\nRisk Distribution:");
            var riskSummary = p.GetAll().Where(i => i is IRiskAssessable).GroupBy(i => ((IRiskAssessable)i).GetRiskCategory()).Select(g => new 
            { 
                Risk = g.Key,
                Count = g.Count()
            });
            foreach (var rs in riskSummary)
            {
                Console.WriteLine($"{rs.Risk}: {rs.Count}");
            }
        }
        public void GenerateFileReport(Portfolio p)
        {
            string fileName = $"PortfolioReport_{DateTime.Now:yyyyMMdd}.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("===== PORTFOLIO REPORT =====");
                    sw.WriteLine($"Generated: {DateTime.Now}");
                    sw.WriteLine("\nInstrument Details:");
                    foreach (var i in p.GetAll())
                    {
                        sw.WriteLine(i is IReportable rep ? rep.GenerateReportLine() : i.GetInstrumentSummary());
                    }
                    sw.WriteLine("\nAggregated Summary:");
                    sw.WriteLine("Total Portfolio Value: " + p.GetTotalPortfolioValue().ToString("C"));
                }
                Console.WriteLine("\nFile report generated: " + fileName);
            }
            catch(Exception ex) 
            { 
                Console.WriteLine("File Error: " + ex.Message);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Portfolio portfolio = new Portfolio();
            ReportGenerator reporter = new ReportGenerator();

            try
            { 
                portfolio.AddInstrument(ReportGenerator.ParseCsv("EQ001,Equity,Zomato,INR,200,1500,1600"));
                portfolio.AddInstrument(ReportGenerator.ParseCsv("BD002,Bond,BJP,INR,150,1000,1500"));
                portfolio.AddInstrument(ReportGenerator.ParseCsv("MF003,MutualFund,HDFC,INR,100,500,550"));
                
                Transaction[] transactionArray = new Transaction[]
                {
                    new Transaction
                    { 
                        TransactionId = 1,
                        InstrumentId = 1,
                        Type = TransactionType.Buy,
                        Units = 10,
                        Date = DateOnly.FromDateTime(DateTime.Now) 
                    }
                };
                
                List<Transaction> transactionList = transactionArray.ToList();
                foreach (var t in transactionList)
                {
                    t.Execute(portfolio);
                }
                
                reporter.GenerateConsoleReport(portfolio);
                reporter.GenerateFileReport(portfolio);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error: {ex.Message}"); 
            }
        }
    }
}