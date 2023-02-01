using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace g2.Datastructures.DesktopWPFUI;
public class FPSCounterViewModel : INotifyPropertyChanged
{

    DateTime lastUpdate;
    uint framesSinceLastUpdate;

    public FPSCounterViewModel()
    {
        lastUpdate = DateTime.Now;
        framesSinceLastUpdate = 0;
    }
 

    private String fpsCounter = String.Empty;
    public String FPSCounter
    {
        get { return fpsCounter; }
        set
        {
            fpsCounter = value;
            NotifyPropertyChanged();
        }
    }

    public void Draw()
    {
        framesSinceLastUpdate++;
        if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
        {
            fpsCounter = $"  {framesSinceLastUpdate} fps";

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
