using System;
using System.Collections.Generic;
using System.Linq;

namespace Tarea
{
    class Cliente
    {
        public int Cedula { get; set; }
        public int Estrato { get; set; }
        public int MetaAhorroEnergia { get; set; }
        public int ConsumoActualEnergia { get; set; }
        public int PromedioConsumoDeAgua { get; set; }
        public int ConsumoActualDeAgua { get; set; }
    }

    class Program
    {
        static List<Cliente> clientes = new List<Cliente>();

        static void Main(string[] args)
        {
            clientes.Add(new Cliente() { Cedula = 3145, Estrato = 3, MetaAhorroEnergia = 150, ConsumoActualEnergia = 180, PromedioConsumoDeAgua = 25, ConsumoActualDeAgua = 20 });
            clientes.Add(new Cliente() { Cedula = 8947, Estrato = 3, MetaAhorroEnergia = 190, ConsumoActualEnergia = 187, PromedioConsumoDeAgua = 25, ConsumoActualDeAgua = 30 });
            clientes.Add(new Cliente() { Cedula = 9812, Estrato = 4, MetaAhorroEnergia = 260, ConsumoActualEnergia = 320, PromedioConsumoDeAgua = 25, ConsumoActualDeAgua = 25 });

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("Menú:");
                Console.WriteLine("1. Calcular valor a pagar por servicios de energía y agua para un cliente");
                Console.WriteLine("2. Calcular promedio del consumo actual de energía");
                Console.WriteLine("3. Calcular valor total de descuentos por incentivo de energía");
                Console.WriteLine("4. Mostrar cantidad total de mt3 de agua consumidos por encima del promedio");
                Console.WriteLine("5. Mostrar porcentajes de consumo excesivo de agua por estrato");
                Console.WriteLine("6. Contabilizar clientes con consumo de agua mayor al promedio");
                Console.WriteLine("7. Salir");

                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CalcularValorAPagar();
                        break;
                    case "2":
                        CalcularPromedioConsumoEnergia();
                        break;
                    case "3":
                        CalcularValorTotalDescuentos();
                        break;
                    case "4":
                        MostrarCantidadTotalAguaEncimaPromedio();
                        break;
                    case "5":
                        MostrarPorcentajeConsumoExcesivoAguaPorEstrato();
                        break;
                    case "6":
                        ContabilizarClientesConConsumoAguaMayorPromedio();
                        break;
                    case "7":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void CalcularValorAPagar()
        {
            Console.WriteLine("Ingrese la cédula del cliente:");
            int cedula = int.Parse(Console.ReadLine());

            Cliente cliente = clientes.FirstOrDefault(c => c.Cedula == cedula);
            if (cliente != null)
            {
                double valorEnergia = cliente.ConsumoActualEnergia * 850;
                double valorIncetivoEnergia = (cliente.MetaAhorroEnergia - cliente.ConsumoActualEnergia) * 850;
                double valorTotalEnergia = valorEnergia - valorIncetivoEnergia;

               
                if (cliente.ConsumoActualDeAgua > cliente.PromedioConsumoDeAgua) 
                {
                    double valorAgua = cliente.ConsumoActualDeAgua * 4600;
                    double valorExceso = (cliente.ConsumoActualDeAgua - cliente.PromedioConsumoDeAgua) * 9200;
                    double valorTotalAgua = valorAgua + valorExceso;
                    double valorTotal = valorTotalEnergia + valorTotalAgua;

                    Console.WriteLine($"Valor a pagar por servicios de energía y agua: {valorTotal}");

                }
                else 
                {
                    double valorAgua = cliente.ConsumoActualDeAgua * 4600;
                    double valorTotalAgua = valorAgua;
                    double valorTotal = valorTotalEnergia + valorTotalAgua;
                    Console.WriteLine($"Valor a pagar por servicios de energía y agua: {valorTotal}");
                }
                

                
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        static void CalcularPromedioConsumoEnergia()
        {
            double sumaEnergia = clientes.Sum(c => c.ConsumoActualEnergia);
            double promedioEnergia = sumaEnergia / clientes.Count;
            Console.WriteLine($"Promedio del consumo actual de energía: {promedioEnergia}");
        }

        static void CalcularValorTotalDescuentos()
        {
            double totalDescuentos = clientes.Sum(cliente => (cliente.MetaAhorroEnergia - cliente.ConsumoActualEnergia) * 850*-1);
            Console.WriteLine($"Valor total de descuentos por incentivo de energía: {totalDescuentos}");
        }

        static void MostrarCantidadTotalAguaEncimaPromedio()
        {
            int totalAguaExcedente = clientes.Where(c => c.ConsumoActualDeAgua > c.PromedioConsumoDeAgua).Sum(c => c.ConsumoActualDeAgua - c.PromedioConsumoDeAgua);
            Console.WriteLine($"Cantidad total de mt3 de agua consumidos por encima del promedio: {totalAguaExcedente}");
        }

        static void MostrarPorcentajeConsumoExcesivoAguaPorEstrato()
        {
            var gruposPorEstrato = clientes.GroupBy(c => c.Estrato);
            foreach (var grupo in gruposPorEstrato)
            {
                int totalClientes = grupo.Count();
                int clientesConExceso = grupo.Count(c => c.ConsumoActualDeAgua > c.PromedioConsumoDeAgua);
                double porcentaje = (double)clientesConExceso / totalClientes * 100;
                Console.WriteLine($"Porcentaje de consumo excesivo de agua para estrato {grupo.Key}: {porcentaje}%");
            }
        }

        static void ContabilizarClientesConConsumoAguaMayorPromedio()
        {
            int clientesConExceso = clientes.Count(c => c.ConsumoActualDeAgua > c.PromedioConsumoDeAgua);
            Console.WriteLine($"Cantidad de clientes con consumo de agua mayor al promedio: {clientesConExceso}");
        }
    }
}



