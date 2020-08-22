using LOP.People.Models;
using LOP.People.ModelsModels;
using LOP.SystemProfelis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOP.People
{
    public class SearchWorkerById
    {
        WorkerContext _context;
        
        public SearchWorkerById(WorkerContext context)
        {
            _context = context;
        }

        // Find workers by reading TagId
        public async Task<Worker> SearchById(int? TagId)
        {
            Worker RWorker = null;
            if (TagId != null)
            {
                await Task.Run(()=> {

                    RWorker = GetWorker(TagId);

                    if (RWorker != null)
                    {
                        AddStat(RWorker);
                    }

                });
                return RWorker;
            }
            else
            {
                return RWorker;
            }
            

        }

        //method for search workers by tagId

        private Worker GetWorker(int? TagId)
        {
            if (TagId != null)
            {
                return _context.Workers.Find(TagId);
            }
            else {
                return null;
            }
        
        }

        private void AddStat(Worker Worker)
        {
           var StatCheckRes =  EntericeCheck(Worker);

            if (StatCheckRes != null)
            {
                _context.Stat.Remove(StatCheckRes);

                StatCheckRes.EndWork = DateTime.Now;
                _context.Stat.Add(StatCheckRes);
                _context.SaveChanges();

            }
            else
            {
                Statistics AddNewEnterice = new Statistics { Person = Worker, StartWork = DateTime.Now, Late = null, Overtime = null };
                _context.Stat.Add(AddNewEnterice);
                _context.SaveChanges();
            }
            
        }

        //check todey enterices
        private Statistics EntericeCheck(Worker Worker)
        {
            var CWStat = _context.Stat.Where(WStat => WStat.Person == Worker);
            if (CWStat != null)
            {
                var DEntCheck = CWStat.Where(W => W.StartWork.Date == DateTime.Today);
                if (DEntCheck != null)
                {
                    var DStatEt = DEntCheck.Where(W => W.EndWork == null);
                    if (DStatEt != null)
                    {
                        foreach(Statistics LStat in DStatEt)
                        {
                            return LStat;
                        }
                    }
                }
            }

            return null;
        }


    }
}
