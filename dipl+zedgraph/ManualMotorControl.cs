using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dipl_zedgraph
{
    internal class ManualMotorControl
    {
        private static ManualMotorControl instance;

        private double leftLimit = 2;
        private double rightLimit = 10;
        public double rotationSpeed;
        private string text = "Допустимый поворот от %LEFT% до %RIGHT% градусов";
        public static ManualMotorControl GetInstance()
        {
            if (instance == null)
            {
                instance = new ManualMotorControl();
            }
            return instance;
        }
        public double GetLeftLimit()
        {
            return leftLimit;
        }

        public double GetRightLimit()
        {
            return rightLimit;
        }
        public string GetTextAllowedTurnRange()
        {
            return text.Replace("%LEFT%", leftLimit.ToString()).Replace("%RIGHT%", rightLimit.ToString());
        }
    
    }
}
