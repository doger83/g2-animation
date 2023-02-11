//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace g2.Animation.WPFDesktopApp.ViewModels;
//internal class FPSCounterViewModel : INotifyPropertyChanged
//{
//    private DateTime lastUpdate;
//    private uint framesSinceLastUpdate;

//    public FPSCounterViewModel()
//    {
//        lastUpdate = DateTime.Now;
//        framesSinceLastUpdate = 0;
//    }

//    private string fps_UI = string.Empty;
//    public string FPS_UI
//    {
//        get
//        {
//            return fps_UI;
//        }

//        set
//        {
//            fps_UI = value;
//            // ToDo: Make FPS Counter work and use Timer from Time class
//            // ToDo: put in wpf project only calculation here
//            NotifyPropertyChanged(nameof(FPS_UI));
//        }
//    }

//    private string fps_Update = string.Empty;
//    public string FPS_Update
//    {
//        get
//        {
//            return fps_Update;
//        }

//        set
//        {
//            fps_Update = value;
//            // ToDo: Make FPS Counter work and use Timer from Time class
//            // ToDo: put in wpf project only calculation here
//            NotifyPropertyChanged(nameof(FPS_Update));
//        }
//    }

//    private string dateAndTime = string.Empty;
//    public string DateAndTime
//    {
//        get
//        {
//            return DateTime.Now.ToString();
//        }
//        set
//        {
//            dateAndTime = value;
//            NotifyPropertyChanged(nameof(DateAndTime));
//        }
//    }

//    public void Update()
//    {
//        // ToDo: Wenn Counter die UI Updated stottert es?
//        framesSinceLastUpdate++;
//        if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
//        {
//            fps_UI = $"{framesSinceLastUpdate:n0} fps (UI)";
//            fps_Update = $"{framesSinceLastUpdate:n0} fps (Update)";
//            dateAndTime = DateTime.Now.ToString();

//            framesSinceLastUpdate = 0;
//            lastUpdate = DateTime.Now;

//            FPS_UI = fps_UI;
//            FPS_Update= fps_Update;
//            DateAndTime= dateAndTime
//                ;
//        }
//    }

//    public event PropertyChangedEventHandler? PropertyChanged;

//    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
//    {
//        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//    }
//}
