﻿using System;
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
            InitializeComponent();
            label3.Text=motorControl.GetTextAllowedTurnRange();
            /////
            label4.Text =rollingMechanismManager.GetTextAllowedSpeedLimit();
            label8.Text = rollingMechanismManager.GetTextTurnLengthLimit();
            label9.Text = rollingMechanismManager.GetTextRollingTimeLimit();
            /////
            

            CreateGraph(zedGraphControl1);
            CreateGraph(zedGraphControl2);  
        }


        private void CreateGraph(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = "Пример графика";
            myPane.XAxis.Title.Text = "X";
            myPane.YAxis.Title.Text = "Y";

            PointPairList list = new PointPairList();
            for (double x = 0; x < 10; x += 0.1)
            {
                double y = Math.Sin(x);
                list.Add(x, y);
            }

            LineItem myCurve = myPane.AddCurve("Синус", list, System.Drawing.Color.Blue, SymbolType.None);

            zgc.AxisChange();
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
            
            if (int.TryParse(textBox1.Text, out int number))
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
            if (textBox3.Text == "")
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
            if (!(double.Parse(textBox9.Text) >= RollingMechanismManager.GetInstance().LeftTurnLengthLimit && double.Parse(textBox9.Text) <= RollingMechanismManager.GetInstance().RightTurnLengthLimit))
            {
                MessageBox.Show($"Число должно быть в пределах от {RollingMechanismManager.GetInstance().LeftTurnLengthLimit} до {RollingMechanismManager.GetInstance().RightTurnLengthLimit}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (!(int.Parse(textBox4.Text) >= 2 && int.Parse(textBox4.Text) <= 10))
            {
                MessageBox.Show("Число должно быть в пределах от 2 до 10", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(int.Parse(textBox5.Text) >= 2 && int.Parse(textBox5.Text) <= 1000))
            {
                MessageBox.Show("Число должно быть в пределах от 2 до 1000", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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



            if (!(int.Parse(textBox6.Text) >= 2 && int.Parse(textBox6.Text) <= 10))
            {
                MessageBox.Show("Число должно быть в пределах от 2 до 10", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!(int.Parse(textBox7.Text) >= 2 && int.Parse(textBox7.Text) <= 1000))
            {
                MessageBox.Show("Число должно быть в пределах от 2 до 1000", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
    }
}
