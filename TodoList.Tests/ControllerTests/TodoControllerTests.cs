using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Controllers;
using TodoList.Models;
using TodoList.store;
using Xunit;

namespace TodoList.Tests.ControllerTests
{
    // Assertions 
    // test actions that are going to be objects
    // test View model for what it is returning
    public class TodoControllerTests
    {
        private TodoController _todoController;
      
        private ITodoStore _todoStore;
        public TodoControllerTests()
        {
            // dependancy - mocking repository
            _todoStore = A.Fake<ITodoStore>();

            // SUT - system under test
            _todoController = new TodoController(_todoStore);
        }
        [Fact]
        // naming convention - class_actionMethod_expectedResult
        public void TodoController_Index_ReturnsSuccess()
        {
            // Arrange - what do i need to bring in
            var todos = A.Fake<List<TodoItem>>();
            A.CallTo(() => _todoStore.GetAllTodos()).Returns(todos);
            // act
            var result = _todoController.Index();
            // assert - object check, actions, view model
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public void TodoController_Add_ReturnsSuccess() 
        {
            var result = _todoController.Add();

            result.Should().BeOfType<ViewResult>();
        }
        [Fact]
        public void TodoController_Add_ReturnsView()
        {
            var todo = A.Fake<TodoItem>();
            
            //var idResult = _todoStore.GetItemId();
            //idResult.Should().BeOfType(typeof(int));
            
            A.CallTo(() => _todoStore.GetItemId()).Returns(todo.Id);
            A.CallTo(() => _todoStore.AddOrUpdateTodoItem(todo));

            var result = _todoController.Add(todo);
            result.Should().BeOfType<ViewResult>();
        }
        [Fact]
        public void TodoController_Edit_ReturnsTodoItem() {
            // arrange what do i need to bring in
            var todo =  A.Fake<TodoItem>();
            A.CallTo(() => _todoStore.GetTodoById(1)).Returns(todo);

            // act
            var result = _todoController.Edit(1);

            //assert
            result.Should().BeOfType<ViewResult>();
        }
       
        
    }
}
