using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Helpers
{
    public interface IHexagonal
    {
        Position CenterLocation { get; }
        Position ExactLocation { get; }

        Polygon HexagonalPolygon(Position center);
        Polygon HexagonalPolygon(Position center, int column, int row);
    }
}
