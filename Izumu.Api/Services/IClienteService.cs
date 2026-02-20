namespace Izumu.Api.Services;
using Izumu.Shared.Models;
using Izumu.Api;
using Microsoft.EntityFrameworkCore;

public interface IClienteService {
    Task<IEnumerable<Cliente>> GetAll();
    Task<Cliente?> GetById(int id);
    Task<bool> Create(Cliente cliente);
    Task<bool> Update(Cliente cliente);
    Task<bool> Delete(int id);
}