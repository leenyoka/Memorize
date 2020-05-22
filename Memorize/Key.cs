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

namespace Memorize
{
    public class Key: Grid
    {
        int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        state state;

        public state State
        {
            get { return state; }
            set { state = value; }
        }
        Click click;

        public Click Click
        {
            get { return click; }
            set { click = value; }
        }

    }
    public enum state
    {
        highlighted, normal
    }
    public enum Click
    {
        Activated, Deactivated
    }
}
