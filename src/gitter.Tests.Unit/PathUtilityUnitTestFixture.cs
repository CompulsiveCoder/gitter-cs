using System;
using NUnit.Framework;
using System.IO;

namespace gitter.Tests.Unit
{
    [TestFixture(Category="Unit")]
    public class PathUtilityUnitTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_ToRelative()
        {
            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = "Folder/File.txt";

            var path = Path.Combine (workingDirectory, relativeFilePath);

            var createdRelativeFilePath = PathUtility.ToRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }

        [Test]
        public void Test_EnsureRelative_NotRelative()
        {

            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = "Folder/File.txt";

            var path = Path.Combine (workingDirectory, relativeFilePath);

            var createdRelativeFilePath = PathUtility.EnsureRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }

        [Test]
        public void Test_EnsureRelative_IsRelative()
        {
            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = "Folder/File.txt";

            var path = relativeFilePath;

            var createdRelativeFilePath = PathUtility.EnsureRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }
    }
}

