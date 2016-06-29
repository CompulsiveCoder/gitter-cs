using System;
using NUnit.Framework;
using System.IO;

namespace gitter.Tests.Unit
{
    [TestFixture(Category="Unit")]
    public class GitterUnitTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_Init()
        {
            var gitter = new Gitter ();

            gitter.Init ();

            var gitDir = Path.GetFullPath (".git");

            var gitDirExists = Directory.Exists (gitDir);

            Assert.IsTrue (gitDirExists);
        }
     

        //[Test]
        public void Test_AddFile()
        {
            var gitter = new Gitter ();
            gitter.Init ();

            throw new NotImplementedException ();
        }
    }
}

