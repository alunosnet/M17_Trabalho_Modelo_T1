using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M17_TrabalhoModelo_T1;
using System.Data;

namespace TestarBD
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestarAdicionarLivros()
        {
            BaseDados.Instance.adicionarLivro("Livro 1", DateTime.Now.Year, DateTime.Now, 10);
        }
        [TestMethod]
        public void TestarLogin()
        {
            string nome = "admin@gmail.com";
            string password = "123456";
            DataTable dados = BaseDados.Instance.verificarLogin(nome, password);
            Assert.IsNotNull(dados);
            Assert.IsTrue(dados.Rows.Count == 1);
        }
    }
}
