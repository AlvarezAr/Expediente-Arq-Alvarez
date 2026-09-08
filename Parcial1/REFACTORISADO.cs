// PARCIAL 1 · VARIANTE A — Farmacia "San Rafael"
// Sistema de pedidos de medicamentos. El código FUNCIONA, pero su diseño tiene
// 4 violaciones de principios SOLID. Tu trabajo: encontrarlas y curar dos.
//despues
namespace Parcial1.Farmacia;

public interface IEmpleadoDeFarmacia
{
    void RegistrarPedido(string medicamento, int cantidad);
    void AutorizarVentaControlada(string medicamento);
    void AjustarPrecio(string medicamento, decimal nuevoPrecio);
    void VerLibroDeControlados();
}

public class Farmaceutico : IEmpleadoDeFarmacia
{
    public void RegistrarPedido(string medicamento, int cantidad)
        => Console.WriteLine($"[FARM] Pedido: {cantidad} x {medicamento}");
    public void AutorizarVentaControlada(string medicamento)
        => Console.WriteLine($"[FARM] Venta controlada de {medicamento} autorizada");
    public void AjustarPrecio(string medicamento, decimal nuevoPrecio)
        => Console.WriteLine($"[FARM] {medicamento} ahora cuesta {nuevoPrecio:0.00} Bs");
    public void VerLibroDeControlados()
        => Console.WriteLine("[FARM] Libro de medicamentos controlados");
}

public class Cajero : IEmpleadoDeFarmacia
{
    public void RegistrarPedido(string medicamento, int cantidad)
        => Console.WriteLine($"[CAJA] Pedido: {cantidad} x {medicamento}");
    public void AutorizarVentaControlada(string medicamento)
        => throw new NotSupportedException("Un cajero no autoriza controlados.");
    public void AjustarPrecio(string medicamento, decimal nuevoPrecio)
        => throw new NotSupportedException("Un cajero no ajusta precios.");
    public void VerLibroDeControlados()
        => throw new NotSupportedException("Un cajero no accede al libro.");
}
//SOLUCION PROPUESTA PARA LA VIOLACION DE LA (S)
public class GestorDePedidos
{
    private readonly CalculadoraDeDescuentoAbierta calculadora;
    private readonly GeneradorDeComprobante comprobante;
    private readonly BaseDeDatosMySql baseDeDatos;
    private readonly CorreoSmtp correo;

    public GestorDePedidos(CalculadoraDeDescuentoAbierta calculadora,GeneradorDeComprobante comprobante,BaseDeDatosMySql baseDeDatos,CorreoSmtp correo)
    {
        this.calculadora = calculadora;
        this.comprobante = comprobante;
        this.baseDeDatos = baseDeDatos;
        this.correo = correo;
    }

    public void ProcesarPedido(string cliente,string tipoCliente,string medicamento,int cantidad,decimal precioUnitario)
    {
        decimal total = cantidad * precioUnitario;
        // La responsabilidad del descuento está delegada.
        decimal descuento =
            calculadora.Calcular(tipoCliente, total);
        decimal totalFinal = total - descuento;
            // La responsabilidad de guardar sigue en
            // BaseDeDatosMySql.
            baseDeDatos.GuardarPedido(cliente,medicamento,cantidad,totalFinal);
            // La responsabilidad del comprobante está delegada.
            comprobante.Mostrar(cliente,tipoCliente,medicamento,cantidad,totalFinal);
            // Se mantiene la clase original CorreoSmtp.
            correo.Enviar($"Su pedido de {medicamento} fue registrado, {cliente}");
    }
}

// Esta clase solamente calcula el descuento.
public class CalculadoraDeDescuentoAbierta
{
    private readonly List<IPoliticaDescuento> politicas;
    public CalculadoraDeDescuentoAbierta(List<IPoliticaDescuento> politicas)
    {
        this.politicas = politicas;
    }
    public decimal Calcular(string tipoCliente,decimal total)
    {
        foreach (var politica in politicas)
        {
            if (politica.AplicaA(tipoCliente))
            {
                return politica.Calcular(total);
            }
        }
        return 0;
    }
}
// Esta clase solamente genera/muestra el comprobante.
public class GeneradorDeComprobante
{
    public void Mostrar(string cliente,string tipoCliente,string medicamento,int cantidad,decimal totalFinal)
    {
        Console.WriteLine("----- COMPROBANTE -----");
        Console.WriteLine($"{cantidad} x {medicamento}");
        Console.WriteLine($"Cliente: {cliente} ({tipoCliente})");
        Console.WriteLine($"TOTAL: {totalFinal:0.00} Bs");
    }
}
//ESTA CLASE SOLAMENTE GUARDA EL PEDIDO
public class BaseDeDatosMySql
{
    public void GuardarPedido(string cliente,string medicamento,int cantidad,decimal total)
        => Console.WriteLine($"[MYSQL] INSERT INTO pedidos VALUES " + $"('{cliente}', '{medicamento}', {cantidad}, {total})");
}
//ESTA CLASE SOLAMENTE ENVIA EL MENSAJE EN AL CORREO
public class CorreoSmtp
{
    public void Enviar(string mensaje)
        => Console.WriteLine($"[SMTP] {mensaje}");
}
// Contrato para las diferentes reglas de descuento.
public interface IPoliticaDescuento
{
    bool AplicaA(string tipoCliente);
    decimal Calcular(decimal total);
}
// Cada tipo de cliente tiene su propia regla.
// Cliente particular
public class DescuentoParticular : IPoliticaDescuento
{
    public bool AplicaA(string tipoCliente)
        => tipoCliente == "particular";
    public decimal Calcular(decimal total)
        => 0;
}
// Cliente asegurado
public class DescuentoAsegurado : IPoliticaDescuento
{
    public bool AplicaA(string tipoCliente)
        => tipoCliente == "asegurado";
    public decimal Calcular(decimal total)
        => total * 0.20m;
}
// Cliente con convenio
public class DescuentoConvenio : IPoliticaDescuento
{
    public bool AplicaA(string tipoCliente)
        => tipoCliente == "convenio";
    public decimal Calcular(decimal total)
        => total * 0.10m;
}

public static class Demo
{
public static void Correr()
{
var politicas = new List<IPoliticaDescuento>
{
new DescuentoParticular(),
new DescuentoAsegurado(),
new DescuentoConvenio()
};


    var calculadora =
        new CalculadoraDeDescuentoAbierta(politicas);

    var comprobante =
        new GeneradorDeComprobante();

    var baseDeDatos =
        new BaseDeDatosMySql();

    var correo =
        new CorreoSmtp();

    var gestor =
        new GestorDePedidos(
            calculadora,
            comprobante,
            baseDeDatos,
            correo);

    gestor.ProcesarPedido(
        "Noelia",
        "asegurado",
        "Paracetamol 500mg",
        2,
        8.50m);
}

}
