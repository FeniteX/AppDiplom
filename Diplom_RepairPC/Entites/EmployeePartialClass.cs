using System;
using System.IO;
using System.Windows.Media;

namespace Diplom_RepairPC.Entites
{
    public partial class Diplom_Employee
    {
        public string FIO
        {
            get
            {
                return $"{Surname} {Name} {SecondName}";
            }
        }

        public string Cost
        {
            get
            {
                return Convert.ToInt32(Convert.ToDouble(CostWork) * 60) + " руб./час";
            }
        }

        public ImageSource Image
        {
            get
            {
                try
                {
                    byte[] image = File.ReadAllBytes(@"Image\ImageEmployee.png");
                    return (ImageSource)new ImageSourceConverter().ConvertFrom(image);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}