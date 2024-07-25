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
    public class CertificacionesController : ControllerBase
    {
        private readonly string _connectionString;

        public CertificacionesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conexion");
        }

        [HttpGet]
        public IEnumerable<Certificacion> Get()
        {
            List<Certificacion> certificaciones = new();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("ListarCertificaciones", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Certificacion certificacion = new()
                            {
                                id_certificacion = Convert.ToInt32(reader["id_certificacion"]),
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                tipo_certificacion = reader["tipo_certificacion"].ToString(),
                                fecha_emision = Convert.ToDateTime(reader["fecha_emision"]),
                                fecha_expiracion = reader["fecha_expiracion"] as DateTime?,
                                certificado_url = reader["certificado_url"].ToString(),
                                estado = reader["estado"].ToString()
                            };
                            certificaciones.Add(certificacion);
                        }
                    }
                }
            }
            return certificaciones;
        }

        [HttpGet("{id}")]
        public Certificacion Get(int id)
        {
            Certificacion certificacion = null;
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("ObtenerCertificacion_id", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_certificacion", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            certificacion = new Certificacion
                            {
                                id_certificacion = Convert.ToInt32(reader["id_certificacion"]),
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                tipo_certificacion = reader["tipo_certificacion"].ToString(),
                                fecha_emision = Convert.ToDateTime(reader["fecha_emision"]),
                                fecha_expiracion = reader["fecha_expiracion"] as DateTime?,
                                certificado_url = reader["certificado_url"].ToString(),
                                estado = reader["estado"].ToString()
                            };
                        }
                    }
                }
            }
            return certificacion;
        }

        [HttpPost]
        public void Post([FromBody] Certificacion certificacion)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("InsertarCertificacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_documento", certificacion.id_documento);
                    cmd.Parameters.AddWithValue("@tipo_certificacion", certificacion.tipo_certificacion);
                    cmd.Parameters.AddWithValue("@fecha_emision", certificacion.fecha_emision);
                    cmd.Parameters.AddWithValue("@fecha_expiracion", certificacion.fecha_expiracion);
                    cmd.Parameters.AddWithValue("@certificado_url", certificacion.certificado_url);
                    cmd.Parameters.AddWithValue("@estado", certificacion.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Certificacion certificacion)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new("ActualizarCertificacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_certificacion", id);
                    cmd.Parameters.AddWithValue("@id_documento", certificacion.id_documento);
                    cmd.Parameters.AddWithValue("@tipo_certificacion", certificacion.tipo_certificacion);
                    cmd.Parameters.AddWithValue("@fecha_emision", certificacion.fecha_emision);
                    cmd.Parameters.AddWithValue("@fecha_expiracion", certificacion.fecha_expiracion);
                    cmd.Parameters.AddWithValue("@certificado_url", certificacion.certificado_url);
                    cmd.Parameters.AddWithValue("@estado", certificacion.estado);
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
                using (SqlCommand cmd = new("BorrarCertificacion", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_certificacion", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}