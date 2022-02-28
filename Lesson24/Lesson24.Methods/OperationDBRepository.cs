using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson24.Methods
{
    public class OperationDBRepository 
    {
        public List<Operation> OperationList { get; internal set;}

        public OperationDBRepository()
        {
            OperationList = ReadOperationDB();
        }
        internal static List<Operation> ReadOperationDB()
        {
            try
            {
                var operationList = new List<Operation>();
                var ReadClients = new FileService("OperationDB.txt");
                var stringList = ReadClients.GetAllLines();
                for (int i = 0; i < stringList.Count; i++)
                {
                    var operation = new Operation();
                    string[] temp = stringList[i].Split(";");
                    operation.CardID = ExceptionsHandle.FromStringToGuid(temp[0]);
                    operation.OperationName = temp[1];
                    operation.CashOutSum = ExceptionsHandle.FromStringToDouble(temp[2]);
                    operation.OperationDate = ExceptionsHandle.FromStringToDateTime(temp[4]);
                    operation.MoneyAmountInCard = ExceptionsHandle.FromStringToDouble(temp[6]);
                    operationList.Add(operation);
                }

                return operationList;
            }
            catch (IndexOutOfRangeException ex)
            {
                ExceptionsHandle.Logging(ex.Message, ex.StackTrace);
                var operationList = new List<Operation>();
                var operation = new Operation();
                operation.CardID = default;
                operation.OperationName = "";
                operation.CashOutSum = default;
                operation.OperationDate = default;
                operation.MoneyAmountInCard = default;
                operationList.Add(operation);
                return operationList;          
            }
            
        }
        public List<Operation> GetClientHistoryOperations(Guid clientCardID)
        {
            List<Operation> clientOperationlist = OperationList.Where(operation => operation.CardID == clientCardID).ToList();
            return clientOperationlist;
        }
    }
}
