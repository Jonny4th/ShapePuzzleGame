using NUnit.Framework;
using UnityEngine;
using Scripts.Walls;

public class WallCreationTests
{
    private WallCreatable creator;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        creator = new GameObject()
            .AddComponent<WallCreator>()
            .SetTilePrototype(new GameObject().AddComponent<TileableMono>());
    }

    // A Test behaves as an ordinary method
    [Test]
    public void Create1x1WallPasses()
    {
        //Arrange
        Assert.IsNotNull(creator);

        //Act
        creator.SetDimention(1, 1)
               .Build();

        //Assert
        Assert.AreEqual(1, creator.TileInfos.Count);
        Assert.AreEqual(Vector3.zero, creator.TileInfos[0].transform.position);
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
               .Build();

        //Assert
        Assert.AreEqual(4, creator.TileInfos.Count);

        for(int i = 0; i < expectedPos.Length; i++)
        {
            Assert.AreEqual(expectedPos[i], creator.TileInfos[i].transform.position);
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
               .Build();

        //Assert
        Assert.AreEqual(9, creator.TileInfos.Count);

        for(int i = 0; i < expectedPos.Length; i++)
        {
            Assert.AreEqual(expectedPos[i], creator.TileInfos[i].transform.position);
        }
    }

    [Test]
    public void Create5x3WallPasses()
    {
        //Arrange
        Assert.IsNotNull(creator);

        //Expected Result Array
        float[] expectedX = { -2f, -1f, 0f, 1f, 2f };
        float[] expectedY = { -1f, 0f, 1f };
        Vector3[] expectedPos = new Vector3[15];

        int n = 0;
        for(var j = 0; j < 3; j++)
        {
            for(var i = 0; i < 5; i++)
            {
                expectedPos[n] = new(expectedX[i], expectedY[j]);
                n++;
            }
        }

        //Act
        creator.SetDimention(5, 3)
               .Build();

        //Assert
        Assert.AreEqual(15, creator.TileInfos.Count);
        for(int i = 0; i < expectedPos.Length; i++)
        {
            Assert.AreEqual(expectedPos[i], creator.TileInfos[i].transform.position);
        }
    }

    [Test]
    public void Create3x5WallPasses()
    {
        //Arrange
        Assert.IsNotNull(creator);

        //Expected Result Array
        float[] expectedX = { -1f, 0f, 1f };
        float[] expectedY = { -2f, -1f, 0f, 1f, 2f };
        Vector3[] expectedPos = new Vector3[15];

        int n = 0;
        for(var j = 0; j < 5; j++)
        {
            for(var i = 0; i < 3; i++)
            {
                expectedPos[n] = new(expectedX[i], expectedY[j]);
                n++;
            }
        }

        //Act
        creator.SetDimention(3, 5)
               .Build();

        //Assert
        Assert.AreEqual(15, creator.TileInfos.Count);
        for(int i = 0; i < expectedPos.Length; i++)
        {
            Assert.AreEqual(expectedPos[i], creator.TileInfos[i].transform.position);
        }
    }
}
