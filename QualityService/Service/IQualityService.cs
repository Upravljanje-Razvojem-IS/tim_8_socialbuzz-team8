using QualityService.Models;
using QualityService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityService.Service
{
    public interface IQualityService
    {
        public List<Feedback> ListAll();
        public Feedback? GetById(int Id);
        public Feedback? Create(FeedbackDTO feedbackDTO);
        public Feedback? Update(Feedback feedback, FeedbackDTO feedbackDTO);
        public bool Delete(Feedback feedback);
    }
}
