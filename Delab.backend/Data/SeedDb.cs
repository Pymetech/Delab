using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delab.backend.Data;

// Esta clase llena la BD con data inicial
public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context) => _context = context;

    public async Task SeedAsync()
    {
        // Aplica migraciones pendientes (NO usar EnsureCreated en escenarios con migrations)
        await _context.Database.MigrateAsync();

        await CheckCountries();
    }
    
    private async Task CheckCountries()
    {
        if (!await _context.Countries.AnyAsync())
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                CodPhone = "+57",
                States = new List<State>()
                {
                    new State
                    {
                        Name = "Antioquia",
                        Cities = new List<City>()
                        {
                            new City { Name = "Medellín"},
                            new City { Name = "Itagüí"},
                            new City { Name = "Envigado"},   // corregido
                            new City { Name = "Bello"},
                            new City { Name = "Rionegro"},    // mayúsculas/acentos
                        }
                    },
                    new State
                    {
                        Name = "Cundinamarca",               // corregido
                        Cities = new List<City>()
                        {
                            new City { Name = "Soacha"},
                            new City { Name = "Facatativá"}, // acento
                            new City { Name = "Fusagasugá"}, // acento
                            new City { Name = "Chía"},       // acento
                            new City { Name = "Rionegro"},
                        }
                    }
                }
            });

            _context.Countries.Add(new Country
            {
                Name = "México",                              // corregido
                CodPhone = "+52",                             // corregido
                States = new List<State>()
                {
                    new State
                    {
                        Name = "Chiapas",
                        Cities = new List<City>()
                        {
                            new City { Name = "Tuxtla Gutiérrez"}, // corregido
                            new City { Name = "Tapachula"},
                            new City { Name = "San Cristóbal de las Casas"},
                            new City { Name = "Comitán"},
                            new City { Name = "Cintalapa"},
                        }
                    },
                    new State
                    {
                        Name = "Colima",
                        Cities = new List<City>()
                        {
                            new City { Name = "Manzanillo"},
                            new City { Name = "Quesería"},   // acento
                            new City { Name = "El Colomo"},
                            new City { Name = "Comala"},
                            new City { Name = "Armería"},    // acento
                        }
                    }
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}
