using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employees> employees = new List<Employees>();
            
         
            Employees employees1 = new Employees();
         

            employees.Add(new Employees { Id = 1, FirstName = "Alice", LastName = "Johnson", Gender = 'F', IsManager = false, AnnualSalary = 55000 });
            employees.Add(new Employees { Id = 2, FirstName = "Bob", LastName = "Smith", Gender = 'M', IsManager = true, AnnualSalary = 75000 });
            employees.Add(new Employees { Id = 3, FirstName = "Charlie", LastName = "Brown", Gender = 'M', IsManager = false, AnnualSalary = 60000 });
               foreach (var employee in employees)
          {
         employee.LuckyEmployee += BonusAdded;
        }

            Bank bank = new Bank(0);
            string heading = "-Banking System-";
            System.Console.WriteLine(heading);
            System.Console.WriteLine(new string('-', heading.Length));

            while (true)
            {
                System.Console.WriteLine(" Main Menu : ");
                System.Console.WriteLine("Enter 1 to add a deposit to the balance : \nEnter 2 to withdraw money from the balance : \nEnter 3 to show balance : \nEnter 4 to add interest to balance\nEnter 5 to add compound interest :\nEnter 6 to show all employees : \nEnter 7 to filter the employees :\nEnter 8 to activate suprise bonus Day! :\nEnter 9 To exit :    ");
                System.Console.WriteLine();
                int respond = Convert.ToInt32(Console.ReadLine());
                System.Console.WriteLine();

                switch (respond)
                {
                    case 1:
                        System.Console.WriteLine("Enter the amount you want to deposit from your account: ");
                        double deposit = Convert.ToDouble(Console.ReadLine());
                        bank.Deposit(deposit);
                        break;
                    case 2:
                        System.Console.WriteLine("Enter the amount you want to withdraw from your account: ");
                        double withdraw = Convert.ToDouble(Console.ReadLine());
                        bank.Withdraw(withdraw);
                        break;
                    case 3:
                        bank.ShowBalance();
                        break;
                    case 4:
                        Console.Write("Enter principal amount (balance): ");
                        double principal = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter rate (%): ");
                        double rate = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter time (years): ");
                        double time = Convert.ToDouble(Console.ReadLine());
                        bank.CalculateSimpleInterest(principal, rate, time);
                        break;
                    case 5:
                        System.Console.WriteLine("Enter principal amount (balance): ");
                        double p = Convert.ToDouble(Console.ReadLine());
                        System.Console.WriteLine("Enter rate (%) : ");
                        double r = Convert.ToDouble(Console.ReadLine());
                        System.Console.WriteLine("Enter time (years):");
                        double t = Convert.ToDouble(Console.ReadLine());
                        System.Console.WriteLine("Enter times compounded per year: ");
                        int n = Convert.ToInt32(Console.ReadLine());
                        bank.AddCompoundInterest(p, r, t, n);
                        break;
                    case 6:
                        foreach (var employee in employees)
                        {
                            System.Console.WriteLine($"Id({employee.Id})\nFirstname({employee.FirstName})\nLastname({employee.LastName})\nGender({employee.Gender})\nIsmanager({employee.IsManager})\nAnnualsalary({employee.AnnualSalary})\n");
                        }
                        break;
                    case 7:
                        Console.WriteLine("\nFilter Employees:");
                        Console.WriteLine("1 - Show only managers");
                        Console.WriteLine("2 - Show only males");
                        Console.WriteLine("3 - Show only females");
                        Console.WriteLine("4 - Show employees with salary above a value");
                        Console.WriteLine("5 - Show employees with salary below a value");
                        int filterOption = Convert.ToInt32(Console.ReadLine());
                        double salaryThreshold = 0;
                        if (filterOption == 4 || filterOption == 5)
                        {
                            salaryThreshold = GetSalaryInput();
                        }
                        List<Employees> filtered = filterOption switch
                        {
                            1 => Employees.FilterEmployees(employees, e => e.IsManager),
                            2 => Employees.FilterEmployees(employees, e => e.Gender == 'M'),
                            3 => Employees.FilterEmployees(employees, e => e.Gender == 'F'),
                            4 => Employees.FilterEmployees(employees, e => e.AnnualSalary > salaryThreshold),
                            5 => Employees.FilterEmployees(employees, e => e.AnnualSalary < salaryThreshold),
                            _ => new List<Employees>()
                        };
                        if (filtered.Count > 0)
                        {
                            foreach (Employees employee in filtered)
                            {
                                System.Console.WriteLine($"Id({employee.Id})\nFirstname({employee.FirstName})\nLastname({employee.LastName})\nGender({employee.Gender})\nIsmanager({employee.Gender})\nAnnualsalary({employee.AnnualSalary})\n");
                            }
                        }
                        break;
                    case 8:
                        AddRandomBonusToRandomEmployee(employees);
                        break;
                    case 9:
                        System.Console.WriteLine("Exiting.....");
                        return;
                    break;
                }
            }
        }

        static double GetSalaryInput()
        {
            Console.Write("Enter salary amount: ");
            return Convert.ToDouble(Console.ReadLine());
        }

  
        static void BonusAdded(object sender, EventArgs e)
        {
            Employees employee1 = sender as Employees;
            if (employee1 != null)
            {
                System.Console.WriteLine($"Bonus added to {employee1.FirstName} {employee1.LastName}!");
                System.Console.WriteLine($"New Salary: {employee1.AnnualSalary}$");
            }
        }

        
        static void AddRandomBonusToRandomEmployee(List<Employees> employees)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, employees.Count);
            Employees randomEmployee = employees[randomIndex];
            randomEmployee.AddRandomBonus();
        }
    }

    class Employees
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public bool IsManager { get; set; }
        public double AnnualSalary { get; set; }

        public static List<Employees> FilterEmployees(List<Employees> employees, Predicate<Employees> predicate)
        {
            List<Employees> filteredEmployees = new List<Employees>();
            foreach (Employees employee in employees)
            {
                if (predicate(employee))
                {
                    filteredEmployees.Add(employee);
                }
            }
            return filteredEmployees;
        }

      
        public void AddRandomBonus()
        {
            int bonusPercentage = new Random().Next(6, 16);
            double bonusAmount = AnnualSalary * (bonusPercentage / 100.0);
            AnnualSalary += bonusAmount; 
            OnLuckyEmployee();
        }

        protected void OnLuckyEmployee()
        {
            EventHandler handler = LuckyEmployee;
            handler?.Invoke(this, EventArgs.Empty); 
        }


        public event EventHandler LuckyEmployee;
    }

 
    public class Bank
    {
        public double Balance;

        public Bank(double balance)
        {
            Balance = balance;
        }

        public void Deposit(double total)
        {
            if (total > 0)
            {
                Balance += total;
                System.Console.WriteLine($"Deposited: {total}$ Current balance after deposit: {Balance}$");
            }
            else
            {
                System.Console.WriteLine("Not a valid deposit amount.");
            }
        }

        public void Withdraw(double amount)
        {
            if (amount >= 0 && amount < Balance)
            {
                Balance -= amount;
                System.Console.WriteLine($"Withdrawn: {amount}$ Current balance after withdrawal: {Balance}$");
            }
            else
            {
                System.Console.WriteLine("Invalid withdrawal amount. Not enough funds.");
            }
        }

        public double ShowBalance()
        {
            System.Console.WriteLine($"Balance: {Balance}$");
            return Balance;
        }

        public void CalculateSimpleInterest(double principal, double rate, double time)
        {
            if (principal > 0)
            {
                double interest = principal * (rate / 100) * time;
                Balance += interest;
                System.Console.WriteLine($"Added interest: {interest}$ Balance after result: {Balance}$");
            }
            else
            {
                System.Console.WriteLine("Invalid principal amount.");
            }
        }

        public void AddCompoundInterest(double principal, double rate, double time, int n)
        {
            double amount = principal * Math.Pow(1 + (rate / 100) / n, n * time);
            double interest = amount - principal;
            Balance += interest;
            System.Console.WriteLine($"Added compound interest: {interest}$ New balance: {Balance}$");
        }
    }
}
