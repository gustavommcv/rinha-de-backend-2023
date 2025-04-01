using rinha_de_backend_2023.Entities;

namespace rinha_de_backend_2023.Contracts;

public interface IPersonRepository {
    Task<Person> AddPerson(Person person);
    Task<Person> GetPersonById(Guid id);
    Task<int> GetPersonCount();
}
