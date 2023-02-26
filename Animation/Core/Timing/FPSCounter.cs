using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace g2.Animation.Core.Timing;

public class FPSCounter : INotifyPropertyChanged
{
    // ToDo: LastUpdate aus Time classe erhalten?
    private DateTime lastUpdate;
    private DateTime lastFixedUpdate;
    private uint framesSinceLastUpdate;
    private uint framesSinceLastFixedUpdate;

    // ToDo: Use Time class for messering FPS Counter. 
    public FPSCounter()
    {
        lastUpdate = DateTime.Now;
        framesSinceLastUpdate = 0;
    }

    private string fps = "xxx fps (Update)";
    public string FPS
    {
        get => fps;

        set
        {
            fps = value;
            // ToDo: use Timer from Time class
            // ToDo: put in wpf project only calculation here
            NotifyPropertyChanged(nameof(FPS));
        }
    }

    private string fixedFps = "xxx fps (FixedUpdate)";
    public string FixedFPS
    {
        get => fixedFps;

        set
        {
            fixedFps = value;
            // ToDo: use Timer from Time class
            // ToDo: put in wpf project only calculation here
            NotifyPropertyChanged(nameof(FixedFPS));
        }
    }
    public void FixedUpdate()
    {
        // ToDo: Wenn Counter die UI Updated stottert es?
        DateTime actualFixedUpdate = DateTime.Now;
        framesSinceLastFixedUpdate++;

        if ((actualFixedUpdate - lastFixedUpdate).TotalMilliseconds >= 1000)
        {
            fixedFps = $"{framesSinceLastFixedUpdate:n0} fps (FixedUpdate)";

            framesSinceLastFixedUpdate = 0;
            lastFixedUpdate = actualFixedUpdate;

            FixedFPS = fixedFps;
        }
    }

    internal void Update()
    {
        // ToDo: Wenn Counter die UI Updated stottert es?
        DateTime actualUpdate = DateTime.Now;

        framesSinceLastUpdate++;

        if ((actualUpdate - lastUpdate).TotalMilliseconds >= 1000)
        {
            fps = $"{framesSinceLastUpdate:n0} fps (Update)";

            framesSinceLastUpdate = 0;
            lastUpdate = actualUpdate;

            FPS = fps;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
