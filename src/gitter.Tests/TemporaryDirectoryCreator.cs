using System;
using System.IO;

namespace gitter.Tests
{
	public class TemporaryDirectoryCreator
	{
		public TemporaryDirectoryCreator ()
		{
		}

		public string Create ()
		{
			var tmpDir = Path.GetFullPath ("_tmp");

			if (!Directory.Exists (tmpDir))
				Directory.CreateDirectory (tmpDir);

            var guid = Guid.NewGuid ().ToString();
            var key = guid.Substring (0, guid.IndexOf ("-"));

			var uniqueTmpDir = Path.Combine (tmpDir, key);

			Directory.CreateDirectory (uniqueTmpDir);

			return uniqueTmpDir;
		}
	}
}

