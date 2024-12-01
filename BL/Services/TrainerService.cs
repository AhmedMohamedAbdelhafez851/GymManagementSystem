using BL.Data;
using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ApplicationDbContext _context;

        public TrainerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TrainerDTO> GetAll()
        {
            return _context.Trainers.Select(t => new TrainerDTO
            {
                TrainerId = t.TrainerId,
                TrainerName = t.TrainerName,
                Specialization = t.Specialization
            }).ToList();
        }

        public TrainerDTO GetById(int id)
        {
            var trainer = _context.Trainers.Find(id);
            if (trainer == null) return null!;
            return new TrainerDTO
            {
                TrainerId = trainer.TrainerId,
                TrainerName = trainer.TrainerName,
                Specialization = trainer.Specialization
            };
        }

        public void Create(TrainerDTO trainerDto)
        {
            var trainer = new Trainer
            {
                TrainerName = trainerDto.TrainerName,
                Specialization = trainerDto.Specialization
            };
            _context.Trainers.Add(trainer);
            _context.SaveChanges();
        }

        public void Update(TrainerDTO trainerDto)
        {
            var trainer = _context.Trainers.Find(trainerDto.TrainerId);
            if (trainer == null) return;

            trainer.TrainerName = trainerDto.TrainerName;
            trainer.Specialization = trainerDto.Specialization;

            _context.Trainers.Update(trainer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var trainer = _context.Trainers.Find(id);
            if (trainer == null) return;

            _context.Trainers.Remove(trainer);
            _context.SaveChanges();
        }
    }
}
