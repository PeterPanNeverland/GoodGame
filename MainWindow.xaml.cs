using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeTest
{

    public enum Direction { Stop, Up, Down, Left, Right }

    class Player
    {
        public double posY;
        public double posX;
        public int sizeX, sizeY;
        public int JumpProgress;// 0 - no jump, 1-9
        public Image image;
        public Direction direction;
        public void Animate()
        {
        }
    }

    class Barrel
    {
        public int posY;
        public int posX;
        public int sizeX, sizeY;
        public Image image;
        public Direction direction;
        public void Animate()
        {
            
        }
    }

    class DonkeyKong
    {
        public double posX;
        public double posY;
        public int sizeX, sizeY;
        public Image image;
        public Direction direction;
        public void Animate()
        {

        }
    }

    class GameStatus {
    // state of the game
        public static Player Player;
        public static DonkeyKong DonkeyKong;
        public static List<Barrel> BarrelList;
        public int ScorePoints;
        public string PlayerName;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        private Image biff { get; set; }
        private Image pepsi { get; set; }
        private Image character { get; set; }
        


        private Timer tick { get; set; }
        private Random rnd;

        private int direction = 5;
        private int speed_multiplier = 5;
        private int time_multiplier = 1;
        private int tick_counter = 0;
        private int window_width;
        private int window_height;

        
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            GameStatus.Player = new Player();
            GameStatus.Player.posX = 750;
            GameStatus.Player.posY = 10;

            GameStatus.DonkeyKong = new DonkeyKong();
            GameStatus.DonkeyKong.posX = 200;
            GameStatus.DonkeyKong.posY = 220;

            this.time_multiplier = 10;
            this.rnd = new Random();
            Image pepsi = new Image();
            this.window_height = (Int32)this.Height;
            this.window_width = (Int32)this.Width;
            this.KeyDown += new KeyEventHandler(image_KeyDown);

            Image character = new Image();
            Image biff = new Image();
            biff.Width = 8;
            biff.Height = 8;
            character.Width = 16;
            character.Height = 16;

            Canvas.SetTop(this.Player, 750);
            Canvas.SetLeft(this.Player, 10);

            Canvas.SetTop(this.DonkeyKong, 200);
            Canvas.SetLeft(this.DonkeyKong, 220);


            this.tick = new Timer();
            this.tick.Interval = 100 / this.time_multiplier;
            this.tick.Elapsed += tick_Elapsed;
            this.tick.Start();

        }
        /*
        private void moveBiff()
        {
            double topBiff = Canvas.GetTop(this.Biff);
            double leftBif = Canvas.GetLeft(this.Biff);
            Canvas.SetTop(Biff, topBiff + 1 * speed_multiplier);
        }
        */
        private void tick_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            
            this.tick.Stop();
            this.tick.Interval = 100 / this.time_multiplier;

            this.Dispatcher.Invoke((Action)(() =>
            {
                double top = Canvas.GetTop(this.Player);
                double left = Canvas.GetLeft(this.Player);
                

                switch (direction)
                {
                    case 0:
                        Canvas.SetTop(Player, top - 1 * speed_multiplier);
                        GameStatus.Player.posX = top - 1 * speed_multiplier;
                        break;
                    case 1:
                        Canvas.SetTop(Player, top + 1 * speed_multiplier);
                        GameStatus.Player.posX = top + 1 * speed_multiplier;
                        break;
                    case 2:
                        Canvas.SetLeft(Player, left + 1 * speed_multiplier);
                        GameStatus.Player.posY = left + 1 * speed_multiplier;
                        break;
                    case 3:
                        Canvas.SetLeft(Player, left - 1 * speed_multiplier);
                        GameStatus.Player.posY = left - 1 * speed_multiplier;
                        break;
                    default:
                        Canvas.SetTop(Player, top + 0 * speed_multiplier);
                        break;
                }
                if (GameStatus.Player.posX == 220 &&  GameStatus.Player.posY == 200)
                {
                    MessageBox.Show("YOU FUCKING SUCK");
                }
                bool gameOver;
                if (GameStatus.Player.posX < 0)
                {
                    left = 0;
                    gameOver = true;
                }
                if (GameStatus.DonkeyKong.posY < 0)
                {
                    top = 0;
                    gameOver = true;
                }
                if (GameStatus.Player.posX > paintCanvas.Width)
                {
                    left = 0;
                    gameOver = true;
                }
                if (GameStatus.DonkeyKong.posY > paintCanvas.Height)
                {
                    top = 0;
                    gameOver = true;
                }
                



                this.tick_counter++;
                /*
                if (this.CheckCollision())
                    this.AteAPepsi();
                    */

                this.tick.Start();
            }));
        }
        private void AteAPepsi()
        {
            //add pts to score
        }
        
        
        public static bool CheckCollision(Image e1, Image e2)
        {
            var r1 = e1.ActualWidth / 2;
            var x1 = Canvas.GetLeft(e1) + r1;
            var y1 = Canvas.GetTop(e1) + r1;
            var r2 = e2.ActualWidth / 2;
            var x2 = Canvas.GetLeft(e2) + r2;
            var y2 = Canvas.GetTop(e2) + r2;
            var d = new Vector(x2 - x1, y2 - y1);
            return d.Length <= r1 + r2;
        }



        private void image_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Up:
                    direction = 0;
                    break;
                case Key.W:
                    direction = 0;
                    break;
                case Key.Down:
                    direction = 1;
                    break;
                case Key.S:
                    direction = 1;
                    break;
                case Key.Right:
                    direction = 2;
                    break;
                case Key.D:
                    direction = 2;
                    break;
                case Key.Left:
                    direction = 3;
                    break;
                case Key.A:
                    direction = 3;
                    break;
                case Key.Space:
                    direction = 5;
                    break;
                default:
                    direction = 1;
                    break;
            }
            
        }


        private void image_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(Player);
        }
        public void MoveCharacter(int x, int y)
        {
            Canvas.SetTop(Player, x);
            Canvas.SetLeft(Player, y);
        }

        private void paintCanvas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void image_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}