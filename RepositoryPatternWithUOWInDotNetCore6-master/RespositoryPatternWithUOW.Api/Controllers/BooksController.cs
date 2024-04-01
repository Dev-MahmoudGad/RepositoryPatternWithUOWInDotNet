using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RespositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {

            return Ok(_unitOfWork.Books.GetById(1));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var x = _unitOfWork.Books.GetAll();
            var t = x.Select(a => new { a.AuthorId ,a.Title }).ToList().GroupBy(a => a.AuthorId);

            //var results = (from d in x
            //               group d.AuthorId by d.Title into g
            //               select new 
            //               {
            //                   Department = g.Key.ToString(),
            //                   DeptCount = g.Count()
            //               }).ToList();

            return Ok(_unitOfWork.Books.GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_unitOfWork.Books.Find(b => b.Title == "New Book", new[] { "Author" }));
        }

        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors(string bookName)
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains(bookName), new[] { "Author" }));
        }

        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered(string bookName)
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains(bookName), null, null, b => b.Id, OrderBy.Descending));
        }

        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            var book = _unitOfWork.Books.Add(new Book { Title = "Test 4", AuthorId = 1 });
            _unitOfWork.Complete();
            return Ok(book);
        }
    }
}