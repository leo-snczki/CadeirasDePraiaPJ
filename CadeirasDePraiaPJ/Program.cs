using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadeirasDePraiaPJ
{
    class Program
    {
        struct Cadeira
        {
            public int id;
            public int preço;
            public bool ocupado;
        }
        static string input;
        static int op;
        static Cadeira[] praia = new Cadeira[5];
        static DateTime[] horas = new DateTime[5];
        static int[,] totalmarcado = new int[5, 2] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }}; // Matriz que conta o total de horas que uma cadeira foi reservado.

        static void Main(string[] args)
        {
            praia[0] = new Cadeira { id = 1 , preço = 100 , ocupado = false };
            praia[1] = new Cadeira { id = 2 , preço = 100 , ocupado = false };
            praia[2] = new Cadeira { id = 3 , preço = 100 , ocupado = false };
            praia[3] = new Cadeira { id = 4 , preço = 100 , ocupado = false };
            praia[4] = new Cadeira { id = 5 , preço = 100 , ocupado = false };

            EscolherOpção();

        }
        static void MostrarMenu() // Método para demonstrar as opções que podem ser selecionadas.
        {
            Console.WriteLine("\n===== Menu =====");
            Console.WriteLine("1. Listar todas as cadeiras");
            Console.WriteLine("2. Ocupar cadeira");
            Console.WriteLine("3. Desocupar cadeira");
            Console.WriteLine("4. Editar cadeira");
            Console.WriteLine("5. Adicionar cadeira");
            Console.WriteLine("6. Remover cadeira");
            Console.WriteLine("7. Total de horas marcado por cadeira");
            Console.WriteLine("8. Sair");
            Console.WriteLine("================\n");

        }
        static void EscolherOpção()
        {          
            do
            {
                MostrarMenu();
                Console.Write("Escolha a opção desejada: ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        MostrarCadeiras();
                        Console.Write("Pressione qualquer tecla para voltar ao menu: ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        OcuparCadeira();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        DesocuparCadeira();
                        Console.Clear();
                        break;
                    case "5":
                        Console.Clear();
                        NovaCadeira();
                        Console.Clear();
                        break;
                    case "6":
                        Console.Clear();
                        DeletarCadeira();
                        Console.Clear();
                        break;
                    case "7":
                        Console.Clear();
                        MostrarHorasMarcadas();
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nOpção inválida!");
                        break;
                }
            } while (input != "8");
            return;

        }
        static void MostrarCadeiras()
        {

            foreach (Cadeira cadeira in praia)
            {
                Console.WriteLine($"cadeira {cadeira.id} | preço por hora: {cadeira.preço} | estado: {(cadeira.ocupado ? "Ocupado" : "Livre")}");
            }
            Console.WriteLine($"\nOs quartos vão de 1 a {praia.Length}\n");
        }

        static void OcuparCadeira()
        {
            string check;
            MostrarCadeiras();

            do
            {
                Console.Write("Indique o numero da cadeira a ocupar: ");
                if (int.TryParse(Console.ReadLine(), out op) && op >= 1 && op <= praia.Length)
                {
                    if(praia[op - 1].ocupado == true)
                    {
                        Console.WriteLine("\nEsta cadeira já está marcada\n");
                        Console.WriteLine("Gostaria de ocupar outra cadeira?\n (1 - sim) (outra tecla - volta ao menu)");
                        int.TryParse(Console.ReadLine(), out op);
                        if (op == 1)
                        {
                            Console.Clear();
                            OcuparCadeira();
                        }
                        else
                        {
                            return;
                        }
                    }

                    Console.Write("Quantos horas serão? ");
                    do
                    {
                        int.TryParse(Console.ReadLine(), out op); // Tenta converter o input para Int32, caso o valor de input não possa ser convertido para Int32, atribui -1 no out
                        if (op <= 0) Console.Write("Quantidade de horas inválida, tente novamente: ");
                        
                        else
                        {
                            Console.Write("Queres mesmo reservar esta cadeira de {0} até {1}? \nEscreva '1' para sim ou '2' para não: ", DateTime.Now.ToString("HH:mm"), DateTime.Now.AddHours(op).ToString("HH:mm"));
                            check = Console.ReadLine();
                            praia[op - 1].ocupado = true;
                            horas[op - 1] = DateTime.Now.Date.AddHours(op);
                            if (op - 1 < totalmarcado.GetLength(0)) totalmarcado[op - 1, 1] += op;
                        }
                    } while (op <= 0);

                    return;
                }
                else
                {
                    Console.WriteLine("Inválido!");               
                }
      
            }
            while (op < 1 || op > praia.Length);
        }

        static void DesocuparCadeira()
        {
            MostrarCadeiras();

            do
            {
                Console.Write("\nIndique o numero da cadeira a desocupar: ");
                if (int.TryParse(Console.ReadLine(), out op) && op >= 1 && op <= praia.Length)
                {
                    if (praia[op - 1].ocupado == false)
                    {
                        Console.WriteLine("Cadeira não está ocupada!");
                        Console.WriteLine("Gostaria de desocupar outra cadeira?\n (1 - sim) (outra tecla - volta ao menu)");
                        int.TryParse(Console.ReadLine(), out op);
                        if (op == 1)
                        {
                            Console.Clear();
                            DesocuparCadeira();
                        }
                        else
                        {
                            return;
                        }

                    }
                    praia[op - 1].ocupado = false;
                    return;
                }
                else
                {
                    Console.WriteLine("Inválido!");
                }
            }
            while (op < 1 || op > praia.Length);
        }
        static void NovaCadeira()
        {
            int novoid = praia.Length + 1;
            int novopreço;

            Console.Write("Insira o preço da cadeira: ");

            if (int.TryParse(Console.ReadLine(), out novopreço))
            {
                Array.Resize(ref praia, praia.Length + 1);
                Array.Resize(ref horas, horas.Length + 1);

                for (int i = praia.Length - 1; i > 0; i--)
                {
                    praia[i].id = i + 1;
                }

                praia[0].id = 1; 
                praia[praia.Length - 1] = new Cadeira { id = novoid, preço = novopreço, ocupado = false };
            }
            else
            {
                Console.WriteLine("O valor que introduziu não é um número inteiro válido.");
            }
        }

        static void DeletarCadeira()
        {
            MostrarCadeiras();
            Console.Write("Selecione a cadeira que será removida: ");
            int.TryParse(Console.ReadLine(), out op);
            if (praia[op - 1].ocupado == true)
            {
                Console.WriteLine("a cadeira não pode ser apagada porque está ocupada.");
                Console.WriteLine("Gostaria de deletar outra cadeira?\n (1 - sim) (outra tecla - volta ao menu)");
                int.TryParse(Console.ReadLine(), out op);
                if (op == 1)
                {
                    Console.Clear();
                    DeletarCadeira();
                }
                else
                {
                    return;
                }     
            }


            for (int i = op; i < praia.Length - 1; i++)
            {
                praia[i] = praia[i + 1];
            }

            Array.Resize(ref praia, praia.Length - 1);

            for (int i = 0; i < praia.Length; i++)
            {
                praia[i].id = i + 1;
            }
        }
        static void MostrarHorasMarcadas()
        {
            for (int i = 0; i < totalmarcado.GetLength(0); i++)
            {
                for (int j = 0; j < totalmarcado.GetLength(1); j++)
                {
                    Console.Write((j == 0) ? $"a cadeira {totalmarcado[i, j]} tem um total de " : $"{totalmarcado[i, j]} horas marcados{((i >= praia.Length) ? " (DELETADO)" : "")} \n");
                }
            }
            Console.ReadKey();
        }
    }
}
