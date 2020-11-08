
namespace test_project.tasks
{
    public interface ITask {
        void Init(params string[] args);
        void Run();
    }
}