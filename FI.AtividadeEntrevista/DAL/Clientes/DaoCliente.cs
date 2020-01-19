using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiarios
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiário">Objeto de beneficiário</param>
        internal long Incluir(DML.Cliente beneficiário)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiário.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", beneficiário.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiário.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", beneficiário.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", beneficiário.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", beneficiário.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", beneficiário.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", beneficiário.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", beneficiário.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", beneficiário.Telefone));

            DataSet ds = base.Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiário">Objeto de beneficiário</param>
        internal DML.Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificaCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiário">Objeto de beneficiário</param>
        internal void Alterar(DML.Cliente beneficiário)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiário.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", beneficiário.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiário.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", beneficiário.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", beneficiário.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", beneficiário.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", beneficiário.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", beneficiário.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", beneficiário.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", beneficiário.Telefone));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiário.Id));

            base.Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Beneficiarios
        /// </summary>
        /// <param name="beneficiário">Objeto de beneficiário</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelCliente", parametros);
        }

        private List<DML.Cliente> Converter(DataSet ds)
        {
            List<DML.Cliente> lista = new List<DML.Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Cliente cli = new DML.Cliente();
                    cli.Id = row.Field<long>("Id");
                    cli.CEP = row.Field<string>("CEP");
                    cli.Cidade = row.Field<string>("Cidade");
                    cli.Email = row.Field<string>("Email");
                    cli.Estado = row.Field<string>("Estado");
                    cli.Logradouro = row.Field<string>("Logradouro");
                    cli.Nacionalidade = row.Field<string>("Nacionalidade");
                    cli.Nome = row.Field<string>("Nome");
                    cli.Sobrenome = row.Field<string>("Sobrenome");
                    cli.Telefone = row.Field<string>("Telefone");
                    cli.CPF = row.Field<string>("CPF");
                    lista.Add(cli);
                }
            }

            return lista;
        }
    }
}
