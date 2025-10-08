using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delab.AccessData.Data.ModelConfig;

public class CountryConfig : IEntityTypeConfiguration<Country>

{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.CountryId);          // Valida que el CountryId es el campo llave que corresponde
        builder.HasIndex(e => e.Name).IsUnique();  // Valida que el campo name de countries sea unico en la tabla
    }
}
