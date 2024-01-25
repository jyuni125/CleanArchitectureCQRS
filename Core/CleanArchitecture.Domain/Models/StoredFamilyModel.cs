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
        
        public StoredFamilyModel(string Firstname,string Lastname,short Gender,int status)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Gender = (Gender)Gender;
            this.status = status;
        }
        
        
        public StoredFamilyModel(string Firstname, string Lastname, int status) //, string Gender
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
          //  this.Gender = (Gender)Enum.Parse(typeof(Gender), Gender,true);
            this.status = status;
        }
        

        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public Gender Gender { get;private set; }
        public int status { get;private set; }

        /*

        public Gender converttoGender(string data)
        {
            return (Gender)Enum.Parse(typeof(Gender), data);
            //return (int)(Gender)Enum.Parse(typeof(Gender), data);
           // return (Gender)(int)Enum.Parse(typeof(Gender), data);
        }
        */
    }

    
}
