using QualityService.Models;
using QualityService.Models.DTO;
using System;
using System.Collections.Generic;
using QualityService.Context;
using System.Linq;
using System.Threading.Tasks;

namespace QualityService.Service
{
    public class QualityService : IQualityService
    {
        private readonly QualityContext _qualityContext;

        public QualityService(QualityContext qualityContext)
        {
            this._qualityContext = qualityContext;
        }

        public Feedback? Create(FeedbackDTO feedbackDTO)
        {
            var newFeedback = new Feedback()
            {

                UserId = feedbackDTO.UserId,
                ProductId = feedbackDTO.ProductId,
                IsPositive = feedbackDTO.IsPositive,
                IsSelling = feedbackDTO.IsSelling,
                Comment = feedbackDTO.Comment,
                Response = null
            };

            this._qualityContext.Feedback.Add(newFeedback);

            return this._qualityContext.SaveChanges() >= 1 ? newFeedback : null;
        }

        public bool Delete(Feedback feedback)
        {
            this._qualityContext.Feedback.Remove(feedback);
            return this._qualityContext.SaveChanges() >= 1;
        }

        public Feedback? GetById(int Id)
        {
            return this._qualityContext.Feedback.Find(Id);
        }

        public List<Feedback> ListAll()
        {
            return this._qualityContext.Feedback.ToList();
        }

        public Feedback? Update(Feedback feedback, FeedbackDTO feedbackDTO)
        {
            feedback.IsSelling = feedbackDTO.IsSelling;
            feedback.IsPositive = feedbackDTO.IsPositive;
            feedback.Comment = feedbackDTO.Comment;
            feedback.Response = feedbackDTO.Response;

            this._qualityContext.Feedback.Update(feedback);

            return this._qualityContext.SaveChanges() >= 1 ? feedback : null;
        }
    }
}
