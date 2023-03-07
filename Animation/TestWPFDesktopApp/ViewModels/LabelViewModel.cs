namespace g2.Animation.TestWPFDesktopApp.ViewModels;
public abstract class LabelViewModel : ViewModelBase
{
    protected string content = string.Empty;
    public string Content
    {
        get => content;

        set
        {
            content = value;
            NotifyPropertyChanged();
        }
    }

    public abstract void UpdateContent();
}
