using BDGestionDeStock.Data;
using BDGestionDeStock.Data.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionDeStock.Server.Controllers
{
    [Route("api/GestionStock")]
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
            var resp = await Context.Productos.ToListAsync();
            return resp; 
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var Producto = await Context.Productos
                                         .Where(e => e.Id == id)
                                         //.Include(m => m.Matriculas)
                                         .FirstOrDefaultAsync();
            if (Producto == null)
            {
                return NotFound($"No existe el producto de Id={id}");
            }
            return Producto;
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

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var updatep = Context.Productos.Where(e => e.Id == id).FirstOrDefault();

            if (updatep == null)
            {
                return NotFound("No existe el producto a modificar");
            }

            updatep.CodigoProducto = producto.CodigoProducto;
            updatep.NombreProducto = producto.NombreProducto;
            updatep.DescripcionProducto = producto.DescripcionProducto;
            updatep.Stock = producto.Stock;

            try
            {
                //throw(new Exception("Cualquier Verdura"));
                Context.Productos.Update(updatep);
                Context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no han sido actualizados por: {e.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var Borrar = Context.Productos.Where(x => x.Id == id).FirstOrDefault();

            if (Borrar == null)
            {
                return NotFound($"El registro {id} no fue encontrado");
            }

            try
            {
                Context.Productos.Remove(Borrar);
                Context.SaveChanges();
                return Ok($"El registro de {Borrar.NombreProducto} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
            }
        }
    }
}
