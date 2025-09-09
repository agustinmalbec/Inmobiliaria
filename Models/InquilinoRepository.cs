using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class InquilinoRepository
    {
        readonly string ConnectionString = "server=localhost;port=3306;database=inmobiliaria;user=root;password=";

        public List<Inquilino> GetAll()
        {
            List<Inquilino> inquilinos = [];
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT {nameof(Inquilino.Id)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Dni)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.Email)} FROM Inquilinos";
                using (MySqlCommand command = new(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        inquilinos.Add(new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            Email = reader.GetString(nameof(Inquilino.Email))
                        });
                    }
                }
                connection.Close();
            }
            return inquilinos;
        }


        public Inquilino? GetInquilinoById(int id)
        {
            Inquilino? inquilino = null;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT {nameof(Inquilino.Id)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Dni)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.Email)} 
                FROM Inquilinos
                WHERE {nameof(Inquilino.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inquilino = new Inquilino
                        {
                            Id = reader.GetInt32(nameof(Inquilino.Id)),
                            Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                            Dni = reader.GetString(nameof(Inquilino.Dni)),
                            Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            Email = reader.GetString(nameof(Inquilino.Email))
                        };
                    }
                }
                connection.Close();
            }
            return inquilino;
        }

        public int InsertInquilino(Inquilino inquilino)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"INSERT INTO Inquilinos ({nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, {nameof(Inquilino.Dni)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.Email)}) 
                VALUES (@nombre, @apellido, @dni, @telefono, @email);
                SELECT LAST_INSERT_ID()";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"nombre", inquilino.Nombre);
                    command.Parameters.AddWithValue(@"apellido", inquilino.Apellido);
                    command.Parameters.AddWithValue(@"dni", inquilino.Dni);
                    command.Parameters.AddWithValue(@"telefono", inquilino.Telefono);
                    command.Parameters.AddWithValue(@"email", inquilino.Email);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int EditInquilino(Inquilino inquilino)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"UPDATE Inquilinos 
                SET {nameof(Inquilino.Nombre)} = @nombre, {nameof(Inquilino.Apellido)} = @apellido, {nameof(Inquilino.Dni)} = @dni, {nameof(Inquilino.Telefono)} = @telefono, {nameof(Inquilino.Email)} = @email
                WHERE {nameof(Inquilino.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"nombre", inquilino.Nombre);
                    command.Parameters.AddWithValue(@"apellido", inquilino.Apellido);
                    command.Parameters.AddWithValue(@"dni", inquilino.Dni);
                    command.Parameters.AddWithValue(@"telefono", inquilino.Telefono);
                    command.Parameters.AddWithValue(@"email", inquilino.Email);
                    command.Parameters.AddWithValue(@"id", inquilino.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int DeleteInquilino(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"DELETE FROM Inquilinos
                WHERE {nameof(Inquilino.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
    }
}
