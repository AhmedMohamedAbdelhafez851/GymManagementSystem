//using Domains.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BL.Interfaces
//{
//    public interface IFaceRecognitionService
//    {
//        Task<int?> RecognizeCustomerAsync(string base64Image);
//        Task TrainFaceRecognitionModelAsync();
//        Task AddCustomerFaceSamplesAsync(int customerId, List<string> base64Images);
//    }

//    public interface IAttendanceService
//    {
//        Task<AttendanceDTO> RecordAttendanceAsync(string base64Image);
//        Task<List<AttendanceDTO>> GetTodayAttendanceAsync();
//        Task<List<AttendanceDTO>> GetAttendanceByDateRangeAsync(DateTime startDate, DateTime endDate);
//    }
//}
