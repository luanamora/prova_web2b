using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Business.Produto;

namespace ApiProductTest
{
    class TestProduto
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(null, false)]
        [TestCase(0, false)]
        [TestCase(123, true)]
        public void Test_NumberNotNullOrZero(double numero, bool resultado)
        {
            //Verifica se o valor numérico informado e é maior que 0
            bool notNullNotZero = numero != null;
            if (notNullNotZero)
            {
                notNullNotZero = numero > 0;
            }
            Assert.AreEqual(notNullNotZero, resultado);
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("     ", false)]
        [TestCase("teste", true)]
        public void Test_StringNotNullOrEmpty(String cadeiaCaracteres, bool resultado)
        {
            //Verifica se a string foi informada
            bool notNullNotEmpty = cadeiaCaracteres != null;
            if(notNullNotEmpty)
            {
                notNullNotEmpty = cadeiaCaracteres.Trim().Length > 0;
            }
            Assert.AreEqual(notNullNotEmpty, resultado);
        }

        [TestCase(1, 1, true)]
        [TestCase(15687, 58691, false)]
        public void Test_ImagemFound(int cd_produto, int cd_imagem, bool resultado)
        {
            //Verifica se a imagem da PK informada existe
            Produto produto = new Produto();
            String sql = String.Format("Select 1 from imagemproduto where cd_produto = {0} and cd_imagem = {1}",
                cd_produto, cd_imagem);
            bool found = produto.findData(sql);
            Assert.AreEqual(found, resultado);
        }

        [TestCase(1, true)]
        [TestCase(15687, false)]
        public void Test_MarcaFound(int cd_marca, bool resultado)
        {
            //Verifica se a marca da PK informada existe
            Produto produto = new Produto();
            bool found = produto.findData("Select 1 from marca where cd_marca = " + cd_marca);
            Assert.AreEqual(found, resultado);
        }

        [TestCase(1, true)]
        [TestCase(15687, false)]
        public void Test_ProdutoFound(int cd_produto, bool resultado)
        {
            //Verifica se o produto da PK informada existe
            Produto produto = new Produto();
            bool found = produto.findData("Select 1 from produto where cd_produto = " + cd_produto);
            Assert.AreEqual(found, resultado);
        }

        [TestCase(1, true)]
        [TestCase(15687, false)]
        public void Test_MarcaNotReferencedOnDelete(int cd_marca, bool resultado)
        {
            //Verifica se a marca informada possui uma referencia com outro objeto no banco
            Produto produto = new Produto();
            bool referenced = produto.findData("Select cd_produto from produto where cd_marca = " + cd_marca);
            Assert.AreEqual(referenced, resultado);
        }

        [TestCase(1, true)]
        [TestCase(15687, false)]
        public void Test_ProdutoNotReferencedOnDelete(int cd_produto, bool resultado)
        {
            //Verifica se o produto informado possui uma referencia com outro objeto no banco
            Produto produto = new Produto();
            bool referenced = produto.findData("Select cd_imagem from imagemproduto where cd_produto = " + cd_produto);
            Assert.AreEqual(referenced, resultado);
        }

        [TestCase("String com menos de 60 caracteres", 60, true)]
        [TestCase("Isto é apenas um teste de uma string exemplo com mais de 60 caracteres", 60, false)]
        public void Test_StringAttributeLengthInBound(String cadeiaCaracteres, int lenght, bool resultado)
        {
            //Verifica se o length dos atributos no formato string estão no tamanho imposto no DB
            bool stringLenghtInBound = cadeiaCaracteres.Length <= lenght;
            Assert.AreEqual(stringLenghtInBound, resultado);
        }
    }
}
