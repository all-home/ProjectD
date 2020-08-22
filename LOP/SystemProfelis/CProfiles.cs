using LOP.SystemProfelis.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Threading.Tasks;

namespace LOP.SystemProfelis
{
    public class CProfiles
    {

        ProfileContext _context;

        public CProfiles(ProfileContext context)
        {
            _context = context;
        
        }

        // add new Profile
        public async void AddNewProfileAsync(
                                   String PName,
                                   DateTime WSart,
                                   DateTime WEnd,
                                   bool Act)
        {
            if (PName != null &&
                WSart != null &&
                WEnd != null)
            {
                try
                {
                    ProfileModel NProfile = new ProfileModel { 
                        Name = PName, 
                        WorkEndParam = WEnd, 
                        WorkStartParam = WSart };
                    await _context.Files.AddAsync(NProfile);
                    await _context.SaveChangesAsync();
                
                }
                catch 
                { }
            
            }
            
        
        }

        //Check for select
        public bool Selected(int? id)
        {
            if (id != null)
            {
                var CProfile = SProfileResult(id);

                if (CProfile != null)
                {
                    return true;
                }
                                        
            }


            return false;
        }

       //Edit by ID 
       public async void EditExitingProfileAasync(
            int? id,
            string Name = null,
            DateTime? WSart = null,
            DateTime? WEnd = null, 
            bool? Act = null)
        {
            if (id != null)
            {
                var SToEdit = await SProfileResult(id);

                if (SToEdit != null)
                {
                   RemoweProfileAsync(SToEdit.id);

                    if (Name != null)
                    {
                        SToEdit.Name = Name;
                    }
                    if (WSart != null)
                    {
                        SToEdit.WorkStartParam = WSart ?? default(DateTime);
                    }
                    if (WEnd != null)
                    {
                        SToEdit.WorkEndParam = WEnd ?? default(DateTime);
                    }
                    if (Act != null)
                    {
                        SToEdit.Active = Act ?? default(bool);
                    }
                    await _context.Files.AddAsync(SToEdit);
                    await _context.SaveChangesAsync();


                }

            }
           

        }

        //Delete by id
        public async void RemoweProfileAsync(int? id)
        {
            if (id != null)
            {
                var PToRemove = await SProfileResult(id);
                if (PToRemove != null)
                {
                    _context.Files.Remove(PToRemove);
                    await _context.SaveChangesAsync();

                }
            
            }

        
        }

        //Find by id 
        private Task<ProfileModel> SProfileResult(int? id)
        {
            if (id != null)
            {
                var SResProf = _context.Files
                       .FindAsync(id);

                if (SResProf != null)
                {
                    return SResProf;

                }
            }

            return null;

        }
        

    }
}
