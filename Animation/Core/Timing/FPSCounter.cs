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

    private string fps = "xxx fps (Update)";
    public string FPS
    {
        get
        {
            return fps;
        }

        set
        {
            fps = value;
            // ToDo: use Timer from Time class
            // ToDo: put in wpf project only calculation here
            NotifyPropertyChanged(nameof(FPS));
        }
    }

    public void UpdateContent()
    {
        // ToDo: Wenn Counter die UI Updated stottert es?
        framesSinceLastUpdate++;
        if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
        {
            fps = $"{framesSinceLastUpdate:n0} fps (Update)";

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
