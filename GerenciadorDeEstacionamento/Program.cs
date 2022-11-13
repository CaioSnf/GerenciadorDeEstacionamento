using GerenciadorDeEstacionamento.Classes;
using GerenciadorDeEstacionamento.Data;

//variaveis - tipo nome = valor inicial
string escolhaMenu = "";
string fecharPrograma = "";
string placaEntradaPatio = "";
string placaSaidaPatio = "";
Patio patio = new Patio();
patio.ValorHora = 4.50M;
patio.QuantidadeVagasDisponiveis = 60;
patio.Vagas = new List<Vaga>();

List<Carro> carros = new List<Carro>();
Carro carroEscolhido = new Carro();

AppDbContext db = new AppDbContext();


// função== sempre é verbo
List<Carro> recuperarCarros(bool carroEstacionado)
{
    return carros.Where(carro => carro.EstaEstacionado == carroEstacionado).ToList();//Return retorna o valor do tipo da função (ex string retorna um texto)

}
void obterTotalFaturado()
{
    Console.Clear();
    Console.WriteLine($@"O valor total foi {patio.TotalFaturado}");
    Console.ReadLine();
    Console.Clear();

}

void mostrarCarrosEstacionadosTempo() 
{
    Console.Clear();

    foreach (Vaga vaga in patio.Vagas)
    {
        Console.WriteLine($"O carro com a placa {vaga.Carro.Placa} esta estacionado a {(int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes} minutos e {(int)DateTime.Now.Subtract(vaga.HorarioEntrada).Seconds} segundos!");
    }
    Console.ReadLine();
    Console.Clear();

}



void mostrarCarros(bool estaEstacionado)
{
    foreach (var carro in carros)
    {
        if (carro.EstaEstacionado == estaEstacionado) //o IF vai executar o que esta entre chaves se o que estiver entre parenteses for verdadeiro
        {

            Console.WriteLine($"Placa: {carro.Placa} - Proprietario: {carro.Proprietario}");
        }

    }
}
void balizarCarro(bool entrada, string placa)
{
    carroEscolhido = carros.FirstOrDefault(carro => carro.Placa == placa);
    if (entrada)
    {
        if (carroEscolhido != null && carroEscolhido.EstaEstacionado == false)
        {
            carros.FirstOrDefault(carro => carro.Placa == placa).EstaEstacionado = true;
            Vaga vagaOcupada = new Vaga();
            vagaOcupada.Carro = carroEscolhido;
            patio.ocuparVaga(vagaOcupada);
            Console.WriteLine($"O carro com placa {vagaOcupada.Carro.Placa} foi estacionado");
        }
        else if (carroEscolhido != null && carroEscolhido.EstaEstacionado == true)
        {
            Console.WriteLine($"O carro {carroEscolhido.Placa} ja esta estacionado");
        }
        else
        {
            Console.WriteLine("O carro inserido nao existe");
        }

    }
    else
    {
        if (carroEscolhido != null && carroEscolhido.EstaEstacionado == true)
        {
            carros.FirstOrDefault(carro => carro.Placa == placa).EstaEstacionado = false;
            decimal tarifa = patio.cobrar(carroEscolhido);
            patio.desocuparVaga(carroEscolhido);
            Console.WriteLine($"O carro com a placa {carroEscolhido.Placa} saiu do patio e foi cobrado a tarifa de R$ {tarifa}");
        }
        else if (carroEscolhido != null && carroEscolhido.EstaEstacionado == false)
        {
            Console.WriteLine($"O carro com a placa {carroEscolhido.Placa} não esta estacionado");
        }

        else 
        {
            Console.WriteLine("O carro inserido nao existe");
        }
    }

}
void retirarCarro()
{
    List<Carro> carrosEstacionados = recuperarCarros(true);

    Console.Clear();

    if (carrosEstacionados.Count != 0)
    {
        Console.WriteLine("Qual carro esta saindo do patio?");
        mostrarCarros(true);

        placaSaidaPatio = Console.ReadLine();
        balizarCarro(false, placaSaidaPatio);
    }
    else { Console.WriteLine("Não existe carro para ser retirado"); }
    Console.ReadLine();
        Console.Clear();
}
void cadastrarCarro()
{
    Carro carro = new Carro();

    Console.Clear();

    Console.WriteLine("Tela de cadastro de veículo");

    Console.WriteLine("Placa do carro");

    carro.Placa = Console.ReadLine();


    Console.WriteLine("Proprietario");

    carro.Proprietario = Console.ReadLine();
    carros.Add(carro);
    Console.Clear();
}

void estacionarCarro()
{

    Console.Clear();
    List<Carro> carrosDisponiveis = recuperarCarros(false);
    if (carrosDisponiveis.Count != 0)
    {
        Console.WriteLine("Escolha qual placa que entrou no patio");
        mostrarCarros(false);

        placaEntradaPatio = Console.ReadLine();

        balizarCarro(true, placaEntradaPatio);
    }
    else
    {
        Console.WriteLine("Não existem carros disponiveis para estacionar");

    }
    Console.ReadLine();

    Console.Clear();
}

void montarMenu()
{
    Console.WriteLine("Bem vindo ao estacionamento Ki Vaga");
    Console.WriteLine("1- Cadastro de placa de dono");
    Console.WriteLine("2- Entrada de veículo no pátio");
    Console.WriteLine("3- Saida do veículo do pátio");
    Console.WriteLine("4- Quantidade de vagas disponievis");
    Console.WriteLine("5- Total faturado");
    Console.WriteLine("6- Mostrar veiculos estacionados");
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
            retirarCarro();

            break;

        case "4":
            mostrarQuantidadeDeVagasDisponiveis();
            break;

        case "5":

            obterTotalFaturado();
            break;

        case "6":
            Console.Clear();
            mostrarCarros(true);
            Console.ReadLine();
            Console.Clear();
            break;

        case "7":
            
            Console.Clear();
            Console.WriteLine(" lista de veiculos");
            mostrarCarros(false);
            Console.ReadLine();
            Console.Clear();
            break;
        case "8":

            mostrarCarrosEstacionadosTempo();
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