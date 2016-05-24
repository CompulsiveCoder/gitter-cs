using System;
using NUnit.Framework;
using System.IO;
using System.Threading;

namespace gitter.Tests.Integration
{
    [TestFixture(Category="Integration")]
    public class GitterIntegrationTestFixture : BaseIntegrationTestFixture
    {
        [Test]
        public void Test_Init_Add_Commit_Clone()
        {
            Console.WriteLine ("");
            Console.WriteLine ("===== Preparing Test =====");
            Console.WriteLine ("");

            var gitter = new Gitter ();

            // Create a temporary path to the source project
            var tmpProjectDir = Path.GetFullPath("TestProject");

            // Create the source project directory
            Directory.CreateDirectory(tmpProjectDir);

            // Create a temporary path to the destination project
            var tmpProjectCloneDir = Path.GetFullPath("TestProjectClone");

            // Create the destination project directory
            Directory.CreateDirectory(tmpProjectCloneDir);

            Console.WriteLine ("");
            Console.WriteLine ("===== Executing Test =====");
            Console.WriteLine ("");

            // Initialize source git repo
            gitter.Init(tmpProjectDir);

            // Create a test file path
            var testFile = tmpProjectDir
                + Path.DirectorySeparatorChar
                + "TestFile.cs";

            var testContents = "Test contents";

            // Create the test file
            File.WriteAllText(testFile, testContents);

            Console.WriteLine ("Adding test file...");

            // Add the test file
            //gitter.Add(testFile);




            Console.WriteLine ("Committing...");

            // Commit the test file
            gitter.Commit();

            /*
            Console.WriteLine("Cloning...");

            // Clone the temporary project into a new directory
            gitter.Clone (tmpProjectDir, tmpProjectCloneDir);

             * var clonedTestFile = tmpProjectCloneDir
                + Path.DirectorySeparatorChar
                + "TestFile.txt";

            // Assert that the file was clone
            Assert.IsTrue(File.Exists(clonedTestFile), "The test file wasn't cloned.");

            Assert.AreEqual(testContents, File.ReadAllText(clonedTestFile), "The cloned test file doesn't have the expected contents.");
            */
        }
    }
}

