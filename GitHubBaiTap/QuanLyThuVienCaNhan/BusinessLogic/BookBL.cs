using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class BookBL
    {
        BookDA bookDA = new BookDA();
        public List<Book> GetAll()
        {
            return bookDA.GetAll();
        }
    }
}
