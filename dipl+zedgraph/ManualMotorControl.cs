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

        private int leftLimit = 1;
        private int rightLimit = 11;
        private string text = "Допустимый поворот от %LEFT% до %RIGHT% градусов";
        public static ManualMotorControl GetInstance()
        {
            if (instance == null)
            {
                instance = new ManualMotorControl();
            }
            return instance;
        }
        public int GetLeftLimit()
        {
            return leftLimit;
        }

        public int GetRightLimit()
        {
            return rightLimit;
        }
        public string GetTextAllowedTurnRange()
        {
            return text.Replace("%LEFT%", leftLimit.ToString()).Replace("%RIGHT%", rightLimit.ToString());
        }
    
    }
}
