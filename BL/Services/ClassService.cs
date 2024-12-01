using BL.Interfaces;
using Domains.Dtos;
using Domains.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class ClassService : IClassService
    {
        private readonly ITrainerService _trainerService;

        // Mock database context (replace with your actual DbContext)
        private static List<Class> _classes = new List<Class>
        {
            new Class
            {
                ClassId = 1,
                ClassName = "Yoga",
                Schdule = DateTime.Now.AddHours(2),
                Capacity = 20,
                TrainerId = 1,
                Trainer = new Trainer { TrainerId = 1, TrainerName = "Alice", Specialization = "Yoga" }
            },
            new Class
            {
                ClassId = 2,
                ClassName = "Pilates",
                Schdule = DateTime.Now.AddDays(1),
                Capacity = 15,
                TrainerId = 2,
                Trainer = new Trainer { TrainerId = 2, TrainerName = "Bob", Specialization = "Pilates" }
            }
        };

        public ClassService(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public IEnumerable<ClassDTO> GetAll()
        {
            // Map `Class` entities to `ClassDTO`
            return _classes.Select(c => new ClassDTO
            {
                ClassId = c.ClassId,
                ClassName = c.ClassName,
                Schdule = c.Schdule,
                Capacity = c.Capacity,
                TrainerId = c.TrainerId,
                Trainer = c.Trainer != null
                    ? new TrainerDTO
                    {
                        TrainerId = c.Trainer.TrainerId,
                        TrainerName = c.Trainer.TrainerName,
                        Specialization = c.Trainer.Specialization
                    }
                    : null
            });
        }

        public ClassDTO GetById(int id)
        {
            var classEntity = _classes.FirstOrDefault(c => c.ClassId == id);
            if (classEntity == null) return null!;

            return new ClassDTO
            {
                ClassId = classEntity.ClassId,
                ClassName = classEntity.ClassName,
                Schdule = classEntity.Schdule,
                Capacity = classEntity.Capacity,
                TrainerId = classEntity.TrainerId,
                Trainer = classEntity.Trainer != null
                    ? new TrainerDTO
                    {
                        TrainerId = classEntity.Trainer.TrainerId,
                        TrainerName = classEntity.Trainer.TrainerName,
                        Specialization = classEntity.Trainer.Specialization
                    }
                    : null
            };
        }

        public void Create(ClassDTO classDto)
        {
            // Map `ClassDTO` to `Class` entity
            var classEntity = new Class
            {
                ClassId = _classes.Max(c => c.ClassId) + 1,
                ClassName = classDto.ClassName,
                Schdule = classDto.Schdule,
                Capacity = classDto.Capacity,
                TrainerId = classDto.TrainerId,
                Trainer = _trainerService.GetById(classDto.TrainerId) != null
                    ? new Trainer
                    {
                        TrainerId = classDto.TrainerId,
                        TrainerName = classDto.Trainer?.TrainerName ?? "",
                        Specialization = classDto.Trainer?.Specialization ?? ""
                    }
                    : null
            };

            _classes.Add(classEntity);
        }

        public void Update(ClassDTO classDto)
        {
            var existingClass = _classes.FirstOrDefault(c => c.ClassId == classDto.ClassId);
            if (existingClass == null) return;

            // Update properties
            existingClass.ClassName = classDto.ClassName;
            existingClass.Schdule = classDto.Schdule;
            existingClass.Capacity = classDto.Capacity;
            existingClass.TrainerId = classDto.TrainerId;

            // Update related trainer (if any)
            existingClass.Trainer = _trainerService.GetById(classDto.TrainerId) != null
                ? new Trainer
                {
                    TrainerId = classDto.TrainerId,
                    TrainerName = classDto.Trainer?.TrainerName ?? "",
                    Specialization = classDto.Trainer?.Specialization ?? ""
                }
                : null;
        }

        public void Delete(int id)
        {
            var classEntity = _classes.FirstOrDefault(c => c.ClassId == id);
            if (classEntity != null)
            {
                _classes.Remove(classEntity);
            }
        }
    }
}
