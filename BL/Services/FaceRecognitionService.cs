//using Emgu.CV.Structure;
//using Emgu.CV;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BL.Data;
//using BL.Interfaces;

//namespace BL.Services
//{
//    public class FaceRecognitionService : IFaceRecognitionService
//    {
//        private readonly ApplicationDbContext _context;
//        private FaceRecognitionModel _recognitionModel;

//        public FaceRecognitionService(ApplicationDbContext context )
//        {
//            _context = context;
//        }

//        public async Task TrainFaceRecognitionModelAsync()
//        {
//            // Load all customer face samples
//            var customerFaceSamples = await _context.Customers
//                .SelectMany(c => c.FaceSamples.Select(fs => new { CustomerId = c.CustomerId, ImagePath = fs.ImagePath }))
//                .ToListAsync();

//            // Train model using ML library
//            _recognitionModel = await ML.TrainFaceRecognitionModel(customerFaceSamples);
//        }

//        public async Task<int?> RecognizeCustomerAsync(string base64Image)
//        {
//            // Convert base64 to image
//            var imageBytes = Convert.FromBase64String(base64Image);

//            // Detect and recognize face
//            var recognizedCustomerId = await _recognitionModel.RecognizeFace(imageBytes);

//            return recognizedCustomerId;
//        }

//        public async Task AddCustomerFaceSamplesAsync(int customerId, List<string> base64Images)
//        {
//            foreach (var base64Image in base64Images)
//            {
//                var imagePath = await SaveFaceSampleAsync(customerId, base64Image);

//                // Save to database
//                var faceSample = new FaceSample
//                {
//                    CustomerId = customerId,
//                    ImagePath = imagePath
//                };

//                _context.FaceSamples.Add(faceSample);
//            }

//            await _context.SaveChangesAsync();
//        }
//    }
//}
