using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookDA
    {
        public List<Book> GetAll()
        {
            SqlConnection connection = new SqlConnection(Ultilities.ConnectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = Ultilities.Book_GetAll;
            SqlDataReader reader = command.ExecuteReader();
            List<Book> list = new List<Book>();
            while (reader.Read())
            {
                Book book = new Book();
                book.Ma = Convert.ToInt32(reader["MaSach"]);
                book.Name = reader["TenSach"].ToString();
                book.MaTheLoai = Convert.ToInt32(reader["MaTL"]);
                book.TacGia = reader["TacGia"].ToString();
                book.NhaXB = reader["NXB"].ToString();
                book.NamXB = reader["NamXB"].ToString();
                book.KeSach = Convert.ToInt32(reader["KeSach"]);
                book.VTNgan = Convert.ToInt32(reader["VTNgan"]);
                book.TrangThai = Convert.ToInt32(reader["TrangThai"]);
                list.Add(book);
            }
            connection.Close();
            return list;
        }
    }
}
