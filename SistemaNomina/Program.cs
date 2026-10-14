
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
// PARTE DE MADDONALL - CREAR LAS PRIMERAS 3 OPCIONES DEL MENU
// CALCULO TSS
decimal porcentajeSFS = 0.0304m; // 3.04% Seguro Familiar de Salud (aporte empleado)
decimal porcentajeAFP = 0.0287m; // 2.87% Fondo de Pensiones (aporte empleado)
decimal porcentajeTssTotal = porcentajeSFS + porcentajeAFP; // 5.91% Total empleado

Console.Write("Ingrese su sueldo bruto: RD$ ");
decimal sueldo = Convert.ToDecimal(Console.ReadLine());

decimal totalTss = CalcularTSS(sueldo, porcentajeTssTotal);

static decimal CalcularTSS(decimal sueldoBruto, decimal porcentajeTss)
{
    decimal montoDescuentoTss = sueldoBruto * porcentajeTss;
    
    return montoDescuentoTss;
}
// METODO IMPUESTO SOBRE LA RENTA
// 2. CÁLCULO DE IMPUESTO SOBRE LA RENTA (ISR - DGII)


// VARIABLES DE LÍMITES Y TRAMOS ANUALES DE LA DGII
decimal limiteExentoAnual = 416220.00m; // Escala Anual Exenta: Hasta RD$416,220.00 al año NO paga ISR
decimal limiteTramo2Anual = 624329.00m; // Límite superior del 15%: De RD$416,220.01 a RD$624,329.00
decimal limiteTramo3Anual = 867123.00m; // Límite superior del 20%: De RD$624,329.01 a RD$867,123.00

// VARIABLES DE TASAS E IMPUESTOS BASE DE LA DGII 
decimal tasaTramo1 = 0.15m;    // 15% aplicado al excedente del primer tramo
decimal baseTramo2 = 31216.00m; // Monto fijo de impuesto acumulado para el segundo tramo
decimal tasaTramo2 = 0.20m;    // 20% aplicado al excedente del segundo tramo
decimal baseTramo3 = 79776.00m; // Monto fijo de impuesto acumulado para el tercer tramo
decimal tasaTramo3 = 0.25m;    // 25% aplicado al excedente del tercer tramo

//VARIABLE DE CONVERSIÓN DE TIEMPO
decimal mesesDelAnio = 12m;    // Cantidad de meses en el año para calcular el salario anual y mensual

// Llamada al método ISR usando el sueldo bruto y la TSS previamente calculada
decimal totalIsr = CalcularISR(sueldo, totalTss, limiteExentoAnual, limiteTramo2Anual, limiteTramo3Anual, tasaTramo1, baseTramo2, tasaTramo2, baseTramo3, tasaTramo3, mesesDelAnio);

Console.WriteLine($"Descuento ISR Mensual: RD$ {totalIsr:N2}");

// MÉTODO PARA CALCULAR EL ISR CON PARÁMETROS

static decimal CalcularISR(decimal sueldoBruto, decimal tss, decimal limExento, decimal limTramo2, decimal limTramo3, decimal tasa1, decimal base2, decimal tasa2, decimal base3, decimal tasa3, decimal meses)
{
    // Restamos la TSS al sueldo bruto para obtener el sueldo imponible o gravable mensual
    decimal sueldoGravableMensual = sueldoBruto - tss;
    
    // Multiplicamos por 12 para obtener el sueldo gravable proyectado al año
    decimal sueldoAnual = sueldoGravableMensual * meses;
    
    decimal isrAnual = 0m; // Variable para almacenar el impuesto anual total

    // Evaluación de los tramos según la escala impositiva de la DGII
    if (sueldoAnual <= limExento)
    {
        isrAnual = 0m; // Exento, no paga impuestos
    }
    else if (sueldoAnual <= limTramo2)
    {
        decimal excedente = sueldoAnual - limExento;
        isrAnual = excedente * tasa1; // Paga el 15% sobre el excedente
    }
    else if (sueldoAnual <= limTramo3)
    {
        decimal excedente = sueldoAnual - limTramo2;
        isrAnual = base2 + (excedente * tasa2); // Paga RD$31,216.00 + 20% del excedente
    }
    else
    {
        decimal excedente = sueldoAnual - limTramo3;
        isrAnual = base3 + (excedente * tasa3); // Paga RD$79,776.00 + 25% del excedente
    }

    // Dividimos el ISR anual entre 12 meses para obtener la retención mensual exacta
    decimal isrMensual = isrAnual / meses;
    
    return isrMensual;
}

// CLASE CONTENEDORA
class Cliente
{
    string Nombre { get; set; }
    string Apellido { get; set; }
    int Edad { get; set; }
    char sexo { get; set; }
    DateTime FechaNacimiento { get; set; }
    string Licencia { get; set; }
    int Sueldo { get; set; }
}
// PARTE DE CLEIBEL - CREAR EL METODO DE MENU DE OPCIONES, E INICIALIZAR LA SETENCIA SHWITC PARA SU USO


