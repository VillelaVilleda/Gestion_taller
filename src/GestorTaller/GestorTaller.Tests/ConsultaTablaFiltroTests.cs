using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas de ColumnaTabla y del filtrado de ConsultaTabla (issue #90).
public class ConsultaTablaFiltroTests
{
    private record Persona(string Nombre, string Correo, int Edad);

    private static readonly ColumnaTabla<Persona> ColNombre = new("Nombre", p => p.Nombre);
    private static readonly ColumnaTabla<Persona> ColCorreo = new("Correo", p => p.Correo);
    private static readonly ColumnaTabla<Persona> ColEdad = new("Edad", p => p.Edad);

    private static List<Persona> Datos() => new()
    {
        new Persona("Jose Perez", "jose@taller.com", 30),
        new Persona("Ana Lopez", "ana@correo.com", 25),
        new Persona("José Gomez", "pepe@taller.com", 41),
        new Persona("Maria Diaz", "maria@correo.com", 33)
    };

    private static ConsultaTabla<Persona> CrearConsulta(List<Persona>? datos = null)
        => new(new[] { ColNombre, ColCorreo, ColEdad }, datos ?? Datos());

    // ---- ColumnaTabla ----

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ColumnaTabla_ConTituloVacio_LanzaArgumentException(string? titulo)
    {
        Assert.Throws<ArgumentException>(() => new ColumnaTabla<Persona>(titulo!, p => p.Nombre));
    }

    [Fact]
    public void ColumnaTabla_SinSelectorDeValor_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ColumnaTabla<Persona>("Nombre", null!));
    }

    [Fact]
    public void ColumnaTabla_Texto_PorDefectoUsaElValor()
    {
        Assert.Equal("30", ColEdad.Texto(new Persona("A", "a", 30)));
    }

    [Fact]
    public void ColumnaTabla_Texto_ConValorNulo_EsCadenaVacia()
    {
        var columna = new ColumnaTabla<Persona>("Correo", p => null);

        Assert.Equal(string.Empty, columna.Texto(new Persona("A", "a", 1)));
    }

    [Fact]
    public void ColumnaTabla_Texto_UsaElTextoPersonalizadoSiSeIndica()
    {
        var columna = new ColumnaTabla<Persona>("Edad", p => p.Edad, p => $"{p.Edad} anios");

        Assert.Equal("30 anios", columna.Texto(new Persona("A", "a", 30)));
    }

    // ---- Constructor de ConsultaTabla ----

    [Fact]
    public void Constructor_ConColumnasNulas_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ConsultaTabla<Persona>(null!, Datos()));
    }

    [Fact]
    public void Constructor_SinColumnas_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ConsultaTabla<Persona>(new List<ColumnaTabla<Persona>>(), Datos()));
    }

    [Fact]
    public void Constructor_ConDatosNulos_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ConsultaTabla<Persona>(new[] { ColNombre }, null!));
    }

    [Fact]
    public void Constructor_LaColumnaDeBusquedaInicialEsLaPrimera()
    {
        Assert.Same(ColNombre, CrearConsulta().ColumnaBusqueda);
    }

    // ---- Buscar / FilasFiltradas ----

    [Fact]
    public void FilasFiltradas_SinBusqueda_DevuelveTodasLasFilas()
    {
        Assert.Equal(4, CrearConsulta().FilasFiltradas.Count);
    }

    [Fact]
    public void Buscar_ConTextoParcial_DevuelveLasCoincidencias()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColNombre, "lop");

        Assert.Equal("Ana Lopez", Assert.Single(consulta.FilasFiltradas).Nombre);
    }

    [Fact]
    public void Buscar_IgnoraMayusculasYAcentos()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColNombre, "JOSE");

        Assert.Equal(2, consulta.FilasFiltradas.Count);
    }

    [Fact]
    public void Buscar_SoloMiraLaColumnaElegida()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColCorreo, "taller");

        Assert.Equal(2, consulta.FilasFiltradas.Count);
        Assert.All(consulta.FilasFiltradas, p => Assert.Contains("taller", p.Correo));
    }

    [Fact]
    public void Buscar_ElTextoDeUnNombreNoCoincideEnLaColumnaDeCorreo()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColCorreo, "Perez");

        Assert.Empty(consulta.FilasFiltradas);
    }

    [Fact]
    public void Buscar_EnColumnaNumerica_UsaElTextoMostrado()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColEdad, "3");

        Assert.Equal(2, consulta.FilasFiltradas.Count);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Buscar_ConTextoVacio_DevuelveTodasLasFilas(string? texto)
    {
        var consulta = CrearConsulta();
        consulta.Buscar(ColNombre, "ana");

        consulta.Buscar(ColNombre, texto);

        Assert.Equal(4, consulta.FilasFiltradas.Count);
    }

    [Fact]
    public void Buscar_ConColumnaQueNoPerteneceALaTabla_LanzaArgumentException()
    {
        var consulta = CrearConsulta();
        var ajena = new ColumnaTabla<Persona>("Otra", p => p.Nombre);

        Assert.Throws<ArgumentException>(() => consulta.Buscar(ajena, "a"));
    }

    [Fact]
    public void Buscar_ConColumnaNula_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CrearConsulta().Buscar(null!, "a"));
    }

    [Fact]
    public void Buscar_GuardaLaColumnaYElTextoDeBusqueda()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColCorreo, "ana");

        Assert.Same(ColCorreo, consulta.ColumnaBusqueda);
        Assert.Equal("ana", consulta.TextoBusqueda);
    }

    [Fact]
    public void Buscar_NoModificaLosDatosOriginales()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColNombre, "ana");

        Assert.Equal(4, consulta.TotalDatos);
    }

    [Fact]
    public void Buscar_SinCoincidencias_DevuelveListaVacia()
    {
        var consulta = CrearConsulta();

        consulta.Buscar(ColNombre, "zzz");

        Assert.Empty(consulta.FilasFiltradas);
    }
}
