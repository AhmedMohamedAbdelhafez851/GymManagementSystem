//using BL.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace GymManagementSystem.Controllers
//{
  
//    public class AttendanceController : Controller
//    {
//        private readonly IFaceRecognitionService _faceRecognitionService;
//        private readonly IAttendanceService _attendanceService;

//        public AttendanceController(
//            IFaceRecognitionService faceRecognitionService,
//            IAttendanceService attendanceService)
//        {
//            _faceRecognitionService = faceRecognitionService;
//            _attendanceService = attendanceService;
//        }

//        [HttpPost]
//        public async Task<IActionResult> MarkAttendance([FromBody] CaptureRequest request)
//        {
//            try
//            {
//                var customerId = await _faceRecognitionService.RecognizeCustomerAsync(request.Base64Image);

//                if (customerId == null)
//                {
//                    return Json(new
//                    {
//                        success = false,
//                        message = "Face not recognized. Please try again or register face samples."
//                    });
//                }

//                var attendance = await _attendanceService.RecordAttendanceAsync(customerId.Value, request.Base64Image);

//                return Json(new
//                {
//                    success = true,
//                    message = $"Attendance recorded for {attendance.Customer.FirstName} {attendance.Customer.SecondName}"
//                });
//            }
//            catch (Exception ex)
//            {
//                return Json(new
//                {
//                    success = false,
//                    message = "An error occurred while marking attendance."
//                });
//            }
//        }
//    }
//}
