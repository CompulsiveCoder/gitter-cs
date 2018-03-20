using System;
using NUnit.Framework;
using System.IO;

namespace gitter.Tests.Unit
{
    public abstract class BaseUnitTestFixture : BaseTestFixture
    {
		public string OriginalDirectory { get; set; }

        public BaseUnitTestFixture ()
        {
        }

		[SetUp]
		public void Setup()
		{
			OriginalDirectory = Environment.CurrentDirectory;
			
			Environment.CurrentDirectory = new TemporaryDirectoryCreator ().Create ();

			Console.WriteLine ("Current directory:");
			Console.WriteLine (Environment.CurrentDirectory);
		}

		[TearDown]
		public void TearDown()
		{
			var tmpDir = Environment.CurrentDirectory;

			Environment.CurrentDirectory = OriginalDirectory;

			Directory.Delete (tmpDir, true);
		}
    }
}

