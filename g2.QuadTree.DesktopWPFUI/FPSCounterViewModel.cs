using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace g2.Datastructures.DesktopWPFUI;
public class FPSCounterViewModel : INotifyPropertyChanged
{
    // ToDo: LastUpdate aus Time classe erhalten?
    private DateTime lastUpdate;
    private uint framesSinceLastUpdate;
    private Canvas canvas;

    // ToDo: Use Time class for messering FPS Counter. 
    // ToTo: Use Ticks and GetTimestamp() continuesly for measuring time like her using frametime before and actual
    public FPSCounterViewModel(Canvas canvas)
    {
        this.lastUpdate = DateTime.Now;
        this.framesSinceLastUpdate = 0;
        this.canvas = canvas;
    }


    private String fpsCounter = String.Empty;
    public String FPSCounter
    {
        get { return this.fpsCounter; }
        set
        {
            this.fpsCounter = value;
            NotifyPropertyChanged(nameof(FPSCounter));
            NotifyPropertyChanged(nameof(SekCounter));
        }
    }

    public String SekCounter
    {
        get { return DateTime.Now.ToString(); }
    }

    public void Draw()
    {
        // ToDo: Wenn Counter die UI Updated stottert es und es kann zu einem glitch kommen wenn dies genau im moment der Bande passierts. Auch wenn der FPS COunt nicht gezeichnet wird!
        this.framesSinceLastUpdate++;
        if ((DateTime.Now - this.lastUpdate).TotalMilliseconds >= 1000)
        {
            this.fpsCounter = $"{this.framesSinceLastUpdate:n0} fps";

            this.framesSinceLastUpdate = 0;
            this.lastUpdate = DateTime.Now;

            FPSCounter = this.fpsCounter;
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        var handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
