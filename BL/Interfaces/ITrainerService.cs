using Domains.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerDTO> GetAll();
        TrainerDTO GetById(int id);
        void Create(TrainerDTO trainerDto);
        void Update(TrainerDTO trainerDto);
        void Delete(int id);
    }
}
