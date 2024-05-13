using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dipl_zedgraph
{
    internal class EfficiencyMechanism
    {
        private static EfficiencyMechanism instance;

        public double axialLoad;
        public double leftBoundary=1000;
        public double rightBoundary=3000;

        public double degresPerMeasurment = 2;

        public double KPF = RollingMechanismManager.GetInstance().KPF;

        public List<double> dynamicArray1 = new List<double>();
        public List<double> dynamicArray2 = new List<double>();

        public static EfficiencyMechanism GetInstance()
        {
            if (instance == null)
            {
                instance = new EfficiencyMechanism();
            }
            return instance;
        }
        public double CalculateEfficienceMechanism(double moment, double P)
        {

            return ( (360*moment*100)/(P*KPF));
        }
        public double CalculateMoment()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            double minValue = 1;
            double maxValue = 2;
            return (random.NextDouble() * (maxValue - minValue)) + minValue;
        }
        public void makeExperimentalData()
        {
            dynamicArray1.Clear();
            dynamicArray2.Clear();

            for(double i = 0; i < 360; i = i + degresPerMeasurment)
            {
                dynamicArray1.Add(i);
                dynamicArray2.Add(CalculateEfficienceMechanism(CalculateMoment(),axialLoad));
            }

        }
        public void WriteDataToTextFile(string fileName)
        {
            File.Delete(fileName);
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    for (int i = 0; i < dynamicArray1.Count; i++)
                    {
                        writer.WriteLine($"{dynamicArray1[i]}\t{dynamicArray2[i]}");
                    }
                }

                MessageBox.Show($"Данные успешно записаны в файл {fileName}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи данных в файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }


}

