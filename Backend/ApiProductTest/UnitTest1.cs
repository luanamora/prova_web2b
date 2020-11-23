using Business.Domain;
using Business.IR;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Teste_Soma()
        {
            Assert.GreaterOrEqual(1, 1, "Valore diferentes");
        }

        [Test]
        public void Teste_Multiplicacao()
        {
            //Processamento preparatório para os testes 
            Assert.AreEqual(MultiplicaRegistros(58, 126), 7308, "Valor da multiplicação incorreta!");
            Assert.AreEqual(MultiplicaRegistros(2, 58.69), 117.38, "Valor da multiplicação incorreta!");
            Assert.AreEqual(MultiplicaRegistros(544, 2), 1088, "Valor da multiplicação incorreta!");
            Assert.AreEqual(MultiplicaRegistros(128, 236), 30208, "Valor da multiplicação incorreta!");
            //Assert.AreEqual(MultiplicaRegistros(965, 4548.5), 7308.5, "Valor da multiplicação incorreta!");
            //Assert.AreEqual(MultiplicaRegistros(59, 487.7), 7308.5, "Valor da multiplicação incorreta!");
            //Assert.AreEqual(MultiplicaRegistros(1, 44), 7308.5, "Valor da multiplicação incorreta!");
        }

        [TestCase(58, 126, 7308)]
        [TestCase(2, 58.69, 117.38)]
        [TestCase(544, 2, 1088)]
        [TestCase(128, 236, 30208)]
        public void Teste_Multiplicacao_Multiplos(double v1, double v2, double result)
        {
            //Processamento preparatório para os testes 
            Assert.AreEqual(MultiplicaRegistros(v1, v2), result, "Valor da multiplicação incorreta!");
        }


        public double MultiplicaRegistros(double v1, double v2) => v1 * v2;


        [Test]
        public void Verifica_inss_funcionario()
        {
            Pessoa pessoa = new Pessoa(1, "Marcelo", 1045, 0);
            CalcIR calcIR = new CalcIR();
            calcIR.CalculaInss(pessoa);
            Assert.AreEqual(pessoa.Inss, 78.38);

            pessoa.Salario = 2000;
            calcIR.CalculaInss(pessoa);
            Assert.AreEqual(pessoa.Inss, 164.33);
        }

        [Test]
        public void Verifica_inss_funcionario_sem_instancia()
        {
            Pessoa pessoa = new Pessoa();
            CalcIR calcIR = new CalcIR();
            calcIR.CalculaInss(pessoa);
            Assert.AreEqual(pessoa.Inss, 0);

        }

        [Test]
        public void Verifica_ir_faixa_15()
        {
            Pessoa pessoa = new Pessoa(1, "Marcelo", 3000, 1);
            CalcIR calcIR = new CalcIR();
            Assert.AreEqual(calcIR.RetornaIR(pessoa), 43.23);
        }


        [Test]
        public void tipos_assert()
        {
            string teste = string.Empty;

            //Assert.AreNotEqual(10, 10);//Testa valores diferentes
            //Assert.Greater();//Maior
            //Assert.IsEmpty(teste);
            //Assert.IsTrue(string.IsNullOrEmpty(teste));
            //Assert.NotNull();
            //var ex = Assert.Throws<ArgumentNullException>(() => foo.Bar(null));
            //Assert.That(ex.ParamName, Is.EqualTo("bar"));
            Assert.That(1 == 2);
        }

    }
}