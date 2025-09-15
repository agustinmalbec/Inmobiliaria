using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class InmuebleRepository
    {
        readonly string ConnectionString = "server=localhost;port=3306;database=inmobiliaria;user=root;password=";

        public List<Inmueble> GetAll()
        {
            List<Inmueble> inmuebles = [];
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT Inmuebles.{nameof(Inmueble.Id)}, Inmuebles.{nameof(Inmueble.Direccion)}, Inmuebles.{nameof(Inmueble.Inmueble_propietario)}, Propietarios.{nameof(Propietario.Id)}, Propietarios.{nameof(Propietario.Nombre)} FROM Inmuebles
                JOIN Propietarios ON Inmuebles.{nameof(Inmueble.Inmueble_propietario)} = Propietarios.Id";
                using (MySqlCommand command = new(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        inmuebles.Add(new Inmueble
                        {
                            Id = reader.GetInt32(nameof(Inmueble.Id)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Inmueble_propietario = reader.GetInt32(nameof(Inmueble.Inmueble_propietario))
                        });
                    }
                }
                connection.Close();
            }
            return inmuebles;
        }


        public Inmueble? GetInmuebleById(int id)
        {
            Inmueble? inmueble = null;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT Inmuebles.*, Propietarios.Id, Propietarios.Nombre
                FROM Inmuebles
                JOIN Propietarios ON Inmuebles.{nameof(Inmueble.Inmueble_propietario)} = Propietarios.Id
                WHERE Inmuebles.{nameof(Inmueble.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inmueble = new Inmueble
                        {
                            Id = reader.GetInt32(nameof(Inmueble.Id)),
                            Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                            Inmueble_propietario = reader.GetInt32(nameof(Inmueble.Inmueble_propietario))
                        };
                    }
                }
                connection.Close();
            }
            return inmueble;
        }

        public int InsertInmueble(Inmueble inmueble)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"INSERT INTO Inmuebles ({nameof(Inmueble.Direccion)}, {nameof(Inmueble.Inmueble_propietario)})
                VALUES (@direccion, @inmueble_propietario);
                SELECT LAST_INSERT_ID()";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue(@"inmueble_propietario", inmueble.Inmueble_propietario);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int EditInmueble(Inmueble inmueble)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"UPDATE Inmuebles 
                SET {nameof(Inmueble.Direccion)} = @direccion, {nameof(Inmueble.Inmueble_propietario)} = @inmueble_propietario
                WHERE {nameof(Inmueble.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"nombre", inmueble.Direccion);
                    command.Parameters.AddWithValue(@"apellido", inmueble.Inmueble_propietario);
                    command.Parameters.AddWithValue(@"id", inmueble.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int DeleteInmueble(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"DELETE FROM Inmuebles
                WHERE {nameof(Inmueble.Id)} = @id";
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
