using GestorTareasBlazor.Models;

namespace GestorTareasBlazor.Services;

public class TareaStateService
{
    private readonly List<TareaItem> _tareas = new()
    {
        new TareaItem { Id = 1, Titulo = "Configurar entorno Blazor", Descripcion = "Instalar .NET y crear la estructura del proyecto.", Prioridad = "Alta", FechaLimite = DateTime.Today, Completada = true },
        new TareaItem { Id = 2, Titulo = "Diseñar componentes Razor", Descripcion = "Crear tarjetas, formularios y filtros reutilizables.", Prioridad = "Alta", FechaLimite = DateTime.Today.AddDays(1), Completada = false },
        new TareaItem { Id = 3, Titulo = "Probar JS Interop", Descripcion = "Implementar función de copiado al portapapeles.", Prioridad = "Media", FechaLimite = DateTime.Today.AddDays(2), Completada = false }
    };

    private int _nextId = 4;

    // Evento para notificar cambios a todos los componentes suscritos
    public event Action? OnChange;

    public IReadOnlyList<TareaItem> ObtenerTodas() => _tareas.AsReadOnly();

    public TareaItem? ObtenerPorId(int id) => _tareas.FirstOrDefault(t => t.Id == id);

    public void AgregarTarea(TareaItem nueva)
    {
        nueva.Id = _nextId++;
        _tareas.Add(nueva);
        NotificarCambio();
    }

    public void AlternarEstado(int id)
    {
        var tarea = ObtenerPorId(id);
        if (tarea is not null)
        {
            tarea.Completada = !tarea.Completada;
            NotificarCambio();
        }
    }

    public void EliminarTarea(int id)
    {
        var tarea = ObtenerPorId(id);
        if (tarea is not null)
        {
            _tareas.Remove(tarea);
            NotificarCambio();
        }
    }

    private void NotificarCambio() => OnChange?.Invoke();
}