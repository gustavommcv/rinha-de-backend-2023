using Microsoft.EntityFrameworkCore;
using rinha_de_backend_2023.Context;
using rinha_de_backend_2023.Contracts;
using rinha_de_backend_2023.Entities;

namespace rinha_de_backend_2023.Repositories;

public class PersonRepository : IPersonRepository {
    private readonly AppDbContext _dbContext;

    public PersonRepository(AppDbContext dbContext) { _dbContext = dbContext; }

    public async Task<Person> AddPerson(Person person) {
        await _dbContext.AddAsync(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }

    public async Task<Person> GetPersonById(Guid id) {
        var person = await _dbContext.FindAsync<Person>(id);
        return person ?? throw new KeyNotFoundException($"Person with ID {id} not found.");
    }

    public async Task<int> GetPersonCount() {
        var people = await _dbContext.People.ToArrayAsync();
        return people.Length;
    }

    public Task<List<Person>> SearchPeople(string t) {
        if (string.IsNullOrWhiteSpace(t))
        return Task.FromResult(new List<Person>());

        t = t.ToLower();
        return _dbContext.People
            .Where(p =>
                p.Apelido.ToLower().Contains(t) ||
                p.Nome.ToLower().Contains(t) ||
                (p.Stack != null && p.Stack.Any(s => s.ToLower().Contains(t))))
            .ToListAsync();
    }
}
