namespace Decorator;
// El contrato: cualquier cosa que se pueda emitir como diploma.
public interface IFactura
{
    void EmitirFactura(decimal subtotal);
}

// La pieza base: la factura con el SubTotal.
public class FacturaBase : IFactura
{
    public void EmitirFactura(double subtotal)
        => Console.WriteLine($"[FACTURA] Hoja Carta con un SubTotal de {subtotal}");
}

// EL DECORADOR: ES un IDetalleVenta... y CONTIENE otro IDetalleVenta. Esa dualidad es todo el truco.
public abstract class CapaDeFactura : IFactura
{
    protected readonly IDetalleVenta Interno;
    protected CapaDeFactura(IDetalleVenta interno) => Interno = interno;
    public abstract void EmitirFactura(double subtotal);
}

// Cada capa: primero deja pasar al de adentro, después suma lo suyo.
public class ConImpuestos : CapaDeFactura
{
    public ConImpuestos(IDetalleVenta interno) : base(interno) { }
    public override void EmitirFactura(double subtotal);
    {
        Interno.EmitirFactura(subtotal);
        Console.WriteLine(" ↳ subtotal * 0.13");
    }
}

public class ConDescuento : CapaDeFactura
{
    public ConDescuento(IDetalleVenta interno) : base(interno) { }
    public override void EmitirFactura(double subtotal);
    {
        Interno.EmitirFactura(subtotal);
        Console.WriteLine("   ↳ subtotal - (subtotal-20%)");
    }
}

public static class Demo
{
    public static void Correr()
    {
        // Las capas se APILAN en la llamada, como se le suman agregados a la Factura:
        IFactura completo =
            new ConDescuento(
                new ConImpuestos(
                    new FacturaBase()));
        completo.EmitirFactura("220");
    }
}