using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Получение_СВ
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f1 = Application.OpenForms[0];    //отрываем главную форму
            f1.Show();      //показываем главную форму
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            listBox1.Items.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 44)) e.KeyChar = (char)0;
            if (e.KeyChar == '-') e.KeyChar = (char)0;
            if (e.KeyChar == 44) e.KeyChar = (char)0;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 44)) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox2.Text.Length > 0) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox2.SelectionStart != 0) e.KeyChar = (char)0;
            if (e.KeyChar == 44 && textBox2.Text.Contains(",")) e.KeyChar = (char)0;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || (int)e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 44)) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox3.Text.Length > 0) e.KeyChar = (char)0;
            if (e.KeyChar == '-' && textBox3.SelectionStart != 0) e.KeyChar = (char)0;
            if (e.KeyChar == 44 && textBox3.Text.Contains(",")) e.KeyChar = (char)0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = int.Parse(textBox1.Text);
            double a = double.Parse(textBox2.Text);
            double b = double.Parse(textBox3.Text);
            Random rnd = new Random();
            double[] x = new double[count];
            double[] F = new double[count];
            for (int i = 0; i < count; i++)
            {
                double R = rnd.NextDouble();    //генерация случайного числа в диапазоне от 0 до 1
                double X = (b - a) * R + a;   //формула для получения случайной величины, равномерно распределённой на интервале [a, b]
                X = Math.Round(X, 3);
                listBox1.Items.Add(X.ToString());
                //Запись значения X в массив x[]
                x[i] = X;
                //Нахождение min и max значений массива x[]


            }
            //Настраиваем осии графика
            chart1.ChartAreas[0].AxisX.Maximum = a;
            chart1.ChartAreas[0].AxisX.Maximum = b;
            //Определяем шаг сетки
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            //Добавляем вычисленные значения на график
            chart1.Series[0].Points.DataBindXY(x, F);
        }
    }
}
