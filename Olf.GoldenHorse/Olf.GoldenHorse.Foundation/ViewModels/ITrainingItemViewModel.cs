

using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Properties;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public enum TrainingItemStatus
    {
        NotCompleted,
        Completed
    };

    public interface ITrainingItemViewModel
    {
        TrainingItemStatus Status { get; set; }
        TestItem TestItem { get; }
        string Description { get; }
    }
}