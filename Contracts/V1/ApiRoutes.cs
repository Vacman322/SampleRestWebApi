﻿using System;
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

        public static class Identity
        {
            public const string Login = "/Identity/login";
            public const string Register = "/Identity/register";
        }
    }
}
