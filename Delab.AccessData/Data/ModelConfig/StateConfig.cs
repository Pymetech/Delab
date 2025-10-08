using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delab.AccessData.Data.ModelConfig;

public class StateConfig : IEntityTypeConfiguration<State>

{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasIndex(e => e.StateId);          // Valida que el StateId es el campo llave que corresponde
        builder.HasIndex(e => new { e.Name , e.CountryId }).IsUnique();  // Valida que el campo name no se repita pero solo a nivel de un mismo pais                                                                      
        builder.HasOne(e => e.Country).WithMany(e => e.States).OnDelete(DeleteBehavior.Restrict); //Proteccion de borrado en cascada

    }
}
