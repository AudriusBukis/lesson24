using System;
using System.Linq;


namespace Lesson24.Methods
{
    public class ATM
    {

        public void MainWindow(Client client)
        {
            var clientRep = new ClientRepository();
            var clientList = clientRep.ReadAllClientsFromFile();
            var clientInATM = new Client();
            clientInATM = clientList.Single(x => x.CardID == client.CardID);
            while (true)
            {

                Console.Clear();
                Console.WriteLine("Welcom to cash masine ATM");
                Console.WriteLine("Choose what you want to do");
                Console.WriteLine("     1 see cash left in card");
                Console.WriteLine("     2 see last 5 transactions");
                Console.WriteLine("     3 Cashout");
                Console.WriteLine("     4 Exit");
                if (Int32.TryParse(Console.ReadLine(), out int selection))
                {
                    bool exitATM = false;
                    if (selection > 0 && selection < 5)
                    {
                        switch (selection)
                        {
                            case 1:
                                SeeCashInATM(clientInATM);
                                break;
                            case 2:
                                SeeLast5Transactions(clientInATM);
                                break;
                            case 3:
                                if (CachLimitation(clientInATM))
                                {
                                    TakeCash(clientInATM);
                                    clientRep.WriteAllClientsToFile(clientList);
                                }
                                break;
                            case 4:
                                exitATM = true;
                                break;

                        }
                        if (exitATM) break;
                    }
                    else
                    {
                        Console.WriteLine("No such comand");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("you entered not the number");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }



            }

        }
        public void SeeCashInATM(Client client)
        {
            Console.WriteLine();
            Console.WriteLine($"{client.Name} {client.LastName}");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("     Cash amount in your card is");
            Console.WriteLine($"        {client.CardMoneyAmount} Euro");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public void SeeLast5Transactions(Client client)
        {
            var operations = new OperationDBRepository();
            var allOperationList = operations.GetClientHistoryOperations(client.CardID).ToList();
            if (allOperationList.Count <= 5)
            {
                foreach (var operation in allOperationList)
                { Console.WriteLine($"No.{allOperationList.IndexOf(operation) + 1} {operation}"); }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                var operationComparer = new OperationComparer();
                allOperationList.Sort(operationComparer);
                for (int i = 0; i < 5; i++)
                {
                    { Console.WriteLine($"No.{allOperationList.IndexOf(allOperationList[i]) + 1} {allOperationList[i]}"); }
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
        public void TakeCash(Client client)
        {
            Console.WriteLine("Enter the amount of money you want to withdraw ");
            if (Double.TryParse(Console.ReadLine(), out double money))
            {
                var operations = new OperationDBRepository();
                var allOperationList = operations.GetClientHistoryOperations(client.CardID).ToList();
                var TodayOperationList = allOperationList.Where(x => x.OperationDate.Day.Equals(DateTime.Today.Day)).ToList();
                var sumOfCashTaked = 0D;
                foreach (var operation in TodayOperationList)
                {
                    sumOfCashTaked += operation.CashOutSum;
                }
                if (sumOfCashTaked + money > 1000)
                {
                    Console.WriteLine("You today limit 1000 eu will be exceeded ");
                    Console.WriteLine($"You can today take {1000-sumOfCashTaked} Eu");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    var toFile = new FileService("OperationDB.txt");
                    client.CardMoneyAmount = client.CardMoneyAmount - money;
                    toFile.AppendText($"{client.CardID};Withdraw money;{money}; date ;{DateTime.Now}; : money in the acount is ;{client.CardMoneyAmount}; Euro");
                }
                

            }
            else
            {
                Console.WriteLine("you entered not the number");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
        public bool CachLimitation(Client client)
        {
            var operations = new OperationDBRepository();
            var allOperationList = operations.GetClientHistoryOperations(client.CardID).ToList();
            var TodayOperationList = allOperationList.Where(x => x.OperationDate.Day.Equals(DateTime.Today.Day)).ToList();
            var sumOfCashTaked = 0D;
            foreach (var operation in TodayOperationList)
            {
                sumOfCashTaked += operation.CashOutSum;
            }

            if (TodayOperationList.Count() > 10)
            {
                Console.WriteLine("You reach your today limit 10 operations");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return false;
            }
            else if (1000 <= sumOfCashTaked)
            {
                Console.WriteLine("You reach your today limit 1000 eu");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return false;

            }
            return true;
        }
    }
}
