using System;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Bienvenido a WintC#");

string version = "WintC# 0.1";
string[] Commands = ["echo", "exit", "refetch", "style -cmdl", "ls", "help", "clear", "cls", "pwd", "cd", "logo"];
bool working = true;

// Colores de texto
string bl = "\u001b[30m";
string red = "\u001b[31m";
string gre = "\u001b[32m";
string ye = "\u001b[33m";
string blu = "\u001b[34m";
string vio = "\u001b[35m";
string ci = "\u001b[36m";
string wh = "\u001b[37m";

// Colores de fondo
string redbg = "\u001b[41m";
string grebg = "\u001b[42m";
string yebg = "\u001b[43m";
string blubg = "\u001b[44m";
string viobg = "\u001b[45m";
string cibg = "\u001b[46m";
string whbg = "\u001b[47m";

// Reset de ANSI
string reset = "\u001b[0m";

string logo_ascii = $@"
{wh}...............................   
.{blu}___{wh}.......{blu}____{wh}......{vio}__{wh}....{vio}__{wh}..
.{blu}\##\{wh}..{blu}_{wh}..{blu}/##./{wh}.....{vio}/_/{wh}...{vio}/_/{wh}..
..{blu}\##\/#\/##./{wh}..{vio}/============/{wh}.
...{blu}\#######./{wh}......{vio}/_/{wh}..{vio}/_/{wh}....
....{blu}\#./\#./{wh}....{vio}/===========/{wh}..
.....{blu}\/{wh}..{blu}\/{wh}......{vio}/_/{wh}..{vio}/_/{wh}......
...............................{reset}
";

string cmdlstyle = "> ";

while (working)
{
    Console.Write($"{cmdlstyle}");
    string cmd = Console.ReadLine() ?? "";

    // Manejo especial para 'echo' con mensaje en la misma línea
    if (cmd.StartsWith("echo "))
    {
        string mensaje = cmd.Substring(5);
        Console.WriteLine(mensaje);
        continue; // Vuelve al inicio del bucle while
    }

    switch (cmd)
    {
        case "echo":
            Console.WriteLine(); // Imprime línea vacía
            break;

        case "exit":
            working = false;
            break;

        case "refetch":
            string[] infoSystem = [
            $"{blu}OS:{reset} {version}",
            $"{blu}SO real:{reset} {RuntimeInformation.OSDescription}",
            $"{blu}Arch:{reset} {RuntimeInformation.ProcessArchitecture}"
            ];

            string[] logoLines = logo_ascii.Trim().Split('\n');

            int totalFilas = Math.Max(logoLines.Length, infoSystem.Length);

            for (int i = 0; i < totalFilas; i++)
            {
            string lineaLogo = i < logoLines.Length ? logoLines[i].TrimEnd('\r') : new string(' ', 32);
            string lineaInfo = i < infoSystem.Length ? infoSystem[i] : "";

            Console.WriteLine($"{lineaLogo}   {lineaInfo}");
            }
            break;

        case "ls":
            string dirActual = Directory.GetCurrentDirectory();
            string[] items = Directory.GetFileSystemEntries(dirActual);
            foreach (string item in items)
            {
                string nombre = Path.GetFileName(item);
                if (Directory.Exists(item))
                {
                    Console.WriteLine($"{blu}/{nombre}/{reset}");
                }
                else
                {
                    Console.WriteLine($"{wh}{nombre}{reset}");
                }
            }
            break;

        case "style -cmdl":
            Console.WriteLine("Estilos de prompt");
            Console.WriteLine("Estilo 1. > ejemplo");
            Console.WriteLine("Estilo 2. λ ~");
            Console.WriteLine("Tu propio estilo (escribe myown)");
            Console.WriteLine($"{ye}Elige un estilo (1/2/myown):{reset} ");
            string cmdlstylechoosen = Console.ReadLine() ?? "";
            if (cmdlstylechoosen == "2")
                cmdlstyle = "λ ~ ";
            else if (cmdlstylechoosen == "1")
                cmdlstyle = "> ";
            else if (cmdlstylechoosen == "myown")
            {
                Console.WriteLine("Escribe tu propio estilo (se recomienda dejar un espacio al final): ");
                string selfstyle = Console.ReadLine() ?? "";
                cmdlstyle = selfstyle;
            }
            else
                Console.WriteLine("Estilo no reconocido");

            break;

        case "rm -rf / --no-presserve-root":
            Console.WriteLine($"{blubg}{wh}KERNEL PANIC{reset}");
            break;

        case "help":
            Console.WriteLine($"{blu}echo:{reset} Imprime lo que escribas a continuación o una línea vacía.");
            Console.WriteLine($"{blu}refetch:{reset} Muestra información de WintC# y de tu sistema real");
            Console.WriteLine($"{blu}ls:{reset} Lista los directorios y archivos en los que estás");
            Console.WriteLine($"{blu}pwd:{reset} Muestra la ruta del directorio actual");
            Console.WriteLine($"{blu}cd:{reset} Cambia de directorio");
            Console.WriteLine($"{blu}style -cmdl:{reset} Modifica el estilo de los prompts");
            Console.WriteLine($"{blu}clear/cls:{reset} Limpia todo el contenido de la pantalla");
            break;

        case "clear" or "cls":
            Console.Clear();
            break;

        case "cd":
            Console.Write($"{ye}¿A qué directorio deseas ir?: {reset}");
            string destino = Console.ReadLine() ?? "";

            if (!string.IsNullOrEmpty(destino))
            {
                if (Directory.Exists(destino))
                {
                    Directory.SetCurrentDirectory(destino);
                }
                else
                {
                    Console.WriteLine($"{red}El directorio '{destino}' no existe.{reset}");
                }
            }
            break;

        case "cd ..":
            Directory.SetCurrentDirectory("..");
            break;

        case "pwd":
            Console.WriteLine(Directory.GetCurrentDirectory());
            break;

        default:
            Console.WriteLine("Comando no reconocido.");
            break;
    }
}