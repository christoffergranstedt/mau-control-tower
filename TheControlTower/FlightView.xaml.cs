using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TheControlTower
{
    /// <summary>
    /// Interaction logic for FlightWindow.xaml
    /// </summary>
    public partial class FlightWindow : Window
    {
        private string flightCode;
        public FlightWindow(string flightCode)
        {
            InitializeComponent();
            this.Title = flightCode;
            this.flightCode = flightCode;
            changeRouteBox.ItemsSource = new string[] { "10", "20", "30", "40", "50", "60", "70" };
            ShowAirplaneImage();
        }

        public event EventHandler<FlightEventInfo> TakeOff;
        public event EventHandler<FlightEventInfo> ChangeRoute;
        public event EventHandler<FlightEventInfo> Land;

        /// <summary>
        /// Display airplane image by getting a randomnumber between 1-3 and display on of three possible images
        /// </summary>
        private void ShowAirplaneImage()
        {
            try
            {
                int randomPictureNumber = new Random().Next(1, 4);
                string imageFilePath;
                if (Debugger.IsAttached)
                {
                    var enviroment = System.Environment.CurrentDirectory;
                    imageFilePath = $"{Directory.GetParent(enviroment)?.Parent.Parent.FullName}/Assets/Airplane_{randomPictureNumber}.jpg";
                }

                // If executable, an Assets folder will need to created in same directory as .exe
                else
                {
                    imageFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}/Assets/Airplane_${randomPictureNumber}.jpg";
                }

                ImageSource image = new BitmapImage(new Uri(imageFilePath));
                imageAirplane.Source = image;
            }
            catch (Exception)
            {
                MessageBox.Show("Could not find the image");
            }

        }


        /// <summary>
        /// When start button is clicked, invoke TakeOff Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeOffButton_Click(object sender, RoutedEventArgs e)
        {
            string flightEvent = "Started";

            FlightEventInfo takeOffInfo = new FlightEventInfo(flightCode, flightEvent);
            OnTakeOff(takeOffInfo);
            takeOffButton.IsEnabled = false;
            changeRouteBox.IsEnabled = true;
            landButton.IsEnabled = true;
        }

        /// <summary>
        /// When degree is changed, invoke change route event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeRouteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string flightDegree = changeRouteBox.SelectedItem.ToString();
            string flightEvent = $"Now heading {flightDegree} deg";

            FlightEventInfo changeRouteInfo = new FlightEventInfo(flightCode, flightEvent);
            OnChangeRoute(changeRouteInfo)
                ;
        }

        /// <summary>
        /// When land button is clicked, invoke land event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void landButton_Click(object sender, RoutedEventArgs e)
        {
            string flightEvent = "Landed";
            FlightEventInfo landInfo = new FlightEventInfo(flightCode, flightEvent);
            OnLand(landInfo);
            changeRouteBox.IsEnabled = false;
            landButton.IsEnabled = false;
            this.Close();
        }

        /// <summary>
        /// Event handler method for take off event
        /// </summary>
        /// <param name="takeOffInfo"></param>
        public void OnTakeOff(FlightEventInfo takeOffInfo)
        {
            if (TakeOff != null)
                TakeOff(this, takeOffInfo);
        }

        /// <summary>
        /// Event handler method for change route event
        /// </summary>
        /// <param name="takeOffInfo"></param>
        public void OnChangeRoute(FlightEventInfo changeRouteInfo)
        {
            if (ChangeRoute != null)
                ChangeRoute(this, changeRouteInfo);
        }

        /// <summary>
        /// Event handler method for land event
        /// </summary>
        /// <param name="takeOffInfo"></param>
        public void OnLand(FlightEventInfo landInfo)
        {
            if (Land != null)
                Land(this, landInfo);
        }
    }
}
