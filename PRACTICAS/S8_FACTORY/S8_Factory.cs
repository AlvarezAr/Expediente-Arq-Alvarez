namespace Factory;

// El contrato: todo aviso, sea cual sea el canal, sabe enviarse.
public interface ICanalDeAviso
{
    void Enviar(string mensaje, string destinatario);
}

// Las piezas: un aviso por canal, cada una cumple el contrato.
//Interface de gestion de reportes de mi dominio
public class CanalDeAvisoWhatsApp : ICanalDeAviso
{
    public void Enviar(string mensaje, string destinatario)
        => Console.WriteLine($"[WhatsApp] {mensaje}— pegado para {destinatario}");

}
public class CanalDeAvisoTelegram : ICanalDeAviso
{
    public void Enviar(string mensaje, string destinatario)
        => Console.WriteLine($"[Telegram] {mensaje}— pegado para {destinatario}");

}
// LA FÁBRICA: la ventanilla única. El único lugar del sistema que sabe
// qué canales existen. Un canal nuevo se agrega ACÁ y en ningún otro lado.
public static class FabricaDeCanalDeAviso
{
    public static ICanalDeAviso Crear(string canal) => canal switch
    {
        "WhatsApp" => new AvisoCuaderno(),
        "Telegram"  => new AvisoLlamada(),     // ← el canal nuevo entró TOCANDO SOLO ACÁ
        _ => throw new ArgumentException($"Canal desconocido: {canal}")
    };
}

// Los módulos piden a la ventanilla y usan el contrato. Ni saben qué canal les tocó.
public class GestionDeReportes
{
    public void AvisarReportes(string canal, string propietario)
        => FabricaDeCanalDeAviso.Crear(canal).Enviar("Reporte de Ventas", propietario);
}

public class GestionDeReportesAdm
{
    public void PersistenciaReportes(string canal, string propietario)
        => FabricaDeCanalDeAviso.Crear(canal).Enviar("Persistencia de Reportes de Ventas", propietario);
}

public static class Demo
{
    public static void Correr()
    {
        new GestionDeReportes().AvisarReportes("whatsapp", "el numero personal del propietario de la tienda");
        new GestionDeReportesAdm().PersistenciaReportes("Telegram", "el numero coorporativo del propietario de la tienda");

        Console.WriteLine("El canal LLAMADA entró tocando UN solo lugar. Los módulos ni se enteraron.");
        Console.WriteLine("(El switch no desapareció: se CONCENTRÓ donde hace un solo daño controlado.)");
    }
}






using System;

// =======================
// PRODUCTO
// =======================
public interface ICanalDeAviso
{
    void Enviar(string mensaje);
}

// =======================
// PRODUCTOS CONCRETOS
// =======================
public class WhatsApp : ICanalDeAviso
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine($"[WhatsApp] {mensaje}");
    }
}

public class Telegram : ICanalDeAviso
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine($"[Telegram] {mensaje}");
    }
}

// =======================
// FACTORY METHOD
// =======================
public abstract class AvisoFactory
{
    public abstract ICanalDeAviso CrearCanal();
}

// =======================
// FABRICAS CONCRETAS
// =======================
public class WhatsAppFactory : AvisoFactory
{
    public override ICanalDeAviso CrearCanal()
    {
        return new WhatsApp();
    }
}

public class TelegramFactory : AvisoFactory
{
    public override ICanalDeAviso CrearCanal()
    {
        return new Telegram();
    }
}

public class EmailFactory : AvisoFactory
{
    public override ICanalDeAviso CrearCanal()
    {
        return new Email();
    }
}

// =======================
// RF5 - GESTION DE REPORTES
// =======================
public class GestionDeReportes
{
    private AvisoFactory factory;

    public GestionDeReportes(AvisoFactory factory)
    {
        this.factory = factory;
    }

    public void GenerarReporte()
    {
        Console.WriteLine("================================");
        Console.WriteLine("REPORTE DE VENTAS");
        Console.WriteLine("================================");
        Console.WriteLine("Total Ventas: Bs. 5000");
        Console.WriteLine("Productos Vendidos: 25");

        ICanalDeAviso canal = factory.CrearCanal();

        canal.Enviar("Reporte generado correctamente.");
    }
}

// =======================
// MAIN
// =======================
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Seleccione canal de aviso:");
        Console.WriteLine("1. WhatsApp");
        Console.WriteLine("2. Telegram");
        Console.WriteLine("3. Email");

        string opcion = Console.ReadLine();

        AvisoFactory factory = null;

        switch (opcion)
        {
            case "1":
                factory = new WhatsAppFactory();
                break;

            case "2":
                factory = new TelegramFactory();
                break;

            case "3":
                factory = new EmailFactory();
                break;

            default:
                Console.WriteLine("Opción inválida");
                return;
        }

        GestionDeReportes reporte = new GestionDeReportes(factory);

        reporte.GenerarReporte();

        Console.ReadKey();
    }
}