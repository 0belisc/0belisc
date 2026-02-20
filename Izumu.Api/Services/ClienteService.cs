namespace Izumu.Api.Services;
using Izumu.Shared.Models;
using Izumu.Api;
using Microsoft.EntityFrameworkCore;

public class ClienteService : IClienteService {
    private readonly ApplicationDbContext _context;
    public ClienteService(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Cliente>> GetAll() => await _context.Clientes.ToListAsync();
    
    public async Task<Cliente?> GetById(int id) => await _context.Clientes.FindAsync(id);

    public async Task<bool> Create(Cliente cliente) {
        var existe = await _context.Clientes.AnyAsync(c => 
            c.TipoDocumentoId == cliente.TipoDocumentoId && c.NumeroDocumento == cliente.NumeroDocumento);
        if (existe) return false;
        
        _context.Clientes.Add(cliente);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Cliente cliente) {
        _context.Entry(cliente).State = EntityState.Modified;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id) {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return false;
        _context.Clientes.Remove(cliente);
        return await _context.SaveChangesAsync() > 0;
    }
}