using TodoList.Models;

namespace TodoList.store
{
    public interface ITodoStore
    {
       
        void AddOrUpdateTodoItem(TodoItem item);
        void DeleteTodo(int id);
        List<TodoItem> GetAllTodos();
        int GetItemId();
        TodoItem GetTodoById(int id);
        void HandleIsDone(string item);
    }
}