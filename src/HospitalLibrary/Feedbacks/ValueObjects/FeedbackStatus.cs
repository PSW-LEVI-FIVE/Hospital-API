using System;
using HospitalLibrary.Feedbacks.Dtos;
using HospitalLibrary.Shared.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Feedbacks.ValueObjects
{
    [Owned]
    public class FeedbackStatus: ValueObject<FeedbackStatus>
    {
        public Boolean AllowPublishment { get;private set; }
        public Boolean Published { get;private set; }
        public Boolean Anonimity { get;private set; }

        public FeedbackStatus(bool allowPublishment, bool published, bool anonimity)
        {
            AllowPublishment = allowPublishment;
            Published = published;
            Anonimity = anonimity;
            Validate();
        }

        public FeedbackStatus(FeedbackStatus feedbackStatus)
        {
            AllowPublishment = feedbackStatus.AllowPublishment;
            Published = feedbackStatus.Published;
            Anonimity = feedbackStatus.Anonimity;
            Validate();
        }
        public FeedbackStatus(FeedbackStatusDTO feedbackStatus)
        {
            AllowPublishment = feedbackStatus.AllowPublishment;
            Published = feedbackStatus.Published;
            Anonimity = feedbackStatus.Anonimity;
            Validate();
        }

        public bool GetPublished()
        {
            FeedbackStatus statusCopy = (FeedbackStatus)this.MemberwiseClone();
            return statusCopy.Published;
        }
        public bool GetAnonymity()
        {
            FeedbackStatus statusCopy = (FeedbackStatus)this.MemberwiseClone();
            return statusCopy.Anonimity;
        }

        public void Publish()
        {
            if(Published)
                throw new Exception("Feedback already published");
            Published = true;
            Validate();
        }
        public void Withdraw()
        {
            if(!Published)
                throw new Exception("Feedback isn't published");
            Published = false;
            Validate();
        }

        private void Validate()
        {
            if (!AllowPublishment && Published)
                throw new Exception("Publishing feedback denied");
        }

        protected override bool EqualsCore(FeedbackStatus other)
        {
            return base.Equals(other) &&
                   AllowPublishment == other.AllowPublishment &&
                   Published == other.Published &&
                   Anonimity == other.Anonimity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FeedbackStatus)obj);
        }

        protected bool Equals(FeedbackStatus other)
        {
            return base.Equals(other) && AllowPublishment == other.AllowPublishment && Published == other.Published && Anonimity == other.Anonimity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), AllowPublishment, Published, Anonimity);
        }

        protected override int GetHashCodeCore()
        {
            int hashCode = Anonimity.GetHashCode();
            hashCode = (hashCode * 397) ^ AllowPublishment.GetHashCode();
            hashCode = (hashCode * 397) ^ Published.GetHashCode();
            return hashCode;
        }
    }
}