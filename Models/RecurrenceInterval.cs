namespace Budget1.Models
{
    public class RecurrenceInterval
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;  // Ej: "Daily", "Weekly", etc.
        public int DaysInterval { get; set; } // Para cálculos simples, cuántos días dura la recurrencia (puede ser 0 si es variable como mensual)
        public string? Description { get; set; } // Opcional para detalle
    }

}
