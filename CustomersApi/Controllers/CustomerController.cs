using CustomersApi.Casos_de_uso;
using CustomersApi.Dtos;
using CustomersApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomersApi.Controllers;
[ApiController]
[Route("api/controller")]

//No es recomendable poner toda la logica en el controlador, es mejor crear casos de usos
public class CustomerController : Controller
{
    private readonly CustomerDataBaseContext _customerDataBaseContext; // la db referenciada
    
    private readonly IUpdateCustomerUseCase _updateCustomerUseCase; //la interface
    public CustomerController(CustomerDataBaseContext customerDataBaseContext, IUpdateCustomerUseCase updateCustomerUseCase)
    {
        _customerDataBaseContext = customerDataBaseContext;
        _updateCustomerUseCase = updateCustomerUseCase;
    }
    //api/customers
    /// <summary>
    ///  obtiene todos los clientes
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(List<CustomersDto>))]
    public async Task<IActionResult> GetCustomers()
    {
        var result = _customerDataBaseContext.Customers.Select(c=>c.ToDto()).ToList();
        return new OkObjectResult(result);
    }
    
    /// <summary>
    /// obtiene un cliente por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetCustomers")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(CustomersDto))]
    public async Task<IActionResult> GetCustomers(long id)
    {
        CustomersEntity result = await _customerDataBaseContext.GetCustomerById(id);
        return new ObjectResult(result.ToDto());
    }
    
    /// <summary>
    ///  elimina un cliente por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeleteCustomers")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(bool))]
    public async Task<IActionResult> DeleteCustomers(long id)
    {
        var result = await _customerDataBaseContext.DeleteCustomer(id);
        return new OkObjectResult(result);
    }
    
    
    /// <summary>
    ///  crea un cliente
    /// </summary>
    /// <param name="customers"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, type: typeof(CustomersDto))]
    public async Task<IActionResult> CreateCustomers(CreateCustomerDto customers)
    {
        CustomersEntity result = await _customerDataBaseContext.AddCustomer(customers);
        return new CreatedResult($"http://localhost:5000/api/customers/{result.Id}", null);
    }
    
    
    /// <summary>
    ///  actualiza un cliente
    /// </summary>
    /// <param name="createCustomerDto"> </param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(CustomersDto))]
    public async Task<IActionResult> UpdateCustomer(CustomersDto customersDto)
    {
        CustomersDto? result = await _updateCustomerUseCase.Execute(customersDto);
        if (result == null)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(result);
    }
}