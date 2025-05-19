using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D8M7Q2_SportEvents.Entities.Helpers;

namespace D8M7Q2_SportEvents.Entities
{
    public class Competitor: IIdEntity
    {
        public Competitor(string sportEventId, string name, string email, string phone)
        {
            Id = Guid.NewGuid().ToString();
            SportEventId = sportEventId;
            Name = name;
            Email = email;
            Phone = phone;
        }

        [StringLength(50)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(50)]
        public string SportEventId { get; set; }

        [NotMapped]
        public virtual SportEvent? SportEvent { get; set; }

        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }
    }
}
