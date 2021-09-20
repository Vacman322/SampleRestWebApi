using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRestWebApi.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class Clients
        {
            public const string GetAll = Base + "/clients";
            public const string Update = Base + "/clients/{clientId}";
            public const string Delete = Base + "/clients/{clientId}";
            public const string Get = Base + "/clients/{clientId}";
            public const string Create = Base + "/clients";
        }

        public static class Tags
        {
            public const string GetAll = Base + "/tags";
            public const string Update = Base + "/tags/{tagName}";
            public const string Delete = Base + "/tags/{tagName}";
            public const string Get = Base + "/tags/{tagName}";
            public const string Create = Base + "/tags";
        }

            public static class Identity
        {
            public const string Login = Base + "/Identity/login";
            public const string Register = Base + "/Identity/register";
            public const string Refresh = Base + "/Identity/refresh";

        }
    }
}
