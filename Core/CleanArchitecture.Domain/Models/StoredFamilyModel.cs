using CleanArchitecture.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Models
{
    public class StoredFamilyModel
    {
        public StoredFamilyModel(string Firstname,string Lastname,string gender,int status)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Gender = gender;
            this.status = status;
        }


        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Gender { get;private set; }
        public int status { get;private set; }
    }
}
