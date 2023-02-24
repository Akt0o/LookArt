using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LookArt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string AUTHOR = "";

        /// <summary>
        /// main loop class object of the app
        /// </summary>
        StartLoop mainLoop;

        /// <summary>
        /// used to update the chrono on the UI
        /// </summary>
        string isChronoToUpdateText = "00:00";
        /// <summary>
        /// used to update the chrono's color on the UI
        /// </summary>
        Brush isChronoToUpdateColor = new SolidColorBrush(Colors.White);
        /// <summary>
        /// used to change the error message displayed on the UI
        /// </summary>
        string errContent = "";
        /// <summary>
        /// window of the image popup
        /// </summary>
        LookArt.image popup = new LookArt.image();


        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += UpdateLoop;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = 0;
        }

        /// <summary>
        /// loop used to update the UI components from the code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateLoop(object sender, EventArgs e)
        {
            if (timeCounter.Content != isChronoToUpdateText)
            {
                timeCounter.Content = isChronoToUpdateText;
            }
            if (timeCounter.Foreground != isChronoToUpdateColor)
            {
                timeCounter.Foreground = isChronoToUpdateColor;
            }
            if (errorLabel.Content != errContent)
            {
                errorLabel.Content = errContent;
            }

            if (mainLoop is not null)
            {
                if (mainLoop.Looping)
                {
                    startStopButton.Content = "Stop";
                }
                else
                {
                    startStopButton.Content = "Start";
                }
                if (mainLoop.Looping & mainLoop.EndLoop)
                {
                    mainLoop.Loop();
                    mainLoop.EndLoop = false;
                }
            }
        }


        /// <summary>
        /// function used to show the image popup with a specified (absolute) path for the image.
        /// </summary>
        /// <param name="src"></param>
        public void ShowImagePopUp(string src)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@src);
            bitmapImage.DecodePixelWidth = 500;
            bitmapImage.EndInit();
            popup.initImg(bitmapImage);
            popup.Topmost = true;
            popup.WindowStyle = WindowStyle.None;
            popup.Left = 0;
            popup.Top = 0;
            popup.Show();
        }

        /// <summary>
        /// function used to hide the image popup
        /// </summary>
        public void HideImagePopUp()
        {
            popup.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// function called when "start" or "stop" is clicked by the user on the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnStartStopClick(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (mainLoop!=null && mainLoop.Looping)
                {
                   StopLoop();
                }
                else
                {
                    InitLoop();
                }
            }
            catch
            {
                InitLoop();
            }

        }


        /// <summary>
        /// function used to stop the loop of the time sequence entered by the user
        /// </summary>
        public void StopLoop()
        {
            mainLoop.Looping = false;
            startStopButton.Content = "Start";
            Brush brush = new SolidColorBrush(Colors.White);
            UpdateChronoColor(brush);
        }


        /// <summary>
        /// function used to initialize the loop with the parameters entered by the user on the UI
        /// </summary>
        public void InitLoop()
        {
            if (sequenceTextBox.Text.Length == 0)
            {
                errorLabel.Content = "Please define the time sequence";
            }
            else if (folderTextBox.Text.Length == 0)
            {
                errorLabel.Content = "Please define the folder path for the images";
            }
            else
            {
                errorLabel.Content = "";
                if (mainLoop is null)
                {
                    mainLoop = new StartLoop(sequenceTextBox.Text, folderTextBox.Text, this);
                }
                else
                {
                    mainLoop.ChangeParam(sequenceTextBox.Text, folderTextBox.Text);
                    mainLoop.Looping = true;
                }
            }
        }


        /// <summary>
        /// function used to update the chrono on the UI
        /// </summary>
        /// <param name="time">string containing the new text for the chrono</param>
        public void UpdateChrono(string time)
        {
            isChronoToUpdateText = (time);
        }


        /// <summary>
        /// function used to update the chrono's color on the UI 
        /// </summary>
        /// <param name="color">brush used to change the color</param>
        public void UpdateChronoColor(Brush color)
        {
            isChronoToUpdateColor = color;
        }


        /// <summary>
        /// function used to display an error message on the UI 
        /// </summary>
        /// <param name="err"></param>
        public void GotError(String err)
        {
            errContent = err;
            if (mainLoop is not null)
            {
                mainLoop.Looping = false;
            }
            Brush brush = new SolidColorBrush(Colors.Red);
            UpdateChronoColor(brush);
        }


        /// <summary>
        /// function called when the user click the "next image" button, change the current image to another random one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnNext(object sender, RoutedEventArgs e)
        {
            if (mainLoop is not null)
            {
                if (popup.Visibility != Visibility.Hidden)
                {
                    mainLoop.ShowImage(mainLoop.GetRdmImage());
                }
            }
        }


        /// <summary>
        /// override of the "OnClosed" function, used to close the app : the image window was causing it to stay active despite the main window being close
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

    }
}
