using GerenciadorDeEstacionamento.Classes;

//variaveis - tipo nome = valor inicial
string escolhaMenu = "";
string fecharPrograma = "";
string placaEntradaPatio = "";

Patio patio = new Patio();
patio.ValorHora = 4.50M;
patio.QuantidadeVagasDisponiveis = 60;
patio.Vagas = new List<Vaga>();

List<Carro> carros = new List<Carro>();
Carro carroEscolhido = new Carro();


// função== sempre é verbo
int somarMaisUm(int numero) {
    return numero + 1;
}

void cadastrarCarro() {
    Carro carro = new Carro();

    Console.Clear();

    Console.WriteLine("Tela de cadastro de veículo");

    Console.WriteLine("Placa do carro");

    carro.Placa = Console.ReadLine();


    Console.WriteLine("Proprietario");

    carro.NomeProp = Console.ReadLine();
    carros.Add(carro);
    Console.Clear();
}

void estacionarCarro() {
    
    Console.Clear();

    if (carros.Count != 0)
    {
        Console.WriteLine("Escolha qual placa que entrou no patio");
        foreach (Carro car in carros)
        {
            Console.WriteLine(car.Placa);
        }

        placaEntradaPatio = Console.ReadLine();
        carroEscolhido = carros.FirstOrDefault(carro => carro.Placa == placaEntradaPatio);
        if (carroEscolhido != null)
        {
            Vaga vagaOcupada = new Vaga(carroEscolhido);
            patio.ocuparVaga(vagaOcupada);
            Console.WriteLine($"O carro com placa {vagaOcupada.Carro.Placa} foi estacionado");
        }
        else
        {
            Console.WriteLine("O carro inserido nao existe");
        }
    }
    else
    {
        Console.WriteLine("Não existe nenhuma placa cadastrada");

    }
    Console.ReadLine();

    Console.Clear();
}

void montarMenu() { 
    Console.WriteLine("Bem vindo ao estacionamento Ki Vaga");
    Console.WriteLine("1- Cadastro de placa de dono");
    Console.WriteLine("2- Entrada de veículo no pátio");
    Console.WriteLine("3- Saida do veículo do pátio");
    Console.WriteLine("4- Quantidade de vagas");
    Console.WriteLine("5- Total faturado no dia especificado");
    Console.WriteLine("6- Tabela de preços");
    Console.WriteLine("7- Carros cadastrados");
    Console.WriteLine("8- Carros estacionados");
    Console.WriteLine("0- Fechar programa");


    escolhaMenu = Console.ReadLine();
}

void mostrarQuantidadeDeVagasDisponiveis()
{
    Console.Clear();

    Console.WriteLine($"Temos {patio.QuantidadeVagasDisponiveis} vagas disponíveis");

    Console.ReadLine();

    Console.Clear();
}
//While mantem o programa aberto enquanto o usuario nao escolhe a opçao 0
while (escolhaMenu != "0")
{
    montarMenu(); 

    switch (escolhaMenu)
    {
        case "1":
            cadastrarCarro();

            break;

        case "2":
            estacionarCarro();

            break;

        case "3":
            Console.WriteLine("Escolheu opção 3");
            Console.Clear();
            break;

        case "4":
            mostrarQuantidadeDeVagasDisponiveis();
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

            foreach (Carro car in carros)
            {
                Console.WriteLine($"{car.Placa} - {car.NomeProp}");
            }
            Console.ReadLine();
            Console.Clear();
            break;
        case "8":
            Console.Clear();

            foreach (Vaga vaga in patio.Vagas)   
            {
                Console.WriteLine($"O carro com a placa {vaga.Carro.Placa} esta estacionado a {(int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes} minutos e {(int)DateTime.Now.Subtract(vaga.HorarioEntrada).Seconds} segundos!");
            }
            Console.ReadLine();
            Console.Clear();

            break;
        case "0":
            Console.Clear();
            Console.WriteLine("Você tem certeza que quer fechar o programa? S para sim e N para não");
            fecharPrograma = Console.ReadLine();
            if (fecharPrograma == "N" || fecharPrograma == "n")
            {
                escolhaMenu = "n";

            }
            Console.Clear();
            break;

        default:
            Console.WriteLine("Escolheu a opção errada,volta otario");

            break;

    }
}