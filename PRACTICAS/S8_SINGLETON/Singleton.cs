public class ConexionBD
{
    // Instancia única
    private static ConexionBD instancia;

    // Constructor privado
    private ConexionBD()
    {
        Console.WriteLine("Conexión a la Base de Datos creada.");
    }

    // Método para obtener la instancia única
    public static ConexionBD ObtenerInstancia()
    {
        if (instancia == null)
        {
            instancia = new ConexionBD();
        }

        return instancia;
    }
}


class Program
{
    static void Main(string[] args)
    {
        ConexionBD c1 = ConexionBD.ObtenerInstancia();
        ConexionBD c2 = ConexionBD.ObtenerInstancia();
        ConexionBD c3 = ConexionBD.ObtenerInstancia();

        Console.WriteLine(c1 == c2); // True
        Console.WriteLine(c2 == c3); // True
    }
}