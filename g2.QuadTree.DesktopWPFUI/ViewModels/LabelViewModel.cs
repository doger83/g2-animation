using System;

namespace g2.Animation.WPFDesktopApp.ViewModels;
public abstract class LabelViewModel : ViewModelBase
{
    protected string content = String.Empty;
    public string Content
    {
        get
        {
            return content;
        }

        set
        {
            content = value;
            RaisePropertyChanged();
        }
    }

    public abstract void UpdateContent();
}
