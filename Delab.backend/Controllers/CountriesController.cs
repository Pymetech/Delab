using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.backend.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet]   // Listar todos los registros
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        var ListCountries = await _context.Countries
            .Include(x => x.States)!.ThenInclude(x=> x.Cities).OrderBy(Orden=> Orden.Name).ToListAsync();   //Pedir la lista de paises ordenado por Nombre
        return Ok(ListCountries);   
    }

    [HttpGet("{id}")]   // Listar solo los registros del ID osea un solo registro
    public async Task<ActionResult<IEnumerable<Country>>> GetCountry(int id)
    {
        try
        {
            var CountryName = await _context.Countries.FindAsync(id);   // Opcion 1
                                                                        // var ListCountries = await _context.Countries.Where(x=> x.CountryId == id).FirstOrDefaultAsync();   // Opcion 2
                                                                        //var IdCountry2 = await _context.Countries.FirstOrDefaultAsync(x=> x.CountryId == id); // Opcion 3

            return Ok(CountryName);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]   // Realiza un nuevo registro
    public async Task<IActionResult> PostCountry([FromBody]Country modelo)  // Modelo es un sobrenombre que se se asigna al modelo que da acceso a la BD
    {
        try
        {
            _context.Countries.Add(modelo); // Segurar el nombre de la tabla
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
        public async Task<ActionResult<Country>> PutCountry(Country modelo)
    {
        try
        {
            var UpdateCountry = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == modelo.CountryId);
            UpdateCountry!.Name = modelo.Name;
            UpdateCountry!.CodPhone = modelo.CodPhone;

            _context.Countries.Update(UpdateCountry);
            await _context.SaveChangesAsync();
            
            return Ok(UpdateCountry);
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

    public async Task<IActionResult> DeleteCountry(int id)
    {
        try
        {
            var DataRemove = await _context.Countries.FindAsync(id);
            if (DataRemove == null)
            {
                return BadRequest("No se encontro el registro para Borrar");
            }
            _context.Countries.Remove(DataRemove);
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