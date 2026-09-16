
namespace Observer;

// El contrato: todo interesado en las ventas sabe reaccionar cuando ocurre una.
public interface IInteresadoEnVentas
{
    void CuandoRealizaVenta(int idVenta, string cajero);
}

// Los interesados de siempre, ahora como suscriptores:
public class NotificacionAdministrador : IInteresadoEnVentas
{
    public void CuandoRealizaVenta(int idVenta, string cajero)
        => Console.WriteLine($"[NOTIFICACION] Venta: {idVenta} realizada por {cajero}");
}

public class NotificacionCliente : IInteresadoEnVentas
{
    public void CuandoRealizaVenta(int idVenta, string cajero)
        => Console.WriteLine($"[NOTIFICACION] Transaccion: {idVenta} realizada exitosamente por {cajero}");
}

// EL CAJERO: mantiene LA LISTA y anuncia. No conoce a nadie en concreto.
public class GestorVentas
{
    private readonly List<IInteresadoEnVentas> _interesados = new();

    public void Suscribir(IInteresadoEnVentas interesado)
        => _interesados.Add(interesado);

    public void RegistrarVenta(int idVenta, string cajero)
    {
        Console.WriteLine($"[CAJERO] Venta registrada: {idVenta} por {cajero}  — avisando a {_interesados.Count} Interesados");
        foreach (var interesado in _interesados)
            interesado.CuandoRealizaVenta(idVenta, cajero);
    }
}

public static class Demo
{
    public static void Correr()
    {
        var cajero = new GestorVentas();
        cajero.Suscribir(new NotificacionAdministrador());
        cajero.Suscribir(new NotificacionCliente());

        cajero.RegistrarVenta(1,"KLM");

        Console.WriteLine();
        Console.WriteLine("el que anuncia no conoce a los que escuchan.");
    }
}