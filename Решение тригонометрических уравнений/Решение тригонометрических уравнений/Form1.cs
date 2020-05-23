using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Решение_тригонометрических_уравнений
{
    /*Вариант 36.
Заголовок:Решение тригонометрических уравненийс помощью численных методов.
Написать в среде VisualC# визуальную программу, которая позволяет решать тригонометрические уравнения 
с помощью численных методов – касательных, простых итераций, хорд, половинного деления.
Предусмотреть рисование графика функции.
Предусмотреть возможность ввода выражения арктангенс (arctg(a*x)) .
Предусмотреть возможность сравнения числа итераций при решении задачи каждым методом, при различной заданной точности.
    */
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public int rbChoosen()// Определение номера выбранного метода решения
        {
            if (radioButton1.Checked) return 1;
            if (radioButton2.Checked) return 2;
            if (radioButton3.Checked) return 3;
            else return 4;
        }

        public double func(double a, double b, double c, double x) //функции нахождения корня
        {
            return Math.Round((b * Math.Atan(a * x) + c * Math.Tan(x)), 6);
        }
        public double xValueFunc(double a, double b, double c, double x)// Выражение х
        {
            return Math.Atan(-b * Math.Atan(a * x)) / c;
        }

        public double derivativeFunc(double a, double b, double c, double x)// Первая производная функции
        {
            return b * (1 + Math.Pow(a * x, 2)) + c * Math.Pow(Math.Cos(x), 2);
        }

        public double xx;

        private void button1_Click(object sender, EventArgs e)
        {
            double a = double.Parse(textBox1.Text);
            double b = double.Parse(textBox5.Text);
            double c = double.Parse(textBox4.Text);
            
            switch (rbChoosen())
            {
                case 1:// Касательных
                    {
                        double n = 0, E = 0.001, x = 0.1, h, m, pr, x1;
                        do
                        {

                            h = func(a, b, c, x) / (derivativeFunc(a, b, c, x));
                            pr = (derivativeFunc(a, b, c, x + h) - derivativeFunc(a, b, c, x)) / h;
                            x1 = x - func(a, b, c, x) / pr;
                            m = Math.Abs(x1 - x);
                            x = x1;
                            n++;
                        }
                        while (m > E);
                        listBox1.Items.Add("x = " + x);
                        listBox1.Items.Add("Количетсво итераций = " + n);
                        listBox1.Items.Add("F(x) = " + func(a, b, c, x));
                        xx = x;
                    }
                    break;
                case 2: // простых итераций
                    {
                        double n = 0, E = 0.001, x = 0, m;
                        do
                        {
                            double x1 = xValueFunc(a, b, c, x);
                            m = Math.Abs(x1 - x);
                            x = x1;
                            n++;
                        }
                        while (m > E);
                        listBox2.Items.Add("x = " + x);
                        listBox2.Items.Add("Количетсво итераций = " + n);
                        listBox2.Items.Add("F(x) = " + func(a, b, c, x));
                        xx = x;
                    }
                    break;
                case 3: // Хорд
                    {
                        double E = 0.001, x = 0, p = 1, m;
                        int n = 0;
                        do
                        {
                            double x1 = x - (func(a, b, c, x) * (x - p)) / (func(a, b, c, x) - func(a, b, c, p));
                            m = Math.Abs(x1 - x);
                            x = x1;
                            n++;
                        }
                        while (m > E);
                        listBox3.Items.Add("x = " + x);
                        listBox3.Items.Add("Количетсво итераций = " + n);
                        listBox3.Items.Add("F(x) = " + func(a, b, c, x));
                        xx = x;
                    }
                    break;
                case 4:// Половинного деления
                    {
                        if ((textBox2.Text == "") || (textBox3.Text == ""))
                        {
                            MessageBox.Show("Введите данные");
                            break;
                        }

                        double a1, b1, n = 0, E = 0.001, x = 0;
                        a1 = double.Parse(textBox2.Text);
                        b1 = double.Parse(textBox3.Text);
                        while (Math.Abs(b - a) > E)
                        {
                            x = (a1 + b1) / 2;
                            if (func(a, b, c, a1) * func(a, b, c, x) > 0) a1 = x;
                            else b = x;
                            n++;
                            if (n > 1000)
                            {
                                break;
                            }
                        }
                        listBox4.Items.Add("x = " + x);
                        listBox4.Items.Add("Количетсво итераций = " + n);
                        listBox4.Items.Add("F(x) = " + func(a, b, c, x));
                        xx = x;
                    }
                    break;

            }
            // построение графика
            chart1.Series[0].Points.Clear();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;      
            double xxx = xx - 0.1;
            for (int i = - 10; i < 10; i++)
            {              
                chart1.Series[0].Points.AddXY(xxx, func(a, b, c, xxx));                         
                xxx += 0.01;
            }
        }
    }   
}
