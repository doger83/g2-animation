using System;

namespace g2.Animation.WPFDesktopApp.ViewModels;
public class FPSUpdateViewModel : FPSViewModel
{

    // ToDo: similar code as in UI viemodel? base method with args?
    public override void UpdateContent()
    {
        framesSinceLastUpdate++;


        if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
        {
            content = $"{framesSinceLastUpdate:n0} fps (Update)";

            framesSinceLastUpdate = 0;
            lastUpdate = DateTime.Now;

            Content = content;
        }
    }
}
