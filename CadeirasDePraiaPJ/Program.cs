using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadeirasDePraiaPJ
{
    class Program
    {
        struct Cadeira // Struct com informações em comuns que podem ser reutilizadas.
        {
            public int Id; // Identificação única da cadeira.
            public double Preço; // Preço da cadeira.
            public bool Ocupado; // Indica se a cadeira está ocupada ou não.
        }

        static string input; // Variável para armazenar algum  texto selecionado pelo utilizador,
        static int op; // Variável para armazenar algum número inteiro selecionado pelo utilizador.
        static Cadeira[] cadeiradepraia = new Cadeira[5]; // Array de cadeiras com capacidade para 5 cadeiras.
        static DateTime[] horas = new DateTime[5]; // Array para armazenar as horas reservadas para cada cadeira.
        static int[,] totalmarcado = new int[5, 2] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } }; // Matriz que armazena o total de horas que uma cadeira foi reservado.

        static void Main(string[] args)
        {
            // Inicialização de algumas cadeiras com valores padrões.

            cadeiradepraia[0] = new Cadeira { Id = 1, Preço = 100, Ocupado = false };
            cadeiradepraia[1] = new Cadeira { Id = 2, Preço = 100, Ocupado = false };
            cadeiradepraia[2] = new Cadeira { Id = 3, Preço = 100, Ocupado = false };
            cadeiradepraia[3] = new Cadeira { Id = 4, Preço = 100, Ocupado = false };
            cadeiradepraia[4] = new Cadeira { Id = 5, Preço = 100, Ocupado = false };

            EscolherOpção(); // Chama o método para começar a interação com o utilizador.

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
            // Método para permitir que o utilizador escolha uma opção do menu.
            do
            {
                MostrarMenu(); // Exibe o menu
                Console.Write("Escolha a opção desejada: ");
                input = Console.ReadLine(); // Lê a entrada de string do utilizador.
                switch (input) // com base na opção escolhida será executada tal ação.
                {
                    case "1":
                        Console.Clear();
                        MostrarCadeira(); // Chama o método para mostrar todas as cadeiras.
                        Console.Write("Pressione qualquer tecla para voltar ao menu: ");
                        Console.ReadKey(); // Aguarda algum input do utilizador para proseguir.
                        Console.Clear(); // Limpa o console.
                        break;
                    case "2":
                        Console.Clear(); // Limpa o console.
                        OcuparCadeira(); // Chama o método para ocupar uma cadeira.
                        Console.Clear(); // Limpa o console.
                        break;
                    case "3":
                        Console.Clear(); // Limpa o console.
                        DesocuparCadeira(); // Chama o método para desocupar uma cadeira.
                        Console.Clear(); // Limpa o console.
                        break;
                    case "4":
                        Console.Clear(); // Limpa o console.
                        EditarCadeira(); // Chama o método para editar uma cadeira.
                        Console.Clear(); // Limpa o console.
                        break;
                    case "5":
                        Console.Clear(); // Limpa o console.
                        NovaCadeira(); // Chama o método para adicionar uma nova cadeira.
                        Console.Clear(); // Limpa o console.
                        break;
                    case "6":
                        Console.Clear(); // Limpa o console.
                        DeletarCadeira(); // Chama o método para remover uma cadeira.
                        Console.Clear(); // Limpa o console.
                        break;
                    case "7":
                        Console.Clear(); // Limpa o console.
                        MostrarHorasMarcadas(); // Chama o método para mostrar o total de horas marcadas em cada cadeira.
                        Console.Clear(); // Limpa o console.
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nOpção inválida!"); // se não for escolhida nenhuma opção válida, ocorrerá esta mensagem.
                        break;
                }
            } while (input != "8"); // Continua o loop até que o utilizador escolha a opção de sair.
        }// Método que serve para escolher as opções dísponíveis no programa.
        static void MostrarCadeira() // Método para fins de demonstração de como usaro forearch e consequentemente mostrar todas as cadeiras disponíveis.
        {

            foreach (Cadeira cadeira in cadeiradepraia)
            {
                Console.WriteLine($"cadeira {cadeira.Id} | Preço por hora: {cadeira.Preço} | estado: {(cadeira.Ocupado ? "Ocupado" : "Livre")}"); // Exibe as informações de cada cadeira.
            }
            Console.WriteLine($"\nOs quartos vão de 1 a {cadeiradepraia.Length}\n"); // Exibe o intervalo de cadeiras disponíveis.
        }
        static void OcuparCadeira() // Método para ocupar uma cadeira.
        {
            int agendamento; // Declaração da variável para armazenar o número de horas que serão agendadas.
            DateTime fimReserva;
            DateTime limite = DateTime.Now.Date.AddHours(20); // Declaração da var que será usada para delimitar até as 20 horas o horário de marcação.
            TimeSpan horasDisponiveis = limite - DateTime.Now; // Declaração da var que calcula e armazena a diferença entre a hora e minutos atual e a hora delimitada.
            MostrarCadeira(); // Chama o método MostrarCadeira para exibir o estado atual das cadeiras.

            do
            {
                Console.Write("Indique o número da cadeira que será ocupada (0 para sair):  "); // Solicita ao utilizador que indique o número da cadeira a ser ocupada.
                if (!int.TryParse(Console.ReadLine(), out op)) // Tenta a entrada do usuário e converter para inteiro validando através da negação, armazenando o resultado em 'op'.
                {
                    // Valida se a entrada é um número válido.
                    Console.WriteLine("Por favor, insira um número válido.");
                    continue; // Volta ao começo do loop para selecionar outra cadeira.
                }

                else if (op == 0) return; // Se o utilizador escolher sair, pressionando 0, será retornado ao main.

                else if (op >= 1 && op <= cadeiradepraia.Length) // Verifica se o número da cadeira está dentro do intervalo válido.
                {
                    if (cadeiradepraia[op - 1].Ocupado) // Verifica se a cadeira já está ocupada.
                    {
                        Console.WriteLine("\nEsta cadeira já está marcada.\n");
                        Console.WriteLine("Gostaria de ocupar outra cadeira?\n (1 - sim) (outra tecla - volta ao menu)"); // Pergunta ao utilizador se gostaria de ocupar outra cadeira.
                        if (int.TryParse(Console.ReadLine(), out op) && op == 1)
                        {
                            Console.Clear(); // Limpa a tela do console.
                            continue; // Volta ao começo do loop para selecionar outra cadeira.
                        }
                        else return; // Retorna ao main.
                    }

                    Console.Write("Quantas horas serão? "); // Solicita ao utilizador a quantidade de horas para a reserva.
                    if (!int.TryParse(Console.ReadLine(), out agendamento)) // Valida a quantidade de horas.
                    {
                        // Valida a quantidade de horas.
                        Console.WriteLine("Quantidade de horas inválida, tente novamente.");
                        continue; // Retorna ao início do loop para inserir uma quantidade válida de horas.
                    }

                    fimReserva = DateTime.Now.AddHours(agendamento); //Tem como objetivo estabelecer quando a reserva irá terminar, obtendo a data e hora e adicionando a var agendamento.


                    if (fimReserva > limite)
                    {
                        Console.WriteLine("A reserva não pode ultrapassar o limite das 20:00. Por favor, escolha um número menor de horas. o  tempo restante é de {0}h e {1}m", horasDisponiveis.Hours, horasDisponiveis.Minutes);
                        continue; // Retorna ao início do loop para solicitar uma nova quantidade de horas.
                    }

                    // Confirmação da reserva, exibição do tempo marcado e cálculo do Preço total.

                    Console.Write("Confirmar a reserva desta cadeira das {0} até {1} pelo Preço de {2}$.\nDigite '1' para sim ou '2' para não: ", DateTime.Now.ToString("HH:mm"), DateTime.Now.AddHours(agendamento).ToString("HH:mm"), agendamento * cadeiradepraia[op - 1].Preço);
                    input = Console.ReadLine(); // Lê a confirmação do utilizador.
                    if (input == "1")
                    {
                        // Marca a cadeira como ocupada e atualiza o total de horas reservadas.
                        cadeiradepraia[op - 1].Ocupado = true;
                        horas[op - 1] = DateTime.Now.Date.AddHours(op);
                        if (op - 1 < totalmarcado.GetLength(0)) totalmarcado[op - 1, 1] += agendamento;
                    }
                    else if (input == "2") return; // Se for 2, o utilizador volta ao main.

                    else Console.WriteLine("Valor inválido!"); // Mensagem de alerta de valor inválido.                    
                }
                else Console.WriteLine("Cadeira inválida!"); // Mensagem de alerta de cadeira inválida.

            } while (true); // Loop infinito que só é finalizado quando o utilizador sair pelo return.
        }
        static void DesocuparCadeira() // Método para desocupar uma cadeira.
        {
            MostrarCadeira(); // Chama o método MostrarCadeira para exibir o estado atual das cadeiras.

            do
            {
                Console.Write("Indique o número da cadeira que será desocupada (0 para sair): "); // Solicita ao utilizador o número da cadeira a ser desocupada.
                if (!int.TryParse(Console.ReadLine(), out op)) // Valida se a entrada é um número válido, atráves da negação.
                {
                    Console.WriteLine("Por favor, insira um número válido.");
                    continue; // Volta ao começo do loop para selecionar outra cadeira.
                }

                if (op == 0) return; // Se o utilizador escolher sair, retorna ao main.

                if (op >= 1 && op <= cadeiradepraia.Length)
                {
                    if (!cadeiradepraia[op - 1].Ocupado) // Verifica se a cadeira não está ocupada, através da negação.
                    {
                        Console.WriteLine("Cadeira não está ocupada!");
                        Console.WriteLine("Gostaria de desocupar outra cadeira?\n (1 - sim) (outra tecla - volta ao menu)");
                        if (int.TryParse(Console.ReadLine(), out op) && op == 1)
                        {
                            Console.Clear();
                            continue; // Volta ao começo do loop para selecionar outra cadeira.
                        }
                        else return; // Retorna ao main.

                    }

                    // Desocupa a cadeira e exibe uma mensagem de sucesso.
                    cadeiradepraia[op - 1].Ocupado = false;
                    Console.WriteLine("Cadeira desocupada com sucesso!");
                    return; // Retorna ao main.
                }
                else Console.WriteLine("Cadeira inválida!"); // Mensagem de cadeira inválida.

            } while (true); // Loop até que o utilizador saia.
        }
        static void EditarCadeira() // Método para editar uma cadeira.
        {
            double novoPreco;
            MostrarCadeira(); //Chama o método de mostrar cadeiras.
            do
            {

                Console.Write("Selecione a cadeira que deseja editar (0 para sair): "); // Solicita ao utilizador que selecione a cadeira a ser editada.
                if (!int.TryParse(Console.ReadLine(), out op)) // Válida o número inserido.
                {
                    Console.WriteLine("Por favor, insira um número válido."); // caso nao for válido, exibe esta mensagem solicitando um número válido.
                    continue; // Volta ao começo do loop para selecionar outra cadeira.
                }

                if (op == 0) break; // Sai do loop se o usuário inserir 0 para sair

                if (op >= 1 && op <= cadeiradepraia.Length)
                {
                    Console.Write("Insira o novo Preço da cadeira: ");
                    if (double.TryParse(Console.ReadLine(), out novoPreco) && novoPreco > 0)
                    {
                        cadeiradepraia[op - 1].Preço = novoPreco;
                        Console.WriteLine("Preço atualizado com sucesso."); // Mensagem de alerta sobre o Preço ter sido atualizado com sucesso.
                    }
                    else Console.WriteLine("Preço inválido!"); // Mensagem de alerta sobre Preço inválido.

                }
                else Console.WriteLine("Cadeira inválida!"); // Mensagem de alerta sobre cadeira inválida.

            } while (true); // Loop infinito até que o utilizador saia.
        }
        static void NovaCadeira() // Método para adicionar uma cadeira.
        {
            int novoid = cadeiradepraia.Length + 1;
            double novoPreço;

            do
            {
                // Solicita ao utilizador o Preço da nova cadeira.
                Console.Write("Insira o Preço da cadeira (0 para sair): ");

                if (!double.TryParse(Console.ReadLine(), out novoPreço))
                {
                    // Valida se a entrada é um número válido, atráves da negação.
                    Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
                    continue; // Retorna ao início do loop para solicitar nova entrada.
                }

                else if (novoPreço == 0) return; // Se o utilizador escolher sair, é retornado ao main.

                else
                {
                    novoid = cadeiradepraia[cadeiradepraia.Length - 1].Id + 1; // Define o ID da nova cadeira como o ID da última cadeira + 1.

                    // Aumenta o tamanho do array de cadeiras para adicionar a nova cadeira.
                    Array.Resize(ref cadeiradepraia, cadeiradepraia.Length + 1);
                    Array.Resize(ref horas, horas.Length + 1);

                    // Cria uma nova matriz para armazenar o total de horas marcadas e copia os valores da matriz original.
                    int[,] novototalmarcado = new int[totalmarcado.GetLength(0) + 1, 2]; // Cria uma nova matriz para armazenar o total de horas marcadas com uma linha extra para a nova cadeira que será adicionada.
                    Array.Copy(totalmarcado, novototalmarcado, totalmarcado.Length); // copia o conteúdo de uma matriz para a outra.

                    novototalmarcado[totalmarcado.GetLength(0), 0] = totalmarcado.GetLength(0) + 1; // Atribui o número da nova cadeira(a anterior +1).
                    totalmarcado = novototalmarcado; // Atribui a nova matriz à matriz original.

                    // Atualiza os IDs das cadeiras.
                    for (int i = cadeiradepraia.Length - 1; i > 0; i--)
                    {
                        cadeiradepraia[i].Id = i + 1;
                    }

                    cadeiradepraia[0].Id = 1;
                    // Adiciona a nova cadeira ao final do array.
                    cadeiradepraia[cadeiradepraia.Length - 1] = new Cadeira { Id = novoid, Preço = novoPreço, Ocupado = false };
                    Console.WriteLine("Adicionada com sucesso!"); // Mensagem de confirmação.
                }
            } while (true); // Continua indefinidamente até que o utilizador escolha sair.
        }
        static void DeletarCadeira() // Método para deletar uma cadeira.
        {
            MostrarCadeira(); // Chama o método MostrarCadeira para exibir o estado atual das cadeiras.

            do
            {
                // Solicita ao utilizador que selecione a cadeira a ser removida.
                Console.Write("Selecione a cadeira que será removida (0 para sair): ");
                if (!int.TryParse(Console.ReadLine(), out op)) // testa se o input é é um número válido.
                {
                    Console.WriteLine("Por favor, insira um número válido.");
                    continue; // Volta ao começo do loop para selecionar outra cadeira.
                }

                if (op == 0) return; // Se o utilizador escolher sair, retorna ao main.

                if (op >= 1 && op <= cadeiradepraia.Length)
                {
                    if (cadeiradepraia[op - 1].Ocupado) // Verifica se a cadeira está ocupada e impede a remoção.
                    {
                        Console.WriteLine("A cadeira não pode ser apagada porque está ocupada.");
                        Console.WriteLine("Gostaria de deletar outra cadeira?\n (1 - sim) (outra tecla - volta ao menu)");
                        if (int.TryParse(Console.ReadLine(), out op) && op == 1)
                        {
                            Console.Clear();
                            continue; // Volta ao começo do loop para selecionar outra cadeira.
                        }
                        else return; // Retorna ao main.

                    }

                    for (int i = op - 1; i < cadeiradepraia.Length - 1; i++) // Ajusta os indices do vetor.
                    {
                        cadeiradepraia[i] = cadeiradepraia[i + 1];
                    }

                    Array.Resize(ref cadeiradepraia, cadeiradepraia.Length - 1); // Diminui o tamanho do vetor.

                    for (int i = 0; i < cadeiradepraia.Length; i++) // Ajusta os Ids.
                    {
                        cadeiradepraia[i].Id = i + 1;
                    }
                    return; // Retorna ao main.
                }
                else Console.WriteLine("Cadeira inválida!"); // Mensagem de cadeira inválida.

            } while (true); // Loop infinito até que o utilizador saia.
        }
        static void MostrarHorasMarcadas() // Método para demonstrar as horas totais marcadas em cada cadeira.
        {
            for (int i = 0; i < totalmarcado.GetLength(0); i++) // loop para a primeira dimensão da matriz.
            {
                for (int j = 0; j < totalmarcado.GetLength(1); j++) // loop para a segunda dimensão da matriz.
                {
                    Console.Write((j == 0) ? $"a cadeira {totalmarcado[i, j]} tem um total de " : $"{totalmarcado[i, j]} horas marcadas \n"); // Demonstração dos resultados.
                }
            }
            Console.Write("\nPressione qualquer tecla para voltar ao menu: "); // Solicita ao utilizador para pressionar qualquer tecla para voltar ao main.
            Console.ReadKey();
        }
    }
}
