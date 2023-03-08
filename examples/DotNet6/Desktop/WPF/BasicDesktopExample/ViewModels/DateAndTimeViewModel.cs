using System;

namespace g2.Animation.TestWPFDesktopApp.ViewModels;
public class DateAndTimeViewModel : FPSViewModel   // ToDo: FPSViewModel ? it is more a updatetable? or sthlt   , put timing of second and the labels in fixed update calls bzw put functionality in time class to reduze amount of abstract base classe between label an viewmodel
{
    public override void UpdateContent()
    {
        framesSinceLastUpdate++;

        if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
        {
            content = DateTime.Now.ToString();

            framesSinceLastUpdate = 0;
            lastUpdate = DateTime.Now;

            Content = content;
        }
    }
}
