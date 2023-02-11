using System;

namespace g2.Animation.TestWPFDesktopApp.ViewModels;
public class FPSUIViewModel : FPSViewModel
{
    public override void UpdateContent()
    {
        framesSinceLastUpdate++;

        if ((DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
        {
            content = $"{framesSinceLastUpdate:n0} fps (UI)";

            framesSinceLastUpdate = 0;
            lastUpdate = DateTime.Now;

            Content = content;
        }
    }
}
