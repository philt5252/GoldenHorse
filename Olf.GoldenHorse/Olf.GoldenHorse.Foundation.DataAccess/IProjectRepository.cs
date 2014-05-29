using System.Xml.Linq;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.DataAccess
{
    public interface IProjectRepository
    {
        Project Open(string filePath);
        void Save(Project project);
    }
}