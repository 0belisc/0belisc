using System.ComponentModel.DataAnnotations;

namespace Izumu.Shared.Models;

public class Cliente {
    public int Id { get; set; }
    [Required] public int TipoDocumentoId { get; set; }
    [Required] public string NumeroDocumento { get; set; } = null!;
    [Required] public string PrimerNombre { get; set; } = null!;
    public string? SegundoNombre { get; set; }
    [Required] public string PrimerApellido { get; set; } = null!;
    public string? SegundoApellido { get; set; }
    [Required] public DateTime FechaNacimiento { get; set; }
    public string? Direccion { get; set; }
    public string? Celular { get; set; }
    [Required, EmailAddress] public string Email { get; set; } = null!;
    [Required] public int PlanId { get; set; }
}