using CleanArchitecture.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Models
{
    public class FamilyModel 
    {

        
        public FamilyModel(int status, string FirstName, string LastName, short Gender)
        {

            this.status = status;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = (Gender)Gender;

        }

        public FamilyModel(Guid id, string FirstName, string LastName, short Gender)
        {

            this.Id = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = (Gender)Gender;

        }

  

        public Guid Id { get; set; }
        public DateTime AddedDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public int status { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; set; }
        public Gender Gender { get; private set; }
    }
}
