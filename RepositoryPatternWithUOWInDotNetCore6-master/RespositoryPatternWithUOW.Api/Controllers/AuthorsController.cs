using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RespositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetById(int? Id)
        {
            if (Id == null)
                return BadRequest();

            var author = _unitOfWork.Authors.GetById(Id.Value);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            var x = _unitOfWork.Authors.GetAll();
            var y = _unitOfWork.Authors.GetAll().GetEnumerator().Current;
            var z = _unitOfWork.Authors.GetAll().GetEnumerator().MoveNext;
            //ArrayList arr = new ArrayList();
            int[] aa = new int[3] { 1, 20, 3 };
            int[] hh = new int[] { 44, 55, 66 };
            List<object> nn = new List<object>()
            {
                //new { a1 = "a1", b1 = 111, c1 = "c1" },
                //new { a2 = "a2", b2 = 222, c2 = "c2" },
                //new { a3 = "a3", b3 = 333, c3 = "c3" }
            }
            .DefaultIfEmpty(new { a1 = "def1", b1 = 111, c1 = "def1" }).ToList();

            List<int> bb = new List<int>()
            { 500,600};

            bb.Add(10);
            bb.Add(20);
            bb.Add(30);
            bb.Add(40);
            var dataCompare = bb.Union(aa).ToList();
            var data = bb.Concat(aa).ToList();
            var data2 = bb.Clear;

            Stack<int> count = new Stack<int>();
            count.Push(10);
            count.Push(20);
            count.Push(30);
            count.Push(40);

            var dic = new Dictionary<int, string>()
            {
                { 1 , "Ali"},
                { 2 , "Mostafa"},
                { 3 , "mohamed"},
            };
            return Ok(await _unitOfWork.Authors.GetByIdAsync(1));
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(Author obj)
        {
            var author = await _unitOfWork.Authors.AddAsync(obj);
            var x = _unitOfWork.Complete();
            return Ok(author);
        }
    }

    sealed public class ggg
    {
        public ggg()
        {

        }
        public int id { get; set; }
        public string name { get; set; }
        public double salary { get; set; }
    }


    abstract public class abc
    {
        public abc()
        {

        }
        abstract public int getint();
        public int id { get; set; }
        public string name { get; set; }
        public double salary { get; set; }
    }
}