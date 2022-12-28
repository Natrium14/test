using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Model.Entities;
using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            using(var db = new Entities())
            {
                var employees = db.Сотрудники.ToList();
                foreach(var emp in employees)
                {
                    Console.WriteLine(emp.Имя);
                }
            }
            */

            Citizen citizen = new Citizen("Alex", "Times", new DateTime(1991, 1, 1), "6576-989898");
            citizen.Address = "Tomsk, Lenina 30";

            Console.WriteLine(citizen.GetInfo());

            Person a = new Person("Bob");
            string json = JsonSerializer.Serialize(a);
            Console.WriteLine(json);
            Person restoredCitizen = JsonSerializer.Deserialize<Person>(json);
            //Console.WriteLine(restoredCitizen.GetInfo());

            Client clientOne = new Client("Alex", "Times", new DateTime(1991, 1, 1), "6576-989898", "Bomj");
            IAccount debitAccOne = new AccountDebit(1.5);
            clientOne.AddAccount(debitAccOne);

            foreach(IAccount acc in clientOne.GetAccounts())
            {
                Console.WriteLine(acc.GetInfo());
            }

            Console.Read();
        }
    }

    class Person
    {
        protected string _name;
        protected string _surname;
        protected DateTime _dateBirth;

        public Person()
        {
            _name = "-";
            _surname = "-";
        }
        public Person(string name)
        {
            _name = name;
            _surname = "-";
        }
        public Person(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }
        public Person(string name, string surname, DateTime dateBirth)
        {
            _name = name;
            _surname = surname;
            _dateBirth = dateBirth;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
            }
        }
        public int CurrentAge
        {
            get
            {
                return DateTime.Now.Year - _dateBirth.Year;
            }
        }

        public string GetFullName()
        {
            return _name + " " + _surname;
        }
        public virtual string GetInfo()
        {
            return _name + " " + _surname + " " + CurrentAge;
        }
    }

    class Citizen : Person
    {
        protected string _passport;
        protected string _address;

        public Citizen(string name, string surname, DateTime dateBirth, string passport)
            : base(name, surname, dateBirth)
        {
            _passport = passport;
        }
        public Citizen(string name, string surname, DateTime dateBirth, string passport, string address)
           : base(name, surname, dateBirth)
        {
            _passport = passport;
            Address = address;
        }

        public string Passport { get => _passport; }
        public string Address { get => _address; set => _address = value; }

        public override string GetInfo()
        {
            return " Name: " + _name
                + "\n Surname: " + _surname
                + "\n Age: " + CurrentAge
                + "\n Passport: " + Passport
                + "\n Address: " + Address
                + "\n-----";
        }
    }

    class Client : Citizen
    {
        protected List<IAccount> _accounts;

        public Client(string name, string surname, DateTime dateBirth, string passport, string address)
           : base(name, surname, dateBirth, passport, address)
        {
            _accounts = new List<IAccount>();  
        }

        public override string GetInfo()
        {
            return " Name: " + _name
                + "\n Surname: " + _surname
                + "\n Age: " + CurrentAge
                + "\n Passport: " + Passport
                + "\n Address: " + Address
                + "\n-----";
        }
        public List<IAccount> GetAccounts() => _accounts;
        public void AddAccount(IAccount newAccount)
        {
            _accounts.Add(newAccount);
        }
        public void RemoveAccount(Guid id)
        {
            _accounts.RemoveAll(x => x.GetId == id);
        }

    }

    interface IAccount
    {
        double GetBalance();
        string GetInfo();
        void PutAsset(double sum);
        void TakeAsset(double sum);
        Guid GetId { get; }
    }

    abstract class Account: IAccount
    {
        protected Guid id;
        protected double _balance;

        public Account()
        {
            id = Guid.NewGuid();
            _balance = 0;
        }
        public Account(double balance)
        {
            id = Guid.NewGuid();
            this._balance = balance;
        }

        public Guid GetId => id;
        public double GetBalance() => _balance;
        public string GetInfo()
        {
            return " Number: " + id.ToString() + "\n Balance: " + _balance + "\n-----";
        }
        public void PutAsset(double sum)
        {
            if (sum >= 0)
                _balance += sum;
        }
        public virtual void TakeAsset(double sum)
        {
            if (sum >= 0)
                if (sum <= _balance)
                    _balance -= sum;
        }
    }

    class AccountDebit: Account
    {
        public AccountDebit(double balance): base(balance)
        {
            id = Guid.NewGuid();
            if (balance >= 0)
                this._balance = balance;
            else
                this._balance = 0;
        }
    }

    class AccountCredit : Account
    {
        public AccountCredit(double balance) : base(balance)
        {
            id = Guid.NewGuid();
            this._balance = balance;
        }
        public override void TakeAsset(double sum)
        {
            if (sum >= 0)
                _balance -= sum;
        }
    }
}
