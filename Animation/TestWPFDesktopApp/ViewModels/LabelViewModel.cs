namespace g2.Animation.TestWPFDesktopApp.ViewModels;
public abstract class LabelViewModel : ViewModelBase
{
    protected string content = string.Empty;
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
