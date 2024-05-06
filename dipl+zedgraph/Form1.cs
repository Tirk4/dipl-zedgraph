using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dipl_zedgraph
{
    public partial class Form1 : Form
    {
        private string RemoveNonDigits(string input)
        {
            string result = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c))
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
            InitializeComponent();
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

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

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
                if (!(number >= 2 && number <= 10)) // потом заменим
                {
                    MessageBox.Show("Число должно быть в пределах от 2 до 10", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
