using System.Text;
using System.Text.Json;
using Core.Entities;

namespace Frontend
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string apiUrl = "https://localhost:7295/api";

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Consultas ===\n");
                Console.WriteLine("1. Login Cliente");
                Console.WriteLine("2. Login Médico");
                Console.WriteLine("3. Cadastrar Cliente");
                Console.WriteLine("4. Cadastrar Médico");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        await LoginCliente();
                        break;
                    case "2":
                        await LoginMedico();
                        break;
                    case "3":
                        await CadastrarCliente();
                        break;
                    case "4":
                        await CadastrarMedico();
                        break;
                    case "5":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }


        private static async Task CadastrarCliente()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastro de Cliente ===");

            Console.Write("Nome: ");
            var nome = Console.ReadLine();

            Console.Write("CPF (apenas números): ");
            var cpf = Console.ReadLine();

            Console.Write("Endereço: ");
            var endereco = Console.ReadLine();

            Console.Write("Data de Nascimento (yyyy-MM-dd): ");
            var dataNascimento = Console.ReadLine();

            Console.Write("Telefone: ");
            var telefone = Console.ReadLine();

            Console.Write("Senha: ");
            var senha = Console.ReadLine();

            var cliente = new
            {
                Nome = nome,
                CPF = cpf,
                Endereco = endereco,
                DataNascimento = dataNascimento,
                Telefone = telefone,
                Senha = senha
            };

            var response = await client.PostAsync(
                $"{apiUrl}/Cliente",
                new StringContent(JsonSerializer.Serialize(cliente), Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Cliente cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao cadastrar cliente. Detalhes: " + await response.Content.ReadAsStringAsync());
            }

            Console.ReadKey();
        }
        private static async Task CadastrarMedico()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastro de Médico ===\n");

            Console.Write("Nome: ");
            var nome = Console.ReadLine();

            Console.Write("CPF: ");
            var cpf = Console.ReadLine();

            Console.Write("Especialidade: ");
            var especialidade = Console.ReadLine();

            Console.Write("Senha: ");
            var senha = Console.ReadLine();

            var medicoRequest = new
            {
                Nome = nome,
                CPF = cpf,
                Especialidade = especialidade,
                Senha = senha
            };

            var response = await client.PostAsync(
                $"{apiUrl}/Medico",
                new StringContent(JsonSerializer.Serialize(medicoRequest), Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Médico cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao cadastrar médico.");
            }

            Console.ReadKey();
        }


        private static async Task LoginCliente()
        {
            Console.Clear();
            Console.WriteLine("=== Login Cliente ===");

            Console.Write("Digite seu CPF: ");
            var cpf = Console.ReadLine();

            Console.Write("Digite sua senha: ");
            var senha = Console.ReadLine();

            var loginRequest = new { CPF = cpf, Senha = senha };

            var response = await client.PostAsync(
                $"{apiUrl}/Cliente/login",
                new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Login realizado com sucesso!");
                await MenuCliente(cpf);
            }
            else
            {
                Console.WriteLine("Erro ao realizar login. Verifique seu CPF e senha.");
                Console.ReadKey();
            }
        }

        private static async Task MenuCliente(string cpf)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Menu Cliente ===\n");
                Console.WriteLine("1. Ver minhas consultas");
                Console.WriteLine("2. Agendar uma consulta");
                Console.WriteLine("3. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        await VerConsultasCliente(cpf);
                        break;
                    case "2":
                        await AgendarConsulta(cpf);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        private static async Task VerConsultasCliente(string cpf)
        {
            Console.Clear();
            Console.WriteLine("=== Minhas Consultas ===\n");

            var response = await client.GetAsync($"{apiUrl}/Consulta/cliente/{cpf}");

            if (response.IsSuccessStatusCode)
            {
                var consultas = JsonSerializer.Deserialize<List<dynamic>>(await response.Content.ReadAsStringAsync());

                foreach (var consulta in consultas)
                {
                    Console.WriteLine($"Consulta ID: {consulta.id}, Data: {consulta.data}, Médico: {consulta.medicoNome}");
                }
            }
            else
            {
                Console.WriteLine("Erro ao buscar suas consultas.");
            }

            Console.ReadKey();
        }

        private static async Task AgendarConsulta(string cpf)
        {
            Console.Clear();
            Console.WriteLine("=== Agendar Consulta ===\n");

            Console.Write("Digite o ID do médico: ");
            var medicoId = Console.ReadLine();

            Console.Write("Digite a data da consulta (yyyy-MM-dd): ");
            var dataConsulta = Console.ReadLine();

            var consultaRequest = new { ClienteCPF = cpf, MedicoId = medicoId, DataConsulta = dataConsulta };

            var response = await client.PostAsync(
                $"{apiUrl}/Consulta/marcar",
                new StringContent(JsonSerializer.Serialize(consultaRequest), Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Consulta agendada com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao agendar consulta.");
            }

            Console.ReadKey();
        }

        private static async Task LoginMedico()
        {
            Console.Clear();
            Console.WriteLine("=== Login Médico ===");

            Console.Write("Digite seu CPF: ");
            var cpf = Console.ReadLine();

            Console.Write("Digite sua senha: ");
            var senha = Console.ReadLine();

            var loginRequest = new { CPF = cpf, Senha = senha };

            var response = await client.PostAsync(
                $"{apiUrl}/Medico/login",
                new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                // Corrigir desserialização para um tipo fortemente tipado
                var responseContent = await response.Content.ReadAsStringAsync();
                var medico = JsonSerializer.Deserialize<Medico>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignora diferença de maiúsculas/minúsculas nos nomes das propriedades
                });

                if (medico != null)
                {
                    Console.WriteLine($"Bem-vindo, {medico.Nome} - ID: {medico.Id}");
                    await MenuMedico(medico.Id);
                }
                else
                {
                    Console.WriteLine("Erro: Não foi possível obter os dados do médico.");
                }
            }
            else
            {
                Console.WriteLine("Erro ao realizar login. Verifique seu CPF e senha.");
                Console.ReadKey();
            }
        }

        private static async Task MenuMedico(int medicoId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Menu Médico ===\n");
                Console.WriteLine("1. Ver todos os clientes");
                Console.WriteLine("2. Adicionar consulta");
                Console.WriteLine("3. Remover consulta");
                Console.WriteLine("4. Alterar consulta");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        await VerTodosClientes();
                        break;
                    case "2":
                        await AdicionarConsulta(medicoId);
                        break;
                    case "3":
                        await RemoverConsulta();
                        break;
                    case "4":
                        await AlterarConsulta();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        private static async Task VerTodosClientes()
        {
            Console.Clear();
            Console.WriteLine("=== Todos os Clientes ===\n");

            var response = await client.GetAsync($"{apiUrl}/Cliente");

            if (response.IsSuccessStatusCode)
            {
                var clientes = JsonSerializer.Deserialize<List<dynamic>>(await response.Content.ReadAsStringAsync());

                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"ID: {cliente.id}, Nome: {cliente.nome}, CPF: {cliente.cpf}");
                }
            }
            else
            {
                Console.WriteLine("Erro ao buscar clientes.");
            }

            Console.ReadKey();
        }

        private static async Task AdicionarConsulta(int medicoId)
        {
            Console.Clear();
            Console.WriteLine("=== Adicionar Consulta ===\n");

            Console.Write("Digite o CPF do cliente: ");
            var clienteCpf = Console.ReadLine();

            Console.Write("Digite a data da consulta (yyyy-MM-dd): ");
            var dataConsulta = Console.ReadLine();

            var consultaRequest = new { ClienteCPF = clienteCpf, MedicoId = medicoId, DataConsulta = dataConsulta };

            var response = await client.PostAsync(
                $"{apiUrl}/Consulta/marcar",
                new StringContent(JsonSerializer.Serialize(consultaRequest), Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Consulta adicionada com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao adicionar consulta.");
            }

            Console.ReadKey();
        }


        private static async Task RemoverConsulta()
        {
            Console.Clear();
            Console.WriteLine("=== Remover Consulta ===\n");

            Console.Write("Digite o ID da consulta que deseja remover: ");
            var consultaId = Console.ReadLine();

            var response = await client.DeleteAsync($"{apiUrl}/Consulta/{consultaId}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Consulta removida com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao remover consulta.");
            }

            Console.ReadKey();
        }

        private static async Task AlterarConsulta()
        {
            Console.Clear();
            Console.WriteLine("=== Alterar Consulta ===\n");

            Console.Write("Digite o ID da consulta que deseja alterar: ");
            var consultaId = Console.ReadLine();

            Console.Write("Digite a nova data da consulta (yyyy-MM-dd): ");
            var novaData = Console.ReadLine();

            var consultaUpdateRequest = new { DataConsulta = novaData };

            var response = await client.PutAsync(
                $"{apiUrl}/Consulta/{consultaId}",
                new StringContent(JsonSerializer.Serialize(consultaUpdateRequest), Encoding.UTF8, "application/json")
            );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Consulta alterada com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao alterar consulta.");
            }

            Console.ReadKey();
        }

    }
}
