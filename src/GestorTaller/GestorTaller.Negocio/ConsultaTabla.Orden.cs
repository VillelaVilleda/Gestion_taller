namespace GestorTaller.Negocio;

public enum DireccionOrden
{
    Ascendente,
    Descendente
}

/// <summary>Ordenamiento de la tabla generica (issue #91).</summary>
public partial class ConsultaTabla<T>
{
    /// <summary>Columna por la que esta ordenada la tabla (null = orden original).</summary>
    public ColumnaTabla<T>? ColumnaOrden { get; private set; }

    public DireccionOrden Direccion { get; private set; } = DireccionOrden.Ascendente;

    /// <summary>Filas filtradas y ordenadas (si no se ha ordenado, en su orden original).</summary>
    public IReadOnlyList<T> FilasOrdenadas
    {
        get
        {
            var filas = FilasFiltradas;
            var columna = ColumnaOrden;

            if (columna is null)
            {
                return filas;
            }

            var ordenadas = Direccion == DireccionOrden.Ascendente
                ? filas.OrderBy(fila => columna.Valor(fila), ComparadorDeValores.Instancia)
                : filas.OrderByDescending(fila => columna.Valor(fila), ComparadorDeValores.Instancia);

            return ordenadas.ToList();
        }
    }

    /// <summary>
    /// Comportamiento del clic en un encabezado: la primera vez ordena ascendente;
    /// si se vuelve a pulsar la misma columna, alterna entre descendente y ascendente.
    /// </summary>
    public void Ordenar(ColumnaTabla<T> columna)
    {
        ValidarColumna(columna);

        Direccion = ReferenceEquals(ColumnaOrden, columna) && Direccion == DireccionOrden.Ascendente
            ? DireccionOrden.Descendente
            : DireccionOrden.Ascendente;
        ColumnaOrden = columna;
        AlCambiarConsulta();
    }

    /// <summary>Ordena por la columna y direccion indicadas (por ejemplo, el orden inicial).</summary>
    public void Ordenar(ColumnaTabla<T> columna, DireccionOrden direccion)
    {
        ValidarColumna(columna);

        ColumnaOrden = columna;
        Direccion = direccion;
        AlCambiarConsulta();
    }

    /// <summary>
    /// Compara valores de celdas: texto sin distinguir mayusculas ni acentos, numeros,
    /// fechas y enums por su valor (el enum EstadoOrden sigue el flujo de la orden);
    /// null va primero. Si los tipos no coinciden, compara su texto.
    /// </summary>
    private sealed class ComparadorDeValores : IComparer<object?>
    {
        public static readonly ComparadorDeValores Instancia = new();

        public int Compare(object? x, object? y)
        {
            if (x is null && y is null)
            {
                return 0;
            }

            if (x is null)
            {
                return -1;
            }

            if (y is null)
            {
                return 1;
            }

            if (x is string textoX && y is string textoY)
            {
                return string.CompareOrdinal(FiltroDeBusqueda.Normalizar(textoX), FiltroDeBusqueda.Normalizar(textoY));
            }

            if (x.GetType() == y.GetType() && x is IComparable comparable)
            {
                return comparable.CompareTo(y);
            }

            return string.CompareOrdinal(
                FiltroDeBusqueda.Normalizar(x.ToString()),
                FiltroDeBusqueda.Normalizar(y.ToString()));
        }
    }
}
