using System;

namespace Diplom_RepairPC.Entites
{
    public partial class Diplom_Order
    {
        public string DateEnd
        {
            get
            {
                if (DateOrderEnd == null) return "Заказ не завершён";
                else return Convert.ToDateTime(DateOrderEnd).ToShortDateString();
            }
        }

        public string DateStart
        {
            get
            {
                return Convert.ToDateTime(DateOrderStart).ToShortDateString();
            }
        }

        public string CostOrder
        {
            get
            {
                return Convert.ToInt32(Cost) + " руб.";
            }
        }
    }
}