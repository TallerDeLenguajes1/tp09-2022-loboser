using System;
using System.IO;   
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tp8
{
    class Program
    {
        public static void Main(string[] args)
        {   
            string path;

            do
            {
                Console.WriteLine("Escribir el path de una carpeta para listar su contenido");
                path = Console.ReadLine();

                if (!Directory.Exists(path))
                {
                    Console.Clear();
                    Console.WriteLine("Ese path no existe!...\n\nPresione ENTER para intentar con otro!");
                    Console.ReadLine();
                }
            } while (!Directory.Exists(path));

            string[] archivos = Directory.GetFiles(path);
            string[] separado;

            int id = 0;
            var ListaDeLineas = new List<Linea>();

            foreach (var archivo in archivos)
            {
                var Linea = new Linea();
                separado = archivo.Split('\\','.');

                Linea.NumeroDeRegistro = id;
                Linea.Nombre = separado[separado.Length-2];
                Linea.Extension = separado[separado.Length-1];
                ListaDeLineas.Add(Linea);
                
                Console.WriteLine((id+1) + ") " + Linea.Nombre + "." + Linea.Extension);
                id++;
            }

            string jsonString = JsonSerializer.Serialize(ListaDeLineas);
            
            var Archivo = new FileStream("index.json",FileMode.Create);
            var StrmWriter = new StreamWriter(Archivo);
            StrmWriter.WriteLine(jsonString);
            StrmWriter.Close();
        }
    }
    class Linea
    {
        int numeroDeRegistro;
        string nombre, extension;

        public int NumeroDeRegistro{get => numeroDeRegistro;set => numeroDeRegistro = value;}
        public string Nombre{get => nombre;set => nombre = value;}
        public string Extension{get => extension;set => extension = value;}
    }
}