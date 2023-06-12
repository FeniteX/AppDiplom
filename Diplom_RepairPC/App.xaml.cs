using System;
using System.Windows;

namespace Diplom_RepairPC
{
    public partial class App : Application
    {
        public static Entites.DataBaseEntities Context { get; set; } = 
            new Entites.DataBaseEntities();

        public static Entites.Diplom_Account CurrentUser = null;
    }
}