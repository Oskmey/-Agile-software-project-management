using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(0)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(-1)]
    public void Counter_GivenValue_SetCounterToValue(int expectedValue)
    {
        // Arrange
        var counter = 0;
        // Act
        counter = expectedValue;

        // Assert 
        // Example of same assertion using different syntax
        Assert.That(counter, Is.EqualTo(expectedValue));
        //Assert.Equals(expectedValue, counter);
    }

    [Test]
    [TestCase(0)]
    public void Counter_GivenValue_SetCounterToZero(int expectedValue)
    {
        // Arrange
        int counter;
        // Act
        counter = expectedValue;

        // Assert
        // Example of same assertion using different syntax
        Assert.That(counter, Is.EqualTo(0));
        // Assert.AreEqual(0, counter);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
