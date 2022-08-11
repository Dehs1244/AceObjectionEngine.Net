using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    public interface IStorageProvider
    {
        void CreatePoint(string path);
        void WriteContentInPoint(string point, string content);
        bool IsPointExists(string point);
        string ReadPointContent(string point);
        IEnumerable<string> EnumerateAllPoints(string route);


        void CreateRoute(string route);
        void RemoveRoute(string route);
        bool IsRouteExists(string route);
        IEnumerable<string> SeparateRoute(string route);
        IEnumerable<string> EnumerateAllRoutes(string inRoute);
    }
}
