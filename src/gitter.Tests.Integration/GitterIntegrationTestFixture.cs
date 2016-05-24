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

            var testProjectName = "TestProject";

            // Create a path to the test project
            var testProjectDir = Path.GetFullPath (testProjectName);

            // Create the source project directory
            Directory.CreateDirectory (testProjectDir);

            // Create a temporary path to the destination project
            var tmpProjectCloneDir = Path.GetFullPath (testProjectName + "Clone");

            // Create the destination project directory
            Directory.CreateDirectory (tmpProjectCloneDir);

            // Move to the test project directory
            Directory.SetCurrentDirectory (testProjectDir);

            Console.WriteLine ("");
            Console.WriteLine ("===== Executing Test =====");
            Console.WriteLine ("");

            // Initialize source git repo
            gitter.Init (testProjectDir);

            var testFileName = "TestFile.txt";

            // Create a test file path
            var testFilePath = testProjectDir
                           + Path.DirectorySeparatorChar
                           + testFileName;

            var testFileContent = "Test content";

            // Create the test file
            File.WriteAllText (testFilePath, testFileContent);

            Console.WriteLine ("Adding test file...");

            // Add the test file
            gitter.Add(testFilePath);

            Console.WriteLine ("Committing...");

            // Commit the test file
            gitter.Commit ();

            Console.WriteLine ("Cloning...");

            // Clone the temporary project into a new directory
            gitter.Clone (".", "../" + testProjectName + "Clone");

            var clonedTestFile = tmpProjectCloneDir
                                 + Path.DirectorySeparatorChar
                                 + testFileName;


            Console.WriteLine ("");
            Console.WriteLine ("===== Checking Result =====");
            Console.WriteLine ("");


            // Assert that the file was clone
            Assert.IsTrue (File.Exists (clonedTestFile), "The test file wasn't cloned.");

            Assert.AreEqual (testFileContent, File.ReadAllText (clonedTestFile), "The cloned test file doesn't have the expected contents.");
        }
    }
}

