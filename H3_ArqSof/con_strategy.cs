namespace Strateg;

// El contrato: toda regla de cálculo sabe producir la nota final.
public interface ICalcularDescuento
{
    double CalcularDescuento(double Subtotal);
}

// Las reglas, una clase por plan — cada una sabe SU fórmula y nada más:
public class SinDescuento : ICalcularDescuento
{
    public double CalcularDescuento(double Subtotal)
        => Subtotal;
}

public class ConDescuento20 : ICalcularDescuento
{
    public double CalcularDescuento(double Subtotal)
        => Subtotal - (Subtotal*0.20);
}
public class ConDescuento50 : ICalcularDescuento
{
    public double CalcularDescuento(double Subtotal)
        => Subtotal - (Subtotal*0.50);
}

// La calculadora: recibe LA REGLA por constructor y la aplica. No hay ningún if.
public class CalculadoraDeVenta
{
    private readonly ICalcularDescuento _regla;

    public CalculadoraDeVenta(ICalcularDescuento regla) => _regla = regla;

    public double Calcular(double Subtotal)
        => _regla.CalcularDescuento(Subtotal);
}

public static class Demo
{
    public static void Correr()
    {
        double Subtotal = 350;

        Console.WriteLine($"[SIN_DESCUENTO]     CAJERO_1:  {new CalculadoraDeVenta(new SinDescuento()).Calcular(Subtotal):0}");
        Console.WriteLine($"[CON_DESCUENTO20]   CAJERO_2: {new CalculadoraDeVenta(new ConDescuento20()).Calcular(practicas):0}");
        
        Console.WriteLine("-- POR ANIVERSARIO TODO A MITAD DE PRECIO--");
        Console.WriteLine($"[CON_DESCUENTO50]  CAJERO_1:  {new CalculadoraDeVenta(new ConDescuento50()).Calcular(Subtotal):0}");
    }
}