using GerenciadorDeEstacionamento.Classes;

string escolhaMenu = "";

string fecharPrograma = "";
string placaEntradaPatio = "";

Patio patio = new Patio();
patio.ValorHora = 4.50M;
patio.QuantidadeVagasDisponiveis = 60;
patio.CarrosEstacionados = new List<Estacionado>();

List<Carro> carros = new List<Carro>();
Carro carroFiltrado = new Carro();

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
    Console.WriteLine("8- Carros estacionados");
    Console.WriteLine("0- Fechar programa");
    

    escolhaMenu = Console.ReadLine();

    switch (escolhaMenu)
    {
        case "1":
            Carro carro = new Carro();

            Console.Clear();

            Console.WriteLine("Tela de cadastro de veículo");

            Console.WriteLine("Placa do carro");

            carro.Placa = Console.ReadLine();


            Console.WriteLine("Proprietario");
            
            carro.NomeProp = Console.ReadLine();
            carros.Add(carro);
            Console.Clear();

            break;

        case "2":
            Console.Clear();

            if (carros.Count != 0)
            {
                Console.WriteLine("Escolha qual placa que entrou no patio");
                foreach (Carro car in carros)
                {
                    Console.WriteLine(car.Placa);
                }
               
                placaEntradaPatio = Console.ReadLine();
                carroFiltrado = carros.FirstOrDefault(carro => carro.Placa == placaEntradaPatio);
                if (carroFiltrado != null) {
                    Estacionado carroEstacionado = new Estacionado();
                    carroEstacionado.Carro = carroFiltrado;
                    carroEstacionado.HorarioEntrada = DateTime.Now;
                    patio.CarrosEstacionados.Add(carroEstacionado);
                    Console.WriteLine($"O carro com placa {placaEntradaPatio} foi estacionado");

                }
                else
                {
                    Console.WriteLine("O carro inserido nao existe");

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
            
            Console.WriteLine($"Temos {patio.QuantidadeVagasDisponiveis} vagas disponíveis");

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

            foreach ( Carro car in carros)
            {
                Console.WriteLine($"{car.Placa} - {car.NomeProp}"); 
            }
            Console.ReadLine();
            Console.Clear();
            break;
        case "8":
            Console.Clear();

            foreach(Estacionado estacionado in patio.CarrosEstacionados )
            {
                Console.WriteLine($"O carro com a placa {estacionado.Carro.Placa} esta estacionado a {(int)DateTime.Now.Subtract(estacionado.HorarioEntrada).TotalMinutes} minutos e {(int)DateTime.Now.Subtract(estacionado.HorarioEntrada).Seconds} segundos!");
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
