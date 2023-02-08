using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace g2.Animation.DesktopWPFUI;
public class FPSCounterViewModel : INotifyPropertyChanged
{
    // ToDo: LastUpdate aus Time classe erhalten?
    private DateTime lastUpdate;
    private uint framesSinceLastUpdate;


    // ToDo: Use Time class for messering FPS Counter. 
    public FPSCounterViewModel()
    {
        lastUpdate = DateTime.Now;
        framesSinceLastUpdate = 0;
    }


    private string fpsCounter = string.Empty;
    public string FPSCounter
    {
        get
        {
            return fpsCounter;
        }

        set
        {
            fpsCounter = value;
            NotifyPropertyChanged(nameof(FPSCounter));
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
            fpsCounter = $"{framesSinceLastUpdate:n0} fps";

            framesSinceLastUpdate = 0;
            lastUpdate = DateTime.Now;

            FPSCounter = fpsCounter;
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
