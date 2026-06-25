using CRUDBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDBasico.ViewModel
{
    public class VeiculosViewModel
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public short Ano { get; set; }
        public short Fabricacao { get; set; }
        public string Cor { get; set; }
        public EnumCombustivel Combustivel { get; set; }
        public EnumSimNao Automatico { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }

        public VeiculosViewModel(int id, string nome, string modelo, short ano, short fabricacao, string cor, EnumCombustivel combustivel, EnumSimNao automatico, decimal valor, bool ativo)
        {
            Id = id;
            Marca = nome;
            Modelo = modelo;
            Ano = ano;
            Fabricacao = fabricacao;
            Cor = cor;
            Combustivel = combustivel;
            Automatico = automatico;
            Valor = valor;
            Ativo = ativo;
        }

    }
}