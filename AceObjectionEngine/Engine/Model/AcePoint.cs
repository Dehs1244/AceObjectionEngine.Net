using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public struct AcePoint
    {
        public float X { get; }
        public float Y { get; }

        public AcePoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public AcePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static AcePoint operator +(AcePoint point1, AcePoint point2)
        {
            return point1.Add(point2);
        }

        public static bool operator ==(AcePoint point1, AcePoint point2) => point1.X == point2.X && point2.Y == point1.Y;

        public static bool operator !=(AcePoint point1, AcePoint point2) => !(point1 == point2);

        public AcePoint Add(float x, float y) => new AcePoint(X + x, Y + y);

        public AcePoint Set(float x, float y) => new AcePoint(x, y);

        public AcePoint Add(AcePoint point) => Add(point.X, point.Y);

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AcePoint)) return false;

            return this == (AcePoint)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
