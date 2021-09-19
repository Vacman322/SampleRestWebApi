using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRestWebApi.Domain
{
    public class ClientTag
    {
        [Key]
        public int Id { get; set; }

        public int ClientID { get; set; }
        [ForeignKey(nameof(ClientID))]
        public virtual Client Client { get; set; }

        public int TagID { get; set; }
        [ForeignKey(nameof(TagID))]
        public virtual Tag Tag { get; set; }
    }
}
