using System;

namespace Lesson24.Methods
{
    public class Client
    {
        internal FileService ToFile = new ($"UserInfo.txt");
        public string Name { get;  set; }
        public string LastName { get; set; }
        public string PinNumber {get; set; }
        public Guid CardID { get; set; }
        public double CardMoneyAmount { get;  set; }
        public Client() { }
        public Client(string name, string lastName, string pinNumber)
        {
            Name = name;
            LastName = lastName;
            PinNumber = pinNumber;
            CardID = GenerateCardID();
            CardMoneyAmount = MoneyGenerator();
            ToFile.AppendText(ToString());
            AccountGenerationInfo();
        }
        public Client(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
            PinNumber = "0000";
            CardID = Guid.NewGuid();
            CardMoneyAmount = MoneyGenerator();
            ToFile.AppendText(ToString());
           

        }
        protected double MoneyGenerator()
        {
            var randGenerator = new Random();
            return randGenerator.Next(5000, 9000);
        }
        internal Guid GenerateCardID()
        {
            var toFile = new FileService("CardIDList.txt");
            var newCardID = Guid.NewGuid();
            toFile.AppendText(newCardID.ToString());
            return newCardID;
        }
        internal void AccountGenerationInfo()
        {
            var toFile = new FileService("OperationDB.txt");
            toFile.AppendText($"{CardID};Account created;; date ;{DateTime.Now}; : money in the acount is ;{CardMoneyAmount}; Euro");
        }
        public override string ToString()
        {
            return $"{Name};{LastName};{PinNumber};{CardID};{CardMoneyAmount}";
        }
    }
}
