using AuthenticationClassLibrary.Models;

namespace AuthenticationApi.Infrastructure
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllActiveCustomersAsync();

        Task<Customer> GetActiveCustomerByIdAsync(int customerId);

        Task<Customer> UpdateCustomerAsync(Customer customer);

        Task<Customer> DeactivateCustomerAsync(int customerId);
        Task<Customer> GetCustomerByUserId(int userId);
    }
}
