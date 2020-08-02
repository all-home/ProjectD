using System;
using System.Threading.Tasks;
using LOP.FileUpload.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace LOP.FileUpload
{
    public class FileUploads
    {
        FileContext _context;
        IHostingEnvironment _appEnvironment;
        public FileUploads(FileContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        //Upload file to server method
        public async Task<int> UploadFileToServer(IFormFile uploadedFile, string type)
        {
            string path;
            string FileName = GetFilename();
            int id = 0;

            if (FileName != null)
            {
                if (type.Contains("avatar"))
                {
                    path = _appEnvironment.WebRootPath + "/Files/Avatars" + FileName;
                }
                else
                {
                    path = _appEnvironment.WebRootPath + "/Files/PeoplePhotos" + FileName;

                }

                if (uploadedFile != null)
                {
                    try
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        FileModel file = new FileModel { Name = FileName, Patch = path };
                        id = file.id;
                        _context.Files.Add(file);
                        _context.SaveChanges();
                    }
                    catch { }
                }
            }
            return id;
        }

        //Generate file name and check them
        private string GetFilename()
        {
            string FileName = null;
            do
            {
                FileName = String.Format("%s.%s", RandomStringUtils.RandomStringUtils.RandomAlphanumeric(8));

            }
            while (CheckName(FileName));

            // Check Name
            bool CheckName(string _FileName)
            {
                var FName = _context.Files.
                    FirstOrDefaultAsync(f => f.Name == _FileName);
                if (FName == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return FileName;
        }

        //find file adress by id
        public string GetFileAdress(int? id)
        {
         
            if (id != null)
            { 
                var PFile = _context.Files
                   .FindAsync(id);
                if (PFile != null)
                {
                    return PFile.Result.Patch;
                                       
                }
            }

            return null;
        }
           

    }
}        




