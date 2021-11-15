using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OscarSnake
{
    public partial class Form1 : Form
    {
        bool lose = false;
        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;
        int win = 1;
        Graphics gfx;
        Food food;
        List<SNAKEPIECE> snake;
        Bitmap canvas;
        Random generator = new Random();

        public Form1()
        {
            ClientSize -= new Size(ClientSize.Width % 20, ClientSize.Height % 20);
            InitializeComponent();

            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(canvas);

            snake = new List<SNAKEPIECE>();
            snake.Add(new SNAKEPIECE(40, 40, 20));


            food = new Food(100, 100, 20, 20);


        }

        Stopwatch watch = new Stopwatch();

        private void timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(Color.Black);

            if (snake[0].x > ClientSize.Width || snake[0].x < 0 || snake[0].y < 0 || snake[0].y > ClientSize.Height)
            {
                lose = true;
            }
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[0].HitBox.IntersectsWith(snake[i].HitBox))
                {
                    lose = true;
                }
            }
            if (lose)
            {
                lose = false;
                score.Text = $"{win}"; 
                win = 1;
                revive.Visible = true;
                yes.Visible = true;
                no.Visible = true;
                pictureBox2.Visible = true;
                timer1.Enabled = false;

                snake[0].direction = Direction.None;
                left = false;
                right = false;
                down = false;
                up = false;
            }
            for (int i = 0; i < snake.Count; i++)
            {
                snake[i].SnakeDirection();
            }

            for (int i = snake.Count - 1; i > 0; i--)
            {
                //each snake piece direction should change to be the direction of the snake piece in front of it
                snake[i].direction = snake[i - 1].direction;
            }

            if (snake[0].HitBox.IntersectsWith(food.HitBox))
            {
                timer1.Enabled = false;

                QuestionForm qForm = new QuestionForm();
                DialogResult res = qForm.ShowDialog();

                countDownTimer.Enabled = true;

                //timer1.Enabled = true;

                int x = generator.Next(0, ClientSize.Width - 20);
                int y = generator.Next(0, ClientSize.Height - 20);

                food.x = x - (x % 20);
                food.y = y - (y % 20);


                if (res == DialogResult.OK || res == DialogResult.Yes)
                {
                    for (int i = 0; i < snake.Count - 1; i++)
                    {
                        if (food.x > snake[i].x && food.x < snake[i].x + snake[i].width || food.y > snake[i].y && food.y < snake[i].y + snake[i].width)
                        {
                            x = generator.Next(0, ClientSize.Width - snake[i].width);
                            y = generator.Next(0, ClientSize.Height - snake[i].width);

                            food.x = x - (x % snake[i].width);
                            food.y = y - (y % snake[i].width);
                            i = 0;
                        }
                    }
                    SNAKEPIECE tail = snake[snake.Count - 1];
                    if (tail.direction == Direction.Left)
                    {
                        snake.Add(new SNAKEPIECE(tail.x + 20, tail.y, 20));
                    }
                    else if (tail.direction == Direction.Up)
                    {
                        snake.Add(new SNAKEPIECE(tail.x, tail.y + 20, 20));
                    }
                    else if (tail.direction == Direction.Down)
                    {
                        snake.Add(new SNAKEPIECE(tail.x, tail.y - 20, 20));
                    }
                    else if (tail.direction == Direction.Right)
                    {
                        snake.Add(new SNAKEPIECE(tail.x - 20, tail.y, 20));
                    }
                    snake[snake.Count - 1].direction = tail.direction;
                    win++;
                    score.Text = $"{win}";
                }


            }


            for (int i = 0; i < snake.Count; i++)
            {
                snake[i].Draw(gfx);
            }
            food.Draw(gfx);
            pictureBox1.Image = canvas;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && !right)
            {
                snake[0].direction = Direction.Left;
                left = true;
                up = false;
                down = false;
            }
            if (e.KeyCode == Keys.Right && !left)
            {
                snake[0].direction = Direction.Right;
                right = true;
                up = false;
                down = false;
            }
            if (e.KeyCode == Keys.Down && !up)
            {
                snake[0].direction = Direction.Down;
                down = true;
                right = false;
                left = false;
            }
            if (e.KeyCode == Keys.Up && !down)
            {
                snake[0].direction = Direction.Up;
                up = true;
                right = false;
                left = false;
            }
        }

        private void yes_Click(object sender, EventArgs e)
        {
            //shrink the snake to 1 item
            //set snake and food to starting positions
            //hide the end screen
            //start timer again
            snake.Clear();
            snake.Add(new SNAKEPIECE(40, 40, 20));
            food = new Food(100, 100, 20, 20);
            pictureBox2.Visible = false;
            revive.Visible = false;
            yes.Visible = false;
            no.Visible = false;
            timer1.Enabled = true;
        }

        private void no_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(canvas);
            ClientSize -= new Size(ClientSize.Width % 20, ClientSize.Height % 20);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            images.Add(Properties.Resources.ThreeScaled);
            images.Add(Properties.Resources.twoScaled);
            images.Add(Properties.Resources.oneSnakeMusicScaled);

            box.Size = new Size(200, 200);
            box.Location = new Point(pictureBox1.Width / 2 - Properties.Resources.ThreeScaled.Width / 2, pictureBox1.Height / 2 - Properties.Resources.ThreeScaled.Height / 2);
            pictureBox1.Controls.Add(box);
            box.Visible = false;
            box.SizeMode = PictureBoxSizeMode.AutoSize;
            box.BackColor = Color.Green;

            countDownTimer.Enabled = false;
        }

        List<Image> images = new List<Image>();
        int index = 0;

        PictureBox box = new PictureBox();
        private void countDownTimer_Tick(object sender, EventArgs e)
        {
            if (index >= images.Count)
            {
                timer1.Enabled = true;
                countDownTimer.Enabled = false;
                box.Visible = false;
                index = 0;
                return;
            }

            Image cur = images[index];
            index++;


            box.Location = new Point(pictureBox1.Width / 2 - box.Width / 2, pictureBox1.Height / 2 - box.Height / 2);
            box.Visible = true;
            box.Image = cur;

        }
    }
}