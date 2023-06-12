using System;

namespace Diplom_RepairPC.Entites
{
    public partial class Diplom_Component
    {
        public string CostItem
        {
            get
            {
                return Convert.ToDouble(Cost) + " руб.";
            }
        }
    }
}