using System.ComponentModel;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ILogShellViewModel 
    {
        ILogDetailsViewModel LogDetailsViewModel { get; }
    }
}