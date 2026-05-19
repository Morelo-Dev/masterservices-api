using MasterServicesAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MasterServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductosServiciosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductosServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/ProductosServicios
        [HttpPost]
        public async Task<ActionResult<MsgResult>> PostProductosServicio(ProductosServicio productosServicio)
        {
            // Verificar si el objeto recibido es nulo
            if (productosServicio == null)
            {
                return BadRequest(new MsgResult("El objeto 'productosServicio' no puede ser nulo."));
            }

            // Validar que los campos obligatorios estén presentes
            if (string.IsNullOrEmpty(productosServicio.Nombre))
            {
                return BadRequest(new MsgResult("El nombre del producto/servicio es obligatorio."));
            }

            if (string.IsNullOrEmpty(productosServicio.Tipo))
            {
                return BadRequest(new MsgResult("El tipo del producto/servicio es obligatorio."));
            }

            // Validar que el precio sea mayor a cero
            if (productosServicio.Precio <= 0)
            {
                return BadRequest(new MsgResult("El precio del producto/servicio debe ser mayor a cero."));
            }

            // Validar que el stock no sea negativo
            if (productosServicio.Stock < 0)
            {
                return BadRequest(new MsgResult("El stock no puede ser negativo."));
            }

            // Validar si la categoría existe si se incluye un ID de categoría
            if (productosServicio.CategoriaId.HasValue)
            {
                var categoriaExistente = await _context.Categorias.FindAsync(productosServicio.CategoriaId.Value);
                if (categoriaExistente == null)
                {
                    return BadRequest(new MsgResult("La categoría especificada no existe."));
                }
            }

            // Validar si el proveedor existe si se incluye un ID de proveedor
            if (productosServicio.ProveedorId.HasValue)
            {
                var proveedorExistente = await _context.Proveedores.FindAsync(productosServicio.ProveedorId.Value);
                if (proveedorExistente == null)
                {
                    return BadRequest(new MsgResult("El proveedor especificado no existe."));
                }
            }

            // Asignar un valor por defecto de 'true' para el estado si es nulo
            productosServicio.Estado ??= true;

            // Validar que el código interno no sea vacío si se proporciona
            if (!string.IsNullOrEmpty(productosServicio.CodigoInterno) && productosServicio.CodigoInterno.Length < 5)
            {
                return BadRequest(new MsgResult("El código interno debe tener al menos 5 caracteres si se proporciona."));
            }

            // Validar que la unidad de medida sea proporcionada si se incluye un tipo que lo requiere
            if (productosServicio.Tipo == "Servicio" && string.IsNullOrEmpty(productosServicio.UnidadMedida))
            {
                return BadRequest(new MsgResult("La unidad de medida es obligatoria para los productos/servicios de tipo 'Servicio'."));
            }

            // Añadir el producto/servicio al contexto
            _context.ProductosServicios.Add(productosServicio);

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones para errores en la base de datos
                return StatusCode(500, new MsgResult($"Error al guardar el producto/servicio en la base de datos: {ex.Message}"));
            }

            // Respuesta exitosa cuando se crea correctamente el producto/servicio
            return CreatedAtAction("GetProductosServicio", new { id = productosServicio.Id }, new MsgResult("Producto/Servicio creado correctamente.", productosServicio));
        }

        // GET: api/ProductosServicios
        [HttpGet]
        public async Task<ActionResult<MsgResult>> GetProductosServicios()
        {
            var productosServicios = await _context.ProductosServicios
                .Include(c => c.Categoria)
                .Include(c => c.Proveedor)
                .ToListAsync();

            // Verificar si no hay productos
            if (productosServicios == null || !productosServicios.Any())
            {
                return NotFound(new MsgResult("No se encontraron productos/servicios."));
            }

            return Ok(new MsgResult("Productos/Servicios obtenidos correctamente.", productosServicios));
        }

        // GET: api/ProductosServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsgResult>> GetProductosServicio(int id)
        {
            // Validar que el ID sea mayor que cero
            if (id <= 0)
            {
                return BadRequest(new MsgResult("El ID del producto/servicio debe ser mayor a cero."));
            }

            var productosServicio = await _context.ProductosServicios.FindAsync(id);

            if (productosServicio == null)
            {
                return NotFound(new MsgResult("Producto/Servicio no encontrado."));
            }

            return Ok(new MsgResult("Producto/Servicio encontrado.", productosServicio));
        }

        // PUT: api/ProductosServicios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductosServicio(int id, [FromBody] ProductosServicio productosServicio)
        {
            var productoExistente = await _context.ProductosServicios.FindAsync(id);

            if (productoExistente == null)
                return Ok(new MsgResult("Producto/Servicio no encontrado."));

            if (productoExistente.Estado == false && productosServicio.Estado == false)
                return Ok(new MsgResult("El producto/servicio ya está eliminado."));

            List<string> cambios = new List<string>();

            if (!string.IsNullOrEmpty(productosServicio.Nombre) && productosServicio.Nombre != productoExistente.Nombre)
            {
                cambios.Add($"Nombre cambiado de '{productoExistente.Nombre}' a '{productosServicio.Nombre}'");
                productoExistente.Nombre = productosServicio.Nombre;
            }

            if (!string.IsNullOrEmpty(productosServicio.Tipo) && productosServicio.Tipo != productoExistente.Tipo)
            {
                cambios.Add($"Tipo cambiado de '{productoExistente.Tipo}' a '{productosServicio.Tipo}'");
                productoExistente.Tipo = productosServicio.Tipo;
            }

            if (productosServicio.Precio != productoExistente.Precio)
            {
                cambios.Add($"Precio cambiado de '{productoExistente.Precio}' a '{productosServicio.Precio}'");
                productoExistente.Precio = productosServicio.Precio;
            }

            if (productosServicio.Stock != productoExistente.Stock)
            {
                cambios.Add($"Stock cambiado de '{productoExistente.Stock}' a '{productosServicio.Stock}'");
                productoExistente.Stock = productosServicio.Stock;
            }


            if (productosServicio.Descripcion != productoExistente.Descripcion)
            {
                cambios.Add($"Stock cambiado de '{productoExistente.Descripcion}' a '{productosServicio.Descripcion}'");
                productoExistente.Descripcion = productosServicio.Descripcion;
            }


            if (productosServicio.UnidadMedida != productoExistente.UnidadMedida)
            {
                cambios.Add($"Stock cambiado de '{productoExistente.UnidadMedida}' a '{productosServicio.UnidadMedida}'");
                productoExistente.UnidadMedida = productosServicio.UnidadMedida;
            }


            if (productosServicio.CodigoInterno != productoExistente.CodigoInterno)
            {
                cambios.Add($"Stock cambiado de '{productoExistente.CodigoInterno}' a '{productosServicio.CodigoInterno}'");
                productoExistente.CodigoInterno = productosServicio.CodigoInterno;
            }


            // Si la categoría cambia, validar que exista la nueva categoría
            if (productosServicio.CategoriaId.HasValue && productosServicio.CategoriaId != productoExistente.CategoriaId)
            {
                var categoriaExistente = await _context.Categorias.FindAsync(productosServicio.CategoriaId.Value);
                if (categoriaExistente == null)
                {
                    return BadRequest(new MsgResult("La categoría especificada no existe."));
                }

                cambios.Add($"Categoría cambiada de '{productoExistente.CategoriaId}' a '{productosServicio.CategoriaId}'");
                productoExistente.CategoriaId = productosServicio.CategoriaId;
            }

            // Si el proveedor cambia, validar que exista el nuevo proveedor
            if (productosServicio.ProveedorId.HasValue && productosServicio.ProveedorId != productoExistente.ProveedorId)
            {
                var proveedorExistente = await _context.Proveedores.FindAsync(productosServicio.ProveedorId.Value);
                if (proveedorExistente == null)
                {
                    return BadRequest(new MsgResult("El proveedor especificado no existe."));
                }

                cambios.Add($"Proveedor cambiado de '{productoExistente.ProveedorId}' a '{productosServicio.ProveedorId}'");
                productoExistente.ProveedorId = productosServicio.ProveedorId;
            }
            // Validación de IVA y ExcluidoIva con detección de cambios
            if (productosServicio.ExcluidoIva == true)
            {
                if (productosServicio.IVA.HasValue && productosServicio.IVA != 0)
                {
                    return BadRequest(new MsgResult("Si el producto está excluido de IVA, el campo IVA debe ser null o 0."));
                }

                if (productoExistente.IVA != 0)
                {
                    cambios.Add($"IVA cambiado de '{productoExistente.IVA}' a '0'");
                    productoExistente.IVA = 0;
                }
            }
            else if (productosServicio.ExcluidoIva == false)
            {
                if (!productosServicio.IVA.HasValue || productosServicio.IVA <= 0)
                {
                    return BadRequest(new MsgResult("Si el producto NO está excluido de IVA, debe especificar un valor de IVA mayor a 0."));
                }

                if (productoExistente.IVA != productosServicio.IVA)
                {
                    cambios.Add($"IVA cambiado de '{productoExistente.IVA}' a '{productosServicio.IVA}'");
                    productoExistente.IVA = productosServicio.IVA;
                }
            }

            // Si IVA es null, marcamos ExcluidoIva como true automáticamente
            if (!productosServicio.IVA.HasValue && productoExistente.ExcluidoIva != true)
            {
                cambios.Add($"Excluido IVA cambiado de '{productoExistente.ExcluidoIva}' a 'true'");
                productoExistente.ExcluidoIva = true;
            }
            else if (productosServicio.ExcluidoIva.HasValue && productosServicio.ExcluidoIva != productoExistente.ExcluidoIva)
            {
                cambios.Add($"Excluido IVA cambiado de '{productoExistente.ExcluidoIva}' a '{productosServicio.ExcluidoIva}'");
                productoExistente.ExcluidoIva = productosServicio.ExcluidoIva;
            }


            // Asignar el valor por defecto de 'true' si el estado es nulo
            productoExistente.Estado = productosServicio.Estado ?? productoExistente.Estado;

            _context.Entry(productoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Obtener el usuario actual desde el claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuarioLogueado = userIdClaim != null
                ? await _context.Usuarios.FindAsync(int.Parse(userIdClaim))
                : new Usuario { Id = 1, Nombre = "Usuario Predeterminado" }; // Valor predeterminado

            // Registrar auditoría de cambios
            if (cambios.Any())
            {
                var auditLog = new AuditLog
                {
                    UsuarioId = usuarioLogueado.Id, // Usuario logueado o predeterminado
                    UserName = usuarioLogueado.Nombre,
                    TableName = "productos_servicios",
                    Action = "Actualización",
                    Changes = string.Join("; ", cambios),
                    Timestamp = DateTime.UtcNow
                };

                _context.AuditLogs.Add(auditLog);
                await _context.SaveChangesAsync();
            }

            return Ok(new MsgResult("Producto/Servicio actualizado correctamente.", new
            {
                productoExistente.Id,
                productoExistente.Nombre,
                productoExistente.Tipo,
                productoExistente.Precio,
                productoExistente.Stock,
                productoExistente.Estado
            }));
        }

        // DELETE: api/ProductosServicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductosServicio(int id)
        {
            // Validar que el ID sea mayor que cero
            if (id <= 0)
            {
                return BadRequest(new MsgResult("El ID del producto/servicio debe ser mayor a cero."));
            }

            var productosServicio = await _context.ProductosServicios.FindAsync(id);

            if (productosServicio == null)
            {
                return NotFound(new MsgResult("Producto/Servicio no encontrado."));
            }

            // Eliminar el producto
            _context.ProductosServicios.Remove(productosServicio);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new MsgResult("Producto/Servicio eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MsgResult($"Error al eliminar el producto/servicio de la base de datos: {ex.Message}"));
            }

        }


    }
}
