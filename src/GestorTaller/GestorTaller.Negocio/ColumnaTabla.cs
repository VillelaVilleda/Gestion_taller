namespace GestorTaller.Negocio;

/// <summary>
/// Describe una columna de una tabla generica: su titulo, como obtener el valor
/// (usado para ordenar) y, opcionalmente, como mostrarlo como texto (usado para
/// buscar). Si no se da el texto, se usa <c>ToString()</c> del valor.
/// </summary>
public class ColumnaTabla<T>
{
    private readonly Func<T, object?> _valor;
    private readonly Func<T, string>? _texto;

    public ColumnaTabla(string titulo, Func<T, object?> valor, Func<T, string>? texto = null)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new ArgumentException("El titulo de la columna es obligatorio.");
        }

        if (valor is null)
        {
            throw new ArgumentException("El selector de valor de la columna es obligatorio.");
        }

        Titulo = titulo;
        _valor = valor;
        _texto = texto;
    }

    public string Titulo { get; }

    public object? Valor(T fila) => _valor(fila);

    public string Texto(T fila) => _texto?.Invoke(fila) ?? _valor(fila)?.ToString() ?? string.Empty;
}
