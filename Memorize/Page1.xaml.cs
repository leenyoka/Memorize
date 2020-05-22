using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Memorize
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            timeKeeper = new DispatcherTimer();
            timeKeeper.Interval = new TimeSpan(0, 0, 1);
            timeKeeper.Tick += new EventHandler(DispatcherTimerStep);

            HightController = new DispatcherTimer();
            HightController.Interval = new TimeSpan(0, 0, 0, 0, 500);
            HightController.Tick += new EventHandler(ClearHighlights);

            GameController = new DispatcherTimer();
            GameController.Interval = new TimeSpan(0, 0, 0, 0, 800);
            GameController.Tick += new EventHandler(Control);
            seconds = 0;
            game = new Game();
            CurrentQuestion = 1;
            questioning = 0;
            answers = new List<int>();
            
        }
        DispatcherTimer timeKeeper;
        DispatcherTimer HightController;
        DispatcherTimer GameController;
        int CurrentQuestion;

        public void Control(object o, EventArgs e)
        {
            if (game.GameState == GameState1.Questioning)
            {
                foreach (Key key in Keys.Children)
                {
                    //if (key.State == state.highlighted)
                    //{
                    if (key.Number == game.Questions[CurrentQuestion -1])
                    {
                        key.State = state.normal;

                        if (key.Number > 4)
                        {
                            key.Background = getMyImage("three");
                        }
                        else
                        {
                            key.Background = getMyImage("Four");
                        }
                    }
                    //}

                }
                HightController.Start();
            }
            CurrentQuestion++;
            if (CurrentQuestion > game.Questions.Count)
            {
                game.GameState = GameState1.Answering;

                //YourTurn.Visibility = System.Windows.Visibility.Visible;
            }
        }
        int questioning;


        public void ClearHighlights(object o, EventArgs e)
        {

            foreach (Key key in Keys.Children)
            {
                //if (key.State == state.highlighted)
                //{
                key.State = state.normal;

                if (key.Number > 4)
                {
                    key.Background = getMyImage("One");
                }
                else
                {
                    key.Background = getMyImage("Two");
                }
                //}



            }
            if (game.GameState == GameState1.Answering)
            {
                YourTurn.Visibility = System.Windows.Visibility.Visible;
            }
            HightController.Stop();
            //else
            //{
            //    HightCounter--;
            //}
        }
        List<int> answers;

        public bool AnsersRightSoFar()
        {
            bool yes = true;

            for(int m =0; m < answers.Count;m++)
            {
                if (answers[m] != game.Questions[m])
                {
                    yes = false;
                    break;
                }
            }
            return yes;
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            timeKeeper.Start();
            //HightController.Start();
            GameController.Start();
            initializeKeys();
        }
        Game game;
        public void initializeKeys()
        {
            foreach (Key key in Keys.Children)
            {
                key.State = state.normal;
                key.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(KeyClicked);
                key.Click = Click.Deactivated;

            }
        }
        public void KeyClicked(object o, System.Windows.Input.GestureEventArgs e)
        {
            
            Key key = (Key)o;

            if (game.GameState == GameState1.Answering)
            {
                key.State = state.highlighted;

                if (key.Number > 4)
                {
                    key.Background = getMyImage("three");
                }
                else
                {
                    key.Background = getMyImage("Four");
                }

                answers.Add(key.Number);
                questioning++;

                bool rightanswers = AnsersRightSoFar();
                
                if (!rightanswers)
                {
                    MessageBox.Show("You lose", "Incorrect", MessageBoxButton.OK);
                    NavigationService.GoBack();
                }
                else if (questioning >= game.Questions.Count)
                {
                    //MessageBox.Show("Start questioning again");
                    Random random = new Random(DateTime.Now.Millisecond);
                    game.Questions.Add(random.Next(1, 9));
                    game.GameState = GameState1.Questioning;
                    YourTurn.Visibility = System.Windows.Visibility.Collapsed;
                    CurrentQuestion = 1;
                    questioning = 0;
                    answers.Clear();

                    btnHitCount.Content = game.Questions.Count.ToString();
                }
                
                

                HightController.Start();
            }
        
        }
        int seconds;
        public void DispatcherTimerStep(object o, EventArgs e)
        {
            seconds++;
            showTime(seconds);
        }
        public void showTime(int seconds)
        {
            int myseconds = seconds;
            int myminutes = 0;
            int myhours = 0;
            string temptime = "";

            do
            {
                if (myseconds >= 60)
                {
                    myminutes += 1;
                    myseconds -= 60;
                }

            }
            while (myseconds >= 60);


            do
            {
                if (myminutes >= 60)
                {
                    myhours += 1;
                    myminutes -= 60;
                }
            }
            while (myminutes >= 60);



            if (myhours > 0)
            {
                temptime = prepare(myhours.ToString()).ToString() + ":" + prepare(myminutes.ToString()).ToString() +
                    ":" + prepare(myseconds.ToString()).ToString();
            }
            else
            {
                temptime = prepare(myminutes.ToString()).ToString() +
                    ":" + prepare(myseconds.ToString()).ToString();
            }

            btnTime.Content = temptime;



        }

        public string prepare(string tempvalue)
        {
            string thisvalue = "";
            if (tempvalue.Length < 2)
            {
                thisvalue = "0" + tempvalue;
            }
            if (tempvalue.Length == 2)
            {
                thisvalue = tempvalue;
            }

            if (tempvalue.Length > 2)
            {
                thisvalue = tempvalue;
            }

            return thisvalue;
        }

        public ImageBrush getMyImage(string choice)
        {
            ImageBrush bi = new ImageBrush();
            bi.ImageSource = new BitmapImage(new Uri(@"../Images/" + choice + ".png", UriKind.Relative));
            //bi.Stretch = Stretch.Fill;
            //bi.Stretch = Stretch.Uniform;

            //return new Uri((@"pack://application:,,/Brian's%20Bubbles;component/Images/" + choice + ".png"));
            return bi;
        }
        
    }
}