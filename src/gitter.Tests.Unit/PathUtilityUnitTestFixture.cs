using System;
using NUnit.Framework;
using System.IO;

namespace gitter.Tests.Unit
{
    // TODO: Remove or reimplement
    //[TestFixture(Category="Unit")]
    public class PathUtilityUnitTestFixture : BaseUnitTestFixture
    {
        //[Test]
        public void Test_ToRelative_SimilarFolderName()
        {
            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = "Folder/File.txt";

            var path = Path.Combine(workingDirectory + "2", relativeFilePath);

            var createdRelativeFilePath = PathUtility.ToRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }

        //[Test]
        public void Test_ToRelative_SubFolder()
        {
            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = "Folder/File.txt";

            var path = Path.Combine (workingDirectory, relativeFilePath);

            var createdRelativeFilePath = PathUtility.ToRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }

        //[Test]
        public void Test_ToRelative_CurrentFolder()
        {
            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = ".";

            var path = workingDirectory;

            var createdRelativeFilePath = PathUtility.ToRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }

        //[Test]
        public void Test_ToRelative_OutsideFolder()
        {
            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = "Folder/File.txt";

            var path = Path.Combine(workingDirectory + "2", relativeFilePath);

            var createdRelativeFilePath = PathUtility.ToRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }

        //[Test]
        public void Test_EnsureRelative_NotRelative()
        {

            var workingDirectory = Environment.CurrentDirectory;

            var relativeFilePath = "Folder/File.txt";

            var path = Path.Combine (workingDirectory, relativeFilePath);

            var createdRelativeFilePath = PathUtility.EnsureRelative (path, workingDirectory);

            Assert.AreEqual (relativeFilePath, createdRelativeFilePath);
        }

        //[Test]
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

