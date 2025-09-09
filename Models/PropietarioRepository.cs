using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class PropietarioRepository
    {
        readonly string ConnectionString = "server=localhost;port=3306;database=inmobiliaria;user=root;password=";

        public List<Propietario> GetAll()
        {
            List<Propietario> propietarios = [];
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)}, {nameof(Propietario.Telefono)}, {nameof(Propietario.Email)} FROM Propietarios";
                using (MySqlCommand command = new(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        propietarios.Add(new Propietario
                        {
                            Id = reader.GetInt32(nameof(Propietario.Id)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetString(nameof(Propietario.Dni)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),
                            Email = reader.GetString(nameof(Propietario.Email))
                        });
                    }
                }
                connection.Close();
            }
            return propietarios;
        }


        public Propietario? GetPropietarioById(int id)
        {
            Propietario? propietario = null;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT {nameof(Propietario.Id)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)}, {nameof(Propietario.Telefono)}, {nameof(Propietario.Email)} 
                FROM Propietarios
                WHERE {nameof(Propietario.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            Id = reader.GetInt32(nameof(Propietario.Id)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Dni = reader.GetString(nameof(Propietario.Dni)),
                            Telefono = reader.GetString(nameof(Propietario.Telefono)),
                            Email = reader.GetString(nameof(Propietario.Email))
                        };
                    }
                }
                connection.Close();
            }
            return propietario;
        }

        public int InsertPropietario(Propietario propietario)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"INSERT INTO Propietarios ({nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, {nameof(Propietario.Dni)}, {nameof(Propietario.Telefono)}, {nameof(Propietario.Email)}) 
                VALUES (@nombre, @apellido, @dni, @telefono, @email);
                SELECT LAST_INSERT_ID()";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"nombre", propietario.Nombre);
                    command.Parameters.AddWithValue(@"apellido", propietario.Apellido);
                    command.Parameters.AddWithValue(@"dni", propietario.Dni);
                    command.Parameters.AddWithValue(@"telefono", propietario.Telefono);
                    command.Parameters.AddWithValue(@"email", propietario.Email);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int EditPropietario(Propietario propietario)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"UPDATE Propietarios 
                SET {nameof(Propietario.Nombre)} = @nombre, {nameof(Propietario.Apellido)} = @apellido, {nameof(Propietario.Dni)} = @dni, {nameof(Propietario.Telefono)} = @telefono, {nameof(Propietario.Email)} = @email
                WHERE {nameof(Propietario.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"nombre", propietario.Nombre);
                    command.Parameters.AddWithValue(@"apellido", propietario.Apellido);
                    command.Parameters.AddWithValue(@"dni", propietario.Dni);
                    command.Parameters.AddWithValue(@"telefono", propietario.Telefono);
                    command.Parameters.AddWithValue(@"email", propietario.Email);
                    command.Parameters.AddWithValue(@"id", propietario.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int DeletePropietario(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"DELETE FROM Propietarios
                WHERE {nameof(Propietario.Id)} = @id";
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
