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
            Console.Write("Ingresar la cantidad de productos a crear: ");
            int cantDeProductos = Convert.ToInt32(Console.ReadLine());

            var Archivo = new FileStream("productos.json", FileMode.Create);

            var jsonString = JsonSerializer.Serialize(CrearProductos(cantDeProductos));
            using(var StreamWriter = new StreamWriter(Archivo)){
                StreamWriter.Write(jsonString);
            }
            
            using(var StreamReader = new StreamReader("productos.json")){
                jsonString = StreamReader.ReadToEnd();
            }
            
            var listaDeProductos = JsonSerializer.Deserialize<List<Producto>>(jsonString);

            foreach (var producto in listaDeProductos)
            {
                Console.WriteLine("\nNombre: " + producto.Nombre);
                Console.WriteLine("Fecha De Vencimiento: {0}/{1}/{2}", producto.FechaVencimiento.Day, producto.FechaVencimiento.Month, producto.FechaVencimiento.Year);
                Console.WriteLine("Tamanio: " + producto.Tamanio);
                Console.WriteLine("Precio: " + producto.Precio);
            }

            Archivo.Close();
        }

        public static List<Producto> CrearProductos(int cantDeProductos){
            var ListaDeProductos = new List<Producto>();
            var rnd = new Random();
            int anio,mes,dia; 
            string[] tamanios = {"Chico","Mediano","Grande"};

            for (int i = 0; i < cantDeProductos; i++)
            {
                var NuevoProducto = new Producto();

                NuevoProducto.Nombre = "Producto " + (i+1);

                anio = rnd.Next(2023,2035);
                mes = rnd.Next(1,13);
                dia = rnd.Next(1,29);   // truchada para evitar que se incluya un dia que no tiene tal mes
                NuevoProducto.FechaVencimiento = new DateTime(anio,mes,dia);

                NuevoProducto.Tamanio = tamanios[rnd.Next(3)];
                NuevoProducto.Precio = rnd.Next(1000,5001);

                ListaDeProductos.Add(NuevoProducto);
            }
            return ListaDeProductos;
        }

    }
    class Producto{
        string nombre, tamanio;
        DateTime fechaVencimiento;
        float precio; //entre 1000 y 5000;

        public string Nombre{get => nombre;set => nombre = value;}
        public string Tamanio{get => tamanio;set => tamanio = value;}
        public DateTime FechaVencimiento{get => fechaVencimiento;set => fechaVencimiento = value;}
        public float Precio{get => precio;set => precio = value;}
}

}