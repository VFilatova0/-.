using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Получение_СВ
{
    public partial class Form8 : Form
    {
        public Form8()
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
            double l = double.Parse(textBox2.Text);
            double k = double.Parse(textBox3.Text);
            double sum = 0;
            double[] x = new double[count];
            double[] F = new double[count];
            Random rnd = new Random();
            //расчёт факториала k 
            double fact = 1;
            for (int i = 1; i <= k; i++)
            {
                fact *= i;
                if (fact == 0) fact = 1;
            }
            //получение случайной величины и расчет функции
            for (int i = 0; i < count; i++)
            {
                for (int j = 1; j < (k + 1); j++)
                {
                    double R = rnd.NextDouble();    //генерация случайного числа в диапазоне от 0 до 1
                    while (sum > 1)
                    {
                        sum += -1 / l * Math.Log(1 - R);
                        k++;
                    }
                }
                k = sum;
                double X = k;  //формула для получения случайной величины, соответствующей распределению Пуассона 
                X = Math.Round(X, 3);
                listBox1.Items.Add(X.ToString());
                x[i] = X;   //Присвоение значения X
                //Вычисление значения функции в точке X
                F[i] = (Math.Pow(l, k) / fact) * Math.Exp(-l);
            }
            //Настраиваем осии графика
            chart1.ChartAreas[0].AxisX.Maximum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 50;
            //Определяем шаг сетки
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 5;
            //Добавляем вычисленные значения на график
            chart1.Series[0].Points.DataBindXY(x, F);
        }
    }
}
//генерируются только 0
