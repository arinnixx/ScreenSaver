using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class Form1 : Form
    {
        private readonly List<Snowflake> snowflakes = new List<Snowflake>();
        private readonly Random random = new Random();
        private readonly Image villageImage = Properties.Resources.village;
        private readonly Image snowflakeImage = Properties.Resources.snow;
        private readonly System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 100;
        }

        private void CreateSnowflakes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = random.Next(0, this.Width),
                    Y = random.Next(-this.Height, 0),
                    Size = random.Next(10, 40),
                    Speed = random.Next(2, 8)
                });
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Обновление позиций снежинок
            foreach (var snowflake in snowflakes)
            {
                snowflake.Y += snowflake.Speed;

                // Если снежинка ушла за нижнюю границу, перемещаем её наверх
                if (snowflake.Y > this.Height)
                { 
                    snowflake.Y = -snowflake.Size;
                    snowflake.X = random.Next(0, this.Width);
                }
            }

            // Перерисовка формы
            this.Invalidate();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Создание снежинок
            CreateSnowflakes(120);

            timer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем снежинки
            foreach (var snowflake in snowflakes)
            {
                var rect = new Rectangle(
                    (int)snowflake.X,
                    (int)snowflake.Y,
                    snowflake.Size,
                    snowflake.Size
                );
                e.Graphics.DrawImage(snowflakeImage, rect);
            }
        }
    }
}