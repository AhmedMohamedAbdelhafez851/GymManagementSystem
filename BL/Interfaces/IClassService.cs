using Domains.Dtos;

namespace BL.Interfaces
{
    public interface IClassService
    {
        IEnumerable<ClassDTO> GetAll();
        ClassDTO GetById(int id);
        void Create(ClassDTO classDto);
        void Update(ClassDTO classDto);
        void Delete(int id);
    }
}
