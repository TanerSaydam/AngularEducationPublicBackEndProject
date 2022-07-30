using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaseTermsController : ControllerBase
    {
        private readonly ILeaseTermService _leaseTermService;
        private readonly IBookService _bookService;

        public LeaseTermsController(ILeaseTermService leaseTermService, IBookService bookService)
        {
            _leaseTermService = leaseTermService;
            _bookService = bookService;
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _leaseTermService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getListByBook")]
        public IActionResult GetListByBook(int bookId)
        {
            var result = _leaseTermService.GetListByBook(bookId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _leaseTermService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(LeaseTerm leaseTerm)
        {
            var result = _bookService.RentABook(leaseTerm);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(LeaseTerm leaseTerm)
        {
            var result = _leaseTermService.Update(leaseTerm);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(LeaseTerm leaseTerm)
        {
            var result = _bookService.DeleteRent(leaseTerm);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("returnBook")]
        public IActionResult ReturnBook(Book book)
        {
            var result = _bookService.ReturnBook(book);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
