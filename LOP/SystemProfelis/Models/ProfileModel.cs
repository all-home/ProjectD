using System;

namespace LOP.SystemProfelis.Models
{
    public class ProfileModel
    {
       public int id { get; set; }
       public string Name { get; set; }
       public DateTime WorkStartParam { get; set; }
       public DateTime WorkEndParam { get; set; }
       public bool Active { get; set; }
    }
}
