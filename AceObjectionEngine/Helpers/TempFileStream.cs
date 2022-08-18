using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Helpers
{
    internal class TempFileStream : FileStream, IDisposable
    {
        private static List<TempFileStream> AllTempFiles = new List<TempFileStream>();
        public string FilePath => Name;

        public TempFileStream(string fileName) : base(_PrepeareTempFilePath(fileName), FileMode.OpenOrCreate)
        {
            AllTempFiles.Add(this);
        }

        private static string _PrepeareTempFilePath(string fileName) => Path.Combine(_GetObjectionTempPath(), $"{Guid.NewGuid()}-{fileName}");
        private static string _GetObjectionTempPath()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "objection-lib");
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            return tempPath;
        }

        public void WriteContent(string content)
        {
            byte[] buffer = Encoding.Default.GetBytes(content);
            Write(buffer, 0, buffer.Length);
        }

        public new void Dispose()
        {
            base.Dispose();

            if (File.Exists(FilePath))
                File.Delete(FilePath);
            AllTempFiles.RemoveAll(x => x.FilePath == FilePath);
        }

        public static void ImmediatelyFreeTempFiles()
        {
            foreach(var tempFile in AllTempFiles.ToArray())
            {
                tempFile.Dispose();
            }
        }
    }
}
