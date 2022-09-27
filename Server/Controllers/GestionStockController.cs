using BDGestionDeStock.Data;
using BDGestionDeStock.Data.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionDeStock.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionStockController : ControllerBase
    {
        private readonly BDContext1 Context;

        public GestionStockController(BDContext1 context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await Context.Productos.ToListAsync();
        }

        [HttpGet("BuscarPorCodigo/{codigo}")]
        public async Task<ActionResult<Producto>> EspecialidadPorCodigo(string codigo)
        {
            var producto = await Context.Productos.Where(e => e.CodigoProducto == codigo).FirstOrDefaultAsync();
            if (producto == null)
            {
                return NotFound($"No existe el producto={codigo}");
            }
            return producto;
        }

        [HttpPost]

        public async Task<ActionResult<int>> Post(Producto producto)
        {
            try
            {
                Context.Productos.Add(producto);
                await Context.SaveChangesAsync();
                return producto.Id;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put(string codigo, [FromBody] Producto producto)
        {
            if (codigo != producto.CodigoProducto)
            {
                return BadRequest("Datos incorrectos");
            }

            var updateP = Context.Productos.Where(e => e.CodigoProducto == codigo).FirstOrDefault();

            if (updateP == null)
            {
                return NotFound("No existe el producto");
            }

            updateP.CodigoProducto = producto.CodigoProducto;
            updateP.NombreProducto = producto.NombreProducto;

            try
            {
                //throw(new Exception("Cualquier Verdura"));
                Context.Productos.Update(updateP);
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no han sido actualizados por: {e.Message}");
            }
        }

        [HttpDelete]
        public ActionResult Delete(string codigo)
        {
            var Delete = Context.Productos.Where(x => x.CodigoProducto == codigo).FirstOrDefault();

            if (Delete == null)
            {
                return NotFound($"El producto {codigo} no fue encontrado");
            }

            try
            {
                Context.Productos.Remove(Delete);
                Context.SaveChanges();
                return Ok($"El producto {Delete.NombreProducto} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"El producto no pudo eliminarse por: {e.Message}");
            }
        }
    }
}
