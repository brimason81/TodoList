using TodoList.Models;

namespace TodoList.store
{
    public interface ITodoStore
    {
       
        void AddOrUpdateTodoItem(TodoItem item);
        void DeleteTodo(int id);
        List<TodoItem> GetAllTodos();
        int GetItemId();
        string GetOrCreateDirectory();
        string GetPathForItem(string item);
        TodoItem GetTodo(string path);
        TodoItem GetTodoById(int id);
        void HandleIsDone(string item);
    }
}