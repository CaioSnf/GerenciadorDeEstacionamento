string escolhaMenu = "";
List<string> placas = new List<string>();
List<string> pessoas = new List<string>();
string fecharPrograma = "";
int vagasDisponiveis = 60;
while (escolhaMenu != "0") 
{
    Console.WriteLine("Bem vindo ao estacionamento Ki Vaga");
    Console.WriteLine("1- Cadastro de placa de dono");
    Console.WriteLine("2- Entrada de veículo no pátio");
    Console.WriteLine("3- Saida do veículo do pátio");
    Console.WriteLine("4- Quantidade de vagas");
    Console.WriteLine("5- Total faturado no dia especificado");
    Console.WriteLine("6- Tabela de preços");
    Console.WriteLine("7- Carros cadastrados");
    Console.WriteLine("8- Pessoas cadastradas");
    Console.WriteLine("0- Fechar programa");


    escolhaMenu = Console.ReadLine();

    switch (escolhaMenu)
    {
        case "1":
            Console.Clear();

            Console.WriteLine("Tela de cadastro de veículo");

            Console.WriteLine("Placa do carro");

            placas.Add(Console.ReadLine());

            Console.WriteLine("Proprietario");

            pessoas.Add(Console.ReadLine());
            Console.Clear();

            break;

        case "2":
            Console.Clear();

            if (placas.Count != 0)
            {
                Console.WriteLine("Escolha qual placa que entrou no patio");
                foreach (string placa in placas)
                {

                    Console.WriteLine(placa);

                }
                
            }
            else {
                Console.WriteLine("Não existe nenhuma placa cadastrada");
            
            }
            Console.ReadLine();

            Console.Clear();
            break;

        case "3":
            Console.WriteLine("Escolheu opção 3");
            Console.Clear();
            break;

        case "4":
            Console.Clear();
            
            Console.WriteLine($"Temos {vagasDisponiveis} vagas disponíveis");

            Console.ReadLine();

            Console.Clear();
            break;

        case "5":
            Console.WriteLine("Escolheu opção 5");
            Console.Clear();
            break;

        case "6":
            Console.WriteLine("Escolheu opção 6");
            Console.Clear();
            break;

        case "7":
            
            Console.Clear();
            Console.WriteLine(" lista de veiculos");

            foreach (string placa in placas)
            {
                Console.WriteLine(placa); 
            }
            Console.ReadLine();
            Console.Clear();
            break;

        case "8":
            Console.Clear();
            Console.WriteLine("Pessoas cadastradas");
            foreach (string pessoa in pessoas)
            {
                Console.WriteLine(pessoa);
            }

            Console.ReadLine();
            Console.Clear();

            break;

        case "0":
            Console.Clear();
            Console.WriteLine("Você tem certeza que quer fechar o programa? S para sim e N para não");
            fecharPrograma = Console.ReadLine();
            if (fecharPrograma == "N" || fecharPrograma == "n" ) {
                escolhaMenu = "n";

            }
            Console.Clear();
            break;

        default:
            Console.WriteLine("Escolheu a opção errada,volta otario");

            break;
            
    }
}
