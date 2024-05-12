using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace dipl_zedgraph
{
    public partial class Form1 : Form
    {
        private ManualMotorControl motorControl;
        private RollingMechanismManager rollingMechanismManager;
        private DeadlockMeasurement deadlockMeasurement;

        private string RemoveNonDigits(string input)
        {
            string result = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c) || c==',')
                {
                    result += c;
                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                }
            }

            return result;
        }

        public Form1()
        {
            motorControl = ManualMotorControl.GetInstance();
            rollingMechanismManager = RollingMechanismManager.GetInstance();
            deadlockMeasurement = DeadlockMeasurement.GetInstance();
            InitializeComponent();
            label3.Text=motorControl.GetTextAllowedTurnRange();
            /////
            label4.Text =rollingMechanismManager.GetTextAllowedSpeedLimit();
            label8.Text = rollingMechanismManager.GetTextTurnLengthLimit();
            label9.Text = rollingMechanismManager.GetTextRollingTimeLimit();
            /////
            label11.Text = KinematicAccuracy.GetInstance().GetTextSectionLength();
            label13.Text = KinematicAccuracy.GetInstance().GetTextNumberOfMeasurments();
           
            /////
            label15.Text = deadlockMeasurement.GetTextClockWiseRotationLimit();
            label17.Text = deadlockMeasurement.GetTextCounterClockWiseRotationLimit();
            ////





            
        }

        private void CreateGraph(ZedGraphControl zgc)
        {
            zgc.GraphPane.CurveList.Clear();

            PointPairList points = new PointPairList();

            for (int i = 0; i < KinematicAccuracy.GetInstance().dynamicArray1.Count; i++)
            {
                points.Add(KinematicAccuracy.GetInstance().dynamicArray1[i], KinematicAccuracy.GetInstance().dynamicArray2[i]);
            }
           
            LineItem curve = zgc.GraphPane.AddCurve("График", points, Color.Blue, SymbolType.None);

            zgc.AxisChange();
            zgc.Invalidate();
        }


        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = RemoveNonDigits(textBox1.Text);
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = RemoveNonDigits(textBox2.Text);
            textBox2.SelectionStart = textBox2.Text.Length;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = RemoveNonDigits(textBox4.Text);
            textBox4.SelectionStart = textBox4.Text.Length;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите скорость вращения двигателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (double.TryParse(textBox1.Text, out double number))
            {
                if (!(number >= motorControl.GetLeftLimit() && number <= motorControl.GetRightLimit() ))
                {
                    MessageBox.Show($"Число должно быть в пределах от {motorControl.GetLeftLimit()} до {motorControl.GetRightLimit()}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
           
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                
                MessageBox.Show("Выберите направление вращения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            ManualMotorControl.GetInstance().rotationSpeed = double.Parse(textBox1.Text);
            MessageBox.Show("Механизм запущен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        /////////////////////////////////////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите угол поворота двигателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!radioButton3.Checked && !radioButton4.Checked)
            {
                MessageBox.Show("Выберите направление вращения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox9.Text == "")
            {
                MessageBox.Show("Введите время прикатки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!(double.Parse(textBox2.Text) >= RollingMechanismManager.GetInstance().LeftSpeedLimit && double.Parse(textBox2.Text)<= RollingMechanismManager.GetInstance().RightSpeedLimit) )
            {
                MessageBox.Show($"Число должно быть в пределах от {RollingMechanismManager.GetInstance().LeftSpeedLimit} до {RollingMechanismManager.GetInstance().RightSpeedLimit}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(double.Parse(textBox3.Text) >= RollingMechanismManager.GetInstance().LeftRollingTimeLimit && double.Parse(textBox3.Text) <= RollingMechanismManager.GetInstance().RightRollingTimeLimit))
            {
                MessageBox.Show($"Число должно быть в пределах от {RollingMechanismManager.GetInstance().LeftRollingTimeLimit} до {RollingMechanismManager.GetInstance().RightRollingTimeLimit}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(double.Parse(textBox9.Text) >= RollingMechanismManager.GetInstance().LeftNumberOfRotationsLimit && double.Parse(textBox9.Text) <= RollingMechanismManager.GetInstance().RightNumberOfRotationsLimit))
            {
                MessageBox.Show($"Число должно быть в пределах от {RollingMechanismManager.GetInstance().LeftNumberOfRotationsLimit} до {RollingMechanismManager.GetInstance().RightNumberOfRotationsLimit}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RollingMechanismManager.GetInstance().NumberOfRotations = double.Parse(textBox2.Text);
            RollingMechanismManager.GetInstance().TimeOfRolling = double.Parse(textBox3.Text);
            RollingMechanismManager.GetInstance().NumberOfRotations = double.Parse(textBox9.Text);

            label31.Text = RollingMechanismManager.GetInstance().CountLength(RollingMechanismManager.GetInstance().NumberOfRotations, RollingMechanismManager.GetInstance().KPF).ToString();
            MessageBox.Show("Механизм запущен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //////////////////////////////
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Введите длину участка для проверки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox5.Text == "")
            {
                MessageBox.Show("Введите количество измерений на одном повороте шпинделя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!(double.Parse(textBox4.Text) >= KinematicAccuracy.GetInstance().sectionLengthLeftLimit && double.Parse(textBox4.Text) <= KinematicAccuracy.GetInstance().sectionLengthRightLimit))
            {
                MessageBox.Show($"Число должно быть в пределах от {KinematicAccuracy.GetInstance().sectionLengthLeftLimit} до {KinematicAccuracy.GetInstance().sectionLengthRightLimit}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(double.Parse(textBox5.Text) != Math.Floor(double.Parse(textBox5.Text)))
            {
                MessageBox.Show("Число должно быть целым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(double.Parse(textBox5.Text) >= KinematicAccuracy.GetInstance().numberOfMeasurementsLeftLimit && double.Parse(textBox5.Text) <= KinematicAccuracy.GetInstance().numberOfMeasurementsRightLimit))
            {
                MessageBox.Show($"Число должно быть в пределах от {KinematicAccuracy.GetInstance().numberOfMeasurementsLeftLimit} до {KinematicAccuracy.GetInstance().numberOfMeasurementsRightLimit}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            KinematicAccuracy.GetInstance().sectionLength= double.Parse(textBox4.Text);
            KinematicAccuracy.GetInstance().numberOfMeasurements = int.Parse(textBox5.Text);

            KinematicAccuracy.GetInstance().MakeGetExperimentalData();
            KinematicAccuracy.GetInstance().WriteDataToTextFile("experiment_data.txt");
            CreateGraph(zedGraphControl1);

            MessageBox.Show("Механизм запущен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //////////////////////////////
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Введите значение калибровочного поворта по часовой стрелке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox7.Text == "")
            {
                MessageBox.Show("Введите значение калибровочного поворта против часовой стрелке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!(double.Parse(textBox6.Text) >= DeadlockMeasurement.GetInstance().clockWiseLeftBoundaryValue && double.Parse(textBox6.Text) <= DeadlockMeasurement.GetInstance().clockWiseRightBoundaryValue))
            {
                MessageBox.Show($"Число должно быть в пределах от {DeadlockMeasurement.GetInstance().clockWiseLeftBoundaryValue} до {DeadlockMeasurement.GetInstance().clockWiseRightBoundaryValue}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(double.Parse(textBox7.Text) >= DeadlockMeasurement.GetInstance().counterClockWiseLeftBoundaryValue && double.Parse(textBox7.Text) <= DeadlockMeasurement.GetInstance().counterClockWiseRightBoundaryValue))
            {
                MessageBox.Show($"Число должно быть в пределах от {DeadlockMeasurement.GetInstance().counterClockWiseLeftBoundaryValue} до {DeadlockMeasurement.GetInstance().counterClockWiseRightBoundaryValue}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            DeadlockMeasurement.GetInstance().clockWiseRotationValue = double.Parse(textBox6.Text);
            DeadlockMeasurement.GetInstance().counterClockWiseRotationValue = double.Parse(textBox7.Text);
            label20.Text = DeadlockMeasurement.GetInstance().GetExperimentalRotationValue().ToString();


            MessageBox.Show("Механизм запущен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        ///////////////////////////////
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                MessageBox.Show("Введите значение калибровочного поворта по часовой стрелке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!(int.Parse(textBox8.Text) >= 2 && int.Parse(textBox8.Text) <= 10))
            {
                MessageBox.Show("Число должно быть в пределах от 2 до 10", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //////////////////////////////
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = RemoveNonDigits(textBox3.Text);
            textBox3.SelectionStart = textBox3.Text.Length;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.Text = RemoveNonDigits(textBox5.Text);
            textBox5.SelectionStart = textBox5.Text.Length;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = RemoveNonDigits(textBox8.Text);
            textBox8.SelectionStart = textBox8.Text.Length;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = RemoveNonDigits(textBox6.Text);
            textBox6.SelectionStart = textBox6.Text.Length;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox7.Text = RemoveNonDigits(textBox7.Text);
            textBox7.SelectionStart = textBox7.Text.Length;
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textBox9.Text = RemoveNonDigits(textBox9.Text);
            textBox9.SelectionStart = textBox9.Text.Length;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
