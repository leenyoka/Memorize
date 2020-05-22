using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Memorize
{
    public class Game
    {
        GameState1 gameState;

        public GameState1 GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }
        public Game()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int number = random.Next(1, 9);

            questions.Add(number);

            gameState = Memorize.GameState1.Questioning;
        }
        public Game(int limit)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int number = random.Next(1, limit);

            questions.Add(number);

            gameState = Memorize.GameState1.Questioning;
        }

        public void AddQuestion(int limit)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int number = random.Next(1, limit);

            questions.Add(number);
        }

        List<int> questions = new List<int>();

        public List<int> Questions
        {
            get { return questions; }
            set { questions = value; }
        }
    }
    public enum GameState1
    {
        Questioning, Answering
    }
}
