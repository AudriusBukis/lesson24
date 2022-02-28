using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;


namespace Lesson24.Methods
{
    public class ClientRepository
    {
        public List<Client> ReadAllClientsFromFile()
        {
            try
            {
                var clientList = new List<Client>();
                var ReadClients = new FileService("UserInfo.txt");
                var stringList = ReadClients.GetAllLines();
                for (int i = 0; i < stringList.Count; i++)
                {
                    var client = new Client();
                    string[] temp = stringList[i].Split(";");
                    client.Name = temp[0];
                    client.LastName = temp[1];
                    client.PinNumber = temp[2];
                    client.CardID = ExceptionsHandle.FromStringToGuid(temp[3]);
                    client.CardMoneyAmount = ExceptionsHandle.FromStringToDouble(temp[4]);
                    clientList.Add(client);
                }

                return clientList;
            }
            catch (IndexOutOfRangeException ex)
            {
                ExceptionsHandle.Logging(ex.Message, ex.StackTrace);
                var client = new Client();
                client.Name = "";
                client.LastName = "";
                client.PinNumber = "";
                client.CardID = default;
                client.CardMoneyAmount = default;
                var clientList = new List<Client>();
                clientList.Add(client);
                return clientList;
            }
        }
        public void WriteAllClientsToFile(List<Client> clientList)
        {
            var ToFile = new FileService($"UserInfo.txt");
            var temp = new string[clientList.Count];
            foreach (var client in clientList)
            {
                temp[clientList.IndexOf(client)] = $"{client}";
            }
            ToFile.WriteAllText(temp);
        }


        public void AddNewClient()
        {
            var existingClientList = ReadAllClientsFromFile();
            while(true)
            {
                Console.Clear();
                Console.WriteLine("-------------New client creation------------");
                Console.WriteLine("Enter new client name");
                var name = Console.ReadLine();
                Console.WriteLine("Enter new client last name");
                var lastName = Console.ReadLine();
                if (existingClientList.Exists(client => client.Name.ToLower() == name.ToLower()
                                              && client.LastName.ToLower() == lastName.ToLower()))
                {
                    Console.WriteLine("The Name and last name of new client exists in data base ");
                    Console.WriteLine("Press any key to enter other name and last name");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine("Enter yours new cards four digits PIN number");
                        if (Int32.TryParse(Console.ReadLine(), out int pinNumber))
                        {
                            if (pinNumber == 0)
                            {
                                var fakeClient = new Client(name, lastName);
                                Console.WriteLine("New fake client created press any key to continue");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                var newClient = new Client(name, lastName, pinNumber.ToString("D4"));
                                Console.WriteLine("New client created press any key to continue");
                                Console.ReadKey();
                                break;
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Your pin number contains not only digits try again");
                        }
                    }
                    break;
                    
                }

            }
            
        }
        

    }
}
