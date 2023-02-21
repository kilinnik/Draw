using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace лр1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }

    //DrawCircle(-200, 0, 100, Brushes.Pink);

    //private void DrawCircle(int x0, int y0, int radius, SolidColorBrush color)
    //{
    //    int x = radius;
    //    int y = 0;
    //    int decisionOver2 = 1 - x;

    //    while (y <= x)
    //    {
    //        DrawPoint(x + x0, y + y0, color);
    //        DrawPoint(y + x0, x + y0, color);
    //        DrawPoint(-x + x0, y + y0, color);
    //        DrawPoint(-y + x0, x + y0, color);
    //        DrawPoint(-x + x0, -y + y0, color);
    //        DrawPoint(-y + x0, -x + y0, color);
    //        DrawPoint(x + x0, -y + y0, color);
    //        DrawPoint(y + x0, -x + y0, color);

    //        y++;

    //        if (decisionOver2 <= 0)
    //        {
    //            decisionOver2 += 2 * y + 1;
    //        }
    //        else
    //        {
    //            x--;
    //            decisionOver2 += 2 * (y - x) + 1;
    //        }
    //    }
    //}

    //private void DrawPoint(int x, int y, SolidColorBrush color)
    //{
    //    Ellipse dot = new()
    //    {
    //        Width = 3,
    //        Height = 3,
    //        Fill = color
    //    };

    //    canvas.Children.Add(dot);
    //    Canvas.SetLeft(dot, x);
    //    Canvas.SetBottom(dot, y);
    //}
}
