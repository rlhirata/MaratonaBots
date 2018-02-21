using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace MaratonaBots.Formulario
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe não entendi \"{0}\".")]

    public class Pedido
    {
        public Salgadinhos Salgadinhos { get; set; }
        public Bebidas Bebidas { get; set; }
        public TipoEntrega TipoEntrega{ get; set; }
        public CPFNaNota CPFNaNota { get; set; }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public static IForm<Pedido> BuildForm()
        {
            var form = new FormBuilder<Pedido>();
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            form.Configuration.Yes = new string[] { "yes", "sim", "s", "y", "yep" };
            form.Configuration.No = new string[] { "não", "nao", "no", "not" };
            form.Message("Olá seja bem vindo! Será um prazer atender você!");
            form.OnCompletion(async (context, pedido) =>
            {
                //Salvar na base de dados
                //Gerar o pedido
                //Integar com serviço externo
                await context.PostAsync("Seu pedido número 123456 foi gerado e em instantes será entregue.");
            });
            return form.Build();
        }
    }

    [Describe("Tipo de Entrega")]
    public enum TipoEntrega
    {
        [Terms("RetirnarNoLocal", "retirar no local")]
        [Describe("RetirarNoLocal")]
        RetirarNoLocal = 1,

        [Terms("Motoboy", "Delivery")]
        [Describe("Motoboy")]
        Motoboy
    }

    [Describe("Salgadinhos")]
    public enum Salgadinhos
    {
        [Terms("Esfiha", "esfirra")]
        [Describe("Esfiha")]
        Esfiha = 1,

        [Terms("Quibe", "kibe")]
        [Describe("Quibe")]
        Quibe,

        [Describe("Coxinha")]
        Coxinha
    }

    [Describe("Bebidas")]
    public enum Bebidas
    {
        [Terms("Água", "agua", "h20", "a")]
        [Describe("Água")]
        Agua = 1,

        [Terms("Refrigerante", "refri")]
        [Describe("Refrigerante")]
        Refrigerante,

        [Describe("Suco")]
        Suco
    }

    [Describe("CPF na Nota")]
    public enum CPFNaNota
    {
        [Terms("Sim", "yes")]
        [Describe("Sim")]
        Sim = 1,

        [Terms("Não", "nao", "no")]
        [Describe("Não")]
        Nao
    }
}