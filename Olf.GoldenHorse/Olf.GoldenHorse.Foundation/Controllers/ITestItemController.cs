using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface ITestItemController
    {
        void EditTestItem(TestItem testItem);
        void MinimizeTestItemEditorWindow();
        void RestoreTestItemEditorWindow();
        TestItem CurrentTestItem { get; }
        void EditParameter(OperationParameter parameter);
        void CloseTestItemEditorWindow();
    }
}