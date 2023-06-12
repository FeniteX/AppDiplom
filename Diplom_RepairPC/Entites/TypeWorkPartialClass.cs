using System;

namespace Diplom_RepairPC.Entites
{
    public partial class Diplom_TypeWork
    {
        public string CostWork
        {
            get
            {
                return Convert.ToDouble(Cost) + " руб.";
            }
        }

        public string WorkTime
        {
            get
            {
                return TimeWork + " мин.";
            }
        }
    }
}