using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dipl_zedgraph
{
    internal class RollingMechanismManager
    {
        private static RollingMechanismManager instance;
        public double LeftSpeedLimit=2.0;
        public double RightSpeedLimit=10.0;
        public string TextSpeedLimit = "допустимый поворот от %LEFT% до %RIGHT% градусов";

        public double NumberOfRotations;
        public double KPF=1;
        public double TimeOfRolling;

        public double LeftRollingTimeLimit=1.0;
        public double RightRollingTimeLimit=1000.0;
        public string TextRollingTimeLimit = "допустимое время прикатки от %LEFT% до %RIGHT%";

        public double LeftTurnLengthLimit=2.0;
        public double RightTurnLengthLimit=10.0;
        public string TextTurnLengthLimit = "допустимая длина механизма от %LEFT% до %RIGHT%";

        public static RollingMechanismManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RollingMechanismManager();
            }
            return instance;
        }




        public string GetTextAllowedSpeedLimit()
        {
            return TextSpeedLimit.Replace("%LEFT%", LeftSpeedLimit.ToString()).Replace("%RIGHT%", RightSpeedLimit.ToString());
        }
        public string GetTextRollingTimeLimit()
        {
            return TextRollingTimeLimit.Replace("%LEFT%", LeftRollingTimeLimit.ToString()).Replace("%RIGHT%", RightRollingTimeLimit.ToString());
        }
        public string GetTextTurnLengthLimit()
        {
            return TextTurnLengthLimit.Replace("%LEFT%", LeftRollingTimeLimit.ToString()).Replace("%RIGHT%", RightRollingTimeLimit.ToString());
        }
        public double CountLength(double n, double KPF)
        {
            return (n*KPF);
        }
    }
}
