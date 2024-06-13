using System.ComponentModel.DataAnnotations;

namespace Models.Entidades
{
    public class Cliente
    {
        [Range(1, int.MaxValue, ErrorMessage = "El ClienteId debe ser un número positivo.")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El RUT es obligatorio.")]
        [RegularExpression(@"^\d{1,2}\.\d{3}\.\d{3}-[\dkK]$", ErrorMessage = "El RUT no tiene un formato válido.")]
        public string Rut { get; set; }
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [MaxLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\+?\d{1,4}?[\d\s\-]{7,15}$", ErrorMessage = "El teléfono no tiene un formato válido.")]
        public string Telefono { get; set; }
    }
}
