
// VALIDACIONES
string ValidarCamposVacios( string texto)
{
   
    Console.Write(texto);
    string letra = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(letra))
    {
        Console.WriteLine("EL CAMPO NO PUEDE IR VACIO");
        Console.Write(texto);
        letra = Console.ReadLine();
    }
    
    return letra;
}

int ValidarNumerosEnteros(string texto)
{
    Console.Write(texto);
    string entrada = Console.ReadLine();
    int numero;

    while (!int.TryParse(entrada, out numero) || numero < 0)
    {
        Console.WriteLine("ERROR: DEBE INGRESAR UN NUMERO ENTERO POSITIVO...");
        Console.Write(texto);
        entrada = Console.ReadLine();
    }

    return numero;
}

char ValidarChar(string texto)
{
    Console.Write(texto);
    string sexo = Console.ReadLine().ToUpper();
    while(sexo != "F" && sexo != "M")
    { 
        Console.WriteLine("DATOS INVALIDOS, DEBES ELEGIR LOS DATOS CORRECTOS");
        Console.Write(texto);
        sexo = Console.ReadLine();  
    }
    return Convert.ToChar(sexo);
}

DateTime ValidarFechaNacimiento(string texto)
{
    Console.Write(texto);
    string entrada = Console.ReadLine();
    DateTime fechaResultado;
    DateTime hoy = DateTime.Today;
    int anioMinimo = hoy.Year - 120;
    while (!DateTime.TryParse(entrada, out fechaResultado) || fechaResultado > hoy || fechaResultado.Year < anioMinimo)
    {
        Console.WriteLine($"ERROR: Ingrese una fecha válida (DD/MM/AAAA) entre el año {anioMinimo} y hoy.");
        Console.Write(texto);
        entrada = Console.ReadLine();
    }
    return fechaResultado;
}

string ValidarSiNo (string  texto)
{
    Console.Write(texto);
    string validar = Console.ReadLine().ToUpper();

    while (validar != "SI" && validar != "NO")
    {
        Console.WriteLine("DATOS INVALIDOS, DEBES ELEGIR (SI / NO)");
        Console.Write(texto);
        validar = Console.ReadLine().ToUpper();
    }
    return validar;
}

// CLASE CONTENEDORA
class Cliente
{
    string Nombre { get; set; }
    string Apellido { get; set; }
    int Edad { get; set; }
    char sexo {  get; set; }
    DateTime FechaNacimiento { get; set; }
    string Licencia { get; set; }
    int Sueldo { get; set; }
}
// PARTE DE CLEIBEL - CREAR EL METODO DE MENU DE OPCIONES, E INICIALIZAR LA SETENCIA SHWITC PARA SU USO

// PARTE DE MADDONALL - CREAR LAS PRIMERAS 3 OPCIONES DEL MENU
Console.Write("hola");