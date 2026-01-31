
internal class Program
{
    public class Patient
    {
        public string Name { get; set; }
        public decimal BaseFee { get; set; }

        virtual public decimal CalculateFinalBill()
        {
            return BaseFee;
        }
    }
    public class Inpatient :Patient
    {
        public int DaysStayed { get; set; }
        public decimal DailyRate { get; set; }
        public override decimal CalculateFinalBill()
        {
            return BaseFee+(DaysStayed*DailyRate);
        }

    }
    public class Outpatient:Patient
    {
        public decimal ProcedureFee { get; set; }
        public override decimal CalculateFinalBill()
        {
            return BaseFee + ProcedureFee;
        }
    }
    public class Emergencypatient:Patient
    {
        public int SeverityLevel { get; set; }
        public override decimal CalculateFinalBill()
        {
            return BaseFee*SeverityLevel;
        }
    }
    public class HospitalBilling
    {
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(Patient p)
        {
            patients.Add(p);
        }

        public void GenerateDailyReport()
        {
            Console.WriteLine("Daily Report:");
            foreach (Patient p in patients)
            {
                Console.WriteLine($"{p.Name}: {p.CalculateFinalBill():C2}");
            }
        }
        public decimal CalculateTotalRevenue()
        {
            decimal total = 0;
            foreach (Patient p in patients)
            {
                total += p.CalculateFinalBill();
            }
            return total;
        }

        public int GetInpatientCount()
        {
            int count = 0;
            foreach (Patient p in patients)
            {
                if (p is Inpatient)
                {
                    count++;
                }
            }
            return count;
        }
    }
    private static void Main(string[] args)
    {
        HospitalBilling billing = new HospitalBilling();
        billing.AddPatient(
        new Inpatient
        {
            Name = "Himanshu",
            BaseFee = 500,
            DaysStayed = 3,
            DailyRate = 200
        });
        billing.AddPatient(
        new Outpatient
        {
            Name = "Sudhish",
            BaseFee = 150,
            ProcedureFee = 300
        });
        billing.AddPatient(
        new Emergencypatient()
        {
            Name = "Parth",
            BaseFee = 250,
            SeverityLevel = 4
        });
        billing.GenerateDailyReport();
        Console.WriteLine($"Total Revenue: {billing.CalculateTotalRevenue():C2}");
        Console.WriteLine($"Inpatient Count: {billing.GetInpatientCount()}");
    }
}