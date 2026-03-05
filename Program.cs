using System;

namespace ListaDoblementeEnlazada
{
    // Clase Nodo que representa cada elemento de la lista
    class Nodo
    {
        public int Valor;
        public Nodo Siguiente;
        public Nodo Anterior;

        public Nodo(int valor)
        {
            Valor = valor;
            Siguiente = null;
            Anterior = null;
        }
    }

    // Clase que implementa la lista doblemente enlazada
    class ListaDoblementeEnlazada
    {
        private Nodo cabeza;
        private Nodo cola;
        private int tamaño;

        public ListaDoblementeEnlazada()
        {
            cabeza = null;
            cola = null;
            tamaño = 0;
        }

        // Insertar al inicio de la lista
        public void InsertarAlInicio(int valor)
        {
            Nodo nuevoNodo = new Nodo(valor);
            
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                cola = nuevoNodo;
            }
            else
            {
                nuevoNodo.Siguiente = cabeza;
                cabeza.Anterior = nuevoNodo;
                cabeza = nuevoNodo;
            }
            
            tamaño++;
            Console.WriteLine($"Elemento {valor} insertado al inicio.");
        }

        // Insertar al final de la lista
        public void InsertarAlFinal(int valor)
        {
            Nodo nuevoNodo = new Nodo(valor);
            
            if (cola == null)
            {
                cabeza = nuevoNodo;
                cola = nuevoNodo;
            }
            else
            {
                cola.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = cola;
                cola = nuevoNodo;
            }
            
            tamaño++;
            Console.WriteLine($"Elemento {valor} insertado al final.");
        }

        // Recorrer la lista hacia adelante
        public void RecorrerHaciaAdelante()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Console.WriteLine("Recorrido hacia adelante:");
            Nodo actual = cabeza;
            while (actual != null)
            {
                Console.Write(actual.Valor + " ");
                actual = actual.Siguiente;
            }
            Console.WriteLine();
        }

        // Recorrer la lista hacia atrás
        public void RecorrerHaciaAtras()
        {
            if (cola == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Console.WriteLine("Recorrido hacia atrás:");
            Nodo actual = cola;
            while (actual != null)
            {
                Console.Write(actual.Valor + " ");
                actual = actual.Anterior;
            }
            Console.WriteLine();
        }

        // Mostrar el tamaño de la lista
        public void MostrarTamaño()
        {
            Console.WriteLine($"Tamaño de la lista: {tamaño}");
        }

        // Verificar si la lista está vacía
        public void EstaVacia()
        {
            if (tamaño == 0)
                Console.WriteLine("La lista está vacía.");
            else
                Console.WriteLine("La lista no está vacía.");
        }

        // Buscar elemento por valor
        public void BuscarPorValor(int valor)
        {
            Nodo actual = cabeza;
            int posicion = 0;
            
            while (actual != null)
            {
                if (actual.Valor == valor)
                {
                    Console.WriteLine($"Elemento {valor} encontrado en la posición {posicion}.");
                    return;
                }
                actual = actual.Siguiente;
                posicion++;
            }
            
            Console.WriteLine($"Elemento {valor} no encontrado en la lista.");
        }

        // Buscar elemento por índice
        public void BuscarPorIndice(int indice)
        {
            if (indice < 0 || indice >= tamaño)
            {
                Console.WriteLine("Índice fuera de rango.");
                return;
            }

            Nodo actual = cabeza;
            int posicion = 0;
            
            while (actual != null && posicion < indice)
            {
                actual = actual.Siguiente;
                posicion++;
            }
            
            if (actual != null)
                Console.WriteLine($"Elemento en el índice {indice}: {actual.Valor}");
        }

        // Borrar un elemento por valor
        public void BorrarElemento(int valor)
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Nodo actual = cabeza;
            
            while (actual != null)
            {
                if (actual.Valor == valor)
                {
                    // Caso: el nodo a eliminar es la cabeza
                    if (actual == cabeza)
                    {
                        cabeza = actual.Siguiente;
                        if (cabeza != null)
                            cabeza.Anterior = null;
                        else
                            cola = null; // La lista queda vacía
                    }
                    // Caso: el nodo a eliminar es la cola
                    else if (actual == cola)
                    {
                        cola = actual.Anterior;
                        cola.Siguiente = null;
                    }
                    // Caso: el nodo está en medio
                    else
                    {
                        actual.Anterior.Siguiente = actual.Siguiente;
                        actual.Siguiente.Anterior = actual.Anterior;
                    }
                    
                    tamaño--;
                    Console.WriteLine($"Elemento {valor} eliminado de la lista.");
                    return;
                }
                actual = actual.Siguiente;
            }
            
            Console.WriteLine($"Elemento {valor} no encontrado en la lista.");
        }
    }

    // Programa principal con el menú interactivo
    class Program
    {
        static void Main(string[] args)
        {
            ListaDoblementeEnlazada lista = new ListaDoblementeEnlazada();
            int opcion;
            int valor;
            int indice;

            do
            {
                Console.WriteLine("\n=== Lista Doblemente Enlazada ===");
                Console.WriteLine("1. Insertar al inicio");
                Console.WriteLine("2. Insertar al final");
                Console.WriteLine("3. Recorrer hacia adelante");
                Console.WriteLine("4. Recorrer hacia atrás");
                Console.WriteLine("5. Mostrar tamaño de la lista");
                Console.WriteLine("6. Mostrar si la lista está vacía");
                Console.WriteLine("7. Buscar elemento por valor");
                Console.WriteLine("8. Buscar elemento por índice");
                Console.WriteLine("9. Borrar un elemento");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el valor a insertar al inicio: ");
                        if (int.TryParse(Console.ReadLine(), out valor))
                            lista.InsertarAlInicio(valor);
                        else
                            Console.WriteLine("Valor inválido.");
                        break;

                    case 2:
                        Console.Write("Ingrese el valor a insertar al final: ");
                        if (int.TryParse(Console.ReadLine(), out valor))
                            lista.InsertarAlFinal(valor);
                        else
                            Console.WriteLine("Valor inválido.");
                        break;

                    case 3:
                        lista.RecorrerHaciaAdelante();
                        break;

                    case 4:
                        lista.RecorrerHaciaAtras();
                        break;

                    case 5:
                        lista.MostrarTamaño();
                        break;

                    case 6:
                        lista.EstaVacia();
                        break;

                    case 7:
                        Console.Write("Ingrese el valor a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out valor))
                            lista.BuscarPorValor(valor);
                        else
                            Console.WriteLine("Valor inválido.");
                        break;

                    case 8:
                        Console.Write("Ingrese el índice a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out indice))
                            lista.BuscarPorIndice(indice);
                        else
                            Console.WriteLine("Índice inválido.");
                        break;

                    case 9:
                        Console.Write("Ingrese el valor del elemento a borrar: ");
                        if (int.TryParse(Console.ReadLine(), out valor))
                            lista.BorrarElemento(valor);
                        else
                            Console.WriteLine("Valor inválido.");
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        break;
                }

            } while (opcion != 0);
        }
    }
}
