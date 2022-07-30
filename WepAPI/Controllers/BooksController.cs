using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        

       [HttpGet("getBookRentCountList")]
        public IActionResult GetBookRentCountList()
        {
            var result = _bookService.GetBookRentCountList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            var result = _bookService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getByGuid")]
        public IActionResult GetByGuid(string guid)
        {
            var result = _bookService.GetByGuid(guid);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Book book)
        {
            var result = _bookService.Add(book);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("addImage")]
        public IActionResult AddImage(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + ".jpeg";
            if (file.Length > 0)
            {
                FtpWebRequest request =
        (FtpWebRequest)WebRequest.Create("ftp://www.angulareducation.com/bookshopping.angulareducation.com/assets/img/" + fileName);
                request.Credentials = new NetworkCredential("fpt kullanıcı adı", "ftp şifre");
                request.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream ftpStream = request.GetRequestStream())
                {
                    file.CopyTo(ftpStream);
                }

                FileDto fileDto = new FileDto();
                fileDto.fileName = fileName;


                return Ok(fileDto);
            }
            return BadRequest("Bir hatayla karşılaştık. Lütfen yöneticinize danışın");           

        }

        [HttpPost("update")]
        public IActionResult Update(Book book)
        {
            var result = _bookService.Update(book);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Book book)
        {
            var result = _bookService.Delete(book);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
