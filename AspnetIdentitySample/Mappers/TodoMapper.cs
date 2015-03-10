using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoManager.TodoServiceReference;
using ToDoManager.Models;

namespace ToDoManager.Mappers
{
    public static class TodoMapper
    {

        public static ToDoItem GetItem(ToDoModel model, int userId)
        {
            return new ToDoItem()
            {
                ToDoId = model.Id,
                UserId = userId,
                Name = model.Description,
                IsCompleted = model.IsDone,
                IsCompletedSpecified = true,
                ToDoIdSpecified = true,
                UserIdSpecified = true
            };
        }

        public static ToDoModel GetModel(ToDoItem item)
        {
            return new ToDoModel()
            {
                Id = item.ToDoId,
                Description = item.Name,
                IsDone = item.IsCompleted
            };
        }

    }
}