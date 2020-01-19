﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiarios
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        public long IdCliente { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }
              
        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        public string CPF { get; set; }

    }    
}