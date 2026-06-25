using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CRUDBasico.Models
{
    public class Veiculos
    {
        //private readonly static string _conn = @"Data Source=DESKTOP-M9RL4H2\SQLEXPRESS;Initial Catalog=AgenciaAuto;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public int Id { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage ="A marca do Veiculo é Obrigatória.")]
        public string Nome { get; set; }

        [Display(Name = "Modelo:")]
        [Required(ErrorMessage = "O Modelo do Veiculo é Obrigatório.")]
        public string Modelo { get; set; }

        [Display(Name = "Ano:")]
        public short Ano { get; set; }

        [Display(Name = "Fabricação:")]
        [Range(1800, 2050, ErrorMessage = "O ano deve estar entre {1} e {2}.")]
        public short Fabricacao { get; set; }


        [Display(Name = "Cor:")]
        [Required(ErrorMessage = "A cor do Veiculo é Obrigatória.")]
        public string Cor { get; set; }

        [Display(Name = "Combustivel:")]
        [Range(1, 5, ErrorMessage = "O Combustível deve estar entre {1} e {2}.")]
        public EnumCombustivel Combustivel { get; set; }

        [Display(Name = "Automático:")]
        public EnumSimNao Automatico { get; set; }

        [Display(Name = "Valor:")]
        [Range(0.01, 999999.99, ErrorMessage = "O Valor deve estar entre {1} e {2}.")]
        public decimal Valor { get; set; }

        public Proprietarios Proprietario { get; set; }

        [Display(Name = "Ativo:")]
        public bool Ativo { get; set; }
        public Veiculos() { }
        public Veiculos(int id, string nome, string modelo, short ano, short fabricacao, string cor, EnumCombustivel combustivel, EnumSimNao automatico, decimal valor, int idProprietario, string nomeProprietario, bool ativo)
        {
            Id = id;
            Nome = nome;
            Modelo = modelo;
            Ano = ano;
            Fabricacao = fabricacao;
            Cor = cor;
            Combustivel = combustivel;
            Automatico = automatico;
            Valor = valor;
            Ativo = ativo;
            
            var proprietario = new Proprietarios();
            proprietario.Id = idProprietario;
            proprietario.Nome = nomeProprietario;
            this.Proprietario = proprietario;
        }

        public static List<Veiculos> Get_Carros()
        {
            //Procedimento diferenciado que devolve os itens do banco em uma lista para a chamada desse método
            var listaCarros = new List<Veiculos>();
            //var sql = "Select * from tb_Veiculos";
            var sql = "Select v.*, p.nome NomeProprietario from tb_Veiculos v " +
                "INNER JOIN Proprietarios p on v.Proprietario = p.Id";
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    listaCarros.Add(new Veiculos(
                                    Convert.ToInt32(dr["Id"]),
                                    dr["Nome"].ToString(),
                                    dr["Modelo"].ToString(),
                                    Convert.ToInt16(dr["Ano"]),
                                    Convert.ToInt16(dr["Fabricacao"]),
                                    dr["Cor"].ToString(),
                                    (EnumCombustivel)Convert.ToByte(dr["Combustivel"]),
                                    (EnumSimNao)Convert.ToByte(dr["Automatico"]),//converter primeiro para um tipo primitivo
                                    Convert.ToDecimal(dr["Valor"]),
                                    Convert.ToInt32(dr["Proprietario"]),
                                    dr["NomeProprietario"].ToString(),
                                    Convert.ToBoolean(dr["Ativo"])
                                    ));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha ... " + ex.Message);
            }
            return listaCarros;
        }

        public void Salvar(Veiculos veiculo)
        {
            Id = veiculo.Id;
            var sql = "";
            if (Id == 0)
                sql = "Insert Into tb_veiculos(Nome, Modelo, Ano, Fabricacao, Cor, " +
                    "Combustivel, Automatico, Valor, Ativo) " +
                    "Values(@nome, @modelo, @ano, @fabricacao, @cor, " +
                    "@combustivel, @automatico, @valor, @ativo)";
            else
                sql = "Update tb_veiculos set nome = @nome, modelo = @modelo, " +
                    "ano = @ano, fabricacao = @fabricacao, cor = @cor, " +
                    "combustivel = @combustivel, automatico = @automatico, " +
                    "valor = @valor, ativo = @ativo Where id = " + Id;
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@modelo", Modelo);
                        cmd.Parameters.AddWithValue("@ano", Fabricacao);
                        cmd.Parameters.AddWithValue("@fabricacao", Fabricacao);
                        cmd.Parameters.AddWithValue("@cor", Cor);
                        cmd.Parameters.AddWithValue("@combustivel", Combustivel);
                        cmd.Parameters.AddWithValue("@automatico", Automatico);
                        cmd.Parameters.AddWithValue("@valor", Valor);
                        cmd.Parameters.AddWithValue("@ativo", Ativo);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("falha ... " + ex.Message);
            }
        }




        public void GetVeiculo(int id)
        {
            var sql = "Select nome, modelo, ano, fabricacao, cor, combustivel, automatico, " +
                "valor, ativo from tb_Veiculos where id = " + id;
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    Id = id;
                                    Nome = dr["nome"].ToString();
                                    Modelo = dr["modelo"].ToString();
                                    Ano = Convert.ToInt16(dr["ano"]);
                                    Fabricacao = Convert.ToInt16(dr["fabricacao"]);
                                    Cor = dr["cor"].ToString();
                                    Combustivel = (EnumCombustivel) Convert.ToByte(dr["combustivel"]);
                                    Automatico = (EnumSimNao)Convert.ToByte(dr["automatico"]);
                                    Valor = Convert.ToDecimal(dr["valor"]);
                                    Ativo = Convert.ToBoolean(dr["ativo"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha ... " + ex.Message);
            }
        }


        public void Excluir()
        {

            var sql = "Delete From tb_Veiculos where id = " + Id;
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("falha ... " + ex.Message);
            }
        }

    }
    public enum EnumCombustivel : int
    {
        Gasolina = 1,
        Álcool = 2,
        Flex = 3,
        Diesel = 4,
        Gás = 5,
        Eletricidade = 6
    }
    public enum EnumSimNao : int
    {
        Não = 0,
        Sim = 1
    }
}