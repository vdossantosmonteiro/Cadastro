using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.DAL.Contracts
{
    public interface IBaseRepository<T> where T: class
    {
        void Inserir(T obj);
        void Atualizar(T obj);
        void Deletar(int id);
        List<T> ObterDados();

        T ObterPorId(int id);
    }
}
