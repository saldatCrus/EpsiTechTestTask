using EpsiTechTestTask.Data;
using EpsiTechTestTask.Models.DTO;
using EpsiTechTestTask.Servises.Exeption;
using EpsiTechTestTask.Servises.Search;
using Microsoft.AspNetCore.Mvc;

namespace EpsiTechTestTask.Controllers
{
    [Route("api/array")]
    [ApiController]
    public class TestEpsiTech : ControllerBase
    {

        /// <summary>
        /// Задание 2: Поиск элемента в массиве
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        [HttpPost("GetArrayWithTargetValue")]
        public async Task<ArrayRequest> GetArrayWithTargetValue(int[] arr, int target)
        {
            ArrayRequest result = new() 
            {
                index = SearchAlgorithm.BinarySearch(arr, target),
                Arr = arr,
            };          
            return result;
        }


    }
}
