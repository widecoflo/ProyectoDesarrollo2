-- Insertar registros de ejemplo en cada tabla

-- Miembro
EXEC InsertarMiembro 
    @id_miembro = 1, 
    @dni = '12345678', 
    @nombres = 'Juan', 
    @apellidos = 'Perez', 
    @fecha_nacimiento = '1990-01-01', 
    @direccion = 'Calle Falsa 123', 
    @email = 'juan.perez@example.com', 
    @telefono = '123456789', 
    @universidad = 'UNAM', 
    @titulo = 'Ingeniero', 
    @fecha_graduacion = '2015-12-15', 
    @foto_url = 'http://example.com/foto.jpg', 
    @estado = 'Activo', 
    @fecha_registro = '2023-07-01';
GO

-- Usuario
EXEC InsertarUsuario 
    @id_usuario = 1, 
    @id_miembro = 1, 
    @username = 'jperez', 
    @password_hash = 'hashed_password', 
    @rol = 'Admin', 
    @fecha_creacion = '2023-07-01', 
    @ultimo_acceso = '2023-07-01';
GO

-- Documento
EXEC InsertarDocumento 
    @id_documento = 1, 
    @id_miembro = 1, 
    @tipo_documento = 'Licencia', 
    @documento_url = 'http://example.com/licencia.jpg', 
    @fecha_carga = '2023-07-01', 
    @estado = 'Activo';
GO

-- Certificacion
EXEC InsertarCertificacion 
    @id_certificacion = 1, 
    @id_documento = 1, 
    @tipo_certificacion = 'Certificacion Profesional', 
    @fecha_emision = '2023-07-01', 
    @fecha_expiracion = '2025-07-01', 
    @certificado_url = 'http://example.com/certificado.jpg', 
    @estado = 'Activo';
GO

-- Pago
EXEC InsertarPago 
    @id_pago = 1, 
    @id_miembro = 1, 
    @monto = 100.00, 
    @fecha_pago = '2023-07-01', 
    @tipo_pago = 'Tarjeta', 
    @comprobante_url = 'http://example.com/comprobante.jpg', 
    @estado = 'Completado';
GO

-- Renovacion
EXEC InsertarRenovacion 
    @id_renovacion = 1, 
    @id_miembro = 1, 
    @id_pago = 1, 
    @id_documento = 1, 
    @fecha_solicitud = '2023-07-01', 
    @fecha_aprobacion = '2023-07-01', 
    @estado = 'Aprobado';
GO

-- Verificación de los datos insertados
SELECT * FROM Miembros;
SELECT * FROM Usuarios;
SELECT * FROM Documentos;
SELECT * FROM Certificaciones;
SELECT * FROM Pagos;
SELECT * FROM Renovaciones;
GO