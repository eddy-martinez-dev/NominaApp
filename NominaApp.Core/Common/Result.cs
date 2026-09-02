using System;
using System.Collections.Generic;
using System.Text;

namespace NominaApp.Core.Common
{
    public class Result<T>
    {
        public bool EsExitoso { get; }
        public T? Valor { get; }
        public string? Error { get; }

        private Result(bool esExitoso, T? valor, string? error)
        {
            EsExitoso = esExitoso;
            Valor = valor;
            Error = error;
        }

        public static Result<T> Success(T valor) => new(true, valor, null);
        public static Result<T> Failure(string error) => new(false, default, error);
    }
}
