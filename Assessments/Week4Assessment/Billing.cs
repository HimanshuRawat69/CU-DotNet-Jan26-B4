namespace MemorialBilling
{
    class Patient
    {
        public string Name { get; set; }
        public decimal BaseFee { get; set; }

        public Patient()
        {
            this.Name = string.Empty;
            this.BaseFee = 0;
        }
        public Patient(string name, decimal baseFee)
        {
            this.Name = name;
            this.BaseFee = baseFee;

        }
        public virtual decimal CalculateFinalBill()
        {
            return BaseFee;
        }
    }

    class InPatient : Patient
    {
        public int DayStayed { get; set; }
        public decimal DailyRate { get; set; }

        public InPatient()
        {
            this.DayStayed = 0;
            this.DailyRate = 0;
        }

        public InPatient(string name, decimal baseFee, int days, decimal rate) : base(name, baseFee)
        {
            this.DayStayed = days;
            this.DailyRate = rate;
        }

        public override decimal CalculateFinalBill()
        {
            return base.CalculateFinalBill() + (DayStayed * DailyRate);
        }

    }

    class OutPatient : Patient
    {
        public decimal ProcedureFee { get; set; }

        public OutPatient()
        {
            this.ProcedureFee = 0;
        }

        public OutPatient(string name, decimal baseFee, decimal fee) : base(name, baseFee)
        {
            this.ProcedureFee = fee;
        }

        public override decimal CalculateFinalBill()
        {
            return base.CalculateFinalBill() + ProcedureFee;
        }
    }

    class EmergencyPatient : Patient
    {
        public int SeverityLevel { get; set; }

        public EmergencyPatient()
        {
            this.SeverityLevel = 0;
        }
        public EmergencyPatient(string name, decimal baseFee, int lvl) : base(name, baseFee)
        {
            this.SeverityLevel = lvl;
        }

        public override decimal CalculateFinalBill()
        {
            return base.CalculateFinalBill() * SeverityLevel;
        }
    }

    class HospitalBilling
    {
        List<Patient> patients = new List<Patient>();
        public void AddPatient(Patient p)
        {
            patients.Add(p);
        }
        public void GenerateDailyReport()
        {
            foreach (Patient p in patients)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine($"Name - {p.Name}, Bill - {p.CalculateFinalBill().ToString("C2")}");
            }
        }

        public void CalculateTotalRevenue()
        {
            decimal total = 0.00m;
            foreach (Patient p in patients)
            {
                total += p.CalculateFinalBill();
            }
            Console.WriteLine($"Total Revenue {total:C2}");
        }

        public void GetInPatientCount()
        {
            int cnt = 0;
            foreach (Patient p in patients)
            {
                if (p is InPatient)
                {
                    cnt++;
                }
            }
            Console.WriteLine($"Total InPatient : {cnt}");
        }
    }
    internal class Billing
    {
        static void Main(string[] args)
        {
            HospitalBilling hospital = new HospitalBilling();

            hospital.AddPatient(new EmergencyPatient("Sneha", 6500, 3));
            hospital.AddPatient(new EmergencyPatient("Harpuneet", 5000, 5));
            hospital.AddPatient(new OutPatient("Sonam", 5000, 3000));
            hospital.AddPatient(new OutPatient("Riya", 2000, 3000));
            hospital.AddPatient(new InPatient("Sejal", 7000, 7, 750.50m));
            hospital.AddPatient(new InPatient("Nidhi", 2500, 14, 750.00m));

            hospital.GenerateDailyReport();
            hospital.CalculateTotalRevenue();
            hospital.GetInPatientCount();

        }
    }
}
