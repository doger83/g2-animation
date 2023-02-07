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
    public FPSCounterViewModel(Canvas canvas)
    {
        lastUpdate = DateTime.Now;
        framesSinceLastUpdate = 0;
        this.canvas = canvas;
    }


    private String fpsCounter = String.Empty;
    public String FPSCounter
    {
        get => fpsCounter;
        set
        {
            fpsCounter = value;
            NotifyPropertyChanged(nameof(FPSCounter));
            NotifyPropertyChanged(nameof(SekCounter));
        }
    }

    public String SekCounter => DateTime.Now.ToString();

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

    protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
