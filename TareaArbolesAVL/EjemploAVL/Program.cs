using System;

namespace EjemploAVL;

class Nodo
{
    public int Valor;
    public Nodo? Izquierda;
    public Nodo? Derecha;
    public int Altura;

    public Nodo(int valor)
    {
        Valor = valor;
        Altura = 1;
    }
}

class ArbolAVL
{
    private Nodo? raiz;

    private int Altura(Nodo? nodo) => nodo?.Altura ?? 0;

    private int Balance(Nodo? nodo) => Altura(nodo?.Izquierda) - Altura(nodo?.Derecha);

    private void ActualizarAltura(Nodo nodo)
    {
        nodo.Altura = 1 + Math.Max(Altura(nodo.Izquierda), Altura(nodo.Derecha));
    }

    private Nodo RotarDerecha(Nodo y)
    {
        Nodo x = y.Izquierda!;
        Nodo? T2 = x.Derecha;

        x.Derecha = y;
        y.Izquierda = T2;

        ActualizarAltura(y);
        ActualizarAltura(x);

        return x;
    }

    private Nodo RotarIzquierda(Nodo x)
    {
        Nodo y = x.Derecha!;
        Nodo? T2 = y.Izquierda;

        y.Izquierda = x;
        x.Derecha = T2;

        ActualizarAltura(x);
        ActualizarAltura(y);

        return y;
    }

    public void Insertar(int valor)
    {
        raiz = InsertarRec(raiz, valor);
    }

    private Nodo InsertarRec(Nodo? nodo, int valor)
    {
        if (nodo == null) return new Nodo(valor);

        if (valor < nodo.Valor)
            nodo.Izquierda = InsertarRec(nodo.Izquierda, valor);
        else if (valor > nodo.Valor)
            nodo.Derecha = InsertarRec(nodo.Derecha, valor);
        else
            return nodo;

        ActualizarAltura(nodo);

        int balance = Balance(nodo);

        if (balance > 1 && valor < nodo.Izquierda!.Valor)
            return RotarDerecha(nodo);

        if (balance < -1 && valor > nodo.Derecha!.Valor)
            return RotarIzquierda(nodo);

        if (balance > 1 && valor > nodo.Izquierda!.Valor)
        {
            nodo.Izquierda = RotarIzquierda(nodo.Izquierda);
            return RotarDerecha(nodo);
        }

        if (balance < -1 && valor < nodo.Derecha!.Valor)
        {
            nodo.Derecha = RotarDerecha(nodo.Derecha);
            return RotarIzquierda(nodo);
        }

        return nodo;
    }

    public bool Buscar(int valor)
    {
        return BuscarRec(raiz, valor);
    }

    private bool BuscarRec(Nodo? nodo, int valor)
    {
        if (nodo == null) return false;
        if (valor == nodo.Valor) return true;
        return valor < nodo.Valor ? BuscarRec(nodo.Izquierda, valor) : BuscarRec(nodo.Derecha, valor);
    }

    public void Eliminar(int valor)
    {
        raiz = EliminarRec(raiz, valor);
    }

    private Nodo? EliminarRec(Nodo? nodo, int valor)
    {
        if (nodo == null) return null;

        if (valor < nodo.Valor)
            nodo.Izquierda = EliminarRec(nodo.Izquierda, valor);
        else if (valor > nodo.Valor)
            nodo.Derecha = EliminarRec(nodo.Derecha, valor);
        else
        {
            if (nodo.Izquierda == null) return nodo.Derecha;
            if (nodo.Derecha == null) return nodo.Izquierda;

            Nodo min = EncontrarMin(nodo.Derecha);
            nodo.Valor = min.Valor;
            nodo.Derecha = EliminarRec(nodo.Derecha, min.Valor);
        }

        if (nodo == null) return null;

        ActualizarAltura(nodo);

        int balance = Balance(nodo);

        if (balance > 1 && Balance(nodo.Izquierda) >= 0)
            return RotarDerecha(nodo);

        if (balance > 1 && Balance(nodo.Izquierda) < 0)
        {
            nodo.Izquierda = RotarIzquierda(nodo.Izquierda!);
            return RotarDerecha(nodo);
        }

        if (balance < -1 && Balance(nodo.Derecha) <= 0)
            return RotarIzquierda(nodo);

        if (balance < -1 && Balance(nodo.Derecha) > 0)
        {
            nodo.Derecha = RotarDerecha(nodo.Derecha!);
            return RotarIzquierda(nodo);
        }

        return nodo;
    }

    private Nodo EncontrarMin(Nodo nodo)
    {
        while (nodo.Izquierda != null) nodo = nodo.Izquierda;
        return nodo;
    }

    public void Mostrar()
    {
        if (raiz == null)
        {
            Console.WriteLine("El arbol esta vacio.");
            return;
        }
        Console.WriteLine("\n--- Arbol AVL ---");
        MostrarRec(raiz, "", true);
        Console.WriteLine("-----------------");
    }

    private void MostrarRec(Nodo? nodo, string prefijo, bool esUltimo)
    {
        if (nodo == null) return;

        Console.Write(prefijo);
        Console.Write(esUltimo ? "└── " : "├── ");
        Console.WriteLine($"{nodo.Valor} (h={nodo.Altura})");

        string nuevoPrefijo = prefijo + (esUltimo ? "    " : "│   ");

        if (nodo.Izquierda != null || nodo.Derecha != null)
        {
            if (nodo.Izquierda != null)
                MostrarRec(nodo.Izquierda, nuevoPrefijo, nodo.Derecha == null);
            if (nodo.Derecha != null)
                MostrarRec(nodo.Derecha, nuevoPrefijo, true);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArbolAVL arbol = new ArbolAVL();

        Console.WriteLine("=== ARBOL AVL INTERACTIVO ===\n");

        while (true)
        {
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1. Insertar valor");
            Console.WriteLine("2. Buscar valor");
            Console.WriteLine("3. Eliminar valor");
            Console.WriteLine("4. Mostrar arbol");
            Console.WriteLine("5. Salir");
            Console.Write("Elige una opcion: ");

            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Valor a insertar: ");
                    if (int.TryParse(Console.ReadLine(), out int valInsert))
                    {
                        arbol.Insertar(valInsert);
                        Console.WriteLine($"Insertado: {valInsert}");
                        arbol.Mostrar();
                    }
                    else Console.WriteLine("Valor invalido.");
                    break;

                case "2":
                    Console.Write("Valor a buscar: ");
                    if (int.TryParse(Console.ReadLine(), out int valBuscar))
                    {
                        bool encontrado = arbol.Buscar(valBuscar);
                        Console.WriteLine(encontrado ? $"Valor {valBuscar} ENCONTRADO" : $"Valor {valBuscar} NO encontrado");
                    }
                    else Console.WriteLine("Valor invalido.");
                    break;

                case "3":
                    Console.Write("Valor a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int valEliminar))
                    {
                        arbol.Eliminar(valEliminar);
                        Console.WriteLine($"Eliminado: {valEliminar}");
                        arbol.Mostrar();
                    }
                    else Console.WriteLine("Valor invalido.");
                    break;

                case "4":
                    arbol.Mostrar();
                    break;

                case "5":
                    Console.WriteLine("Adios!");
                    return;

                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }
        }
    }
}
