using System.Globalization;
using System.Text;

namespace GestorTaller.Negocio;

/// <summary>
/// Filtro de busqueda para los combobox: coincidencia parcial, sin distinguir
/// mayusculas/minusculas ni acentos. Texto vacio = todos los elementos.
/// </summary>
public static class FiltroDeBusqueda
{
    public static IReadOnlyList<T> Filtrar<T>(IEnumerable<T> elementos, string? texto, Func<T, string> selectorTexto)
    {
        if (elementos is null)
        {
            throw new ArgumentException("Se requiere la lista de elementos.", nameof(elementos));
        }

        if (selectorTexto is null)
        {
            throw new ArgumentException("Se requiere el selector de texto.", nameof(selectorTexto));
        }

        var lista = elementos.ToList();
        var buscado = Normalizar(texto);

        if (buscado.Length == 0)
        {
            return lista;
        }

        return lista.Where(e => Normalizar(selectorTexto(e)).Contains(buscado)).ToList();
    }

    public static string Normalizar(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            return string.Empty;
        }

        var descompuesto = texto.Trim().Normalize(NormalizationForm.FormD);
        var resultado = new StringBuilder();

        foreach (var caracter in descompuesto)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
            {
                resultado.Append(caracter);
            }
        }

        return resultado.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
    }
}
