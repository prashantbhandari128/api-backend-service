using BackendService.DataAccess.Entities;
using BackendService.DataAccess.UnitOfWork.Interface;
using BackendService.Models.Request;
using BackendService.Models.Response.Template.Operation;
using BackendService.Models.Response.Template.Process;
using BackendService.Models.Response.Template.Query;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<QueryResultList<Book>>> GetBooks()
        {
            try
            {
                var books = await _unitOfWork.Books.ListAsync();
                return Ok(new QueryResultList<Book>().SetResult(true, "Books fetched successfully.", books));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new QueryResultList<Book>().SetResult(false, ex.Message, null));
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueryResultSingle<Book>>> GetBook(Guid id)
        {
            try
            {
                var book = await _unitOfWork.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound(new QueryResultSingle<Book>().SetResult(false, $"{id} : Book not found.", null));
                }
                return Ok(new QueryResultSingle<Book>().SetResult(true, $"{id} : Book fetched successfully.", book));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new QueryResultSingle<Book>().SetResult(false, $"Exception Occurred : {ex.Message}", null));
            }
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<OperationResultSingle<Book>>> PostBook(BookRequest bookRequest)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var book = new Book
                    {
                        Title = bookRequest.Title,
                        Author = bookRequest.Author,
                        PublishedDate = bookRequest.PublishedDate,
                        ISBN = bookRequest.ISBN,
                        Price = bookRequest.Price,
                    };
                    await _unitOfWork.Books.InsertAsync(book);
                    var rowInserted = await _unitOfWork.CompleteAsync();
                    if (rowInserted == 0)
                    {
                        transaction.Rollback();
                        return UnprocessableEntity(new OperationResultSingle<Book>().SetResult(false, "Book failed to save.", rowInserted, null));
                    }
                    transaction.Commit();
                    return CreatedAtAction(nameof(GetBook), new { id = book.Id }, new OperationResultSingle<Book>().SetResult(true, "Book saved successfully.", rowInserted, book));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, new OperationResultSingle<Book>().SetResult(false, $"Exception Occurred : {ex.Message}", 0, null));
                }
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResultSingle<Book>>> PutBook(Guid id, BookRequest bookRequest)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var book = await _unitOfWork.Books.FindAsync(id);
                    if (book == null)
                    {
                        return NotFound(new OperationResultSingle<Book>().SetResult(false, $"{id} : Book not found.", 0, null));
                    }
                    book.Title = bookRequest.Title;
                    book.Author = bookRequest.Author;
                    book.PublishedDate = bookRequest.PublishedDate;
                    book.ISBN = bookRequest.ISBN;
                    book.PublishedDate = bookRequest.PublishedDate;
                    _unitOfWork.Books.Update(book);
                    var rowUpdated = await _unitOfWork.CompleteAsync();
                    if (rowUpdated == 0)
                    {
                        transaction.Rollback();
                        return UnprocessableEntity(new OperationResultSingle<Book>().SetResult(false, $"{id} : Book failed to update.", rowUpdated, null));
                    }
                    transaction.Commit();
                    return Ok(new OperationResultSingle<Book>().SetResult(true, $"{id} : Book updated successfully.", rowUpdated, book));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, new OperationResultSingle<Book>().SetResult(false, $"Exception Occurred : {ex.Message}", 0, null));
                }
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult>> DeleteBook(Guid id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var book = await _unitOfWork.Books.FindAsync(id);
                    if (book == null)
                    {
                        return NotFound(new ProcessResult().SetResult(false, $"{id} : Book not found."));
                    }
                    _unitOfWork.Books.Delete(book);
                    await _unitOfWork.CompleteAsync();
                    transaction.Commit();
                    return Ok(new ProcessResult().SetResult(true, $"{id} : Book deleted successfully."));
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, new ProcessResult().SetResult(false, $"Exception Occurred : {ex.Message}"));
                }
            }
        }
    }
}
