using CustomersApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomersApi.Repositories;

public class CustomerDataBaseContext : DbContext
{
    
    public CustomerDataBaseContext(DbContextOptions<CustomerDataBaseContext> options) : base(options)
    {
        
    }
    public DbSet<CustomersEntity> Customers { get; set; } //mismo nombre de la tabla del db

    public async Task<CustomersEntity?> GetCustomerById(long id)
    {
        return await Customers.FirstOrDefaultAsync(x => x.Id == id); //busca el comprador por id
    }

    public async Task<CustomersEntity> AddCustomer(CreateCustomerDto customerDto)
    {
        var entity = new CustomersEntity
        {
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            Email = customerDto.Email,
            Phone = customerDto.Phone,
            Address = customerDto.Address
        };
        EntityEntry<CustomersEntity> response = await Customers.AddAsync(entity: entity);

        await SaveChangesAsync();
        return await GetCustomerById(response.Entity.Id ?? throw new Exception("no se ha poddido guardar"));
    }

    public async Task<bool> DeleteCustomer(long id)
    {
        CustomersEntity entity = await GetCustomerById(id); //busca el comprador por id
        Customers.Remove(entity); //lo remueve
        await SaveChangesAsync(); //guarda los cambios
        return true;
    }

    public async Task<bool> Actualizar(CustomersEntity customersEntity)
    {
        Customers.Update(customersEntity);
        await SaveChangesAsync();
        return true;
    }

}
public class CustomersEntity // un objeto que utilizas para trnasferir datos
{
    public long? Id { get; set; }//para acceder directo desde clq lado y puede ser nulo
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    
    public CustomersDto ToDto()
    {
        return new CustomersDto
        {
            Id = Id ?? throw new Exception("no se ha podido guardar"),
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = Phone,
            Address = Address
        };
    }
}