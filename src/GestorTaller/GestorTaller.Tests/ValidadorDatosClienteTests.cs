using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Pruebas de las reglas de los datos de un cliente (issue #123).
public class ValidadorDatosClienteTests
{
    [Fact]
    public void Validar_ConTodosLosDatos_LosDevuelveRecortados()
    {
        var datos = ValidadorDatosCliente.Validar("  Juan Perez ", " +502 5555-1234 ", " juan@correo.com ", " Zona 1 ");

        Assert.Equal(new DatosCliente("Juan Perez", "+502 5555-1234", "juan@correo.com", "Zona 1"), datos);
    }

    [Fact]
    public void Validar_ConSoloTelefono_EsValido()
    {
        var datos = ValidadorDatosCliente.Validar("Juan Perez", "5555-1234", null, null);

        Assert.Null(datos.Correo);
        Assert.Null(datos.Direccion);
    }

    [Fact]
    public void Validar_ConSoloCorreo_EsValido()
    {
        var datos = ValidadorDatosCliente.Validar("Juan Perez", "  ", "juan@correo.com", "");

        Assert.Null(datos.Telefono);
        Assert.Null(datos.Direccion);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Validar_SinNombre_LanzaExcepcion(string? nombre)
    {
        Assert.Throws<ArgumentException>(() => ValidadorDatosCliente.Validar(nombre!, "5555-1234", null, null));
    }

    [Fact]
    public void Validar_ConNombreDeMasDe150Caracteres_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => ValidadorDatosCliente.Validar(new string('a', 151), "5555-1234", null, null));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "  ")]
    public void Validar_SinTelefonoNiCorreo_LanzaExcepcion(string? telefono, string? correo)
    {
        Assert.Throws<ArgumentException>(() => ValidadorDatosCliente.Validar("Juan Perez", telefono, correo, "Zona 1"));
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("5555-ABCD")]
    [InlineData("5555.1234")]
    [InlineData("(((()))) +- ")]
    [InlineData("+502 (5555) 1234 5678 9")]
    public void Validar_ConTelefonoInvalido_LanzaExcepcion(string telefono)
    {
        Assert.Throws<ArgumentException>(() => ValidadorDatosCliente.Validar("Juan Perez", telefono, null, null));
    }

    [Theory]
    [InlineData("1234567")]
    [InlineData("5555 1234")]
    [InlineData("+502 5555-1234")]
    [InlineData("(502) 5555-1234")]
    public void Validar_ConTelefonoValido_LoAcepta(string telefono)
    {
        Assert.Equal(telefono, ValidadorDatosCliente.Validar("Juan Perez", telefono, null, null).Telefono);
    }

    [Theory]
    [InlineData("juan")]
    [InlineData("juan@")]
    [InlineData("@correo.com")]
    [InlineData("juan@correo")]
    [InlineData("juan perez@correo.com")]
    public void Validar_ConCorreoInvalido_LanzaExcepcion(string correo)
    {
        Assert.Throws<ArgumentException>(() => ValidadorDatosCliente.Validar("Juan Perez", null, correo, null));
    }

    [Fact]
    public void Validar_ConCorreoDeMasDe150Caracteres_LanzaExcepcion()
    {
        var correo = new string('a', 142) + "@mail.com";

        Assert.Throws<ArgumentException>(() => ValidadorDatosCliente.Validar("Juan Perez", null, correo, null));
    }

    [Fact]
    public void Validar_ConDireccionDeMasDe300Caracteres_LanzaExcepcion()
    {
        Assert.Throws<ArgumentException>(() => ValidadorDatosCliente.Validar("Juan Perez", "5555-1234", null, new string('a', 301)));
    }

    [Fact]
    public void Validar_ConDireccionDe300Caracteres_EsValido()
    {
        var datos = ValidadorDatosCliente.Validar("Juan Perez", "5555-1234", null, new string('a', 300));

        Assert.Equal(300, datos.Direccion!.Length);
    }
}
