using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace g2.Animation.WPFDesktopApp.ViewModels;
internal class FPSCounterViewModel : INotifyPropertyChanged
{
    public FPSCounterViewModel()
    {
    }


    private string fps = string.Empty;
    public string FPS
    {
        get
        {
            return fps;
        }

        set
        {
            fps = value;
            // ToDo: Make FPS Counter work and use Timer from Time class
            // ToDo: put in wpf project only calculation here
            NotifyPropertyChanged(nameof(FPS));
        }
    }

    private string sekCounter = string.Empty;
    public string SekCounter
    {
        get
        {
            return DateTime.Now.ToString();
        }
        set
        {
            sekCounter = value;
            NotifyPropertyChanged(nameof(SekCounter));
        }
    }





    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
