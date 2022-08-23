using UnityEngine;
using Helpers;
using NUnit.Framework;

public class CoordinateConverterTests
{
    [Test]
    public void ConvertPolarToCartesianTest1()
    {
        var polar = new PolarCoordinate(0.3f, 0.3f);
        var cartesian = polar.PolarToCartesian();
        var backPolar = cartesian.CartesianToPolar();

        Assert.That(polar.r, Is.EqualTo(backPolar.r).Within(.00005f));
        Assert.That(polar.phi, Is.EqualTo(backPolar.phi).Within(.00005f));
    }
    
    [Test]
    public void ConvertPolarToCartesianTest2()
    {
        var r = Random.Range(0.1f, 5f);
        var phi = Random.Range(0f, Mathf.PI * 2f);
        var polar = new PolarCoordinate(r, phi);
        var cartesian = polar.PolarToCartesian();
        var backPolar = cartesian.CartesianToPolar();

        Assert.That(r, Is.EqualTo(backPolar.r).Within(.00005f));
        Assert.That(phi, Is.EqualTo(backPolar.phi).Within(.00005f));
    }

    [Test]
    public void ConvertPolarToCartesianTest3()
    {
        var cartesian = new Vector2(5f, 10f);
        var polar = cartesian.CartesianToPolar();
        var cartesian2 = polar.PolarToCartesian();

        Assert.That(cartesian.x, Is.EqualTo(cartesian2.x).Within(.00005f));
        Assert.That(cartesian.y, Is.EqualTo(cartesian2.y).Within(.00005f));
    }
}
