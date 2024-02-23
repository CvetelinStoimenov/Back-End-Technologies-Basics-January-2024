// Ignore Spelling: Nunit

using GetGreeting;
using Moq;

namespace GreetingProviderNunitTests
{
    public class Tests
    {
        private Mock<ITimeProvider> _mockTimeProvider;
        private GetGreeting.GreetingProvider _greetingProvider;

        [SetUp]
        public void Setup()
        {
            _mockTimeProvider = new Mock<ITimeProvider>();
            _greetingProvider = new
            GetGreeting.GreetingProvider(_mockTimeProvider.Object);
        }

        [Test]
        public void GreetingAt9AmShouldBeGoodMorning()
        {
            // Arrange

            _mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 9, 0, 0));

            // Act
            string result = _greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good morning!"));
        }

        [Test]
        public void GreetingAt1pmShouldBeGoodAfternoon()
        {
            // Arrange

            _mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 13, 0, 0));

            // Act
            string result = _greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good afternoon!"));
        }

        [Test]
        public void GreetingAt6pmShouldBeGoodEvening()
        {
            // Arrange

            _mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 18, 0, 0));

            // Act
            string result = _greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good evening!"));
        }

        [Test]
        public void GreetingAt11pmShouldBeGoodNight()
        {
            // Arrange

            _mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, 23, 0, 0));

            // Act
            string result = _greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo("Good night!"));
        }

        [TestCase("Good night!", 4)]
        [TestCase("Good evening!", 19)]
        [TestCase("Good afternoon!", 13)]
        [TestCase("Good morning!", 11)]
        public void GreetingShouldBeCorrectIfCorrectTimeIsGiven(string expectedMessage, int currentTime)
        {
            // Arrange

            _mockTimeProvider.Setup(tp =>
                        tp.GetCurrentTime()).Returns
                        (new DateTime(2024, 1, 1, currentTime, 0, 0));

            // Act
            string result = _greetingProvider.GetGreeting();

            // Assert
            Assert.That(result, Is.EqualTo(expectedMessage));
            _mockTimeProvider.Verify(x => x.GetCurrentTime(), Times.Once);
        }
    }
}