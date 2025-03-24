using NUnit.Framework;
using UnityEngine;

public class WallCreationTests
{
    private WallCreator creator;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        creator = new GameObject().AddComponent<WallCreator>()
                                  .SetTilePrototype(new GameObject());
    }

    // A Test behaves as an ordinary method
    [Test]
    public void Create1x1WallPasses()
    {
        //Arrange
        Assert.IsNotNull(creator);

        //Act
        creator.SetDimention(1, 1)
               .Create();

        //Assert
        Assert.AreEqual(1, creator.Tiles.Count);
        Assert.AreEqual(Vector3.zero, creator.Tiles[0].transform.position);
    }

    [Test]
    public void Create2x2WallPasses()
    {
        //Arrange
        Assert.IsNotNull(creator);

        Vector3[] expectedPos = {
            new(-0.5f, -0.5f),
            new( 0.5f, -0.5f),
            new(-0.5f,  0.5f),
            new( 0.5f,  0.5f)
        };

        //Act
        creator.SetDimention(2, 2)
               .Create();

        //Assert
        Assert.AreEqual(4, creator.Tiles.Count);
        for(int i = 0; i < expectedPos.Length; i++)
        {
            Assert.AreEqual(expectedPos[i], creator.Tiles[i].transform.position);
        }
    }

    [Test]
    public void Create3x3WallPasses()
    {
        //Arrange
        Assert.IsNotNull(creator);

        //Expected Result Array
        float[] prepare = { -1f, 0f, 1f };
        Vector3[] expectedPos = new Vector3[9];

        int n = 0;
        for(var j = 0; j < 3; j++)
        {
            for(var i = 0; i < 3; i++)
            {
                expectedPos[n] = new(prepare[i], prepare[j]);
                n++;
            }
        }

        //Act
        creator.SetDimention(3, 3)
               .Create();

        //Assert
        Assert.AreEqual(9, creator.Tiles.Count);
        for(int i = 0; i < expectedPos.Length; i++)
        {
            Assert.AreEqual(expectedPos[i], creator.Tiles[i].transform.position);
        }
    }
}
