using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Helpers
{
    public class HexagonalEquilateral : IHexagonal
    {
        // coordinates are calculated based off of the diagram that can be seen here:
        // http:--//mathcentral.uregina.ca/QQ/database/QQ.09.07/h/martin4.html
        private const double RADIUS = 0.05;
        private const double HALFRADIUS = 0.025;
        private const double WIDTH = 0.075;
        private readonly double _height = Math.Sqrt(3) * 0.025;
        private readonly double _halfHeight = Math.Sqrt(3) * 0.0125;

        private double _latitude { get; set; }
        private double _longitude { get; set; }

        Position IHexagonal.CenterLocation => new Position(Math.Floor(_latitude / _height) * _height + _halfHeight,
                                                           Math.Floor(_longitude * 10) / 10 + RADIUS);

        Position IHexagonal.ExactLocation => new Position(_latitude, _longitude);

        public Polygon HexagonalPolygon(Position center)
        {
            String debugInfo = String.Format("Lat: {0}\nLon: {1}\n", center.Latitude, center.Longitude);
            double lat_top = center.Latitude + _halfHeight;
            double lat_bottom = center.Latitude - _halfHeight;
            double lon_left = center.Longitude - HALFRADIUS;
            double lon_right = center.Longitude + HALFRADIUS;
            // positions start with top-left, rotating clockwise
            Polygon poly = new Polygon();
            if (poly.Positions == null)
                poly.Positions = new List<Position>();
            poly.Positions.Add(new Position(lat_top, lon_left));
            poly.Positions.Add(new Position(lat_top, lon_right));
            poly.Positions.Add(new Position(center.Latitude, center.Longitude + RADIUS));
            poly.Positions.Add(new Position(lat_bottom, lon_right));
            poly.Positions.Add(new Position(lat_bottom, lon_left));
            poly.Positions.Add(new Position(center.Latitude, center.Longitude - RADIUS));
            return poly;
        }

        public Polygon HexagonalPolygon(Position center, int column, int row)
        {
            if (column % 2 == 0)
                return HexagonalPolygon(new Position(center.Latitude + row * _height,
                                                     center.Longitude + column * WIDTH));
            return HexagonalPolygon(new Position(center.Latitude + (row * _height) - _halfHeight,
                                                 center.Longitude + column * WIDTH));
        }

        public HexagonalEquilateral(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public Boolean WithinHexagon(Position point, Polygon bounds)
        {
            // within outer bounding box
            if (WithinRectangle(point, new Position(bounds.Positions[4].Latitude,
                                                   bounds.Positions[5].Longitude),
                                       new Position(bounds.Positions[0].Latitude,
                                                    bounds.Positions[2].Longitude)))
            {
                // within inner rectangle
                if(WithinRectangle(point, bounds.Positions[4], bounds.Positions[1]))
                {
                    return true;
                }
                // within left-triangle
                else if(WithinTriangle(point, bounds.Positions[0], bounds.Positions[4], bounds.Positions[5]))
                {
                    return true;
                }
                // within right-triangle
                else if(WithinTriangle(point, bounds.Positions[1], bounds.Positions[2], bounds.Positions[3]))
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean OnSegment(Position p, Position q, Position r)
        {
            if(q.Latitude <= Math.Max(p.Latitude, r.Latitude)
               && q.Latitude >= Math.Min(p.Latitude, r.Latitude)
               && q.Longitude <= Math.Max(p.Longitude, r.Longitude)
               && q.Longitude >= Math.Min(p.Longitude, r.Longitude))
            {
                return true;
            }
            return false;
        }

        public PolygonOrientation Orientation(Position p, Position q, Position r)
        {
            double val = (q.Longitude - p.Longitude) * (r.Latitude - q.Latitude)
                         - (q.Latitude - p.Latitude) * (r.Longitude - q.Longitude);
            if (val == 0)
                return PolygonOrientation.Colinear;
            return (val > 0) ? PolygonOrientation.Clockwise : PolygonOrientation.CounterClockwise;
        }

        public Boolean Intersection(Position p1, Position q1, Position p2, Position q2)
        {
            PolygonOrientation o1 = Orientation(p1, q1, p2),
                               o2 = Orientation(p1, q1, q2),
                               o3 = Orientation(p2, q2, p1),
                               o4 = Orientation(p2, q2, q1);
            // General case
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }
            // Special cases
            // p1, q1, and p2 are colinear and p2 lies on segment p1q1
            if (o1 == PolygonOrientation.Colinear
                && OnSegment(p1, p2, q1))
            {
                return true;
            }
            // p1, q1, and p2 are colinear and q2 lies on segment p1q1
            if (o2 == PolygonOrientation.Colinear
                && OnSegment(p1, q2, q1))
            {
                return true;
            }
            // p2, q2, and p1 are colinear and p1 lies on segment p2q2
            if (o3 == PolygonOrientation.Colinear
                && OnSegment(p2, p1, q2))
            {
                return true;
            }
            // p2, q2, and q1 are colinear and q1 lies on segment p2q2
            if (o4 == PolygonOrientation.Colinear
                && OnSegment(p2, q1, q2))
            {
                return true;
            }
            return false;
        }

        public Boolean WithinTriangle(Position point, Position a, Position b, Position c)
        {
            const double INF = 1000;
            Position extreme = new Position(INF, point.Longitude);
            int count = 0,
                i = 0;
            // Check if the line segment from 'point' to 'extreme' intersects
            // with the line segment from 'a' to 'b'
            if (Intersection(a, b, point, extreme))
            {
                if(Orientation(a, point, b) == PolygonOrientation.Colinear)
                {
                    return OnSegment(a, point, b);
                }
                ++count;
            }

            // Same but from 'b' to 'c'
            if (Intersection(b, c, point, extreme))
            {
                if(Orientation(b, point, c) == PolygonOrientation.Colinear)
                {
                    return OnSegment(b, point, c);
                }
                ++count;
            }

            // Same but from 'c' to 'a'
            if (Intersection(c, a, point, extreme))
            {
                if(Orientation(c, point, a) == PolygonOrientation.Colinear)
                {
                    return OnSegment(c, point, a);
                }
                ++count;
            }
            // return true if count is odd.
            return count % 2 == 1;
        }

        public Boolean WithinRectangle(Position point, Position lowerLeft, Position upperRight)
        {
            if(point.Latitude >= lowerLeft.Latitude
               && point.Latitude < upperRight.Latitude)
            {
                if(point.Longitude <= upperRight.Longitude
                   && point.Longitude > lowerLeft.Longitude)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
