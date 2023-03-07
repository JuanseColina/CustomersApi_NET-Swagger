using CustomersApi.Dtos;
using CustomersApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomersApi.Casos_de_uso;

    public interface IUpdateCustomerUseCase //es un contrato que va a tener una clase que implemente
    {
        Task<CustomersDto?> Execute(CustomersDto createCustomerDto);
    }

    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly CustomerDataBaseContext _customerDataBaseContext; // la db referenciada

        public UpdateCustomerUseCase(CustomerDataBaseContext customerDataBaseContext)
        {
            _customerDataBaseContext = customerDataBaseContext;
        }

        public async Task<Dtos.CustomersDto?> Execute(CustomersDto customerDto)
        {
            var entity = await _customerDataBaseContext.GetCustomerById(customerDto.Id);
            if (entity == null)
            {
                return null;
            }

            entity.FirstName = customerDto.FirstName;
            entity.LastName = customerDto.LastName;
            entity.Email = customerDto.Email;
            entity.Phone = customerDto.Phone;
            entity.Address = customerDto.Address;

            await _customerDataBaseContext.Actualizar(entity);

            return entity.ToDto();
        }
        
    }
