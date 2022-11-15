using GerenciadorDeEstacionamento.Classes;
using GerenciadorDeEstacionamento.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

AppDbContext db = new AppDbContext();//contexto do banco

//variaveis - tipo nome = valor inicial
string escolhaMenu = "";
string fecharPrograma = "";
string placaEntradaPatio = "";
string placaSaidaPatio = "";

// função== sempre é verbo
async Task<List<Carro>> recuperarCarros()
{
   
    return await db.Carros.ToListAsync();//Return retorna o valor do tipo da função (ex string retorna um texto)

}
async Task<Patio> recuperarPatio(int idPatio) 
{
    var patioDb = await db.Patios.ToListAsync();
    return patioDb.FirstOrDefault(patio => patio.Id == idPatio);
}
async Task<List<Vaga>> recuperarVagasPorPatio(int idPatio) 
{
    var vagasDb = await db.Vagas.ToListAsync();
    return vagasDb.Where(vaga => vaga.Patio.Id == idPatio).ToList();
}
async Task<List<Vaga>> recuperarVagas() 
{
    return await db.Vagas.Include(vaga => vaga.Patio).ToListAsync();
}
async Task mostrarPatios()
{
    List<Patio> listaDePatio = await recuperarPatios();
    if (listaDePatio == null) 
    {
        Console.WriteLine("Não existe nenhum patio cadastrado");
        return;
    }
    foreach (Patio p in listaDePatio) 
    {
        Console.WriteLine($"Id: {p.Id}, Nome: { p.Nome}");
       
    }
        
    
}
async Task<List<Patio>> recuperarPatios()
{  
   return await db.Patios.ToListAsync();
}

void cadastrarPatio() 
{
    Patio patio = new Patio();
    Console.Clear();
    Console.WriteLine("CADASTRO DE PATIO");
    Console.WriteLine("Qual o nome do Patio?");
    string nomePatio = Console.ReadLine();
    if(nomePatio == "") 
    {
        Console.WriteLine("O nome informado é vazio");
        Console.ReadLine();
        Console.Clear();
        return;

    }
    patio.Nome = nomePatio;
    
    Console.WriteLine("Qual valor da Hora?");
    string valorHora = Console.ReadLine();
    decimal valorHoraConvertido = 0;
    if (Decimal.TryParse(valorHora, out valorHoraConvertido))
    {
        patio.ValorHora = valorHoraConvertido;
    }
    else
    {
        Console.WriteLine("Valor inserido invalido");
        Console.ReadLine();
        return;
    }
    Console.WriteLine("Qual o valor da diaria?");
    string valorDiaria = Console.ReadLine();
    decimal valorDiariaConvertido = 0;
    if (Decimal.TryParse(valorDiaria, out valorDiariaConvertido))
    {
        patio.ValorDiaria = valorDiariaConvertido;
    }
    else 
    {
        Console.WriteLine("Valor invalido inserido");
        Console.ReadLine();
        return;
    }
        
    Console.WriteLine("Qual a quantidade de vagas?");
    string quantidadeVagas = Console.ReadLine();
    int quantidadeVagasConvertido = 0;
    if (int.TryParse(quantidadeVagas, out quantidadeVagasConvertido))
    {
        patio.QuantidadeVagasDisponiveis = quantidadeVagasConvertido;
    }
    else 
    {
        Console.WriteLine("Quantidade de vagas incoerente");
        Console.ReadLine();
        return;
    }
    
    db.Patios.Add(patio);
    db.SaveChanges();
    Console.Clear();



}
async Task obterTotalFaturado()
{
    Console.Clear();
    Console.WriteLine("Qual Id do patio que deseja consultar?");
    string idPatio = Console.ReadLine();
    int idPatioInt = 0;
    if (int.TryParse(idPatio, out idPatioInt))
    {
        Patio patio = await recuperarPatio(idPatioInt);
        if (patio == null)
        {
            Console.WriteLine($"O Id do patio fornecido é invalido");
            Console.ReadLine();
            Console.Clear();
        }
        else
        {

            Console.WriteLine($@"O valor total foi R$ {patio.TotalFaturado}");
            Console.ReadLine();
            Console.Clear();

        }
    }
    else
    {
        Console.WriteLine("Caracter invalido");
        Console.ReadLine();
        Console.Clear();
        return;
    }

    


    

}
void mostrarCadastros ()
{
    Console.Clear();
    Console.WriteLine("Escolha qual item deseja cadastrar");
    Console.WriteLine("1 - Patio");
    Console.WriteLine("2 - Carro ");
    Console.WriteLine("0 - Voltar");
    string itemEscolhido = Console.ReadLine();


    switch (itemEscolhido) 
    { 
        case "1":
            cadastrarPatio();
            break;
        case "2":
            cadastrarCarro();
            break;
       
        default:
            break;

    }
}
async Task mostrarCarrosEstacionadosTempo() 
{   Console.Clear();
    Console.WriteLine("Qual Id do patio que esta saindo o carro?");
    string idPatio = Console.ReadLine();
    int idPatioInt = 0;
    if (int.TryParse(idPatio, out idPatioInt))
    {
        Patio patio = await recuperarPatio(idPatioInt);
        List<Vaga> vagas = await recuperarVagasPorPatio(idPatioInt);
        if (vagas == null)
        {
            Console.WriteLine("Não existem vagas nesse patio");
            Console.ReadLine();
            Console.Clear();
            return;
        }
        else
        {
            foreach (Vaga vaga in vagas)
            {
                Console.WriteLine($"O carro com a placa {vaga.Carro.Placa} esta estacionado a {(int)DateTime.Now.Subtract(vaga.HorarioEntrada).TotalMinutes} minutos e {(int)DateTime.Now.Subtract(vaga.HorarioEntrada).Seconds} segundos!");
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
    else
    {
        Console.WriteLine("Caracter invalido");
        Console.ReadLine();
        Console.Clear();
        return;
    }
   
}
async Task mostrarCarros(bool estaEstacionado)  
{
    if (estaEstacionado)
    {
        List<Vaga> vagasDb = await recuperarVagas();
        if (vagasDb == null)
        {
            Console.WriteLine("Não existem carros estacionados");
            return;
        }
        foreach (var vaga in vagasDb)
        {
            if (vaga.Carro.EstaEstacionado == estaEstacionado) //o IF vai executar o que esta entre chaves se o que estiver entre parenteses for verdadeiro
            {

                Console.WriteLine($"Placa: {vaga.Carro.Placa} - Proprietario: {vaga.Carro.Proprietario} - Patio: {vaga.Patio.Nome}");
            }

        }
    }
    else 
    {
        List<Carro> carrosDb = await recuperarCarros();
        if (carrosDb == null)
        {
            Console.WriteLine("Não existem carros cadastrados");
            Console.ReadLine();
            return;
        }
        foreach (var carro in carrosDb)
        {
            Console.WriteLine($"Placa: {carro.Placa} - Proprietario: {carro.Proprietario}");
        }
    }
    
    

    
}
async Task balizarCarro(bool entrada, string placa, int idPatio)
{
    List<Carro> carrosDb = await recuperarCarros();
    Carro carroEscolhido = carrosDb.FirstOrDefault(carro => carro.Placa == placa);
    Patio patio = await recuperarPatio(idPatio);
    if (entrada)
    {
        if (carroEscolhido != null && carroEscolhido.EstaEstacionado == false)
        {
            carroEscolhido.EstaEstacionado = true;
            db.Carros.Update(carroEscolhido);
            Vaga vagaOcupada = new Vaga();
            vagaOcupada.Carro = carroEscolhido;
            vagaOcupada.Patio = patio;
            db.Vagas.Add(vagaOcupada);
            patio.QuantidadeVagasDisponiveis -= 1;
            db.Patios.Update(patio);
            db.SaveChanges();
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
            carroEscolhido.EstaEstacionado = false;
            db.Carros.Update(carroEscolhido);
            List<Vaga> vagas = await recuperarVagasPorPatio(idPatio);
            Vaga vaga = vagas.FirstOrDefault(vaga => vaga.Carro == carroEscolhido); 
            decimal tarifa = patio.cobrar(vaga);
            db.Vagas.Remove(vaga);
            patio.QuantidadeVagasDisponiveis += 1;
            db.Patios.Update(patio);
            db.SaveChanges();

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
async Task retirarCarro()
{
    List<Carro> carrosEstacionados = await recuperarCarros();
    carrosEstacionados = carrosEstacionados.Where(carro => carro.EstaEstacionado == true).ToList();  

    Console.Clear();

    if (carrosEstacionados.Count != 0)
    {
        Console.WriteLine("Qual Id do patio que esta saindo o carro?");
        string idPatio = Console.ReadLine();
        int idPatioInt = 0;
        if (int.TryParse(idPatio, out idPatioInt)) 
        {
            Patio patio = await recuperarPatio(idPatioInt);
        }
        else
        {
            Console.WriteLine("Caracter invalido");
            Console.ReadLine();
            Console.Clear();
            return;
        }
        Console.WriteLine("Qual carro esta saindo do patio?");
        mostrarCarros(true);

        placaSaidaPatio = Console.ReadLine();
        balizarCarro(false, placaSaidaPatio, idPatioInt);
    }
    else { Console.WriteLine("Não existe carro para ser retirado"); }
    Console.ReadLine();
        Console.Clear();
}
void cadastrarCarro()
{
    Carro carro = new Carro();
    List<Carro> carrosDb = db.Carros.ToList();

    Console.Clear();

    Console.WriteLine("Tela de cadastro de veículo");

    Console.WriteLine("Placa do carro");

    string placaDoCarro  = Console.ReadLine();

    var carroCadastrado = carrosDb.FirstOrDefault(carro => carro.Placa == placaDoCarro);

    if (carroCadastrado != null) 
    {
        Console.WriteLine($"O carro com a placa {placaDoCarro} ja esta cadastrado");
        Console.ReadLine();
        return;
    }
    carro.Placa = placaDoCarro;



    Console.WriteLine("Proprietario");

    carro.Proprietario = Console.ReadLine();
    db.Carros.Add(carro);
    db.SaveChanges();
    Console.Clear();
}

async Task estacionarCarro()
{

    Console.Clear();
    List<Carro> carrosDisponiveis = await recuperarCarros();
        carrosDisponiveis.Where(carro => carro.EstaEstacionado == false).ToList();
    if (carrosDisponiveis.Count != 0)
    {
        Console.WriteLine("Qual Id do patio que esta entrando o carro?");
        string idPatio = Console.ReadLine();
        int idPatioInt = 0;
        if (int.TryParse(idPatio, out idPatioInt))
        {
            Patio patio = await recuperarPatio(idPatioInt);
        }
        else
        {
            Console.WriteLine("Caracter invalido");
            Console.ReadLine();
            Console.Clear();
            return;
        }
        Console.WriteLine("Escolha qual placa que entrou no patio");
        mostrarCarros(false);

        placaEntradaPatio = Console.ReadLine();

        balizarCarro(true, placaEntradaPatio,idPatioInt);
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
    Console.WriteLine("1- Cadastros");
    Console.WriteLine("2- Entrada de veículo no pátio");
    Console.WriteLine("3- Saida do veículo do pátio");
    Console.WriteLine("4- Quantidade de vagas disponievis");
    Console.WriteLine("5- Total faturado");
    Console.WriteLine("6- Mostrar veiculos estacionados");
    Console.WriteLine("7- Carros cadastrados");
    Console.WriteLine("8- Carros estacionados");
    Console.WriteLine("9- Lista de Patios");
    Console.WriteLine("0- Fechar programa");


    escolhaMenu = Console.ReadLine();
}

async Task mostrarQuantidadeDeVagasDisponiveis()
{
    Console.Clear();
    Console.WriteLine("Qual Id do patio que deseja consultar?");
    string idPatio = Console.ReadLine();
    int idPatioInt = 0;
    if (int.TryParse(idPatio, out idPatioInt))
    {
        Patio patio = await recuperarPatio(idPatioInt);
        Console.WriteLine($"Temos {patio.QuantidadeVagasDisponiveis} vagas disponíveis");
    }
    else
    {
        Console.WriteLine("Caracter invalido");
        Console.ReadLine();
        return;
    }

    Console.ReadLine();

    Console.Clear();
}
//While mantem o programa aberto enquanto o usuario nao escolhe a opçao 0
async void menu() 
{
    while (escolhaMenu != "0")
    {
        montarMenu();

        switch (escolhaMenu)
        {
            case "1":
                mostrarCadastros();

                break;

            case "2":
                estacionarCarro();

                break;

            case "3":
                retirarCarro();

                break;

            case "4":
                await mostrarQuantidadeDeVagasDisponiveis();
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

            case "9":
                Console.Clear();
                Console.WriteLine("Lista de Patios cadastrados");
                mostrarPatios();
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
}
menu();