using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using BouncingShapes;
namespace LAB1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer; 

        private GameManager gameManager;

        private WindowState previousWindowState;
        // Для сохранения предыдущего состояния
        private double previousWidth; 
        // Для сохранения предыдущей ширины
        private double previousHeight, previousLeft, previousTop; 
        // Для сохранения предыдущей высоты
        private Point previousPosition; 
        // Для сохранения предыдущей позиции

        public MainWindow()
        {
            InitializeComponent();
            gameManager = new GameManager(GameCanvas);
            CompositionTarget.Rendering += GameLoop; // Игровое поле 
            WindowStyle = WindowStyle.None; // Убираем рамку окна
            WindowState = WindowState.Normal; // Сначала сбрасываем состояние
            Width = SystemParameters.PrimaryScreenWidth; // Ширина всего экрана
            Height = SystemParameters.PrimaryScreenHeight; // Высота всего экрана
            Left = 0; // Позиция слева
            Top = 0; // Позиция сверху
            /*
             gameManager = new GameManager(GameCanvas);
             timer = new DispatcherTimer();
             timer.Interval = TimeSpan.FromMilliseconds(1000 / 60); // 30 FPS
             timer.Tick += GameLoop;
             timer.Start();

         */
        }
        private void GameLoop(object sender, EventArgs e)
        {
            gameManager.Update();
        }
        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Белый":
                        GameCanvas.Background = Brushes.White;
                        break;
                    case "Красный":
                        GameCanvas.Background = Brushes.Red;
                        break;
                    case "Синий":
                        GameCanvas.Background = Brushes.Blue;
                        break;
                    case "Зеленый":
                        GameCanvas.Background = Brushes.Green;
                        break;
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    // Запуск движения по пробелу
                    if(MenuPanel.Visibility == Visibility.Collapsed)
                    {
                        gameManager.StopMotion();
                        MenuPanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        gameManager.StartMotion();
                        MenuPanel.Visibility = Visibility.Collapsed;
                    }
                  
                    e.Handled = true;
                    break;

                case Key.M: // Меню
                    MenuPanel.Visibility = MenuPanel.Visibility == Visibility.Visible
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                    e.Handled = true;
                    break;
                case Key.F11: // Выход из полноэкранного режима
                    ToggleFullScreen();

                    break;
            }


        }

        private void ToggleFullScreen()
        {
            if (WindowState != WindowState.Maximized) // Если не в полноэкранном режиме
            {
                // Сохраняем текущие параметры перед переходом в полный экран
                previousWindowState = WindowState;
                previousWidth = Width;
                previousHeight = Height;
                previousPosition = new Point(Left, Top);

                // Переходим в полноэкранный режим
                WindowStyle = WindowStyle.None; // Убираем рамку окна
                WindowState = WindowState.Maximized;
                ResizeMode = ResizeMode.NoResize; // Отключаем возможность изменения размера
            }
            else // Если уже в полноэкранном режиме
            {
                // Восстанавливаем прежнее состояние
                WindowStyle = WindowStyle.SingleBorderWindow; // Возвращаем стандартную рамку
                WindowState = previousWindowState;
                Width = previousWidth;
                Height = previousHeight;
                Left = previousPosition.X;
                Top = previousPosition.Y;
                ResizeMode = ResizeMode.CanResize; // Восстанавливаем возможность изменения размера
            }
        }

        public class Stage
        {
            private Canvas canvas; // Холст, на котором отображаются фигуры
            private DisplayObject[] shapes; // Массив игровых объектов (фигур)
            private int shapeCount = 0; // Текущее количество добавленных фигур

            private const int MaxShapes = 100; // Максимальное количество фигур

            // Конструктор принимает холст для размещения фигур
            public Stage(Canvas canvas)
            {
                this.canvas = canvas; // Инициализация холста
                shapes = new DisplayObject[MaxShapes]; // Инициализация массива фигур
            }

            // Метод добавляет новую фигуру на поле
            public void AddShape(DisplayObject shape)
            {
                if (shapeCount >= MaxShapes)
                {
                    throw new InvalidOperationException("Превышено максимальное количество фигур.");
                }

                shapes[shapeCount] = shape; // Добавляем фигуру в массив
                shapeCount++; // Увеличиваем счетчик фигур

                if (!canvas.Children.Contains(shape.Element)) // Проверка на дубликат
                {
                    canvas.Children.Add(shape.Element); // Добавление графического элемента на холст
                }
            }

            // Метод запускает движение всех фигур
            public void StartMotion()
            {
                for (int i = 0; i < shapeCount; i++)
                {
                    shapes[i].StartMotion();
                }
            }

            // Метод обновляет состояние всех фигур на поле
            public void Update()
            {
                for (int i = 0; i < shapeCount; i++) // Проходим по всем добавленным фигурам
                {
                    shapes[i].Update(canvas.ActualWidth, canvas.ActualHeight); // Обновляем положение фигуры
                }
            }

            // Метод запускает равномерное прямолинейное движение всех фигур
            public void StartUniformMotion()
            {
                for (int i = 0; i < shapeCount; i++)
                {
                    shapes[i].StartUniformMotion();
                }
            }

            // Метод запускает равноускоренное движение всех фигур
            public void StartAcceleratedMotion()
            {
                for (int i = 0; i < shapeCount; i++)
                {
                    shapes[i].StartAcceleratedMotion();
                }
            }

            // Метод останавливает движение всех фигур
            public void StopMotion()
            {
                for (int i = 0; i < shapeCount; i++)
                {
                    shapes[i].StopMotion();
                }
            }
        }
        private void StartGame_Click(object sender, RoutedEventArgs e) => gameManager.StartGame();
        private void SaveGame_Click(object sender, RoutedEventArgs e) => gameManager.SaveGame();
        private void LoadGame_Click(object sender, RoutedEventArgs e) => gameManager.LoadGame();

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            gameManager.StopMotion();
        }

        private void Settings_Click(object sender, RoutedEventArgs e) => gameManager.ShowSettings();
        private void PauseExit_Click(object sender, RoutedEventArgs e)
        {

            gameManager.PauseOrExit();

            MessageBoxResult result = MessageBox.Show(
                    "Вы уверены, что хотите выйти?",
                    "Выход",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(); // Закрываем приложение
            }
            else
            {
                MenuPanel.Visibility = Visibility.Collapsed; // Просто закрываем меню
            }

        }
    }
}

    


