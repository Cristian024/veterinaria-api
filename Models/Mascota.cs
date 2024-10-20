namespace Models;

public class Mascota{
    public int Mas_Id { get; set; }
    public string? Mas_Nombre { get; set; }
    public string? Mas_Peso{ get; set; }
    public string? Mas_Color{ get; set; }
    public DateTime? Mas_FechaNacimiento { get; set; }
    public string? Mas_Caracteristicas { get; set; }
    public int? Cli_Id { get; set; }
    public int? Ani_Id { get; set; }

    public override string ToString()
    {
        return $"Mascota {{ Masc_Id = {Mas_Id}, Mas_Nombre = {Mas_Nombre}, Mas_Peso = {Mas_Peso}, Mas_Color = {Mas_Color}, Mas_FechaNacimiento = {Mas_FechaNacimiento}, Mas_Caracteristicas = {Mas_Caracteristicas}, Cli_Id = {Cli_Id}, Ani_Id = {Ani_Id} }}";
    }
}