using Lesson24.Methods;
using System;
using System.Collections.Generic;

namespace Lesson24
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Lesson 24 HomeWork cash machine");
            while(true)
            {

                var clientRep = new ClientRepository();
                Console.Clear();
                Console.WriteLine("----Velcome To Cash Machine----");
                Console.WriteLine("slect user from the list by entering number");
                var clientList = clientRep.ReadAllClientsFromFile();
                Console.WriteLine("No.0 - create new user");
                if (clientList != null)
                {
                    foreach (var client in clientList)
                    {
                        Console.WriteLine($"No.{clientList.IndexOf(client) + 1} - {client.Name} {client.LastName}");
                    }
                }
                Console.WriteLine("------------- enter the number -------------------");
                if ( Int32.TryParse(Console.ReadLine(), out var action))
                {
                   if (action == 0)
                   {
                        clientRep.AddNewClient();
                        continue;
                   }
                   else if ((action > 0) && (action <= clientRep.ReadAllClientsFromFile().Count))
                   {
                        var loginRep = new LoginRepository();
                        var client = loginRep.SelectClient(action-1);
                        if (loginRep.CheckCardIDWithDataBase(client))
                        {
                            
                            Console.WriteLine("you card is valid enter the PIN number");
                            short attempts = 1;
                            bool exit = false;
                            while (true)
                            {

                                if (client.PinNumber.Equals(Console.ReadLine()))
                                {
                                    var atm = new ATM();
                                    atm.MainWindow(client);
                                    exit = true;
                                    break;

                                }
                                if (attempts < 3)
                                {
                                    Console.WriteLine("The PIN is incorect, you have " + (3 - attempts) + " attemps more");
                                    attempts++;
                                    continue;

                                }
                                if (attempts == 3)
                                {
                                    Console.WriteLine("You entered 3 times incorect pin " +
                                        "\nPlease try again later" +
                                        "\nPress any key to continue");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            if(!exit)
                            {
                                Console.WriteLine("Take your card and try next time");
                                Console.WriteLine("Press any key to continue");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Take your card ");
                                Console.WriteLine("Press any key to continue");
                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("you card is not valid press any key to continue");
                            Console.ReadKey();
                            break;
                        }

                   }
                   else
                   {
                        Console.WriteLine("you entered to big number press any key to continue");
                        Console.ReadKey();
                        continue;
                   }
             
                  
                }
                else
                {
                    Console.WriteLine("not an int mumber press any key to continue");
                    Console.ReadKey();
                    continue;
                }
                
               
            }
            
                
            
            //var clientList = clientRep.ReadAllClientsFromFile();
            //var cardIDs = clientRep.ReadCardIDFromDataBase();
            //clientRep.AddNewClient();
            //Console.WriteLine();
            //Console.ReadKey();
            /*Programa turi “patikrinti” ar įdėta kortelė yra tinkama (susimuliuoti tarkim Guid’ų sulyginimą)
              Saugoti faile vartotojų bankinės kortelės ir slaptažodžių informaciją ir pinigų sumą.
              Paprašyti vartotojo įvesti slaptažodį.
              Įvedus blogai tris kartus išjungti programą.
              Įvedus taisyklingai prisijungti ir parodyti turimus pinigus.
              Sėkmingai prisijungus turi būti papildomi pasirinkimai:
                  Matyti turimus pinigus
                  5 praeitos transakcijos
                  Pinigų išsiėmimas
              Maksimumas pinigų išsiėmimui turi būti 1000e ir maksimalus transakcijų skaičius per dieną - 10.

              Naudoti gali bet ką, improvizuokit:)*/


        }
       
    }
}
