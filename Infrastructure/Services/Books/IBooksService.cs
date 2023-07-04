using Domain.Models;
using Domain.Models.Books;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Books
{
    public interface IBooksService
    {
        Task<Tuple<Common.CallStatus, List<BookModel>>> GetBooks();
    }
}
