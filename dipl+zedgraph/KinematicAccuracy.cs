using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dipl_zedgraph
{
    internal class KinematicAccuracy
    {
        private static KinematicAccuracy instance;
        public double KPF = RollingMechanismManager.GetInstance().KPF;

        public double sectionLength;
        public double sectionLengthLeftLimit = 1;
        public double sectionLengthRightLimit = 500;


        public int numberOfMeasurements=100;
        public int numberOfMeasurementsLeftLimit = 1;
        public int numberOfMeasurementsRightLimit = 500;

        public string TextNumberOfMeasurments = "допустимое количество измерений от %LEFT% до %RIGHT%";
        public string TextSetctionLength = "допустимое длина секции от %LEFT% до %RIGHT%";

        public List<double> dynamicArray1 = new List<double>();
        public List<double> dynamicArray2 = new List<double>();

        public static KinematicAccuracy GetInstance()
        {
            if (instance == null)
                    instance = new KinematicAccuracy();
                return instance;
        }

        public string GetTextNumberOfMeasurments()
        {
            return TextNumberOfMeasurments.Replace("%LEFT%", numberOfMeasurementsLeftLimit.ToString()).Replace("%RIGHT%", numberOfMeasurementsRightLimit.ToString());
        }
        public string GetTextSectionLength()
        {
            return  TextSetctionLength.Replace("%LEFT%", sectionLengthLeftLimit.ToString()).Replace("%RIGHT%", sectionLengthRightLimit.ToString());
        }
        
        public double CalculateTheoreticalNutPosition(double angle)
        {
            return angle*KPF/360;
        }

        public double CalculateExperimentallNutPosition(double theoretic)
        {

            return theoretic *(new Random().NextDouble() * 0.4 + 0.8);
        }


        public void MakeGetExperimentalData()
        {
            dynamicArray1.Clear();
            dynamicArray2.Clear(); 
            for(double i = 0; i < 360; i=i+ (360.0 / numberOfMeasurements))
            {
                dynamicArray1.Add(i);
                dynamicArray2.Add(CalculateExperimentallNutPosition(CalculateTheoreticalNutPosition(i))-CalculateTheoreticalNutPosition(i));

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
