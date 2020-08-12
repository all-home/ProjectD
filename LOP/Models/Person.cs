using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOP.Models
{
    public class Person
    {
        public int id { get; set; }
        public int TagId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Tel { get; set; }
        public string Position { get; set; }
        public int ImageID { get; set; }
    }

    //class contain Person stat
    public class Statistics
    { 
        [Key]
        [ForeignKey("Person")]
        public DateTime StartWork { get; set; }  
        public DateTime EndWork { get; set; }
        public Person Person { get; set; }
    
    }
}
