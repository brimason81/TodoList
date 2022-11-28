using Google.Apis.Json;
using TodoList.Models;
namespace TodoList.store
{
    public class TodoStore : ITodoStore
    {
        private string _path { get; set; }
        private readonly NewtonsoftJsonSerializer _serializer;

        public TodoStore()
        {
            _path = @"wwwroot";
            _serializer = new NewtonsoftJsonSerializer();
        }
        public int GetItemId()
        {
            int id;
            if (GetAllTodos().Count() < 1)
            {
                id = 1;
            }
            else
            {
                id = GetAllTodos().Count() + 1;
            }
            return id;
        }
        public void AddOrUpdateTodoItem(TodoItem item)
        {

            if (item.Item != null)
            {
                //if (GetTodoById(item.Id) != null) {
                //    DeleteTodo(item.Id);
                //}
                var file = GetPathForItem(item.Item);

                var value = _serializer.Serialize(item);
                File.WriteAllText(file, value);

            }
        }
        public string GetOrCreateDirectory()
        {
            var folder = Path.Combine(_path, "TodoItems");
            if (!Directory.Exists(folder)) { Directory.CreateDirectory(folder); }
            return folder;
        }
        public string GetPathForItem(string item)
        {
            var folder = GetOrCreateDirectory();
            var fileName = Path.Combine(folder, $"{item}.txt");
            return fileName;
        }
        public List<TodoItem> GetAllTodos()
        {
            var todos = new List<TodoItem>();
            string dir = GetOrCreateDirectory();
            foreach (var file in Directory.GetFiles(dir, "*.txt"))
            {
                var item = GetTodo(file);
                if (item != null) { todos.Add(item); }
            }
            return todos;
        }
        public TodoItem GetTodo(string path)
        {
            var content = File.ReadAllText(path);
            return _serializer.Deserialize<TodoItem>(content);
        }
        public TodoItem GetTodoById(int id)
        {
            TodoItem item = new TodoItem();
            foreach (var todo in GetAllTodos())
            {
                if (todo.Id == id)
                {
                    item = todo;
                }
            }
            return item;
        }
        public void DeleteTodo(int id)
        {
            var todo = GetTodoById(id);
            var file = GetPathForItem(todo.Item);
            if (File.Exists(file)) File.Delete(file);
        }
        public void HandleIsDone(string item)
        {
            var path = GetPathForItem(item);
            var todo = GetTodo(path);
            todo.IsDone = todo.IsDone == false ? true : false;
            AddOrUpdateTodoItem(todo);
        }
    }
}
