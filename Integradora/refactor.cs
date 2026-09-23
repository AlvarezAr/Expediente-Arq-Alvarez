// INTEGRADORA · VARIANTE A — Comedor Universitario "Sabor Andino"

namespace Integradora.Comedor;

public class Pedido
{
    public string Estudiante { get; }
    public string TipoMenu { get; }
    public int Cantidad { get; }

    public Pedido(string estudiante, string tipoMenu, int cantidad)
    {
        Estudiante = estudiante;
        TipoMenu = tipoMenu;
        Cantidad = cantidad;
    }
}

// Responsabilidad 1: REGLA DE NEGOCIO
// Cambia si contabilidad o administración modifica los precios.
public class CalculadoraDePrecios
{
    public decimal CalcularPrecioBase(string tipoMenu)
    {
        switch (tipoMenu)
        {
            case "estandar":
                return 12;
            case "vegetariano":
                return 14;
            case "beca":
                return 5;
            default:
                return 12;
        }
    }

    public decimal CalcularTotal(Pedido pedido)
    {
        decimal precioBase = CalcularPrecioBase(pedido.TipoMenu);
        return precioBase * pedido.Cantidad;
    }
}

// Responsabilidad 2: PERSISTENCIA
public class RepositorioDePedidos
{
    public void Guardar(Pedido pedido, decimal total)
    {
        Console.WriteLine(
            $"[BD] INSERT INTO pedidos VALUES " +
            $"('{pedido.Estudiante}', '{pedido.TipoMenu}', " +
            $"{pedido.Cantidad}, {total})"
        );
    }
}

// Responsabilidad 3: PRESENTACIÓN
public class ImpresoraDeVales
{
    public void Imprimir(Pedido pedido, decimal total)
    {
        Console.WriteLine("----- VALE DE COMEDOR -----");
        Console.WriteLine($"{pedido.Estudiante}: {pedido.Cantidad} x menú {pedido.TipoMenu}");
        Console.WriteLine($"TOTAL: {total:0.00} Bs");
    }
}

// Responsabilidad 4: COMUNICACIONES
public class NotificadorDePedidos
{
    public void Notificar(Pedido pedido)
    {
        Console.WriteLine(
            $"[CORREO] Pedido registrado: " +
            $"{pedido.Cantidad} x {pedido.TipoMenu}, {pedido.Estudiante}"
        );
    }
}

public class GestorDePedidos
{
    private readonly CalculadoraDePrecios _calculadora = new();
    private readonly RepositorioDePedidos _repositorio = new();
    private readonly ImpresoraDeVales _impresora = new();
    private readonly NotificadorDePedidos _notificador = new();

    public void ProcesarPedido(Pedido pedido)
    {
        decimal total = _calculadora.CalcularTotal(pedido);
        _repositorio.Guardar(pedido, total);
        _impresora.Imprimir(pedido, total);
        _notificador.Notificar(pedido);
    }
}

// Demostración
public static class Demo
{
    public static void Correr()
    {
        var pedido = new Pedido("Juan","vegetariano",2);

        new GestorDePedidos().ProcesarPedido(pedido);
    }
}
// Refactor: ARIEL ALVAREZ MAMANI.