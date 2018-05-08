using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi
{
    public static class Constants
    {
        public static readonly String ApplicationUserAccountTypeClaim = "AccountType";
        public static readonly String JwtSecretKey = "JWTSecretKey";
        public static readonly String BoundingBoxDelim = "x";
        public static readonly Char LayerDelimChar = '|';
        public static readonly String DefaultSQLConnectionStringKey = "DefaultConnection";
        public static readonly String MongoDbConnectionStringKey = "MongoDbConnection";
        public static readonly String ConnectionString = "Server=tcp:x.x.x.x,1433;Initial Catalog=initcat;Persist Security Info=False;User ID=user;Password=pass;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
