using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BouncingShapes.Stage;
using System.Windows.Controls;
using BouncingShapes;


namespace LAB1
{
    public class GameManager
    {
        private Canvas gameCanvas;
        private Stage stage;
        private Random random = new Random();

        public GameManager(Canvas canvas)
        {
            gameCanvas = canvas;
            stage = new Stage(canvas);
            InitializeShapes();
        }

        private void InitializeShapes()
        {
            for (int i = 0; i < 10; i++) // 10 фигур каждого типа
            {
                stage.AddShape(new CustomShape(random));
                stage.AddShape(new Square(random));
                stage.AddShape(new Circle(random));
                stage.AddShape(new RectangleShape(random));
            }
        }

        public void Update()
        {
            stage.Update();
        }
        public void StartMotion()
        {
            stage.StartMotion();
        }
        public void StartUniformMotion() => stage.StartUniformMotion();
        public void StartAcceleratedMotion() => stage.StartAcceleratedMotion();
        public void StopMotion() => stage.StopMotion();
        public void StartGame() { /* Логика старта */ 
        
            this.StartMotion();
        }
        public void SaveGame() { /* Сохранение в JSON */ }
        public void LoadGame() { /* Загрузка из JSON */ }
        public void ShowSettings() { /* Настройки */ 
        
    }
        public void PauseOrExit() { /* Пауза или выход */ }
    }
}
