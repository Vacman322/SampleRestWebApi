using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRestWebApi.Contracts.V1.Requests
{
    public class UpdateClientRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<TagRequest> Tags { get; set; }
    }
}
