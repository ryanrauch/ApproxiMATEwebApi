using ApproxiMATEwebApi.Models;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Services.Interfaces
{
    public interface IHexagonal
    {
        IList<int> Layers { get; }
        Position CenterLocation { get; }
        Position ExactLocation { get; }
        Polygon HexagonalPolygon(Position center);
        Polygon HexagonalPolygon(Position center, int column, int row);
        void SetCenter(Position center);
        void SetLayer(int layer);
        void Initialize(double latitude, double longitude, int layer);
        String AllLayersDelimited();
    }
}
