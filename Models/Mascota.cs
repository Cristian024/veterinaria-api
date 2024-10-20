namespace Models;

public class Mascota{
    public int Mas_Id { get; set; }
    public string? Mas_Nombre { get; set; }
    public string? Mas_Peso{ get; set; }
    public string? Mas_Color{ get; set; }
    public DateTime Mas_FechaNacimiento { get; set; }
    public string? Mas_Caracteristicas { get; set; }
    public int Cli_Id { get; set; }
    public int Ani_Id { get; set; }
}