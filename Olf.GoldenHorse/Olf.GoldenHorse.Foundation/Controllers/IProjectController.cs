namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface IProjectController
    {
        void New();
        void Open();
        void Create(string projectPath, string project);
    }
}