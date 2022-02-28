using System;
using System.Collections.Generic;

namespace Lesson24.Methods
{
    public class LoginRepository
    {
        internal FileService ReadGiudID = new("CardIDList.txt");
        public Client SelectClient(int index)
        {
            var clientList = new ClientRepository().ReadAllClientsFromFile();
            var client = clientList[index];
            return client;
        }
        public bool CheckCardIDWithDataBase(Client client)
        {
            return ReadCardIDFromDataBase().Contains(client.CardID);
        }
        internal  List<Guid> ReadCardIDFromDataBase()
        {
            var guidIDList = new List<Guid>();
            var stringList = ReadGiudID.GetAllLines();
            foreach (var str in stringList)
            {
                guidIDList.Add(ExceptionsHandle.FromStringToGuid(str));
            }
            return guidIDList;
        }
    }
}
