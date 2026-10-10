// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;

Console.WriteLine("Hello, World!");
Console.WriteLine("prueba de cambios");

string ValidarCamposVacios( string texto)
{
   
    Console.WriteLine(texto);
    string nombre = Console.ReadLine();
    while (nombre == "")
    {
        Console.WriteLine("EL CAMPO NO PUEDE IR VACIO");
        Console.WriteLine(texto);
        nombre = Console.ReadLine();
    }
    
    return nombre;
}

string valido = ValidarCamposVacios("ingrese su nombre");
Console.WriteLine(valido);