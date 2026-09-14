// CLASE 9 · ADAPTER — EL DESPUÉS: el traductor de libretas.
// La escuela define SU contrato en SU idioma (notas 0-100). El traductor
// convierte EN LA FRONTERA. Si llega otro país con otro formato, se escribe
// OTRO traductor — y la secretaría ni se entera.

namespace Adapter.Despues;

// El sistema externo, intocable (de terceros), igual que antes:
public class ProveedorPeruano
{
    public double ObtenerPrecio(string producto)
        => producto == "Botines" ? 60 : 80;
}
public class ProveedorAsiatico
{
    public double ObtenerPrecio(string producto)
        => producto == "Tenis" ? 12.000 : 23.000;
}

// EL CONTRATO: 
// para entrar acá, tiene que hablar en bs.
public interface IProveedor
{
    double ObtenerPrecioBs(string producto);
}
// TRADUCTOR 1: 
public class AdaptadorProveedorSoles : IProveedor
{
    private readonly ProveedorSoles _proveedor = new();

    public double ObtenerPrecioBs(string producto)
    {
        string PrecioSoles = _proveedor.ObtenerPrecio(producto);
        return PrecioSoles * 3.71;
    }
}
// TRADUCTOR 2: 
public class AdaptadorProveedorYuan : IProveedor
{
    private readonly ProveedorYuan _proveedor = new();

    public double ObtenerPrecioBs(string producto)
    {
        string PrecioYuan = _proveedor.ObtenerPrecio(producto);
        return PrecioYuan * 1.86;
    }
}

public class GestorVentas
{
    private readonly IProveedor _proveedor;

    public GestorVentas(IProveedor proveedor) _proveedor = proveedor;

    public void ConsultarPrecio(string precio, string producto)
        => Console.WriteLine($"[CAJERO] {precio} — {producto}: {_proveedor.ObtenerPrecioBs(producto)} Bs.");
}

public static class Demo
{
    public static void Correr()
    {
        var paraProvPeruanos = new GestorVentas(new AdaptadorProveedorSoles());
        paraProvPeruanos.ConsultarPrecio("120", "Botines");

        var paraProvAsiaticos = new GestorVentas(new AdaptadorProveedorYuan());
        paraProvAsiaticos.ConsultarPrecio("12000", "Tenis de Paseo");

        Console.WriteLine("Dos proveedores, dos tipos de cambio , UN cajero que no cambió una línea.");
        Console.WriteLine("El traductor vive en la frontera. Eso es Adapter.");
    }
}