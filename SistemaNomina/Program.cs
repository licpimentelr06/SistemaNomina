
string ValidarCamposVacios( string texto)
{
   
    Console.WriteLine(texto);
    string letra = Console.ReadLine();
    while (letra == "")
    {
        Console.WriteLine("EL CAMPO NO PUEDE IR VACIO");
        Console.WriteLine(texto);
        letra = Console.ReadLine();
    }
    
    return letra;
}

string valido = ValidarCamposVacios("ingrese su nombre: ");
Console.WriteLine(valido);

int ValidarNumero ( string texto)
{
    Console.Write(texto);
    int numero = Convert.ToInt32 (Console.ReadLine());
    return numero;
}
