// Variables y constantes
using System.Globalization;

const decimal porcentajeSFS = 0.0304m; // 3.04% Seguro Familiar de Salud (aporte empleado)
const decimal porcentajeAFP = 0.0287m; // 2.87% Fondo de Pensiones (aporte empleado)
const decimal porcentajeTssTotal = porcentajeSFS + porcentajeAFP; // 5.91% Total empleado
const decimal limiteExentoAnual = 416220.00m; // Escala Anual Exenta: Hasta RD$416,220.00 al año NO paga ISR
const decimal limiteTramo2Anual = 624329.00m; // Límite superior del 15%: De RD$416,220.01 a RD$624,329.00
const decimal limiteTramo3Anual = 867123.00m; // Límite superior del 20%: De RD$624,329.01 a RD$867,123.00
const decimal tasaTramo1 = 0.15m;    // 15% aplicado al excedente del primer tramo
const decimal baseTramo2 = 31216.00m; // Monto fijo de impuesto acumulado para el segundo tramo
const decimal tasaTramo2 = 0.20m;    // 20% aplicado al excedente del segundo tramo
const decimal baseTramo3 = 79776.00m; // Monto fijo de impuesto acumulado para el tercer tramo
const decimal tasaTramo3 = 0.25m;    // 25% aplicado al excedente del tercer tramo
const decimal mesesDelAnio = 12m;    // Cantidad de meses en el año para calcular el salario anual y mensual

// LISTA CONTENEDORA
List<Cliente> CrearEmpleado = new List<Cliente>();


// METODOS Y VALIDACIONES
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
decimal ValidarNumerosDecimales(string texto)
{
    Console.Write(texto);
    string entrada = Console.ReadLine();
    decimal numero;

    while (!decimal.TryParse(entrada, out numero) || numero < 0)
    {
        Console.WriteLine("ERROR: DEBE INGRESAR UN NUMERO DECIMAL POSITIVO...");
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

decimal CalcularTSS(decimal sueldoBruto, decimal porcentajeTss)
{
    decimal montoDescuentoTss = sueldoBruto * porcentajeTss;

    return montoDescuentoTss;
}
decimal CalcularISR
    (
    decimal sueldoBruto,
    decimal tss, 
    decimal limExento,
    decimal limTramo2, 
    decimal limTramo3,
    decimal tasa1, 
    decimal base2, 
    decimal tasa2, 
    decimal base3, 
    decimal tasa3, 
    decimal meses
    )
{
    decimal sueldoGravableMensual = sueldoBruto - tss;

    decimal sueldoAnual = sueldoGravableMensual * meses;

    decimal isrAnual = 0m; 

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

    decimal isrMensual = isrAnual / meses;

    return isrMensual;
}

decimal CalcularSueldoNeto(decimal sueldoBruto, decimal descuentoTss, decimal descuentoIsr)
{
     decimal totalDeducciones = descuentoTss + descuentoIsr; 
    decimal resultadoSueldoNeto = sueldoBruto - totalDeducciones; 
    
    return resultadoSueldoNeto;
}
void MostrarMenu()
{
    Console.WriteLine("\n==================== MENU PRINCIPAL ==========================");
    Console.WriteLine("                     1. Crear clientes");
    Console.WriteLine("                     2. Consultar clientes");
    Console.WriteLine("                     3. Ver la nomina de empleados");
    Console.WriteLine("                     4. Buscar empleados mujeres");
    Console.WriteLine("                     5. Buscar empleados con licencia");
    Console.WriteLine("                     6. Buscar empleados con salario mayor de 50,000");
    Console.WriteLine("                     0. Salir");
    Console.WriteLine("===============================================================");
}
void Presentacion ()
{
    Console.WriteLine("\n===== EXAMEN FINAL =====");
    Console.WriteLine("CURSO: INTERNET DE LAS COSAS");
    Console.WriteLine("MATERIA: PROGRAMACION CON C# ");
    Console.WriteLine("     INTEGRANTES:            ");
    Console.WriteLine("  LUIS MIGUEL PIMENTEL       ");
    Console.WriteLine("  CLEYBY JIMENEZ RAMIREZ     ");
    Console.WriteLine("  MACDONALD PIERRE PROFETA   ");
    Console.WriteLine("================================");

}

// FUJO DEL SISTEMA
bool salir = false;
Presentacion();
do
{
    MostrarMenu();
    int opcion = ValidarNumerosEnteros("OPCION: ");
    switch (opcion)
    {
        case 0: Console.WriteLine("SALIENDO DEL SISTEMA...."); salir = true; break;
        case 1:
            
            string nombre = ValidarCamposVacios("NOMBRE: ").ToUpper();
            string apellido = ValidarCamposVacios("APELLIDO: ").ToUpper();
            int edad = ValidarNumerosEnteros("EDAD: ");
            char sexo = ValidarChar("SEXO: ");
            DateTime nacimiento = ValidarFechaNacimiento("FECHA DE NACIMIENTO (FORMATO:DD/MM/AA):  ");
            string licencia = ValidarSiNo("POSEES LICENCIA: ");
            decimal sueldo = ValidarNumerosDecimales("INGRESE SU SUELDO (FORMATO: 00.00): ");

            CrearEmpleado.Add(new Cliente
            {
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                sexo = sexo,
                FechaNacimiento = nacimiento,
                Licencia = licencia,
                Sueldo = sueldo,
            });
            Console.WriteLine("EMPLEADO CREADO CORRECTAMENTE....");
            break;
        case 2:
            Console.Write("ESCRIBA EL NOMBRE DEL EMPLEADO A CONSULTAR: "); String consultaEmpleado = Console.ReadLine();
            Cliente EncontrarCliente = CrearEmpleado.FirstOrDefault(b => $"{b.Nombre} {b.Apellido}".Equals(consultaEmpleado,StringComparison.OrdinalIgnoreCase));
            if (EncontrarCliente != null)
            {
                Console.WriteLine("\n¡Cliente Encontrado! Aquí están sus datos:");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine($"Nombre Completo: {EncontrarCliente.Nombre} {EncontrarCliente.Apellido}");
                Console.WriteLine($"Edad:            {EncontrarCliente.Edad} años");
                Console.WriteLine($"Nacimiento:      {EncontrarCliente.FechaNacimiento}");
                Console.WriteLine($"Licencia:        {EncontrarCliente.Licencia} ");
                Console.WriteLine($"Sueldo Bruto:   ${EncontrarCliente.Sueldo:N2}");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("             DEDUCCIONES:                ");

                decimal sueldoEmpleado = EncontrarCliente.Sueldo;
                decimal totalTss = CalcularTSS(sueldoEmpleado, porcentajeTssTotal);
                decimal totalIsr = CalcularISR(
                    sueldoEmpleado,
                    totalTss,
                    limiteExentoAnual,
                    limiteTramo2Anual,
                    limiteTramo3Anual,
                    tasaTramo1,
                    baseTramo2,
                    tasaTramo2,
                    baseTramo3,
                    tasaTramo3,
                    mesesDelAnio
                );
                decimal sueldoNeto = CalcularSueldoNeto(sueldoEmpleado, totalTss, totalIsr);

                Console.WriteLine($"TSS:                     ${totalTss:N2}");
                Console.WriteLine($"Impuesto sobre la renta: ${totalIsr:N2}");
                Console.WriteLine($"Sueldo Neto:             ${sueldoNeto:N2}");
            }
            else
            {
                Console.WriteLine($"\n[!] Error: El cliente '{consultaEmpleado}' no está registrado.");
            }
            break;
        case 3:
            Console.WriteLine("================================");
            Console.WriteLine("     NOMINA DE EMPLEADOS:       ");
            Console.WriteLine("================================");

            foreach (Cliente c in CrearEmpleado)
            {
                int i = 1;
                Console.WriteLine($"{i} - NOMBRE: {c.Nombre} {c.Apellido} => SUELDO: ${c.Sueldo:N2}");
                i++;
            }
            break;
        case 4:
            Console.WriteLine("LISTADO DE EMPLEADAS FEMENINAS:");
            foreach(Cliente f in CrearEmpleado)
            {
                if (f.sexo == 'F')
                {
                    int n = 1;
                    Console.WriteLine($"{n} - {f.Nombre} {f.Apellido}");
                    n++;
                }
            }
            break;
        case 5:
            Console.WriteLine(" EMPLEADOS QUE POSEEN LICENCIA: :");
            foreach (Cliente e in CrearEmpleado)
            {
                if (e.Licencia == "SI")
                {
                    int n = 1;
                    Console.WriteLine($"{n} - {e.Nombre} {e.Apellido}");
                    n++;
                }
            }
            break;
        case 6:
            Console.WriteLine("EMPLEADOS CON SUELDO MAYOR A 50,000.00:");
            foreach (Cliente s in CrearEmpleado)
            {
                decimal tope = 50000m;
                if (s.Sueldo >= tope)
                {
                    int n = 1;
                    Console.WriteLine($"{n} - {s.Nombre} {s.Apellido} => {s.Sueldo:N2}");
                    n++;
                }
            }
            break;
    }

}
while (!salir);




// CLASE CONTENEDORA
class Cliente
{
   public string Nombre { get; set; }
   public string Apellido { get; set; }
   public int Edad { get; set; }
   public char sexo { get; set; }
   public DateTime FechaNacimiento { get; set; }
   public  string Licencia { get; set; }
   public decimal Sueldo { get; set; }
}
