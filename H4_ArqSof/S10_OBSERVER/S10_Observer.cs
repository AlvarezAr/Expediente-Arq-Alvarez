namespace Observer.Despues;
//el contrato
public interface IInteresadoEnFaltas
{
    void CuandoFalta(string alumno);
}

//los interesados de siempre, ahora como suscriptores

public class TelefonoAlPapa : IInteresadoEnFaltas
{
    public void CuandoFalta(string alumno)
        =>Console.WriteLine($"[TELEFONO] Llamando al papa: {alumno} falto hoy");
}
public class CuadernoTutor : IInteresadoEnFaltas
{
    public void CuandoFalta(string alumno)
        =>Console.WriteLine($"[TUTOR] Anota en el cuaderno: falta de {alumno}");
}
public class Enfermeria : IInteresadoEnFaltas
{
    public void CuandoFalta(string alumno)
        =>Console.WriteLine($"[ENFERMERIA] Aviso Recibido:verificar si {alumno} esta enfermo");
}