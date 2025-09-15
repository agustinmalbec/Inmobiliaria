using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class ContratoRepository
    {
        readonly string ConnectionString = "server=localhost;port=3306;database=inmobiliaria;user=root;password=";

        public List<Contrato> GetAll()
        {
            List<Contrato> contratos = [];
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT Contratos.*, Inmuebles.{nameof(Inmueble.Direccion)}, Inquilinos.{nameof(Inquilino.Nombre)} FROM Contratos
                JOIN Inmuebles ON Contratos.{nameof(Contrato.Contrato_inmueble)} = Inmuebles.Id
                JOIN Inquilinos ON Contratos.{nameof(Contrato.Contrato_inquilino)} = Inquilinos.Id";
                using (MySqlCommand command = new(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        contratos.Add(new Contrato
                        {
                            Id = reader.GetInt32(nameof(Contrato.Id)),
                            Contrato_inquilino = reader.GetInt32(nameof(Contrato.Contrato_inquilino)),
                            Contrato_inmueble = reader.GetInt32(nameof(Contrato.Contrato_inmueble)),
                            Fecha_desde = reader.GetDateTime(nameof(Contrato.Fecha_desde)),
                            Fecha_hasta = reader.GetDateTime(nameof(Contrato.Fecha_hasta)),
                            Monto = reader.GetInt32(nameof(Contrato.Monto))
                        });
                    }
                }
                connection.Close();
            }
            return contratos;
        }


        public Contrato? GetContratoById(int id)
        {
            Contrato? contrato = null;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"SELECT Contratos.*, Inmuebles.{nameof(Inmueble.Direccion)}, Inquilinos.{nameof(Inquilino.Nombre)} FROM Contratos
                JOIN Inmuebles ON Contratos.{nameof(Contrato.Contrato_inmueble)} = Inmuebles.Id
                JOIN Inquilinos ON Contratos.{nameof(Contrato.Contrato_inquilino)} = Inquilinos.Id
                WHERE Contratos.{nameof(Contrato.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        contrato = new Contrato
                        {
                            Id = reader.GetInt32(nameof(Contrato.Id)),
                            Contrato_inquilino = reader.GetInt32(nameof(Contrato.Contrato_inquilino)),
                            Contrato_inmueble = reader.GetInt32(nameof(Contrato.Contrato_inmueble)),
                            Fecha_desde = reader.GetDateTime(nameof(Contrato.Fecha_desde)),
                            Fecha_hasta = reader.GetDateTime(nameof(Contrato.Fecha_hasta)),
                            Monto = reader.GetInt32(nameof(Contrato.Monto))
                        };
                    }
                }
                connection.Close();
            }
            return contrato;
        }

        public int InsertContrato(Contrato contrato)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"INSERT INTO Contratos ({nameof(Contrato.Contrato_inquilino)}, {nameof(Contrato.Contrato_inmueble)}, {nameof(Contrato.Fecha_desde)}, {nameof(Contrato.Fecha_hasta)}, {nameof(Contrato.Monto)}) 
                VALUES (@contrato_inquilino, @contrato_inmueble, @fecha_desde, @fecha_hasta, @monto);
                SELECT LAST_INSERT_ID()";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"contrato_inquilino", contrato.Contrato_inquilino);
                    command.Parameters.AddWithValue(@"contrato_inmueble", contrato.Contrato_inmueble);
                    command.Parameters.AddWithValue(@"fecha_desde", contrato.Fecha_desde);
                    command.Parameters.AddWithValue(@"fecha_hasta", contrato.Fecha_hasta);
                    command.Parameters.AddWithValue(@"monto", contrato.Monto);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return res;
        }

        public int EditContrato(Contrato contrato)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"UPDATE Contratos 
                SET {nameof(Contrato.Contrato_inquilino)} = @contrato_inquilino, {nameof(Contrato.Contrato_inmueble)} = @contrato_inmueble, {nameof(Contrato.Fecha_desde)} = @fecha_desde, {nameof(Contrato.Fecha_hasta)} = @fecha_hasta, {nameof(Contrato.Monto)} = @monto
                WHERE {nameof(Contrato.Id)} = @id";
                using (MySqlCommand command = new(query, connection))
                {
                    command.Parameters.AddWithValue(@"contrato_inquilino", contrato.Contrato_inquilino);
                    command.Parameters.AddWithValue(@"contrato_inmueble", contrato.Contrato_inmueble);
                    command.Parameters.AddWithValue(@"fecha_desde", contrato.Fecha_desde);
                    command.Parameters.AddWithValue(@"fecha_hasta", contrato.Fecha_hasta);
                    command.Parameters.AddWithValue(@"monto", contrato.Monto);
                    command.Parameters.AddWithValue(@"id", contrato.Id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int DeleteContrato(int id)
        {
            int res = -1;
            using (MySqlConnection connection = new(ConnectionString))
            {
                var query = $@"DELETE FROM Contratos
                WHERE {nameof(Contrato.Id)} = @id";
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
