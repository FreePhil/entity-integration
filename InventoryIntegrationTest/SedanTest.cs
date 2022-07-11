using Inventory.Service;
using Moq;
using NUnit.Framework;

namespace InventoryIntegrationTest;

[TestFixture]
public class SedanTest
{

    [Test]
    public void TestSuperHorsePower()
    {
        var mockEngine = new Mock<IEngine>();
        mockEngine.Setup(engine => engine.Power).Returns(350);
        
        var car = new Sedan(mockEngine.Object);
        int power = car.HorsePower;

        mockEngine.Verify(engine => engine.Power, Times.Once);
        Assert.That(power, Is.EqualTo(300));
    }

        
    [Test]
    public void TestPoorHorsePower()
    {
        var mockEngine = new Mock<IEngine>();
        mockEngine.Setup(engine => engine.Power).Returns(200);
        
        var car = new Sedan(mockEngine.Object);
        int power = car.HorsePower;

        mockEngine.Verify(engine => engine.Power, Times.Exactly(2));
        Assert.That(power, Is.EqualTo(200));
    }
}