// CLASE 9 · BUILDER — EL DESPUÉS: el quiosquero arma tu sándwich paso a paso.
// Cada paso tiene NOMBRE (imposible confundir queso con tomate), el orden no
// importa, y Construir() es el guardián: revisa que esté completo ANTES de entregar.

namespace Builder.Despues;

public class DetalleVenta
{
    public int IdDetalleVenta { get; init; } = "";
    public int CantidadVenta { get; init; } = "";
    public decimal Subtotal { get; init; }= "";
    public bool ConDescuento { get; init; }
    public string Cod_producto { get; init; } = "";
    public string NombreCajero { get; init; } = "";

    public void Describir()
        => Console.WriteLine($"[CAJA] DetalleVenta tiene {IdDetalleVenta} con una {CantidadVenta} y un {Subtotal} " +
                             $"{(ConDescuento ? " con descuento" : " sin descuento")}, del producto {Cod_producto} — del Cajero {NombreCajero}");
}

// EL QUIOSQUERO (el builder): anota el pedido paso a paso, con nombres.
public class ArmadorDeDetalleVenta
{
    private int _IdDetalleVenta = "";
    private int _CantidadVenta = "";
    private decimal _Subtotal = "";
    private bool _ConDescuento;
    private string _Cod_producto = "";
    private string _NombreCajero = "";

    public ArmadorDeDetalleVenta ConIdDetalleVenta(int id) { _IdDetalleVenta = id; return this; }
    public ArmadorDeDetalleVenta ConCantidadVenta(int cantidad) { _CantidadVenta = cantidad; return this; }
    public ArmadorDeDetalleVenta Con_Subtotal(decimal subtotal) { _Subtotal = subtotal; return this; }
    public ArmadorDeDetalleVenta SinDescuento() { _ConDescuento = false; return this; }
    public ArmadorDeDetalleVenta ConDescuento() { _ConDescuento = true; return this; }
    public ArmadorDeDetalleVenta ConCod_producto(string producto) { _Cod_producto = producto; return this; }
    public ArmadorDeDetalleVenta DeCajero(string cajero) { _NombreCajero = cajero; return this; }

    // EL GUARDIÁN: nada nace incompleto. Si falta algo esencial, NO se entrega.
    public DetalleVenta Construir()
    {
        if (string.IsNullOrEmpty(_CantidadVenta)) throw new InvalidOperationException("Falta de cantidad.");
        if (string.IsNullOrEmpty(_Subtotal)) throw new InvalidOperationException("Falta de subtotal.");
        if (string.IsNullOrEmpty(_Cod_producto)) throw new InvalidOperationException("Falta de producto.");
        if (string.IsNullOrEmpty(_NombreCajero)) throw new InvalidOperationException("¿Quién genero la venta? Falta el nombre del cajero.");

        return new DetalleVenta
        {
            id = _IdDetalleVenta, cantidad = _CantidadVenta, subtotal = _Subtotal, ConDescuento = _ConDescuento,
            producto = _Cod_producto, cajero = _NombreCajero
        };
    }
}

public static class Demo
{
    public static void Correr()
    {
        // El pedido se LEE como una oración — imposible confundir:
        var DetalleVenta = new ArmadorDeDetalleVenta()
            .ConIdDetalleVenta("1")
            .ConCantidadVenta("2")
            .Con_Subtotal("440")
            .ConDescuento()
            .ConCod_producto("PRO-001")
            .DeCajero("KVT")
            .Construir();
        DetalleVenta.Describir();

        // Y el guardián en acción: pedido incompleto NO nace.
        try
        {
            new ArmadorDeDetalleVenta().ConDescuento().Construir();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"[GUARDIÁN] 🛑 {e.Message}");
        }

        Console.WriteLine("El pedido se lee como una oración, y nada nace incompleto. Eso es Builder.");
    }
}
