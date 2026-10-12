namespace GestorTaller.Negocio;

/// <summary>
/// Logica de una tabla generica con buscador, orden y paginacion. No depende de
/// WinForms: la pantalla solo le pide las filas a mostrar. El orden de aplicacion
/// es siempre filtrar, luego ordenar y al final paginar.
/// Esta parte (issue #90) contiene las columnas y el filtrado; el orden y la
/// paginacion estan en ConsultaTabla.Orden.cs y ConsultaTabla.Paginacion.cs.
/// </summary>
public partial class ConsultaTabla<T>
{
    private readonly IReadOnlyList<T> _datos;

    public ConsultaTabla(IEnumerable<ColumnaTabla<T>> columnas, IEnumerable<T> datos)
    {
        if (columnas is null)
        {
            throw new ArgumentException("Las columnas son obligatorias.");
        }

        if (datos is null)
        {
            throw new ArgumentException("Los datos son obligatorios.");
        }

        Columnas = columnas.ToList();

        if (Columnas.Count == 0)
        {
            throw new ArgumentException("Se requiere al menos una columna.");
        }

        _datos = datos.ToList();
        ColumnaBusqueda = Columnas[0];
        TextoBusqueda = string.Empty;
    }

    public IReadOnlyList<ColumnaTabla<T>> Columnas { get; }

    /// <summary>Columna elegida en el combobox del buscador (por defecto, la primera).</summary>
    public ColumnaTabla<T> ColumnaBusqueda { get; private set; }

    public string TextoBusqueda { get; private set; }

    /// <summary>Cantidad total de filas, sin aplicar el filtro.</summary>
    public int TotalDatos => _datos.Count;

    /// <summary>Filas que coinciden con la busqueda, en su orden original.</summary>
    public IReadOnlyList<T> FilasFiltradas =>
        FiltroDeBusqueda.Filtrar(_datos, TextoBusqueda, fila => ColumnaBusqueda.Texto(fila));

    /// <summary>
    /// Busca <paramref name="texto"/> (parcial, sin distinguir mayusculas ni acentos) en la
    /// columna indicada. Un texto vacio muestra todas las filas.
    /// </summary>
    public void Buscar(ColumnaTabla<T> columna, string? texto)
    {
        ValidarColumna(columna);

        ColumnaBusqueda = columna;
        TextoBusqueda = texto ?? string.Empty;
        AlCambiarConsulta();
    }

    private void ValidarColumna(ColumnaTabla<T> columna)
    {
        if (columna is null || !Columnas.Contains(columna))
        {
            throw new ArgumentException("La columna no pertenece a la tabla.");
        }
    }

    // Se llama cuando cambia la busqueda o el orden (la paginacion vuelve a la pagina 1).
    partial void AlCambiarConsulta();
}
