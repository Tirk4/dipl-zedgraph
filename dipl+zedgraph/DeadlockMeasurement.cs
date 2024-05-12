using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dipl_zedgraph
{
    internal class DeadlockMeasurement
    {
        private static DeadlockMeasurement instance;

        public double clockWiseRotationValue=0;
        public double clockWiseLeftBoundaryValue=1;
        public double clockWiseRightBoundaryValue=11;

        public double counterClockWiseRotationValue=0;
        public double counterClockWiseLeftBoundaryValue=2;
        public double counterClockWiseRightBoundaryValue=10;

        public double experimentallyMeasuredRotation=12;
        public double KPF = RollingMechanismManager.GetInstance().KPF;

        public string textClockWiseRotationLimit = "допустимый поворот от %LEFT% до %RIGHT% градусов";
        public string textCounterClockWiseRotationLimit = "допустимый поворот от %LEFT% до %RIGHT% градусов";


        public static DeadlockMeasurement GetInstance()
        {
            if (instance == null)
            {
                instance = new DeadlockMeasurement();
            }
            return instance;
        }
        public double GetExperimentalRotationValue()
        {
            // TODO: Реализовать получение экспериментального значения угла поворота
            Random rand = new Random();
            experimentallyMeasuredRotation = rand.Next(2, 9)*clockWiseRotationValue/counterClockWiseRotationValue;

            return experimentallyMeasuredRotation;
        }
        public string GetTextClockWiseRotationLimit()
        {
            return textClockWiseRotationLimit.Replace("%LEFT%", clockWiseLeftBoundaryValue.ToString()).Replace("%RIGHT%", clockWiseRightBoundaryValue.ToString());
        }

        public string GetTextCounterClockWiseRotationLimit()
        {
            return textCounterClockWiseRotationLimit.Replace("%LEFT%", counterClockWiseLeftBoundaryValue.ToString()).Replace("%RIGHT%", counterClockWiseRightBoundaryValue.ToString());
        }
    }
}
