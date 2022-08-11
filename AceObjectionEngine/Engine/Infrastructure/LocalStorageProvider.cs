using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Engine.Infrastructure
{
    public class LocalStorageProvider : IStorageProvider
    {
        public void CreatePoint(string route) => File.Create(route).Close();

        public void CreateRoute(string route) => Directory.CreateDirectory(route);

        public IEnumerable<string> EnumerateAllPoints(string route) => Directory.EnumerateFiles(route);

        public IEnumerable<string> EnumerateAllRoutes(string inRoute) => Directory.EnumerateDirectories(inRoute);

        public bool IsPointExists(string point) => File.Exists(point);

        public bool IsRouteExists(string route) => Directory.Exists(route);

        public string ReadPointContent(string point) => File.ReadAllText(point);

        public void RemoveRoute(string route) => Directory.Delete(route, true);

        public IEnumerable<string> SeparateRoute(string route) => route.Split('\\', '/');

        public void WriteContentInPoint(string point, string content) => File.WriteAllText(point, content);
    }
}
