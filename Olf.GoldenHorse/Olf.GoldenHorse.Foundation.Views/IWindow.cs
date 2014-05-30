namespace Olf.GoldenHorse.Foundation.Views
{
    public interface IWindow : IViewWithDataContext
    {
        void Show();
        void Close();
        void Maximize();
        void Minimize();
        void Restore();
    }
}