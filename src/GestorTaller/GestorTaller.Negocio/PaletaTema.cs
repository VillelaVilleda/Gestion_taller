namespace GestorTaller.Negocio;

/// <summary>
/// Colores del estilo visual (guia de diseno), en texto "#RRGGBB". Viven aqui, sin tipos de
/// WinForms, para poder verificar su contraste en las pruebas; la interfaz los convierte en Tema.
/// </summary>
public static class PaletaTema
{
    public const string Fondo = "#EEF1F4";
    public const string Blanco = "#FFFFFF";
    public const string Pizarra = "#34495E";
    public const string PizarraHover = "#2C3E50";
    public const string Titanio = "#1F2D3D";
    public const string TextoSecundario = "#5B6B7C";
    public const string Aluminio = "#CBD5E1";
    public const string BordeCampo = "#8392A5";
    public const string Cobre = "#E67E22";
    public const string CobreOscuro = "#BA5210";

    // Estados de los botones (pasar el mouse y presionar).
    public const string PizarraPresionado = "#22303F";
    public const string CobreHover = "#EE8B3A";
    public const string CobrePresionado = "#F29A55";
    public const string ErrorHover = "#9E2A20";
    public const string ErrorPresionado = "#86221A";
    public const string SecundarioHover = "#F1F4F8";
    public const string SecundarioPresionado = "#E3E9F0";
    public const string TextoHover = "#E3E9F0";
    public const string TextoPresionado = "#D3DCE6";

    public const string Exito = "#276749";
    public const string ExitoClaro = "#E3F4EA";
    public const string Error = "#B83227";
    public const string ErrorClaro = "#FBE7E4";
    public const string Advertencia = "#975A16";
    public const string AdvertenciaClaro = "#FDF1D8";
    public const string Informacion = "#2B6CB0";
    public const string InformacionClaro = "#E4EEF9";

    /// <summary>Color del texto y del fondo del chip de cada etapa de la orden.</summary>
    public static (string Texto, string Fondo) ColoresEtapa(EstadoOrden estado) => estado switch
    {
        EstadoOrden.Recepcion => ("#5B6B7C", "#E6EAEF"),
        EstadoOrden.Diagnostico => (Informacion, InformacionClaro),
        EstadoOrden.Cotizacion => (Advertencia, AdvertenciaClaro),
        EstadoOrden.EnReparacion => ("#A84608", "#FDEBD9"),
        EstadoOrden.Terminado => ("#256E6F", "#DDF2F0"),
        EstadoOrden.Entregado => (Exito, ExitoClaro),
        _ => throw new ArgumentOutOfRangeException(nameof(estado), estado, "Estado sin color definido.")
    };
}
