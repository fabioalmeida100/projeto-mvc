using FI.AtividadeEntrevista.CPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Gravar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario beneficiarioDAL = new DAL.DaoBeneficiario();
            var beneficiarioExiste = beneficiarioDAL.Consultar(beneficiario.Id);

            if ((beneficiarioExiste != null) && (beneficiarioExiste.Id != 0))
            {
                Alterar(beneficiario);
                return beneficiario.Id;
            }
            else
            {
                return Incluir(beneficiario);
            }

        }


        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiarios">Objeto de beneficiário</param>
        public long Incluir(DML.Beneficiario beneficiarios)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            beneficiarios.CPF = CPFUtils.RemoverFormatacaoCPF(beneficiarios.CPF);

            if (!CPFUtils.Validar(beneficiarios.CPF))
                throw new Exception("CPF inválido.");           

            if (!VerificarExistencia(beneficiarios.CPF, beneficiarios.IdCliente))
                return beneficiario.Incluir(beneficiarios); 
            else
                throw new Exception("CPF já cadastrado.");

        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario beneficiarios = new DAL.DaoBeneficiario();
            beneficiario.CPF = CPFUtils.RemoverFormatacaoCPF(beneficiario.CPF);

            if (!CPFUtils.Validar(beneficiario.CPF))
                throw new Exception("CPF inválido.");

            beneficiarios.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiario beneficiario = new DAL.DaoBeneficiario();
            return beneficiario.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Beneficiario> Listar(long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.Listar(idCliente);
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
        public bool VerificarExistencia(string CPF, long idCliente)
        {
            DAL.DaoBeneficiario cli = new DAL.DaoBeneficiario();
            return cli.VerificarExistencia(CPF, idCliente);
        }


    }
}
