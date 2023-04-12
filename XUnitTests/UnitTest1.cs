namespace XUnitTests
{
    namespace XUnitTests
    {
        public class MathTests
        {
            private int num1;
            private int num2;
            private int result;

            public MathTests()
            {
                // Hier werden die Testdaten initialisiert
                num1 = 10;
                num2 = 5;
            }

            [Fact]
            public void Test_Addition()
            {
                // Hier wird die Addition getestet
                result = num1 + num2;

                // Assert
                Assert.Equal(15, result);
            }

            [Fact]
            public void Test_Subtraction()
            {
                // Hier wird die Subtraktion getestet
                result = num1 - num2;

                // Assert
                Assert.Equal(5, result);
            }

            [Fact]
            public void Test_Multiplication()
            {
                // Hier wird die Multiplikation getestet
                result = num1 * num2;

                // Assert
                Assert.Equal(50, result);
            }

            [Fact]
            public void Test_Division()
            {
                // Hier wird die Division getestet
                result = num1 / num2;

                // Assert
                Assert.Equal(2, result);
            }
        }
    }
}