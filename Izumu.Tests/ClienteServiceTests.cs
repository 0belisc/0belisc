using Microsoft.EntityFrameworkCore;
using Izumu.Api;
using Izumu.Shared.Models;
using Izumu.Api.Services;
using Xunit;

namespace Izumu.Tests;

public class ClienteServiceTests
{
    private ApplicationDbContext GetDatabaseContext()
    {
        // Creamos una base de datos en memoria para cada prueba
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }

    [Fact]
    public async Task Create_ShouldReturnFalse_WhenClienteAlreadyExists()
    {
        var context = GetDatabaseContext();
        var service = new ClienteService(context);
        //generamos datos de un cliente
        var clienteExistente = new Cliente { 
            TipoDocumentoId = 1, 
            NumeroDocumento = "12345", 
            PrimerNombre = "Juan",
            PrimerApellido = "Perez",
            Email = "juan@test.com"
        };
        
        context.Clientes.Add(clienteExistente);
        //guardamos cliente nuevo
        await context.SaveChangesAsync();
        //generamos datos de un cliente
        var nuevoClienteIgual = new Cliente { 
            TipoDocumentoId = 1, 
            NumeroDocumento = "12345", // Mismo documento
            PrimerNombre = "Pedro",
            PrimerApellido = "Gomez",
            Email = "pedro@test.com"
        };
        //guardamos cliente nuevo
        var resultado = await service.Create(nuevoClienteIgual);

        Assert.False(resultado); // Debería fallar por duplicado
    }
    [Fact]
    public async Task Update_ShouldModifyExistingRecord()
    {
        var context = GetDatabaseContext();
        var service = new ClienteService(context);
        var cliente = new Cliente { Id = 5, PrimerNombre = "Original", NumeroDocumento = "555", Email = "a@a.com", TipoDocumentoId = 1, PrimerApellido = "X" };
        context.Clientes.Add(cliente);
        //guardamos cliente nuevo
        await context.SaveChangesAsync();

        //modificamos primer nombre del cliente
        cliente.PrimerNombre = "Modificado";
        var resultado = await service.Update(cliente);
        var clienteEnBase = await context.Clientes.FindAsync(5);

        Assert.True(resultado);//el resultado debe ser exitoso.
        Assert.Equal("Modificado", clienteEnBase.PrimerNombre);
    }

    [Fact]
    public async Task Delete_ShouldRemoveRecordFromDatabase()
    {
        var context = GetDatabaseContext();
        var service = new ClienteService(context);
        var cliente = new Cliente { Id = 10, PrimerNombre = "Borrar", NumeroDocumento = "000", Email = "b@b.com", TipoDocumentoId = 1, PrimerApellido = "X" };
        context.Clientes.Add(cliente);
        await context.SaveChangesAsync();

        var resultado = await service.Delete(10);
        var existe = await context.Clientes.AnyAsync(c => c.Id == 10);

        Assert.True(resultado);
        Assert.False(existe); // El registro ya no debe existir
    }
}