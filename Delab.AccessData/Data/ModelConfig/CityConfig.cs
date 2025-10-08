using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delab.AccessData.Data.ModelConfig;

public class CityConfig : IEntityTypeConfiguration<City>

{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasIndex(e => e.CityId);          // Valida que el StateId es el campo llave que corresponde
        builder.HasIndex(e => new { e.Name, e.StateId }).IsUnique();  // Valida que el campo name no se repita pero solo a nivel de un mismo pais                                                                      
        builder.HasOne(e => e.State).WithMany(e => e.Cities).OnDelete(DeleteBehavior.Restrict); //Proteccion de borrado en cascada

    }
}
