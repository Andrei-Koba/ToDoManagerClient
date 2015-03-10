using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoManager.TodoServiceReference;
using ToDoManager.Mappers;
using System.Threading;

using ToDoManager.Models;

namespace ToDoManager.Services
{
	public class ToDoItemService : IToDoItemService
	{
		// this is fake solution! !NEVER TRY TO ACCESS DB FROM SERVICES WITHOUT A REASON!
		private static IList<ToDoModel> _db = new List<ToDoModel>();
		private IUserRetrieverService _userService;
        private ToDoManager.TodoServiceReference.ToDoManager _client;

		public ToDoItemService(IUserRetrieverService userService)
		{
			_userService = userService;
            _client = new TodoServiceReference.ToDoManager();
		}

		public Task<IEnumerable<ToDoModel>> GetAll()
        {
            int userId = _userService.GetCurrentUser().UserId;
            IEnumerable<ToDoModel> res = _client.GetTodoList(userId,true).Select(TodoMapper.GetModel);
			return Task.FromResult(res);
		}

        public delegate void CreateDeleg(ToDoItem item);

		public async Task<ToDoModel> Create(ToDoModel todo)
		{
            int userId = _userService.GetCurrentUser().UserId;
			ToDoItem res = TodoMapper.GetItem(todo,userId);
            return await Task.Run(() => { _client.CreateToDoItem(res); return todo; });
		}

		public Task<ToDoModel> GetById(int id)
		{
            int userId = _userService.GetCurrentUser().UserId;
            ToDoItem res = _client.GetTodoList(userId, true).FirstOrDefault(x => x.ToDoId == id);
            ToDoModel result = TodoMapper.GetModel(res);
			return Task.FromResult(result);
		}

		public async Task<bool> RemoveById(int id)
		{

            return await Task.Run(() => { _client.DeleteToDoItem(id, true); return true; });
		}

		public async Task<ToDoModel> Update(ToDoModel todo)
		{
            int userId = _userService.GetCurrentUser().UserId;
            return await Task.Run(() => { _client.UpdateToDoItem(TodoMapper.GetItem(todo, userId)); return todo; });
		}
	}
}