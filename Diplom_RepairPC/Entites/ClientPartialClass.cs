using System;
using System.IO;
using System.Windows.Media;

namespace Diplom_RepairPC.Entites
{
    public partial class Diplom_Client
    {
        public string FIO
        {
            get
            {
                return $"{Surname} {Name} {SecondName}";
            }
        }

        public ImageSource Image
        {
            get
            {
                try
                {
                    byte[] image = File.ReadAllBytes(@"Image\ImageClient.png");
                    return (ImageSource)new ImageSourceConverter().ConvertFrom(image);
                }
                catch
                {
                    return null;
                }
            }
        }

        public string Card
        {
            get
            {
                if (DiscountCard == true) return "Есть";
                else return "Нет";
            }
        }
    }
}