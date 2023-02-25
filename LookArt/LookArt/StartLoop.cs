using LookArt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

public class StartLoop {
	/// <summary>
	/// if false = end of the loop, the object is deprecated.
	/// </summary>
	private bool looping = true;
    /// <summary>
    /// if true = end of one loop
    /// </summary>
    private bool endLoop = false;


	/// <summary>
	/// 30,p10,30,p10 = [30,p10,30,p10]
	/// </summary>
	private string[] loopForm;

    /// <summary>
    /// the folderpath entered by the user
    /// </summary>
	private string folderPath;

    /// <summary>
    /// the current index (position) in the time sequence entered by the user
    /// </summary>
    private int currentIndex=0;


    /// <summary>
    /// Integer used to store the last image index, used to avoid the same image of appearing twice
    /// </summary>
    private int currentRandom;

    /// <summary>
    /// used main window
    /// </summary>
    private MainWindow actuWindow; 

    /// <summary>
    /// list of string containing the path of all the images in the folder selected
    /// </summary>
	private List<string> loopList = new List<string>();

    /// <summary>
    /// constructor of this class, verify if the data entered by the user is valid and if not send the error.
    /// It also initialize the loop
    /// </summary>
    /// <param name="strForm"></param>
    /// <param name="fPath"></param>
    /// <param name="mWindow"></param>
	public StartLoop(string strForm, string fPath, MainWindow mWindow) {
        this.actuWindow = mWindow;
        this.folderPath = fPath;
        actuWindow.GotError("");
        string[] temp = strForm.Split(',');//We check if the content of the str array is valid
		bool error = false;
		foreach (var part in temp)
		{
			for (int i=0;i<part.Length;i++)
			{
				char partChar = part[i];

                if (!((partChar == 'p') | (partChar == '0') | (partChar == '1') | (partChar == '2') | (partChar == '3') | (partChar == '4') | (partChar == '5') | (partChar == '6') | (partChar == '7') | (partChar == '8') | (partChar == '9')))
                {
                    looping = false;
                    endLoop = false;
                    error = true;
					actuWindow.GotError("Please enter a correct time sequence, incorrect value : " + part[i]);

                }
			}
		}
		if (!error)//If the content of the string array of the sequence is valid, we assign it to loopForm and continue
		{
			loopForm = temp;
		}
		try//Folder content part, we get the path of every image of the folder and add it to the list (private attribute) for later
		{
            DirectoryInfo d = new DirectoryInfo(@folderPath);
            String[] extensions = new String[] { "*.jpg", "*.png" };
            foreach (String extension in extensions)
            {
                foreach (var file in d.GetFiles(extension))
                {
                    loopList.Add(file.FullName);
                }
            }
            endLoop = true;
        }
		catch (Exception e)
        {
            actuWindow.GotError("Please define a correct folder path");
            looping = false;
            endLoop = false;
        }
    }

    /// <summary>
    /// looks a lot like the constructor, the goal is to re-initialize the attributes with the new data inputed by the user and verify them.
    /// Also re-initialize the loop
    /// </summary>
    /// <param name="strForm"></param>
    /// <param name="fPath"></param>
    public void ChangeParam(string strForm, string fPath)
    {
        loopList.Clear();
        actuWindow.GotError("");
        currentIndex = 0;
        endLoop = true;
        folderPath = fPath;
        string[] temp = strForm.Split(',');//We check if the content of the str array is valid
        bool error = false;
        foreach (var part in temp)
        {
            for (int i = 0; i < part.Length; i++)
            {
                char partChar = part[i];

                if (!((partChar == 'p') | (partChar == '0') | (partChar == '1') | (partChar == '2') | (partChar == '3') | (partChar == '4') | (partChar == '5') | (partChar == '6') | (partChar == '7') | (partChar == '8') | (partChar == '9')))
                {
                    error = true;
                    actuWindow.GotError("Please enter a correct time sequence, incorrect value : " + part[i]);
                    looping = false;
                    endLoop = false;
                }
            }
        }
        if (!error)//If the content of the string array of the sequence is valid, we assign it to loopForm and continue
        {
            loopForm = temp;
        }
        try//Folder content part, we get the path of every image of the folder and add it to the list (private attribute) for later
        {
            DirectoryInfo d = new DirectoryInfo(@folderPath);
            String[] extensions = new String[] { "*.jpg", "*.png" };
            foreach (String extension in extensions)
            {
                foreach (var file in d.GetFiles(extension))
                {
                    loopList.Add(file.FullName);
                }
            }
        }
        catch (Exception e)
        {
            actuWindow.GotError("Please define a correct folder path");
            looping = false;
            endLoop = false;
        }
    }

    /// <summary>
    /// main function of the class, is repeated until the user click on "stop".
    /// </summary>
    public void Loop() {
        bool isPause = false;
        int currentVal;
        string tempPart = "";
        DispatcherTimer _timer=null;
        TimeSpan _time;
        var part = loopForm[currentIndex];//We get the current value of the time sequence for the timer
        for (int i = 0; i < part.Length; i++)//we check if the value is a pause or not
        {
            if (part[i] == 'p')
            {
                isPause = true;
                tempPart = part.Substring(i + 1);
            }
        }
        if (isPause)// depending on the type of timestamp (pause or image) we change the color of the time counter/show an image
        {
            Brush brush1 = new SolidColorBrush(Colors.CornflowerBlue);
            actuWindow.UpdateChronoColor(brush1);
            try
            {
                currentVal = int.Parse(tempPart);
            }
            catch
            {
                looping = false;
                actuWindow.GotError("Please define a correct time sequence");
                currentVal = 0;
            }

        }
        else
        {
            ShowImage(GetRdmImage());
            Brush brush2 = new SolidColorBrush(Colors.IndianRed);
            actuWindow.UpdateChronoColor(brush2);
            currentVal = int.Parse(part);
        }

        _time = TimeSpan.FromSeconds(currentVal);//the desired time of the timer

        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
        {
            actuWindow.UpdateChrono(_time.ToString(@"mm\:ss"));
            if ((_time == TimeSpan.Zero)&(looping))
            {
                _timer.Stop();
                if (!isPause)
                {
                    HideImage();
                }
                if (currentIndex < loopForm.Length - 1)
                {
                    currentIndex += 1;
                }
                else
                {
                    currentIndex = 0;
                }
                endLoop = true;//we restart the loop if the timer reach 0 to go unto the next part of the time sequence (and we hide the current image for the next one)
            }
            _time = _time.Add(TimeSpan.FromSeconds(-1));
            if (!looping)//if looping=false, then the user clicked on stop. Which mean we stop the loop and reset the chrono/close the image
            {
                _timer.Stop();
                actuWindow.UpdateChrono("00:00");
                if (!isPause)
                {
                    HideImage();
                }
            }
        },
        Application.Current.Dispatcher);

        _timer.Start();
    }

    /// <summary>
    /// get a random index from the folder list loopList and return the full path of the image.
    /// To get a different image, we verify that the new random index is not the same as before.
    /// </summary>
    /// <returns></returns>
    public string GetRdmImage()
    {
        if (loopList?.Any() != true)
        {
            actuWindow.GotError("No image found in the folder");
            looping = false;
            return "";
        }
        else
        {
            try
            {
                Random rnd = new Random();
                String[] filesArray = loopList.ToArray();
                int temp = 0;

                bool findLoop = true;

                while (findLoop)
                {
                    temp = rnd.Next(filesArray.Length);
                    if (temp != currentRandom)
                    {
                        currentRandom = temp;
                        findLoop = false;
                    }
                }

                return filesArray[temp];

            }
            catch
            {
                looping = false;
                actuWindow.GotError("error while trying to get an image");
                return "";
            }
        }
    }

    /// <summary>
    /// show the image using the full path
    /// </summary>
    /// <param name="imgPath"></param>
    public void ShowImage(String imgPath) {
		if (imgPath != "")
		{
			try
			{
                actuWindow.ShowImagePopUp(imgPath);
            }
			catch (Exception e)
            {
                looping = false;
                actuWindow.GotError("error while trying to show the image"); 
            }
        }

    }

    /// <summary>
    /// hide the image
    /// </summary>
	public void HideImage()
	{
		actuWindow.HideImagePopUp();

    }

    /// <summary>
    /// setter/getter of looping
    /// </summary>
	public bool Looping{
		get{
			return looping;
		}
		set{
			looping = value; //set looping to false to stop the loop
		}
	}
    /// <summary>
    /// setter/getter of endLoop
    /// </summary>
    public bool EndLoop
    {
        get { 
            return endLoop; 
        } 
        set { 
            endLoop = value; 
        }
    }

}
