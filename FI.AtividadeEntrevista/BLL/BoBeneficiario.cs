using FI.AtividadeEntrevista.CPF;
using System;
using System.Collections.Generic;

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
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario beneficiarioDAL = new DAL.DaoBeneficiario();
            beneficiario.CPF = CPFUtils.RemoverFormatacaoCPF(beneficiario.CPF);

            if (!CPFUtils.Validar(beneficiario.CPF))
                throw new Exception("CPF inválido.");           

            if (!VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente))
                return beneficiarioDAL.Incluir(beneficiario); 
            else
                throw new Exception("CPF já cadastrado.");

        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario beneficiarioDAL = new DAL.DaoBeneficiario();
            beneficiario.CPF = CPFUtils.RemoverFormatacaoCPF(beneficiario.CPF);

            if (!CPFUtils.Validar(beneficiario.CPF))
                throw new Exception("CPF inválido.");

            beneficiarioDAL.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiario beneficiarioDAL = new DAL.DaoBeneficiario();
            return beneficiarioDAL.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario beneficiarioDAL = new DAL.DaoBeneficiario();
            beneficiarioDAL.Excluir(id);
        }

        /// <summary>
        /// Lista os benefiarios
        /// </summary>
        public List<DML.Beneficiario> Listar(long idCliente)
        {
            DAL.DaoBeneficiario beneficiarioDAL = new DAL.DaoBeneficiario();
            return beneficiarioDAL.Listar(idCliente);
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
