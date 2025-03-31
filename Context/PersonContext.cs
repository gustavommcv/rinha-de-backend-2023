using System;
using Microsoft.EntityFrameworkCore;
using rinha_de_backend_2023.Entities;

namespace rinha_de_backend_2023.Context;

public class PersonContext : DbContext {
    public DbSet<Person> Persons { get; set; }

    public PersonContext() {

    }
}
