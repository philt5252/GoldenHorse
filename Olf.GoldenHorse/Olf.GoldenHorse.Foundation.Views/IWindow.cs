namespace Olf.GoldenHorse.Foundation.Views
{
    public interface IWindow : IViewWithDataContext
    {
        void Show();
        bool? ShowDialog();
        void Close();
        void Minimize();
        void Maximize();
        void Restore();
    }
}