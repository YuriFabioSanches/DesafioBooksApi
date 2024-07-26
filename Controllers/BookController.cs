using Microsoft.AspNetCore.Mvc;
using DesafioBooksApi.Entities;
using DesafioBooksApi.Communication.Request;
using DesafioBooksApi.Communication.Response;

namespace DesafioBooksApi.Controllers;

public class BookController : DesafioBooksApiBaseController
{
    private static List<BookEntitie> BookList = [];

    [HttpGet]
    [ProducesResponseType(typeof(List<BookEntitie>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult GetAll()
    {
        return BookList.Count > 0 ? Ok(BookList) : NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string),  StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] RequestCreateBookJson request)
    {
        if (request.Title == "" || request.Author == "" || request.StockCount < 0 || request.Price < 0)
        {
            return BadRequest("Please inform Title, Author, Stock Count or Price valid");
        }

        var newBook = new BookEntitie 
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            GenreType = request.GenreType,
            Price = request.Price,
            StockCount = request.StockCount
        };

        BookList.Add(newBook);

        var response = new ResponseCreatedBookJson { Id = newBook.Id };

        return Created(string.Empty, response);
    }

    [HttpPut ("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] RequestUpdateBookJson request)
    {
        foreach (var book in BookList)
        {
            if (book.Id.Equals(id))
            {
                book.Title = request.Title == "" ? book.Title : request.Title;
                book.Author = request.Author == "" ? book.Author : request.Author;
                book.GenreType = request.GenreType;
                book.Price = request.Price < 0 ? book.Price : request.Price;
                book.StockCount = request.StockCount < 0 ? 0 : request.StockCount;
                return NoContent();
            }
        }
        return BadRequest("Book Not Found");
    }

    [HttpDelete ("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        foreach (var book in BookList) 
        {
            if (book.Id.Equals(id))
            {
                BookList.Remove(book);
                return Ok("Book Removed");
            }
        }
        return BadRequest("Book Not Found");
    }
}
