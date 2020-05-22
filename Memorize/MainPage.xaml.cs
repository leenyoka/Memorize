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
using System.Text;

namespace Memorize
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("The goal of the game is to test your ability to remember sequences of random things");
            builder.AppendLine("All you need to do is watch the screen and then simulate what you saw by ");
            builder.AppendLine("touching the right thing or moving in the right direction");

            MessageBox.Show(builder.ToString(), "Memorize", MessageBoxButton.OK);

        }
    }
}