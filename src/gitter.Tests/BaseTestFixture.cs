using System;
using NUnit.Framework;
using System.IO;

namespace gitter.Tests
{
    public abstract class BaseTestFixture
    {
        public string OriginalDirectory;
        public string TemporaryDirectory;

        public bool DeleteTemporaryDirectory = true;

        public BaseTestFixture ()
        {
        }

        [SetUp]
        public void SetUp()
        {
            OriginalDirectory = Environment.CurrentDirectory;

            TemporaryDirectory = new TemporaryDirectoryCreator ().Create ();

            Directory.SetCurrentDirectory (TemporaryDirectory);

            Console.WriteLine ("Current directory:");
            Console.WriteLine (TemporaryDirectory);
            Console.WriteLine ();
        }

        [TearDown]
        public void TearDown()
        {
            Directory.SetCurrentDirectory (OriginalDirectory);

            if (DeleteTemporaryDirectory && TemporaryDirectory.ToLower().Contains("_tmp")) {
                Directory.Delete (TemporaryDirectory, true);
            }
        }
    }
}

