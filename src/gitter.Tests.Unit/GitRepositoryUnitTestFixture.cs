using System;
using NUnit.Framework;
using System.IO;

namespace gitter.Tests.Unit
{
	[TestFixture(Category="Unit")]
	public class GitRepositoryUnitTestFixture : BaseUnitTestFixture
	{
		[Test]
		public void Test_Pull()
		{
			var workingDirectory = Environment.CurrentDirectory;

			var processStarter = new ProcessStarter ();

			Console.WriteLine ("");
			Console.WriteLine ("Setting up source repository...");
			Console.WriteLine ("");

			// Set up the source directory
			var sourceRepository = Path.GetFullPath ("SourceRepo");
			Console.WriteLine ("Source repo: " + sourceRepository);
			Directory.CreateDirectory (sourceRepository);

			Directory.SetCurrentDirectory (sourceRepository);

			var testFileName = "test.txt";

			// Set up the source file
			var sourceFile = Path.Combine (sourceRepository, testFileName);
			File.WriteAllText (sourceFile, "Hello world 1");
			Console.WriteLine ("Source file: " + sourceFile);

			// Set up the source repository
			processStarter.Start ("git init");
			processStarter.Start ("git add test.txt");
			processStarter.Start ("git commit -am 'Initial commit'");

			Directory.SetCurrentDirectory (workingDirectory);

			Console.WriteLine ("");
			Console.WriteLine ("Setting up destination repository and cloning...");
			Console.WriteLine ("");

			// Set up destination directory and clone
			var destinationRepository = Path.GetFullPath ("DestinationRepo");
			Console.WriteLine ("Destination repo: " + destinationRepository);
			//Directory.CreateDirectory (sourceRepository);

			processStarter.Start ("git clone -v " + sourceRepository + " " + destinationRepository);

			var destinationFile = Path.Combine (destinationRepository, testFileName);
			Console.WriteLine ("Destination file: " + destinationFile);

			Assert.IsTrue (File.Exists (destinationFile), "File not found: " + destinationFile);

			Console.WriteLine ("");
			Console.WriteLine ("Editing the source file...");
			Console.WriteLine ("");

			// Change the source file
			File.WriteAllText(sourceFile, "Hello world 2");

			Directory.SetCurrentDirectory (sourceRepository);

			processStarter.Start ("git commit -am 'Updated test file'");

			Directory.SetCurrentDirectory (destinationRepository);

			var gitter = new Gitter ();

			var repository = gitter.Open (destinationRepository);

			Console.WriteLine ("");
			Console.WriteLine ("Pulling changes to destination repository...");
			Console.WriteLine ("");

			repository.Pull ("origin");

			var expectedContent = "Hello world 2";

			Assert.AreEqual (expectedContent, File.ReadAllText (destinationFile), "Invalid content");
		}

	}
}

