using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CRUDBasico.Models
{
    public class Usuario
    {
        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        //public Usuario() { }
        //public Usuario(int id, string email, string nome, string senha)
        //{
        //    Id = id;
        //    Email = email;
        //    Nome = nome;
        //    Senha = senha;
        //}

        public bool Login()
        {
            var result=false;
            var sql = "Select Id, Nome, Senha From Usuarios Where Email = '" + this.Email + "'";
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
                                    if( this.Senha==dr["senha"].ToString())
                                    {
                                        this.Id = Convert.ToInt32(dr["id"]);
                                        this.Nome = dr["nome"].ToString();
                                        result = true;
                                    }
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
            return result;
        }
    }
}