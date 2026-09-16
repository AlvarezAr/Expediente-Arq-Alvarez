namespace Observer;
// El contrato: EL OBSERVADOR SE RIGE AL VENCIMIENTO DEL SOCIO DEL GIMNACIO
public interface IInteresadoEnVencimiento
{
    void CuandoVence(string Socio, DateTime fechaVencimiento);
}
// Los interesados de siempre, ahora como suscriptores del observador:
public class WhatsAppSocio : IInteresadoEnVencimiento
{
    public void CuandoVence(string socio, DateTime fechaVencimiento)
        => Console.WriteLine($"[WhatsAppSocio] Llamando al Socio: {Socio} su membrecia vencio en {fechaVencimiento}");
}

public class RegistroVencidos: IInteresadoEnVencimiento
{
    public void CuandoVence(string socio, DateTime fechaVencimiento)
        => Console.WriteLine($"[ADMINISTRADOR] Anotado en el Registro de Vencidos: Suscripcion al gimnacio del {socio} vencio en {fechaVencimiento}");
}

public class Recepcion : IInteresadoEnVencimiento
{
    public void CuandoVence(string socio, DateTime fechaVencimiento)
        => Console.WriteLine($"[RECEPCION] Notificacion recibida: verifica si {socio} está vencido en {fechaVencimiento} su membrecia ");
}

// El interesado NUEVO DE PROMOCIONES. 
public class Promociones : IInteresadoEnVencimiento
{
    public void CuandoVence(string socio, DateTime fechaVencimiento)
        => Console.WriteLine($"[PROMOCIONES] Planifica ofrecer al {socio} y hacer seguimiento a su {fechaVencimiento}");
}

// EL ADMINISTRADOR: mantiene LA LISTA y anuncia. No conoce a nadie en concreto.
public class AdminstradorGimnacio
{
    private readonly List<IInteresadoEnVencimiento> _interesados = new();

    public void Suscribir(IInteresadoEnVencimiento interesado)
        => _interesados.Add(interesado);

    public void RegistrarVencimiento(string socio, DateTime fechaVencimiento)
    {
        Console.WriteLine($"[ADMINISTRACION] Vencimiento registrado: {socio} en {fechaVencimiento} — avisando a {_interesados.Count}");
        foreach (var interesado in _interesados)
            interesado.CuandoVence(socio,fechaVencimiento);
    }
}

public static class Demo
{
    public static void Correr()
    {
        var administracion = new AdminstradorGimnacio();
        administracion.Suscribir(new WhatsAppSocio());
        administracion.Suscribir(new RegistroVencidos());
        administracion.Suscribir(new Recepcion());

        administracion.RegistrarVencimiento("Alvaro","14/09/2026");

        Console.WriteLine();
        Console.WriteLine("-- llega el Modulo de Promociones: se SUSCRIBE y listo estara al tanto para hacer sus promociones al que no cuenta con membresia vigente --");
        administracion.Suscribir(new Promociones());
        administracion.RegistrarVencimiento("Daniela","15/09/2026");

        Console.WriteLine("El modulo de promociones entró SIN tocar la Administrador. Esto es Observer:");
    }
}