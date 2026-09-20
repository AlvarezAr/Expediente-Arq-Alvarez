namespace AdapterObserver;
// PATRÓN OBSERVER Este es el contrato que deben cumplir todos los interesados
// que quieran recibir una notificación cuando se realice una venta.
public interface IInteresadoEnVentas
{
    void CuandoRealizaVenta(int idVenta, string cajero);
}
// Esta clase representa al administrador.
// Implementa IInteresadoEnVentas, por lo tanto debe implementar
public class NotificacionAdministrador : IInteresadoEnVentas
{
    public void CuandoRealizaVenta(int idVenta, string cajero)
    =>Console.WriteLine($"[ADMINISTRADOR] Venta: {idVenta} realizada por {cajero}");
        
}
public class NotificacionCliente : IInteresadoEnVentas
{
    public void CuandoRealizaVenta(int idVenta, string cajero)
    =>Console.WriteLine($"[CLIENTE] Transacción: {idVenta} realizada exitosamente por {cajero}");
}
//PATRÓN ADAPTER Esta es una clase de un proveedor externo.
// Este es el CONTRATO que nuestro sistema entiende.
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
// GESTOR DE VENTAS - AQUÍ CONVIVEN ADAPTER + OBSERVER
// Utiliza Adapter para consultar precios - Utiliza Observer para notificar las ventas.
public class GestorVentas
{
    // PARTE DEL ADAPTER - PROVEDORES
    private readonly IProveedor _proveedor;
    public GestorVentas(IProveedor proveedor) _proveedor = proveedor;
    // PARTE DEL OBSERVER - ALMACENA A LOS INTERESADOS 
    private readonly List<IInteresadoEnVentas> _interesados = new();
    public void Suscribir(IInteresadoEnVentas interesado) _interesados.Add(interesado);
    // PARTE DEL ADAPTER
    public void ConsultarPrecio(string precio, string producto)
        => Console.WriteLine($"[CAJERO] {precio} — {producto}: {_proveedor.ObtenerPrecioBs(producto)} Bs.");
    // PARTE DEL OBSERVER
    public void RegistrarVenta(int idVenta, string cajero)
    {
        Console.WriteLine($"[CAJERO] Venta registrada: {idVenta} por {cajero}  — avisando a {_interesados.Count} Interesados");
        foreach (var interesado in _interesados)
            interesado.CuandoRealizaVenta(idVenta, cajero);
    }       
}

// DEMOSTRACIÓN
public static class Demo
{
    public static void Correr()
    {
        var paraProvPeruanos = new GestorVentas(new AdaptadorProveedorSoles());
        paraProvPeruanos.ConsultarPrecio("120", "Botines");

        var paraProvAsiaticos = new GestorVentas(new AdaptadorProveedorYuan());
        paraProvAsiaticos.ConsultarPrecio("12000", "Tenis de Paseo");

        var cajero = new GestorVentas();
        cajero.Suscribir(new NotificacionAdministrador());
        cajero.Suscribir(new NotificacionCliente());

        cajero.RegistrarVenta(1,"KLM");
    }
}