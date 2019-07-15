using Microsoft.AspNetCore.Mvc;
using PartnerGroup.Domain.Models;
using PartnerGroup.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartnerGroup.SharedKernel
{
    public static class ObjectValidator
    {
        public static string PatrimonioValidator( Patrimonio obj)
        {

            MarcaRepository rep = new MarcaRepository();

            string strError = string.Empty;
            int errorCount = 0;

            if (obj == null)
            {
                errorCount++;
                strError = $"{errorCount} - O objeto está nulo ";
            }
            if (rep.GetSingle(obj.MarcaId).Id < 1)
            {
                errorCount++;
                strError += !string.IsNullOrEmpty(strError) ? $", {errorCount} - Não há resultados para marca com ID igual a {obj.MarcaId} " : $" {errorCount} - Não há resultados para marca com ID igual a {obj.MarcaId} ";
            }
            if (string.IsNullOrEmpty(obj.Nome))
            {
                errorCount++;
                strError += !string.IsNullOrEmpty(strError) ? $", {errorCount} - Por favor, preencha o nome " : $"{errorCount} - Por favor, preencha o nome ";
            }

            return strError;
        }


        public static string MarcaValidator(Marca obj)
        {

            string strError = string.Empty;
            int errorCount = 0;

            if (obj == null)
            {
                errorCount++;
                strError = $"{errorCount} - O objeto está nulo ";
            }

            if (string.IsNullOrEmpty(obj.Nome))
            {
                errorCount++;
                strError += !string.IsNullOrEmpty(strError) ? $", {errorCount} - Por favor, preencha o nome " : $"{errorCount} - Por favor, preencha o nome ";
            }

            return strError;
        }
    }
}
