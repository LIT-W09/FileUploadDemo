using System.Collections.Generic;
using System.Data.SqlClient;

namespace FileUpload.Data
{
    public class ImagesRepository
    {
        private string _connectionString;

        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(string title, string imagePath)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Images (Title, ImagePath) VALUES (@title, @path)";
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@path", imagePath);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<UploadedImage> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM  Images";
            conn.Open();
            List<UploadedImage> result = new();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new UploadedImage
                {
                    Id = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    ImagePath = (string)reader["ImagePath"]
                });
            }

            return result;
        }
    }

}
