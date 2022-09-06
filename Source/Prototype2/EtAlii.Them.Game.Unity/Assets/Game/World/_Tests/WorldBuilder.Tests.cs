namespace Game.World.Tests
{
    using System.Collections;
    using NUnit.Framework;
    using UnityEngine.TestTools;

    public class WorldBuilderTests
    {
        [Test]
        public void WorldBuilder_Create()
        {
            // Arrange.

            // Act.
            var builder = new WorldBuilder();

            // Assert.
            Assert.NotNull(builder);
        }

        [UnityTest]
        public IEnumerator WorldBuilder_Create_With_Start_Land()
        {
            // Arrange.
            
            // Act.

            // Assert.
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator WorldBuilder_Create_With_Random_Islands()
        {
            // Arrange.

            // Act.

            // Assert.
            yield return null;
        }
    }
}