using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.store;
namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoStore _todoStore;

        public TodoController(TodoStore store)
        {
            _todoStore = store;
        }
        public IActionResult Index()
        {
            var todos = _todoStore.GetAllTodos();
            return View(todos);
        }
        public IActionResult Add() {
            return View();
        }
        [HttpPost]
        public IActionResult Add(TodoItem todo) {
            // todo.Id = 0;
            if (ModelState.IsValid) { 
              _todoStore.AddOrUpdateTodoItem(todo);
               return View();
            }
            return View(todo);
        }
        [Route("/Todo/Delete/{item}")]
        public IActionResult Delete(string item) {
            _todoStore.DeleteTodo(item);
            return RedirectToAction("Index");
        }
        [Route("/Todo/HandleIsDone/{item}")]
        public IActionResult HandleIsDone(string item) {
            _todoStore.HandleIsDone(item);
            return RedirectToAction("Index");
        }
    }
}
