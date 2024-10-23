namespace Models;

public class Persona
{
    public int? Per_Id { get; set; }
    public string? Per_Nombre { get; set; }
    public string? Per_Apellido { get; set; }
    public DateTime? Per_FechaNacimiento { get; set; }
    public string? Per_Direccion { get; set; }
    public string? Per_Telefono { get; set; }
    public string? Per_Correo { get; set; }
    public DateTime? Per_FechaRegistro { get; set; }
}