using EpsiTechTestTask.Data;
using EpsiTechTestTask.Servises.Exeption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Principal;

namespace EpsiTechTestTask.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _AppDbContext;

        public TaskController(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        /// <summary>
        /// Получение задачи по идентификатору
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        [HttpGet("get")]
        public async Task<Models.Enitity.Task> GetFromId(Guid Id)
        {
            var taskToEdit = await _AppDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == Id)
                ?? throw new AppException($"can`t find task by id:{Id}");

            return taskToEdit;
        }

        /// <summary>
        /// Получение всех задач
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<Models.Enitity.Task[]> GetAll() 
        {
            return await _AppDbContext.Tasks.ToArrayAsync();
        }

        /// <summary>
        /// Редактирование задачи
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(Guid Id, string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new AppException("Name is empty");

            var taskToEdit = await _AppDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == Id)
                ?? throw new AppException($"can`t find task by id:{Id}");

            taskToEdit.Name = name;
            taskToEdit.Description = description;
            taskToEdit.UpdateDate = DateTime.Now.ToUniversalTime();

            await _AppDbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        [HttpPost("remove")]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var taskToRem = await _AppDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == Id) 
                ?? throw new AppException($"can`t find task by id:{Id}");

            _AppDbContext.Tasks.Remove(taskToRem);
            await _AppDbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        [HttpPost("add")]
        public async Task<IActionResult> Add(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) throw new AppException("Name is empty");


            Models.Enitity.Task result = new() 
            {
                Name = name,
                Description = description,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdateDate = DateTime.Now.ToUniversalTime(),

            };

            await _AppDbContext.Tasks.AddAsync(result);
            await _AppDbContext.SaveChangesAsync();

            return Ok();
        }


    }
}
