using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TheControlTower
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int MaxCharactersInput = 30;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendAirplaneButton_OnClick(object sender, RoutedEventArgs e)
        {
            string flightCode = flightCodeInput.Text;
            if (!IsValidInput(flightCode))
            {
                MessageBox.Show($"Flight code can not be empty and have to be less than {MaxCharactersInput} characters");
                return;
            }

            CreateNewAirplane(flightCode);
            string flightEvent = "Sent to runway";
            FlightEventInfo sendToRunWayEvent = new FlightEventInfo(flightCode, flightEvent);
            OneNewFlightEvent(this, sendToRunWayEvent);
            flightCodeInput.Text = "";
        }

        /// <summary>
        /// Check if input is valid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsValidInput(string input)
        {
            return !String.IsNullOrEmpty(input) && input.Length < MaxCharactersInput;
        }

        /// <summary>
        /// Create a new FlightWindow and subscribe to it's events
        /// </summary>
        /// <param name="flightCode"></param>
        private void CreateNewAirplane(string flightCode)
        {
            FlightWindow flight = new FlightWindow(flightCode);
            flight.Show();
            flight.TakeOff += OneNewFlightEvent;
            flight.ChangeRoute += OneNewFlightEvent;
            flight.Land += OneNewFlightEvent;
        }

        /// <summary>
        /// When a new flight event has happened this will add that event to list view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="flightEventInfo"></param>
        private void OneNewFlightEvent(object sender, FlightEventInfo flightEventInfo)
        {
            controlTowerList.Items.Add(flightEventInfo);
        }
    }
}
