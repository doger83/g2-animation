using System;

namespace g2.Animation.WPFDesktopApp.ViewModels;

public abstract class FPSViewModel : LabelViewModel
{
    protected DateTime lastUpdate;
    protected uint framesSinceLastUpdate;
}