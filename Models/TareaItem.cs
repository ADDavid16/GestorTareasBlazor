using System.ComponentModel.DataAnnotations;

namespace GestorTareasBlazor.Models;

public class TareaItem : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(60, MinimumLength = 4, ErrorMessage = "El título debe tener entre 4 y 60 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [MinLength(10, ErrorMessage = "Detalla un poco más la tarea (mínimo 10 caracteres).")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecciona una prioridad.")]
    public string Prioridad { get; set; } = "Media"; // Baja, Media, Alta

    [Required(ErrorMessage = "La fecha límite es obligatoria.")]
    public DateTime FechaLimite { get; set; } = DateTime.Today.AddDays(1);

    public bool Completada { get; set; } = false;

    // Validación personalizada avanzada: no permitir fechas en el pasado
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaLimite.Date < DateTime.Today)
        {
            yield return new ValidationResult(
                "La fecha límite no puede ser anterior al día de hoy.",
                new[] { nameof(FechaLimite) });
        }
    }
}