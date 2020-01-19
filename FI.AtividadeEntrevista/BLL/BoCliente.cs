using FI.AtividadeEntrevista.CPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiário">Objeto de beneficiário</param>
        public long Incluir(DML.Cliente beneficiário)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            beneficiário.CPF = RemoverFormatacaoCPF(beneficiário.CPF);

            if (!CPFUtils.Validar(beneficiário.CPF))
                throw new Exception("CPF inválido.");           

            if (!VerificarExistencia(beneficiário.CPF))
                return cli.Incluir(beneficiário); 
            else
                throw new Exception("CPF já cadastrado.");

        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiário">Objeto de beneficiário</param>
        public void Alterar(DML.Cliente beneficiário)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            beneficiário.CPF = RemoverFormatacaoCPF(beneficiário.CPF);

            if (!CPFUtils.Validar(beneficiário.CPF))
                throw new Exception("CPF inválido.");

            cli.Alterar(beneficiário);
        }

        /// <summary>
        /// Consulta o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF);
        }

        private string RemoverFormatacaoCPF(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }
    }
}
