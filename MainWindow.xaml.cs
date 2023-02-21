using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace лр1
{
    public partial class MainWindow : Window
    {
        int count = 0;
        int x1;
        int y1;
        int a = 0;
        double angle = 1;
        int x2;
        int y2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyCanvas_MouseLeftButtonUp
            (object sender, MouseButtonEventArgs e)
        {
            var selected = myCombox.SelectedItem as TextBlock;
            if (selected?.Text == "Прямая")
            {
                Point clickPoint = e.GetPosition(myCanvas);
                if (count == 0)
                {
                    x1 = (int)clickPoint.X;
                    y1 = (int)clickPoint.Y;
                    count++;
                }
                else
                {
                    Random random = new();
                    Color randomColor = Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                    SolidColorBrush randomBrush = new(randomColor);
                    DrawLine((int)clickPoint.X, (int)clickPoint.Y, randomBrush);
                    count--;
                }
            }
            if (selected?.Text == "Окружность")
            {

                Point clickPoint = e.GetPosition(myCanvas);
                if (count == 0)
                {
                    x1 = (int)clickPoint.X;
                    y1 = (int)clickPoint.Y;
                    count++;
                }
                else
                {
                    Random random = new();
                    Color randomColor = Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                    SolidColorBrush randomBrush = new(randomColor);
                    x2 = (int)clickPoint.X;
                    y2 = (int)clickPoint.Y;
                    int radius = (int)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    DrawCircle(radius, randomBrush);
                    count--;
                }
            }
            if (selected?.Text == "Эллипс")
            {
                Point clickPoint = e.GetPosition(myCanvas);
                if (count == 0)
                {
                    x1 = (int)clickPoint.X;
                    y1 = (int)clickPoint.Y;
                    count++;
                }
                else if (count == 1)
                {
                    a = (int)Math.Sqrt(Math.Pow(clickPoint.X - x1, 2) + Math.Pow(clickPoint.Y - y1, 2));
                    x2 = (int)clickPoint.X;
                    y2 = (int)clickPoint.Y;
                    double angle1 = Math.Atan2(0, 100);
                    double angle2 = Math.Atan2(y2 - y1, x2 - x1);
                    angle = angle1 - angle2;
                    count++;
                }
                else
                {
                    Random random = new();
                    Color randomColor = Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                    SolidColorBrush randomBrush = new(randomColor);
                    int b = (int)Math.Sqrt(Math.Pow(clickPoint.X - x1, 2) + Math.Pow(clickPoint.Y - y1, 2));
                    DrawEllipse(b, randomBrush);
                    count -= 2;
                    angle = 1;
                }
            }
        }

        private void DrawLine(int x2, int y2, SolidColorBrush color)
        {

            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int dirX = x1 < x2 ? 1 : -1;
            int dirY = y1 < y2 ? 1 : -1;
            int error = deltaX - deltaY;

            SetPixel(x2, y2, color);

            while (x1 != x2 || y1 != y2)
            {
               SetPixel(x1, y1, color);

                int error2 = error * 2;
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += dirX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += dirY;
                }
            }
        }

        public void DrawCircle(int radius, SolidColorBrush color)
        {
            int x = 0;
            int y = radius;
            int delta = 1 - 2 * radius;
            while (y >= 0)
            {
                // Рисуем восьмерку симметрично относительно центра окружности
                SetPixel(x1 + x, y1 + y, color);
                SetPixel(x1 - x, y1 + y, color);
                SetPixel(x1 - x, y1 - y, color);
                SetPixel(x1 + x, y1 - y, color);
                SetPixel(x1 + y, y1 + x, color);
                SetPixel(x1 - y, y1 + x, color);
                SetPixel(x1 - y, y1 - x, color);
                SetPixel(x1 + y, y1 - x, color);
                // Обновляем значения переменных
                int error = 2 * (delta + y) - 1;
                if (delta < 0 && error <= 0)
                {
                    x++;
                    delta += 2 * x + 1;
                    continue;
                }
                if (delta > 0 && error > 0)
                {
                    y--;
                    delta -= 2 * y + 1;
                    continue;
                }
                x++;
                delta += 2 * (x - y);
                y--;
            }
        }

        void DrawEllipse(int b, SolidColorBrush color)
        {
            int _x = 0; // Компонента x
            int _y = b; // Компонента y
            //полуоси
            int a_sqr = a * a;
            int b_sqr = b * b;
            int delta = 4 * b_sqr * ((_x + 1) * (_x + 1)) + a_sqr * ((2 * _y - 1) * (2 * _y - 1)) - 4 * a_sqr * b_sqr; // Функция координат точки (x+1, y-1/2)
            while (a_sqr * (2 * _y - 1) > 2 * b_sqr * (_x + 1)) // Первая часть дуги
            {
                Pixel4(_x, _y, color);
                if (delta < 0) // Переход по горизонтали
                {
                    _x++;
                    delta += 4 * b_sqr * (2 * _x + 3);
                }
                else // Переход по диагонали
                {
                    _x++;
                    delta = delta - 8 * a_sqr * (_y - 1) + 4 * b_sqr * (2 * _x + 3);
                    _y--;
                }
            }
            delta = b_sqr * ((2 * _x + 1) * (2 * _x + 1)) + 4 * a_sqr * ((_y + 1) * (_y + 1)) - 4 * a_sqr * b_sqr; // Функция координат точки (x+1/2, y-1)
            while (_y + 1 != 0) // Вторая часть дуги, если не выполняется условие первого цикла, значит выполняется a^2(2y - 1) <= 2b^2(x + 1)
            {
                Pixel4(_x, _y, color);
                if (delta < 0) // Переход по вертикали
                {
                    _y--;
                    delta += 4 * a_sqr * (2 * _y + 3);
                }
                else // Переход по диагонали
                {
                    _y--;
                    delta = delta - 8 * b_sqr * (_x + 1) + 4 * a_sqr * (2 * _y + 3);
                    _x++;
                }
            }
        }

        void Pixel4(double _x, double _y, SolidColorBrush color) // Рисование пикселя для первого квадранта, и, симметрично, для остальных
        {
            double cos = Math.Cos(angle); 
            double sin = Math.Sin(angle);
            _x -= x1;
            _y -= y1;
            double xnew = _x * cos - _y * sin;
            double ynew = _x * sin + _y * cos;
            double resultX = xnew + x1;
            double resultY = ynew + y1;
            SetPixel(x1 + resultX, y1 + resultY, color);
            SetPixel(x1 + resultX, y1 - resultY, color);
            SetPixel(x1 - resultX, y1 - resultY, color);
            SetPixel(x1 - resultX, y1 + resultY, color);
        }

        public void SetPixel(double x, double y, SolidColorBrush color)
        {
            Ellipse ellipse = new()
            {
                Width = 3,
                Height = 3,
                Fill = color
            };
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            myCanvas.Children.Add(ellipse);
        }
    }
}
