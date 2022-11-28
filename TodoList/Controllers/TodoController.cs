using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.store;
namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoStore _todoStore;

        public TodoController(ITodoStore store)
        {
            _todoStore = store;
        }
        public IActionResult Index()
        {
            var todos = _todoStore.GetAllTodos();
            return View(todos);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(TodoItem todo)
        {
            todo.Id = _todoStore.GetItemId();
            if (ModelState.IsValid)
            {
                _todoStore.AddOrUpdateTodoItem(todo);
                return View();
            }
            return View(todo);
        }
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var todo = _todoStore.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem todo)
        {
            if (ModelState.IsValid)
            {
                _todoStore.AddOrUpdateTodoItem(todo);
                return View();
            }
            return RedirectToAction("Index");
        }

        //[Route("/Todo/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _todoStore.DeleteTodo(id);
            return RedirectToAction("Index");
        }
        //[Route("/Todo/HandleIsDone/{item}")]
        public IActionResult HandleIsDone(string item)
        {
            _todoStore.HandleIsDone(item);
            return RedirectToAction("Index");
        }
    }
}
