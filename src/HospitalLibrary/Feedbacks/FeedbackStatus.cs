using System.ComponentModel;

namespace HospitalLibrary.Feedbacks
{
    public enum FeedbackStatus
    {
        [Description("Private")]
        Private,
        [Description("Public")]
        Public
    }
}