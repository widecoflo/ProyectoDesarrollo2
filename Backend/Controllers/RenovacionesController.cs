using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using backend.Models;

namespace backend.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class RenovacionesController : ControllerBase
    {
        private readonly string _connectionString;

        public RenovacionesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conexion");
        }

        [HttpGet]
        public IEnumerable<Renovacion> Get()
        {
            List<Renovacion> renovaciones = new();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("ListarRenovaciones", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Renovacion renovacion = new()
                            {
                                id_renovacion = Convert.ToInt32(reader["id_renovacion"]),
                                id_miembro = Convert.ToInt32(reader["id_miembro"]),
                                id_pago = Convert.ToInt32(reader["id_pago"]),
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                fecha_solicitud = Convert.ToDateTime(reader["fecha_solicitud"]),
                                fecha_aprobacion = reader["fecha_aprobacion"] as DateTime?,
                                estado = reader["estado"].ToString()
                            };
                            renovaciones.Add(renovacion);
                        }
                    }
                }
            }
            return renovaciones;
        }

        [HttpGet("{id}")]
        public Renovacion Get(int id)
        {
            Renovacion renovacion = null;
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("ObtenerRenovacion_id", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_renovacion", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            renovacion = new Renovacion
                            {
                                id_renovacion = Convert.ToInt32(reader["id_renovacion"]),
                                id_miembro = Convert.ToInt32(reader["id_miembro"]),
                                id_pago = Convert.ToInt32(reader["id_pago"]),
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                fecha_solicitud = Convert.ToDateTime(reader["fecha_solicitud"]),
                                fecha_aprobacion = reader["fecha_aprobacion"] as DateTime?,
                                estado = reader["estado"].ToString()
                            };
                        }
                    }
                }
            }
            return renovacion;
        }

        [HttpPost]
        public void Post([FromBody] Renovacion renovacion)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("InsertarRenovacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_miembro", renovacion.id_miembro);
                    cmd.Parameters.AddWithValue("@id_pago", renovacion.id_pago);
                    cmd.Parameters.AddWithValue("@id_documento", renovacion.id_documento);
                    cmd.Parameters.AddWithValue("@fecha_solicitud", renovacion.fecha_solicitud);
                    cmd.Parameters.AddWithValue("@estado", renovacion.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Renovacion renovacion)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("ActualizarRenovacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_renovacion", id);
                    cmd.Parameters.AddWithValue("@id_miembro", renovacion.id_miembro);
                    cmd.Parameters.AddWithValue("@id_pago", renovacion.id_pago);
                    cmd.Parameters.AddWithValue("@id_documento", renovacion.id_documento);
                    cmd.Parameters.AddWithValue("@fecha_solicitud", renovacion.fecha_solicitud);
                    cmd.Parameters.AddWithValue("@fecha_aprobacion", renovacion.fecha_aprobacion);
                    cmd.Parameters.AddWithValue("@estado", renovacion.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("BorrarRenovacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_renovacion", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}