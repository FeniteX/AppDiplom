using System;

namespace Diplom_RepairPC.Entites
{
    public partial class Diplom_TypeComponent
    {
        public string Name2
        {
            get
            {
                if (ID == -1) return null;
                else return Name;
            }
        }

        public string Names
        {
            get
            {
                if (ID == -1) return null;
                else return Name;
            }
        }

        public int IDs
        {
            get
            {
                if (ID == -1) return 0;
                else return ID;
            }
        }
    }
}