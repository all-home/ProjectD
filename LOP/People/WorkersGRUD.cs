using System.Threading.Tasks;
using LOP.People.Models;
using LOP.People.ModelsModels;
using Microsoft.AspNetCore.Http;
using LOP.FileUpload;

namespace LOP.People
{

    public class WorkersGRUD
    {
        WorkerContext _context;

        public WorkersGRUD(WorkerContext context)
        {
            _context = context;
        }

        //add new worker
        async public void AddNewWorker(
            string Name,
            string Surname,
            string Patronymic,
            string Tel,
            string Position,
            IFormFile Image,
            int? TagID
            )
        {
            if (
            Name != null &&
            Surname != null &&
            Tel != null &&
            Position != null &&
            Image != null &&
            TagID != null
            )
            {
                try
                {
                   
                    FileUploads GPictId = null;
                    int? ImageID = await GPictId.UploadFileToServer(Image, 2);
                    await Task.Run(() =>
                    {
                        Worker AWorker = new Worker
                        {
                            Name = Name,
                            Surname = Surname,
                            Patronymic = Patronymic,
                            Position = Position,
                            ImageID = ImageID ?? default(int),
                            TagId = TagID ?? default(int)

                        };

                         _context.Workers.Add(AWorker);
                         _context.SaveChanges();
                    });


                }
                catch { }

            }

        }

        public Worker FindById(int? id)
        {

            if (id != null)
            {
                var SResuWorker = _context.Workers.Find(id);

                if (SResuWorker != null)
                {
                    return SResuWorker;
                }

            }

            return null;
        }

        public async void RemoveWorker(int? id)
        {
            await Task.Run(() => {

                if (id != null)
                {
                    var DeleteWorker = FindById(id);
                    if (DeleteWorker != null)
                    {
                        _context.Workers.Remove(DeleteWorker);
                        _context.SaveChangesAsync();
                    }
                }


            });


        }

        public async void EditWorker(
            int? id,
            string Name,
            string Surname,
            string Patronymic,
            string Tel,
            string Position,
            IFormFile Image,
            int? TagID

            )
        {
            await Task.Run(async () => {

            var WorkerToEdit = FindById(id);

                if (WorkerToEdit != null)
                {
                    if (Name != null)
                    {
                        WorkerToEdit.Name = Name;
                    }
                    if (Surname != null)
                    {
                        WorkerToEdit.Surname = Surname;
                    }
                    if (Patronymic != null)
                    {
                        WorkerToEdit.Patronymic = Patronymic;
                    }
                    if (Tel != null)
                    {
                        WorkerToEdit.Tel = Tel;
                    }
                    if (Position != null)
                    {
                        WorkerToEdit.Position = Position;                    
                    }
                    if (Image != null)
                    {
                        FileUploads files= null;
                        files.DeleteFile(WorkerToEdit.ImageID);
                        WorkerToEdit.ImageID = (int)await files.UploadFileToServer(Image, 2);
                    }
                    if (TagID != null)
                    {
                        WorkerToEdit.TagId = TagID ?? default(int);
                    }

                }
            });
        }



    }
}
