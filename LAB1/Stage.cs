using LAB1;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BouncingShapes
{
    // Класс Stage управляет игровым полем и списком объектов
    public class Stage
    {
        private Canvas canvas; // Холст, на котором отображаются фигуры
        private List<DisplayObject> shapes = new List<DisplayObject>(); // Список игровых объектов (фигур)

        // Конструктор принимает холст для размещения фигур
        public Stage(Canvas canvas)
        {
            this.canvas = canvas; // Инициализация холста
        }

        // Метод добавляет новую фигуру на поле
        public void AddShape(DisplayObject shape)
        {
            shapes.Add(shape); // Добавление фигуры в список объектов
            if (!canvas.Children.Contains(shape.Element)) // Проверка на дубликат
            {
                canvas.Children.Add(shape.Element);
            }    // Добавление графического элемента на холст
        }

        // Метод для запуска движения
        public void StartMotion()
        {
            foreach (var shape in shapes)
            {
                shape.StartMotion();
            }
        }

        // Метод обновляет состояние всех фигур на поле
        public void Update()
        {
            foreach (var shape in shapes) // Проходим по всем фигурам
            {
                shape.Update(canvas.ActualWidth, canvas.ActualHeight); // Обновляем положение фигуры
                // Координаты фигуры обновляются внутри метода UpdatePosition
            }
        }

        // Метод запускает равномерное прямолинейное движение всех фигур
        public void StartUniformMotion()
        {
            foreach (var shape in shapes) shape.StartUniformMotion(); // Запускаем движение для каждой фигуры
        }

        // Метод запускает равноускоренное движение всех фигур
        public void StartAcceleratedMotion()
        {
            foreach (var shape in shapes) shape.StartAcceleratedMotion(); // Запускаем ускоренное движение
        }

        // Метод останавливает движение всех фигур
        public void StopMotion()
        {
            foreach (var shape in shapes) shape.StopMotion(); // Останавливаем движение каждой фигуры
        }

        // Вложенный класс CustomShape - произвольная фигура (равносторонний треугольник)
        public class CustomShape : DisplayObject
        {
            // Конструктор принимает генератор случайных чисел
            public CustomShape(Random random) : base(random) { }

            // Переопределение метода создания фигуры (равносторонний треугольник)
            protected override void CreateShape()
            {
                // Создаем размер для треугольника (сторона)
                double size = random.Next(15, 150);

                // Вычисляем высоту равностороннего треугольника
                double height = Math.Sqrt(3) * size / 2;

                element = new Polygon // Создаем многоугольник
                {
                    // Задаем точки равностороннего треугольника
                    Points = new PointCollection {
                        new Point(0, height),        // Нижняя левая точка
                        new Point(size / 2, 0),      // Верхняя точка
                        new Point(size, height)      // Нижняя правая точка
                    },
                    Width = size,        // Ширина треугольника
                    Height = height,     // Высота треугольника
                    Fill = GetRandomBrush(),     // Случайный цвет заливки
                    Stroke = GetRandomBrush(),      // Случайный цвет обводки
                    StrokeThickness = random.Next(1, 4),  // Случайная толщина обводки от 1 до 3
                };
            }
        }

        // Вложенный класс Square - квадрат
        public class Square : DisplayObject
        {
            // Конструктор принимает генератор случайных чисел
            public Square(Random random) : base(random) { }

            // Переопределение метода создания фигуры (квадрат)
            protected override void CreateShape()
            {
                double size = random.Next(20, 70); // Размер стороны квадрата

                element = new Rectangle // Создаем прямоугольник (для квадрата ширина = высоте)
                {
                    Width = size,        // Одинаковый размер для квадрата
                    Height = size,       // Одинаковый размер для квадрата
                    Fill = GetRandomBrush(),     // Случайный цвет заливки
                    Stroke = Brushes.Black,      // Черная обводка
                    StrokeThickness = 1.5,       // Толщина обводки
                    RadiusX = 4,                 // Скругление углов для красоты
                    RadiusY = 4                  // Скругление углов для красоты
                };
            }
        }

        // Вложенный класс Circle - круг
        public class Circle : DisplayObject
        {
            // Конструктор принимает генератор случайных чисел
            public Circle(Random random) : base(random) { }

            // Переопределение метода создания фигуры (круг)
            protected override void CreateShape()
            {
                double size = random.Next(15, 150); // Диаметр круга

                element = new Ellipse // Создаем эллипс (для круга ширина = высоте)
                {
                    Width = size,        // Одинаковый размер для круга
                    Height = size,       // Одинаковый размер для круга
                    Fill = GetRandomBrush(),     // Случайный цвет заливки
                    Stroke = GetRandomBrush(),      // Случайный цвет обводки
                    StrokeThickness = random.Next(1, 4),  // Случайная толщина обводки от 1 до 3
                };
            }
        }

        // Вложенный класс RectangleShape - прямоугольник
        public class RectangleShape : DisplayObject
        {
            // Конструктор принимает генератор случайных чисел
            public RectangleShape(Random random) : base(random) { }

            // Переопределение метода создания фигуры (прямоугольник)
            protected override void CreateShape()
            {
                element = new Rectangle // Создаем прямоугольник
                {
                    Width = random.Next(25, 180),      // Случайная ширина
                    Height = random.Next(15, 120),     // Случайная высота
                    Fill = GetRandomBrush(),          // Случайный цвет заливки
                    Stroke = GetRandomBrush(),           // Случайный цвет обводки
                    StrokeThickness = random.Next(1, 4), // Случайная толщина обводки от 1 до 3
                    RadiusX = 4,                      // Скругление углов для красоты
                    RadiusY = 4                       // Скругление углов для красоты
                };
            }
        }
    }

    // Расширение базового класса DisplayObject для работы со случайными цветами
    public abstract class DisplayObject
    {
        // Существующие поля и свойства...
        protected Shape element; // Графический элемент (Rectangle, Ellipse или Polygon)
        protected Random random; // Генератор случайных чисел
        protected double x, y; // Текущие координаты фигуры
        protected double vx, vy; // Компоненты скорости по осям X и Y
        protected double ax, ay; // Компоненты ускорения по осям X и Y
        protected bool isMoving; // Флаг движения
        protected MovementType movementType; // Тип движения (равномерное или ускоренное)

        // Свойство для доступа к графическому элементу
        public Shape Element { get { return element; } }

        // Конструктор базового класса
        public DisplayObject(Random random)
        {
            this.random = random; // Инициализация генератора случайных чисел
            CreateShape(); // Вызов абстрактного метода создания фигуры (реализуется в потомках)
            InitializePosition(); // Установка начальной позиции
            InitializeVelocity(); // Установка начальной скорости
            InitializeAcceleration(); // Установка начального ускорения
            isMoving = false; // Изначально фигура неподвижна
            movementType = MovementType.Uniform; // По умолчанию - равномерное движение
        }

        // Абстрактный метод создания фигуры, реализуется в потомках
        protected abstract void CreateShape();

        // Генерация случайного цвета для фигуры
        protected SolidColorBrush GetRandomBrush()
        {
            // Есть два варианта: использовать расширенный набор предустановленных цветов
            // или генерировать полностью случайные цвета

            if (random.Next(2) == 0) // С вероятностью 50% используем предустановленные цвета
            {
                // Расширенный массив красивых цветов для фигур
                Color[] colors = new Color[]
                {
                    Color.FromRgb(255, 87, 51),    // Коралловый
                    Color.FromRgb(52, 152, 219),   // Синий
                    Color.FromRgb(155, 89, 182),   // Фиолетовый
                    Color.FromRgb(46, 204, 113),   // Изумрудный
                    Color.FromRgb(241, 196, 15),   // Желтый
                    Color.FromRgb(231, 76, 60),    // Красный
                    Color.FromRgb(26, 188, 156),   // Бирюзовый
                    Color.FromRgb(230, 126, 34),   // Оранжевый
                    Color.FromRgb(142, 68, 173),   // Темно-фиолетовый
                    Color.FromRgb(41, 128, 185),   // Синяя сталь
                    Color.FromRgb(39, 174, 96),    // Зеленый
                    Color.FromRgb(211, 84, 0),     // Тыквенный
                    Color.FromRgb(22, 160, 133),   // Зеленое море
                    Color.FromRgb(243, 156, 18),   // Оранжевый
                    Color.FromRgb(192, 57, 43),    // Красно-кирпичный
                    Color.FromRgb(127, 140, 141),  // Пепельный
                    Color.FromRgb(44, 62, 80),     // Полуночный синий
                    Color.FromRgb(189, 195, 199),  // Серебристый
                    Color.FromRgb(113, 125, 126),  // Мокрый асфальт
                    Color.FromRgb(91, 44, 111),    // Пурпурный
                    Color.FromRgb(33, 97, 140),    // Морской
                    Color.FromRgb(212, 172, 13),   // Золотой
                    Color.FromRgb(131, 52, 113),   // Сливовый
                    Color.FromRgb(22, 160, 133)    // Изумрудный
                };

                // Выбираем случайный цвет из массива
                return new SolidColorBrush(colors[random.Next(colors.Length)]);
            }
            else // С вероятностью 50% генерируем случайный цвет
            {
                // Метод для создания гармоничных, насыщенных случайных цветов
                byte r = (byte)random.Next(60, 256);
                byte g = (byte)random.Next(60, 256);
                byte b = (byte)random.Next(60, 256);

                // Дополнительная логика для избегания слишком блеклых цветов
                // Как минимум один канал должен быть ярким
                if (r < 100 && g < 100 && b < 100)
                {
                    // Выбираем случайный канал для усиления
                    int channel = random.Next(3);
                    switch (channel)
                    {
                        case 0: r = (byte)random.Next(180, 256); break;
                        case 1: g = (byte)random.Next(180, 256); break;
                        case 2: b = (byte)random.Next(180, 256); break;
                    }
                }

                return new SolidColorBrush(Color.FromRgb(r, g, b));
            }
        }

        // Метод установки начальной позиции фигуры
        protected virtual void InitializePosition()
        {
            // При создании объекта мы не знаем реальные размеры канваса,
            // поэтому установим начальные координаты в 0,0
            // А позже, при первом вызове Update, разместим их случайным образом
            x = 0;
            y = 0;

            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);

            // Флаг, который показывает, что позицию нужно инициализировать при первом Update
            needsInitialPositioning = true;
        }

        // Флаг для отслеживания необходимости начальной расстановки
        protected bool needsInitialPositioning = true;

        // Метод установки начальной скорости
        protected virtual void InitializeVelocity()
        {
            // Генерация случайной начальной скорости
            vx = random.Next(-5, 6);
            vy = random.Next(-5, 6);

            // Проверка, чтобы скорость не была нулевой
            if (vx == 0 && vy == 0)
            {
                vx = 3;
                vy = 3;
            }
        }

        // Метод установки начального ускорения
        protected virtual void InitializeAcceleration()
        {
            // Генерация случайного начального ускорения
            ax = random.NextDouble() * 0.2 - 0.1;
            ay = random.NextDouble() * 0.2 - 0.1;
        }

        // Метод запуска движения фигуры
        public virtual void StartMotion()
        {
            isMoving = true;
        }

        // Метод запуска равномерного движения
        public virtual void StartUniformMotion()
        {
            isMoving = true;
            movementType = MovementType.Uniform;
        }

        // Метод запуска равноускоренного движения
        public virtual void StartAcceleratedMotion()
        {
            isMoving = true;
            movementType = MovementType.Accelerated;
        }

        // Метод остановки движения
        public virtual void StopMotion()
        {
            isMoving = false;
        }

        // Метод обновления состояния фигуры
        public virtual void Update(double canvasWidth, double canvasHeight)
        {
            // Если требуется начальное позиционирование, делаем его здесь
            // когда уже знаем реальные размеры канваса
            if (needsInitialPositioning)
            {
                // Размещаем фигуру в случайном месте по всему пространству канваса
                // с учетом её размеров, чтобы она не выходила за границы
                double maxX = Math.Max(0, canvasWidth - element.Width);
                double maxY = Math.Max(0, canvasHeight - element.Height);

                x = random.NextDouble() * maxX;
                y = random.NextDouble() * maxY;

                Canvas.SetLeft(element, x);
                Canvas.SetTop(element, y);

                needsInitialPositioning = false;
            }

            if (!isMoving) return; // Если фигура неподвижна, ничего не делаем

            UpdatePosition(canvasWidth, canvasHeight); // Обновляем положение фигуры
        }

        // Метод обновления положения фигуры
        protected virtual void UpdatePosition(double canvasWidth, double canvasHeight)
        {
            if (movementType == MovementType.Accelerated)
            {
                // Для ускоренного движения учитываем ускорение
                vx += ax;
                vy += ay;
            }

            // Обновляем координаты
            x += vx;
            y += vy;

            // Проверка на столкновение с границами канваса
            CheckBoundaryCollision(canvasWidth, canvasHeight);

            // Обновляем положение фигуры на канвасе
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }

        // Метод проверки столкновения с границами канваса
        protected virtual void CheckBoundaryCollision(double canvasWidth, double canvasHeight)
        {
            double width = element.Width;
            double height = element.Height;

            // Проверка столкновения с левой или правой границей
            if (x <= 0 || x + width >= canvasWidth)
            {
                vx = -vx; // Инвертируем скорость по оси X

                // Корректируем положение, чтобы фигура не выходила за границы
                if (x <= 0) x = 0;
                if (x + width >= canvasWidth) x = canvasWidth - width;
            }

            // Проверка столкновения с верхней или нижней границей
            if (y <= 0 || y + height >= canvasHeight)
            {
                vy = -vy; // Инвертируем скорость по оси Y

                // Корректируем положение, чтобы фигура не выходила за границы
                if (y <= 0) y = 0;
                if (y + height >= canvasHeight) y = canvasHeight - height;
            }
        }
    }

    // Перечисление для типов движения
    public enum MovementType
    {
        Uniform,    // Равномерное движение
        Accelerated // Ускоренное движение
    }
}