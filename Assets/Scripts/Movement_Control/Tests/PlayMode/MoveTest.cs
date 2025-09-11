using NUnit.Framework;
using Shape.Movement;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class MoveTest
{
    private MovementHandler m_Shape;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        m_Shape = new GameObject().AddComponent<MovementHandler>();
    }

    private (Vector3 position,Quaternion orientation) ResetShapePosition()
    {
        m_Shape.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        m_Shape.transform.GetPositionAndRotation(out var pos, out var rot);

        return (pos, rot);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    /// <returns>Destination of the shape</returns>
    private Vector3 GetDestinationPositionAndMoveShape(Vector3 direction)
    {
        var destination = m_Shape.GetMoveDestination(direction);
        m_Shape.MoveTo(destination);

        return destination;
    }

    private void AssertCurrentDestinationAndOrientation(Vector3 expectedPos, Quaternion expectedRot)
    {
        Assert.AreEqual(expectedPos, m_Shape.transform.position);
        Assert.AreEqual(expectedRot, m_Shape.transform.rotation);
    }

    [UnityTest]
    public IEnumerator GetPositionAndMoveShapeInPlusOneXDirectionPasses()
    {
        //Arrange
        var (originalPosition, originalOrientation) = ResetShapePosition();
        Vector3 direction = new(1, 0, 0);

        //Act
        var destination = GetDestinationPositionAndMoveShape(direction);
        
        yield return null;

        //Assert
        //Check GetMoveDestination Output
        var expectedDestination = originalPosition + new Vector3(1,0,0);
        Assert.AreEqual(expectedDestination, destination);



        //Check MoveTo Output
        var expectedEndPos = expectedDestination;

        //Check orientation
        var expectedOrientation = originalOrientation;
        AssertCurrentDestinationAndOrientation(expectedEndPos, expectedOrientation);
    }

    [UnityTest]
    public IEnumerator GetPositionAndMoveShapeInMinusOneXDirectionPasses()
    {
        //Arrange
        var originalPosition = Vector3.zero;
        m_Shape.transform.SetPositionAndRotation(originalPosition, Quaternion.identity);

        Vector3 direction = new(-1, 0, 0);

        //Act
        var destination = m_Shape.GetMoveDestination(direction);
        m_Shape.MoveTo(destination);

        yield return null;

        //Assert
        //Check GetMoveDestination Output
        var expectedDestination = originalPosition + new Vector3(-1,0,0);
        Assert.AreEqual(expectedDestination, destination);

        //Check MoveTo Output
        var expectedEndPos = expectedDestination;
        Assert.AreEqual(expectedEndPos, m_Shape.transform.position);

        //Check orientation
        var expectedOrientation = Quaternion.identity;
        Assert.AreEqual(expectedOrientation, m_Shape.transform.rotation);
    }
}
