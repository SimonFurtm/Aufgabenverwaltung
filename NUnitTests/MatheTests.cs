namespace NUnitTests
{
    public class MathTests
    {
        private int num1;
        private int num2;
        private int result;

        [SetUp]
        public void SetUp()
        {
            // Hier werden die Testdaten initialisiert
            num1 = 10;
            num2 = 5;
        }

        [Test]
        public void Test_Addition()
        {
            // Hier wird die Addition getestet
            result = num1 + num2;

            // Assert
            Assert.AreEqual(15, result);
        }

        [Test]
        public void Test_Subtraction()
        {
            // Hier wird die Subtraktion getestet
            result = num1 - num2;

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Test_Multiplication()
        {
            // Hier wird die Multiplikation getestet
            result = num1 * num2;

            // Assert
            Assert.AreEqual(50, result);
        }

        [Test]
        public void Test_Division()
        {
            // Hier wird die Division getestet
            result = num1 / num2;

            // Assert
            Assert.AreEqual(2, result);
        }
    }
}
