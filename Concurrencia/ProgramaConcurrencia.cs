using System;
using System.Threading;
using System.Threading.Tasks;

namespace Concurrencia
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJEMPLOS DE CONCURRENCIA ===\n");

            // EJEMPLO 1: Usando Threads
            Console.WriteLine("1. THREADS (Hilos tradicionales):");
            Console.WriteLine("----------------------------------");
            
            Thread hilo1 = new Thread(Contar);
            Thread hilo2 = new Thread(Contar);
            
            hilo1.Start("Hilo 1");
            hilo2.Start("Hilo 2");
            
            hilo1.Join();  // Espera a que termine hilo1
            hilo2.Join();  // Espera a que termine hilo2
            
            Console.WriteLine("\nLos threads terminaron.\n");

            // EJEMPLO 2: Usando Tasks
            Console.WriteLine("2. TASKS (Tareas modernas):");
            Console.WriteLine("----------------------------");
            
            Task tarea1 = Task.Run(() => Contar("Tarea 1"));
            Task tarea2 = Task.Run(() => Contar("Tarea 2"));
            
            Task.WaitAll(tarea1, tarea2);  // Espera a que terminen ambas
            
            Console.WriteLine("\nLas tasks terminaron.\n");

            // EJEMPLO 3: Task que retorna un valor
            Console.WriteLine("3. TASK CON RESULTADO:");
            Console.WriteLine("----------------------");
            
            Task<int> tareaSuma = Task.Run(() => SumarNumeros(1, 10));
            
            Console.WriteLine("Calculando suma...");
            int resultado = tareaSuma.Result;  // Obtiene el resultado
            Console.WriteLine($"La suma es: {resultado}\n");

            Console.WriteLine("=== FIN DEL PROGRAMA ===");
            Console.ReadKey();
        }

        // Función simple para contar
        static void Contar(object nombre)
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"{nombre} cuenta: {i}");
                Thread.Sleep(500);  // Pausa de medio segundo
            }
        }

        // Función que suma números
        static int SumarNumeros(int desde, int hasta)
        {
            int total = 0;
            for (int i = desde; i <= hasta; i++)
            {
                total += i;
            }
            return total;
        }
    }
}
