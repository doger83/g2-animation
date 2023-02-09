using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace g2.Animation.Core.Timing;
public class FPSCounter : INotifyPropertyChanged
{
    // ToDo: LastUpdate aus Time classe erhalten?
    private DateTime lastUpdate;
    private uint framesSinceLastUpdate;


    // ToDo: Use Time class for messering FPS Counter. 
    public FPSCounter()
    {
        lastUpdate = DateTime.Now;
        framesSinceLastUpdate = 0;
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
            // ToDo: Make FPS Counter work
            // ToDo: put in wpf project only calculation here
            NotifyPropertyChanged(nameof(FPS));
            NotifyPropertyChanged(nameof(SekCounter));
        }
    }

    public string SekCounter
    {
        get
        {
            return DateTime.Now.ToString();
        }
    }

    public void Draw()
    {
        // ToDo: Wenn Counter die UI Updated stottert es?
        framesSinceLastUpdate++;
        if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
        {
            fps = $"{framesSinceLastUpdate:n0} fps";

            framesSinceLastUpdate = 0;
            lastUpdate = DateTime.Now;

            FPS = fps;
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
