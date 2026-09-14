namespace TiendaTenis.SinPatrones
{
    // PRODUCTO
    public class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }

        public Producto(string codigo, string nombre, string categoria,
                        decimal precio, int stock, int stockMinimo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            Stock = stock;
            StockMinimo = stockMinimo;
        }

        public void MostrarProducto()
        {
            Console.WriteLine(
                $"{Codigo} | {Nombre} | {Categoria} | Bs. {Precio} | Stock: {Stock}");
        }
    }
    // USUARIO / CAJERO
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }

        public Usuario(int idUsuario, string nombre, string rol)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Rol = rol;
        }
    }
    // DETALLE DE VENTA
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public bool ConDescuento { get; set; }

        public DetalleVenta(int idDetalleVenta, Producto producto,
                            int cantidad, bool conDescuento)
        {
            IdDetalleVenta = idDetalleVenta;
            Producto = producto;
            Cantidad = cantidad;
            ConDescuento = conDescuento;

            Subtotal = producto.Precio * cantidad;

            if (ConDescuento)
            {
                Subtotal = Subtotal * 0.90m;
            }
        }

        public void MostrarDetalle()
        {
            Console.WriteLine(
                $"Producto: {Producto.Nombre} | " +
                $"Cantidad: {Cantidad} | " +
                $"Subtotal: Bs. {Subtotal}");
        }
    }
    // VENTA
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Cajero { get; set; }
        public string Estado { get; set; }

        public List<DetalleVenta> Detalles { get; set; }

        public Venta(int idVenta, Usuario cajero)
        {
            IdVenta = idVenta;
            Fecha = DateTime.Now;
            Cajero = cajero;
            Estado = "Carrito";
            Detalles = new List<DetalleVenta>();
        }

        public void AgregarDetalle(DetalleVenta detalle)
        {
            Detalles.Add(detalle);

            detalle.Producto.Stock -= detalle.Cantidad;

            if (detalle.Producto.Stock <= detalle.Producto.StockMinimo)
            {
                Console.WriteLine(
                    $"ALERTA: El producto {detalle.Producto.Nombre} " +
                    $"está por debajo del stock mínimo.");
            }
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;

            foreach (DetalleVenta detalle in Detalles)
            {
                total += detalle.Subtotal;
            }

            return total;
        }

        public void ConfirmarVenta()
        {
            Estado = "Confirmada";
        }

        public void PagarVenta()
        {
            Estado = "Pagada";
        }

        public void EntregarVenta()
        {
            Estado = "Entregada";
        }

        public void AnularVenta()
        {
            Estado = "Anulada";
        }

        public void MostrarVenta()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("VENTA");
            Console.WriteLine("=================================");
            Console.WriteLine($"Número: {IdVenta}");
            Console.WriteLine($"Fecha: {Fecha}");
            Console.WriteLine($"Cajero: {Cajero.Nombre}");
            Console.WriteLine($"Estado: {Estado}");

            foreach (DetalleVenta detalle in Detalles)
            {
                detalle.MostrarDetalle();
            }

            Console.WriteLine($"TOTAL: Bs. {CalcularTotal()}");
        }
    }
    // GESTIÓN DE REPORTES
    public class GestionDeReportes
    {
        public void GenerarReporte(Venta venta)
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("REPORTE DE VENTAS");
            Console.WriteLine("=================================");
            Console.WriteLine($"Venta: {venta.IdVenta}");
            Console.WriteLine($"Fecha: {venta.Fecha}");
            Console.WriteLine($"Cajero: {venta.Cajero.Nombre}");
            Console.WriteLine($"Estado: {venta.Estado}");
            Console.WriteLine($"Total: Bs. {venta.CalcularTotal()}");
        }

        public void EnviarReporte(string canal, string mensaje)
        {
            // Sin Factory: se decide directamente aquí.
            if (canal == "WhatsApp")
            {
                Console.WriteLine($"Enviando por WhatsApp: {mensaje}");
            }
            else if (canal == "Telegram")
            {
                Console.WriteLine($"Enviando por Telegram: {mensaje}");
            }
            else if (canal == "Email")
            {
                Console.WriteLine($"Enviando por Email: {mensaje}");
            }
            else
            {
                Console.WriteLine("Canal no disponible.");
            }
        }
    }
    // PROGRAMA PRINCIPAL
    class Program
    {
        static void Main(string[] args)
        {
            // Crear productos
            Producto producto1 = new Producto(
                "PRO-001",
                "Tenis Nike",
                "Deportivos",
                220,
                10,
                3
            );
            Producto producto2 = new Producto(
                "PRO-002",
                "Botines Adidas",
                "Botines",
                300,
                5,
                2
            );
            // Crear cajero
            Usuario cajero = new Usuario(
                1,
                "Carlos",
                "Cajero"
            );
            // Crear venta
            Venta venta = new Venta(
                1001,
                cajero
            );
            // Crear detalles de venta
            DetalleVenta detalle1 = new DetalleVenta(
                1,
                producto1,
                2,
                false
            );
            DetalleVenta detalle2 = new DetalleVenta(
                2,
                producto2,
                1,
                true
            );
            // Agregar productos a la venta
            venta.AgregarDetalle(detalle1);
            venta.AgregarDetalle(detalle2);
            // Cambiar estado de la venta
            venta.ConfirmarVenta();
            venta.PagarVenta();
            // Mostrar venta
            venta.MostrarVenta();
            // Generar reporte
            GestionDeReportes reportes = new GestionDeReportes();
            reportes.GenerarReporte(venta);
            // Enviar reporte
            reportes.EnviarReporte(
                "WhatsApp",
                "Reporte de ventas generado correctamente."
            );
            Console.ReadKey();
        }
    }
}