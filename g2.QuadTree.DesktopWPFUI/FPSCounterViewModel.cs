using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace g2.Datastructures.DesktopWPFUI;
public class FPSCounterViewModel : INotifyPropertyChanged
{

    DateTime lastUpdate;
    uint framesSinceLastUpdate;
    Canvas canvas;

    public FPSCounterViewModel(Canvas canvas)
    {
        lastUpdate = DateTime.Now;
        framesSinceLastUpdate = 0;
        this.canvas = canvas;
    }


    private String fpsCounter = String.Empty;
    public String FPSCounter
    {
        get { return fpsCounter; }
        set
        {
            fpsCounter = value;
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

    protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));
    }

 
}
