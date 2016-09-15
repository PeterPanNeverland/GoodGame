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
using System.Windows.Media.Animation;
using Game_Main;

namespace SnakeTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum Direction { Stop, Up, Down, Left, Right }
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
    class Player
    {
        public double posY;
        public double posX;
        public int sizeX, sizeY;
        public int JumpProgress;// 0 - no jump, 1-9
        public Image image;
        public Direction direction;
        private int v1;
        private int v2;
        private int v3;
        private int v4;

        

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
        private int v1;
        private int v2;
        private int v3;
        private int v4;

        /* public Barrel(int v1, int v2, int v3, int v4)
         {
             this.v1 = v1;
             this.v2 = v2;
             this.v3 = v3;
             this.v4 = v4;
         }

         public Barrel()
         {
         }*/
        
        public void Animate()
        {

        }
    }
    class GameStatus
    {
        // state of the game
        public static Player Player;
        public static Barrel Barrel;
        public static DonkeyKong DonkeyKong;

        public int ScorePoints;
        public string PlayerName;
    }
    public partial class MainWindow : Window
    {
        

        private Ellipse snake_head { get; set; }
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

        public partial class AnimatingControl : UserControl
        {
            public AnimatingControl()
            {
                
                Loaded += AnimatingControl_Loaded;
            }

            private void AnimatingControl_Loaded(object sender, RoutedEventArgs e)
            {
                Storyboard sb = (this.FindResource("sbAnimateImage") as Storyboard);
                sb.Begin();
            }
        }

        public MainWindow()
        {

            InitializeComponent();

            this.DataContext = this;

            GameStatus.Player = new Player();
            GameStatus.Player.posX = 50;
            GameStatus.Player.posY = 700;
            Canvas.SetTop(this.Player, 700);
            Canvas.SetLeft(this.Player, 50);
            Player.Width =100;
            Player.Height =100;


            GameStatus.Barrel = new Barrel();
            GameStatus.Barrel.posX = 10;
            GameStatus.Barrel.posY = 500;
            GameStatus.Barrel.sizeX = 50;
            GameStatus.Barrel.sizeY = 50;
            Canvas.SetTop(this.Barrel, 500);
            Canvas.SetLeft(this.Barrel, 10);
            Barrel.Width = 47;
            Barrel.Height = 39;

            GameStatus.DonkeyKong = new DonkeyKong();
            GameStatus.DonkeyKong.posX = 100;
            GameStatus.DonkeyKong.posY = 500;
            GameStatus.DonkeyKong.sizeX = 40;
            GameStatus.DonkeyKong.sizeY = 40;
            Canvas.SetTop(this.DonkeyKong, 500);
            Canvas.SetLeft(this.DonkeyKong, 100);
            DonkeyKong.Width = 40;
            DonkeyKong.Height = 40;

            this.DataContext = this;

            this.time_multiplier = 10;
            this.rnd = new Random();
            Image pepsi = new Image();
            this.window_height = (Int32)this.Height;
            this.window_width = (Int32)this.Width;
            this.KeyDown += new KeyEventHandler(image_KeyDown);

            Image character = new Image();
            Ellipse snake_head = new Ellipse();
            snake_head.Width = 8;
            snake_head.Height = 8;
            character.Width = 16;
            character.Height = 16;



            this.tick = new Timer();
            this.tick.Interval = 100 / this.time_multiplier;
            this.tick.Elapsed += tick_Elapsed;
            this.tick.Start();
        }




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
                        GameStatus.Player.posY = top - 1 * speed_multiplier;
                        break;
                    case 1:
                        Canvas.SetTop(Player, top + 1 * speed_multiplier);
                        GameStatus.Player.posY = top + 1 * speed_multiplier;
                        break;
                    case 2:
                        Canvas.SetLeft(Player, left + 1 * speed_multiplier);
                        GameStatus.Player.posX = left + 1 * speed_multiplier;
                        break;
                    case 3:
                        Canvas.SetLeft(Player, left - 1 * speed_multiplier);
                        GameStatus.Player.posX = left - 1 * speed_multiplier;
                        break;
                    default:
                        Canvas.SetTop(Player, top + 0 * speed_multiplier);
                        break;
                }

                //Collision detection

                Rect Player1 = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);
                Rect Barrel1 = new Rect(Canvas.GetLeft(Barrel), Canvas.GetTop(Barrel), Barrel.Width, Barrel.Height);
                Rect Biff = new Rect(Canvas.GetLeft(DonkeyKong), Canvas.GetTop(DonkeyKong), DonkeyKong.Width, DonkeyKong.Height);
                if (Player1.IntersectsWith(Barrel1) || Player1.IntersectsWith(Biff))

                {
                    GameStatus.Player.direction = Direction.Stop;
                    GameStatus.Player.posX = 10;
                    GameStatus.Player.posY = 680;
                    Canvas.SetTop(this.Player, 680);
                    Canvas.SetLeft(this.Player, 10);
                    
                    ScoreWindow ScoreWindow = new ScoreWindow();
                    ScoreWindow.Show();
                   
                }
                
               
                if (GameStatus.Player.posY == 700)
                {
                    direction = 5;
                }

                bool gameOver;
                if (GameStatus.Player.posX < 0)
                {
                    left = 0;
                    gameOver = true;
                }
                if (GameStatus.Barrel.posY < 0)
                {
                    top = 0;
                    gameOver = true;
                }
                if (GameStatus.Player.posX > paintCanvas.Width)
                {
                    left = 0;
                    gameOver = true;
                }
                if (GameStatus.Barrel.posY > paintCanvas.Height)
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

        /*
        private bool CheckCollision()
        {
            int i = 0;
            foreach (Image pepsi in )
            {
                if (MainWindow.CheckCollision(pepsi, character))
                {
                    this.paintCanvas.Children.Remove(pepsi);
                    return true;
                }
                i++;
            }
            return false;
        }
        */
      
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
                case Key.Down:
                    direction = 1;
                    break;
                case Key.Right:
                    direction = 2;
                    break;
                case Key.Left:
                    direction = 3;
                    break;
                default:
                case Key.Space:
                    direction = 5;
                    break;
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
