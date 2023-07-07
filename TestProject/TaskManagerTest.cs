using LAB5_tests.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = LAB5_tests.Models.Task;
using Xunit;

namespace LAB5_tests
{
    internal class TaskManagerTest : IClassFixture<Program>
    {
        private TaskManager _taskManager;
        private TaskDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;
            _dbContext = new TaskDbContext(options);
            _taskManager = new TaskManager(_dbContext);

        }



        [Test]
        public void AddTask_ShouldIncreaseTaskCount()
        {
            
            // Arrange
            var task = new Task("Test task");
            // Act
            _taskManager.AddTask(task);
            // Assert
            Assert.AreEqual(1, _taskManager.GetTasks().Count);
        }

        [Test]
        public void MarkTaskAsCompleted_ExistingTask_ShouldMarkTaskAsCompleted()
        {
            // arrange
            var task = new Task("Completion Test Task");
            _taskManager.AddTask(task);

            // act
            _taskManager.MarkTaskAsCompleted(task.Id);

            // assert

            Assert.AreEqual(true, task.IsCompleted);
        }

        [Test]
        public void MarkTaskAsCompleted_NonExistingTask_ShouldNotMarkTaskAsCompleted()
        {
            //arrange
            var task = new Task("Completion Nonexisting Task Test"); 
            _taskManager.AddTask(task);

            var completionCheck = _taskManager.MarkTaskAsCompleted(0);
            Assert.IsFalse(completionCheck);
        }




    }
}
