using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.backend.Controllers;

[Route("api/states")]
[ApiController]
public class StatesController : ControllerBase
{
    private readonly DataContext _context;

    public StatesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]   // Listar todos los registros
    public async Task<ActionResult<IEnumerable<State>>> GetListAsync()
    {
        var ListItems = await _context.States.OrderBy(Orden => Orden.Name).ToListAsync();   //Pedir la lista de paises ordenado por Nombre
        return Ok(ListItems);
    }

    [HttpGet("{id}")]   // Listar solo los registros del ID osea un solo registro
    public async Task<ActionResult<IEnumerable<State>>> GetListAsync(int id)
    {
        try
        {
            var ItemModelo = await _context.States.FindAsync(id);   // Opcion 1
                                                                        // var ListCountries = await _context.Countries.Where(x=> x.CountryId == id).FirstOrDefaultAsync();   // Opcion 2
                                                                        //var IdCountry2 = await _context.Countries.FirstOrDefaultAsync(x=> x.CountryId == id); // Opcion 3

            return Ok(ItemModelo);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]   // Realiza un nuevo registro
    public async Task<IActionResult> PostAsync([FromBody] State modelo)  // Modelo es un sobrenombre que se se asigna al modelo que da acceso a la BD
    {
        try
        {
            _context.States.Add(modelo); // Segurar el nombre de la tabla
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicada")) // Pegrunto si el mensaje continie el texto duplicada
            {
                return BadRequest("Ya existe un registro con un mismo nombre");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut]   // Para actualziar el registro de los datos
    public async Task<ActionResult<State>> PutAsync(State modelo)
    {
        try
        {
            var UpdateModelo = await _context.States.FirstOrDefaultAsync(x => x.StateId == modelo.StateId);
            UpdateModelo!.Name = modelo.Name;
            UpdateModelo!.CountryId = modelo.CountryId;

            _context.States.Update(UpdateModelo);
            await _context.SaveChangesAsync();

            return Ok(UpdateModelo);
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicada")) // Pegrunto si el mensaje continie el texto duplicado
            {
                return BadRequest("Ya existe un registro con un mismo nombre");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var DataRemove = await _context.States.FindAsync(id);
            if (DataRemove == null)
            {
                return BadRequest("No se encontro el registro para Borrar");
            }
            _context.States.Remove(DataRemove);
            await _context.SaveChangesAsync();
            return Ok();

        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("referencia")) // Pegrunto si el mensaje continie el texto duplicado
            {
                return BadRequest("No se puede eliminar el registro");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
}