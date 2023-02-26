using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentprogresstable.Models
{
    public class Student
    {
        public string FIO = string.Empty;
        public ushort Ofizra;
        public ushort Ooop;
        public ushort Otrpo;
        public ushort Ophysics;
        public double Average;
        public string AverageColor = string.Empty;
        public string fizraColor = string.Empty;
        public string OOPColor = string.Empty;
        public string trpoColor = string.Empty;
        public string PhysicsColor = string.Empty;
        public string fio
        {
            get => FIO;
            set => FIO = value;
        }
        public ushort PmarkFizra
        {
            get => Ofizra;
            set => Ofizra = value;
        }
        public ushort PmarkOOP
        {
            get => Ooop;
            set => Ooop = value;
        }
        public ushort PmarkTrpo
        {
            get => Otrpo;
            set => Otrpo = value;
        }
        public ushort PmarkPhysics
        {
            get => Ophysics;
            set => Ophysics = value;
        }
        public double GetAverage
        {
            get => Average;
            set => Average = (Ofizra + Ooop + Otrpo + Ophysics) / 4.0;
        }
        public string GetAverageColor
        {
            get => AverageColor;
            set
            {
                if (GetAverage < 1)
                {
                    AverageColor = "Red";
                }
                if (GetAverage <= 1.5 && GetAverage >= 1)
                {
                    AverageColor = "Yellow";
                }
                if (GetAverage > 1.5)
                {
                    AverageColor = "Green";
                }
            }
        }
        public string GetFizraColor
        {
            get => fizraColor;
            set
            {
                if (Ofizra == 0)
                {
                    fizraColor = "Red";
                }
                if (Ofizra == 1)
                {
                    fizraColor = "Yellow";
                }
                if (Ofizra == 2)
                {
                    fizraColor = "Green";
                }
            }
        }
        public string GetOOPColor
        {
            get => OOPColor;
            set
            {
                if (Ooop == 0)
                {
                    OOPColor = "Red";
                }
                if (Ooop == 1)
                {
                    OOPColor = "Yellow";
                }
                if (Ooop == 2)
                {
                    OOPColor = "Green";
                }
            }
        }
        public string GetTrpoColor
        {
            get => trpoColor;
            set
            {
                if (Otrpo == 0)
                {
                    trpoColor = "Red";
                }
                if (Otrpo == 1)
                {
                    trpoColor = "Yellow";
                }
                if (Otrpo == 2)
                {
                    trpoColor = "Green";
                }
            }
        }
        public string GetPhysicsColor
        {
            get => PhysicsColor;
            set
            {
                if (Ophysics == 0)
                {
                    PhysicsColor = "Red";
                }
                if (Ophysics == 1)
                {
                    PhysicsColor = "Yellow";
                }
                if (Ophysics == 2)
                {
                    PhysicsColor = "Green";
                }
            }
        }
    }
}
