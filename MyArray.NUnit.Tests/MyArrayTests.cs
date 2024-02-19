
namespace MyArray.NUnit.Tests
{
    public class MyArrayTests
    {
        [Test]
        public void MyArray_ShouldReplaceValue_WhenPositionIsValid()
        {
            // Arrange
            var arr = new SimpleArray.MyArray(5);

            // Act
            var result = arr.Replace(2, 99);

            // Assert
            Assert.IsTrue(result);
            Assert.That(arr.Array[2], Is.EqualTo(99));
        }

        [Test]
        public void MyArray_ShouldNotReplaceValue_IfPositionIsLesThenZero()
        {
            // Act & Assert
            var arr = new SimpleArray.MyArray(5);
            Assert.Throws<ArgumentOutOfRangeException>(() => arr.Replace(-8, 0));
        }

        [Test]
        public void MyArray_ShouldNotReplaceValue_IfPositionIsGreaterThenArray()
        {
            // Act & Assert
            var arr = new SimpleArray.MyArray(5);
            Assert.Throws<ArgumentOutOfRangeException>(() => arr.Replace(6, 0));
        }
    }
}