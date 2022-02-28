using System;
using System.Collections.Generic;

namespace Lesson24.Methods
{
    public class Operation
    {
        public Guid CardID { get; internal set; }
        public DateTime OperationDate{ get; set; }
        public string OperationName{ get; internal set; }
        public double CashOutSum { get; internal set; }
        public double MoneyAmountInCard { get; set; }

        public Operation(){ }


        public override string ToString()
        {
            return $"Date:{OperationDate} - {OperationName}: - {CashOutSum} Left money in Card {MoneyAmountInCard} ";
        }
  
    }
    public class OperationComparer : IComparer<Operation>
    {
        int IComparer<Operation>.Compare(Operation x, Operation y)
        {
            #region IComparer<Operation.OperationDate?> Members
            if (x.OperationDate > y.OperationDate) return -1;
            else if (x.OperationDate < y.OperationDate) return 1;
            else return 0;
            #endregion
        }


    }
}
