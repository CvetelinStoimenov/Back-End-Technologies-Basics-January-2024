using GetGreeting;
using Moq;

namespace GreetingProviderNunitTests
{
    public class Tests
    {
        private Mock<ITimeProvider> mockTimeProvider;
        private GetGreeting.GreetingProvider greetingProvider;

        [SetUp]
        public void Setup()
        {
            mockTimeProvider = new Mock<ITimeProvider>();
            greetingProvider = new
            GetGreeting.GreetingProvider(mockTimeProvider.Object);
        }

        [Test]
        public void GreetingAt9AmShouldBeGoodMorning()
        {
            // Arrange

            mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 9, 0, 0));

            // Act
            string result = greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good morning!"));
        }

        [Test]
        public void GreetingAt1pmShouldBeGoodAfternoon()
        {
            // Arrange

            mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 13, 0, 0));

            // Act
            string result = greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good afternoon!"));
        }

        [Test]
        public void GreetingAt6pmShouldBeGoodEvening()
        {
            // Arrange

            mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 18, 0, 0));

            // Act
            string result = greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good evening!"));
        }

        [Test]
        public void GreetingAt11pmShouldBeGoodNight()
        {
            // Arrange

            mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 23, 0, 0));

            // Act
            string result = greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good night!"));
        }
    }
}