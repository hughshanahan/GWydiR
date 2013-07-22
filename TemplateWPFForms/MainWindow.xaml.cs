using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GWydiR;

namespace TemplateWPFForms
{

    public delegate void ChangedButtonHandler(object sender, EventArgs e, string SID);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event ChangedButtonHandler ButtonEventHandler;

        /// <summary>
        /// Method to call when a button changes, this might be unnessesary and bloaty. Will have to remove this while documenting probably so
        /// as to make it simler for people to understand. Remember to include discussion on MVVM pattern.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="SID"></param>
        protected virtual void onButtonChange(EventArgs e, string SID)
        {
            // check that there are events registered
            Console.WriteLine("innermethod");
            if (ButtonEventHandler != null)
                // call event handler
                ButtonEventHandler(this, e, SID);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("handler");
            onButtonChange(e, textBox1.Text);
        }
    }
}
