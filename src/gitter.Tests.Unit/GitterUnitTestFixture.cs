using System;
using NUnit.Framework;
using System.IO;

namespace gitter.Tests.Unit
{
    [TestFixture (Category = "Unit")]
    public class GitterUnitTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_Init ()
        {
            var gitter = new Gitter ();

            gitter.Init ();

            var gitDir = Path.GetFullPath (".git");

            var gitDirExists = Directory.Exists (gitDir);

            Assert.IsTrue (gitDirExists);
        }

        [Test]
        public void Test_IsInitialized ()
        {
            var gitter = new Gitter ();

            var repoDir = Path.GetFullPath ("WorkingDir");

            var gitDir = Path.Combine (repoDir, ".git");

            Directory.CreateDirectory (repoDir);
            Directory.CreateDirectory (gitDir);

            Assert.IsTrue (gitter.IsInitialized (repoDir));
        }

        //[Test]
        public void Test_AddFile ()
        {
            var gitter = new Gitter ();
            gitter.Init ();

            throw new NotImplementedException ();
        }

        [Test]
        public void Test_IsWithinRepository_False ()
        {
            var gitter = new Gitter ();

            var repoDir = Path.GetFullPath ("WorkingDir");

            Directory.CreateDirectory (repoDir);

            Assert.IsFalse (gitter.IsWithinRepository (repoDir));
        }


        [Test]
        public void Test_IsWithinRepository ()
        {
            var gitter = new Gitter ();

            var repoDir = Path.GetFullPath ("WorkingDir");

            var gitDir = Path.Combine (repoDir, ".git");

            Directory.CreateDirectory (repoDir);
            Directory.CreateDirectory (gitDir);

            Assert.IsTrue (gitter.IsWithinRepository (repoDir));
        }

        [Test]
        public void Test_IsWithinRepository_Deeper_False ()
        {
            var gitter = new Gitter ();

            var repoDir = Path.GetFullPath ("WorkingDir");

            var subDir = Path.Combine (repoDir, "mysubdir");

            var subDir2 = Path.Combine (subDir, "deepersubdir");

            Directory.CreateDirectory (repoDir);
            Directory.CreateDirectory (subDir);
            Directory.CreateDirectory (subDir2);

            Assert.IsFalse (gitter.IsWithinRepository (subDir2));
        }
    }
}

