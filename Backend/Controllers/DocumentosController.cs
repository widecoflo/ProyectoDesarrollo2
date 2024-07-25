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
    public class DocumentosController : ControllerBase
    {
        public readonly string con;
        public DocumentosController(IConfiguration configuration)
        {
            con = configuration.GetConnectionString("conexion");
        }

        [HttpGet]
        public IEnumerable<Documento> Get()
        {
            List<Documento> documentos = new();
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ListarDocumentos", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Documento documento = new Documento
                            {
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                id_miembro = Convert.ToInt32(reader["id_miembro"]),
                                tipo_documento = reader["tipo_documento"].ToString(),
                                documento_url = reader["documento_url"].ToString(),
                                fecha_carga = reader["fecha_carga"] as DateTime?,
                                estado = reader["estado"].ToString()
                            };

                            documentos.Add(documento);
                        }
                    }
                }
            }
            return documentos;
        }

        [HttpGet("{id}")]
        public Documento Get(int id)
        {
            Documento documento = null;
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ObtenerDocumento_id", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_documento", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            documento = new Documento
                            {
                                id_documento = Convert.ToInt32(reader["id_documento"]),
                                id_miembro = Convert.ToInt32(reader["id_miembro"]),
                                tipo_documento = reader["tipo_documento"].ToString(),
                                documento_url = reader["documento_url"].ToString(),
                                fecha_carga = reader["fecha_carga"] as DateTime?,
                                estado = reader["estado"].ToString()
                            };
                        }
                    }
                }
            }
            return documento;
        }

        [HttpPost]
        public void Post([FromBody] Documento documento)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertarDocumento", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_miembro", documento.id_miembro);
                    cmd.Parameters.AddWithValue("@tipo_documento", documento.tipo_documento);
                    cmd.Parameters.AddWithValue("@documento_url", documento.documento_url);
                    cmd.Parameters.AddWithValue("@fecha_carga", documento.fecha_carga);
                    cmd.Parameters.AddWithValue("@estado", documento.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Documento documento)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("ActualizarDocumento", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_documento", id);
                    cmd.Parameters.AddWithValue("@id_miembro", documento.id_miembro);
                    cmd.Parameters.AddWithValue("@tipo_documento", documento.tipo_documento);
                    cmd.Parameters.AddWithValue("@documento_url", documento.documento_url);
                    cmd.Parameters.AddWithValue("@fecha_carga", documento.fecha_carga);
                    cmd.Parameters.AddWithValue("@estado", documento.estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (SqlConnection connection = new(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("BorrarDocumento", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_documento", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}