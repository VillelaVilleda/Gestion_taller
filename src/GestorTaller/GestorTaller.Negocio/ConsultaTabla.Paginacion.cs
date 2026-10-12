namespace GestorTaller.Negocio;

/// <summary>Paginacion de la tabla generica, de 20 filas por pagina (issue #92).</summary>
public partial class ConsultaTabla<T>
{
    public const int FilasPorPagina = 20;

    private int _pagina = 1;

    /// <summary>Cantidad de filas que coinciden con la busqueda (todas las paginas).</summary>
    public int TotalFilas => FilasFiltradas.Count;

    public int TotalPaginas => (TotalFilas + FilasPorPagina - 1) / FilasPorPagina;

    /// <summary>Pagina actual (empieza en 1; es 0 cuando no hay filas).</summary>
    public int PaginaActual => TotalPaginas == 0 ? 0 : Math.Min(_pagina, TotalPaginas);

    public bool HayAnterior => PaginaActual > 1;

    public bool HaySiguiente => PaginaActual < TotalPaginas;

    /// <summary>Las filas de la pagina actual, ya filtradas y ordenadas.</summary>
    public IReadOnlyList<T> FilasDePagina
    {
        get
        {
            if (TotalPaginas == 0)
            {
                return Array.Empty<T>();
            }

            return FilasOrdenadas
                .Skip((PaginaActual - 1) * FilasPorPagina)
                .Take(FilasPorPagina)
                .ToList();
        }
    }

    public void PaginaSiguiente()
    {
        if (HaySiguiente)
        {
            _pagina = PaginaActual + 1;
        }
    }

    public void PaginaAnterior()
    {
        if (HayAnterior)
        {
            _pagina = PaginaActual - 1;
        }
    }

    partial void AlCambiarConsulta()
    {
        _pagina = 1;
    }
}
