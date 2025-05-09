using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LAB1
{
    public abstract class DisplayObject
    {
        protected Shape element;
        protected double x, y; // Координаты относительно левого нижнего угла
        protected double vx, vy, curvx, curvy; // Вектор скорости
        protected double ax, ay, curax, curay; // Ускорение
        protected Random random;
        protected bool isMoving = false; // Ф

        public Shape Element => element;

        public DisplayObject(Random random)
        {
            this.random = random;
            x = random.NextDouble() * 1000; // Случайные координаты
            y = random.NextDouble() * 500;
            vx = random.Next(20); // Случайная скорость (-2 до 2)
            vy = random.Next(20);
            ax = random.NextDouble() * 0.1 - 0.05; // Случайное ускорение
            ay = random.NextDouble() * 0.1 - 0.05;

            curvx = vx; curvy = vy;


            CreateShape();
            element.Stroke = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
            element.Fill   = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
            UpdatePosition();
        }

        protected abstract void CreateShape();

        public void Update(double canvasWidth, double canvasHeight)
        {
            if (isMoving)
            {
                x += vx;
                y += vy;
                //  vx += ax;
                //  vy += ay;

                // Отскок от стенок (когда половина фигуры пересекает рамку)
                double halfWidth = element.Width / 2;
                double halfHeight = element.Height / 2;

                if (x > canvasWidth) { x = canvasWidth ; vx = -vx; }
                if (x < 0) { x = 0; vx = -vx; }
                if (y > canvasHeight) { y = canvasHeight; vy = -vy; }
                if (y < 0) { y = 0; vy = -vy; }

                UpdatePosition();

            }
            UpdatePosition();
        }

            protected void UpdatePosition()
            {
                Canvas.SetLeft(element, x - element.Width / 2);
                Canvas.SetBottom(element, y - element.Height / 2);
            }
        // Запуск движения с случайной скоростью
        public void StartMotion()
        {
            if (!isMoving)
            {
                curvx = vx; // Задаём скорость при старте
                curvy = vy;
                isMoving = true;
            }
        }

        public void StartUniformMotion() { ax = 0; ay = 0; }
        public void StartAcceleratedMotion() { /* Ускорение уже есть */ }
        public void StopMotion() { curvy = 0; curvx = 0; isMoving = false; }
    }
}
