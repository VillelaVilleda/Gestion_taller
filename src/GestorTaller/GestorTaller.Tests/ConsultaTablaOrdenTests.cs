using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas del ordenamiento de ConsultaTabla (issue #91).
public class ConsultaTablaOrdenTests
{
    private record Fila(string Nombre, int Numero, DateTime Fecha, EstadoOrden Estado, string? Nota = null);

    private static readonly ColumnaTabla<Fila> ColNombre = new("Nombre", f => f.Nombre);
    private static readonly ColumnaTabla<Fila> ColNumero = new("Numero", f => f.Numero);
    private static readonly ColumnaTabla<Fila> ColFecha = new("Fecha", f => f.Fecha);
    private static readonly ColumnaTabla<Fila> ColEstado = new("Estado", f => f.Estado);
    private static readonly ColumnaTabla<Fila> ColNota = new("Nota", f => f.Nota);

    private static ConsultaTabla<Fila> CrearConsulta(params Fila[] filas)
        => new(new[] { ColNombre, ColNumero, ColFecha, ColEstado, ColNota }, filas);

    private static Fila F(string nombre, int numero = 0, int dia = 1, EstadoOrden estado = EstadoOrden.Recepcion, string? nota = null)
        => new(nombre, numero, new DateTime(2026, 10, dia), estado, nota);

    private static string[] Nombres(ConsultaTabla<Fila> consulta)
        => consulta.FilasOrdenadas.Select(f => f.Nombre).ToArray();

    [Fact]
    public void FilasOrdenadas_SinOrdenar_ConservaElOrdenOriginal()
    {
        var consulta = CrearConsulta(F("c"), F("a"), F("b"));

        Assert.Equal(new[] { "c", "a", "b" }, Nombres(consulta));
        Assert.Null(consulta.ColumnaOrden);
    }

    [Fact]
    public void Ordenar_Texto_EsAlfabeticoSinDistinguirMayusculasNiAcentos()
    {
        var consulta = CrearConsulta(F("bravo"), F("Álvaro"), F("carlos"), F("Ana"));

        consulta.Ordenar(ColNombre);

        Assert.Equal(new[] { "Álvaro", "Ana", "bravo", "carlos" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_LaPrimeraVezEsAscendente()
    {
        var consulta = CrearConsulta(F("a"), F("b"));

        consulta.Ordenar(ColNombre);

        Assert.Same(ColNombre, consulta.ColumnaOrden);
        Assert.Equal(DireccionOrden.Ascendente, consulta.Direccion);
    }

    [Fact]
    public void Ordenar_ElMismoCampoDosVeces_CambiaADescendente()
    {
        var consulta = CrearConsulta(F("b"), F("c"), F("a"));

        consulta.Ordenar(ColNombre);
        consulta.Ordenar(ColNombre);

        Assert.Equal(DireccionOrden.Descendente, consulta.Direccion);
        Assert.Equal(new[] { "c", "b", "a" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_ElMismoCampoTresVeces_VuelveAAscendente()
    {
        var consulta = CrearConsulta(F("b"), F("a"));

        consulta.Ordenar(ColNombre);
        consulta.Ordenar(ColNombre);
        consulta.Ordenar(ColNombre);

        Assert.Equal(DireccionOrden.Ascendente, consulta.Direccion);
        Assert.Equal(new[] { "a", "b" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_OtraColumna_ReiniciaAAscendente()
    {
        var consulta = CrearConsulta(F("b", 1), F("a", 2));
        consulta.Ordenar(ColNombre);
        consulta.Ordenar(ColNombre);

        consulta.Ordenar(ColNumero);

        Assert.Same(ColNumero, consulta.ColumnaOrden);
        Assert.Equal(DireccionOrden.Ascendente, consulta.Direccion);
    }

    [Fact]
    public void Ordenar_ConDireccionExplicita_LaRespeta()
    {
        var consulta = CrearConsulta(F("a", 1), F("b", 2), F("c", 3));

        consulta.Ordenar(ColNumero, DireccionOrden.Descendente);

        Assert.Equal(new[] { "c", "b", "a" }, Nombres(consulta));
        Assert.Equal(DireccionOrden.Descendente, consulta.Direccion);
    }

    [Fact]
    public void Ordenar_Numeros_PorValorYNoPorTexto()
    {
        var consulta = CrearConsulta(F("diez", 10), F("dos", 2), F("uno", 1));

        consulta.Ordenar(ColNumero);

        Assert.Equal(new[] { "uno", "dos", "diez" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_Fechas_Cronologicamente()
    {
        var consulta = CrearConsulta(F("tarde", dia: 20), F("temprano", dia: 3), F("medio", dia: 11));

        consulta.Ordenar(ColFecha);

        Assert.Equal(new[] { "temprano", "medio", "tarde" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_Estado_SegunElFlujoDeLaOrden()
    {
        var consulta = CrearConsulta(
            F("entregada", estado: EstadoOrden.Entregado),
            F("recibida", estado: EstadoOrden.Recepcion),
            F("reparando", estado: EstadoOrden.EnReparacion),
            F("cotizando", estado: EstadoOrden.Cotizacion));

        consulta.Ordenar(ColEstado);

        Assert.Equal(new[] { "recibida", "cotizando", "reparando", "entregada" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_ValoresNulos_QuedanPrimeroEnAscendente()
    {
        var consulta = CrearConsulta(F("con", nota: "zeta"), F("sin"), F("con2", nota: "alfa"));

        consulta.Ordenar(ColNota);

        Assert.Equal(new[] { "sin", "con2", "con" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_ConValoresIguales_ConservaElOrdenOriginal()
    {
        var consulta = CrearConsulta(F("primero", 1), F("segundo", 1), F("tercero", 1));

        consulta.Ordenar(ColNumero);

        Assert.Equal(new[] { "primero", "segundo", "tercero" }, Nombres(consulta));
    }

    [Fact]
    public void FilasOrdenadas_OrdenaSoloLasFilasFiltradas()
    {
        var consulta = CrearConsulta(F("banco", 2), F("bota", 1), F("casa", 3));
        consulta.Buscar(ColNombre, "b");

        consulta.Ordenar(ColNumero);

        Assert.Equal(new[] { "bota", "banco" }, Nombres(consulta));
    }

    [Fact]
    public void Ordenar_NoModificaLosDatosOriginales()
    {
        var consulta = CrearConsulta(F("b"), F("a"));

        consulta.Ordenar(ColNombre);

        Assert.Equal(2, consulta.TotalDatos);
        Assert.Equal("b", consulta.FilasFiltradas[0].Nombre);
    }

    [Fact]
    public void Ordenar_ConColumnaQueNoPerteneceALaTabla_LanzaArgumentException()
    {
        var consulta = CrearConsulta(F("a"));
        var ajena = new ColumnaTabla<Fila>("Otra", f => f.Nombre);

        Assert.Throws<ArgumentException>(() => consulta.Ordenar(ajena));
    }

    [Fact]
    public void Ordenar_ConColumnaNula_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CrearConsulta(F("a")).Ordenar(null!));
    }
}
