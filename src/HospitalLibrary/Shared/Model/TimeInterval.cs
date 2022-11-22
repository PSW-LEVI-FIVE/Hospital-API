using System;
using System.Text.Json.Serialization;

namespace HospitalLibrary.Appointments
{
    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [JsonConstructor]
        public TimeInterval(DateTime Start, DateTime End)
        {
            this.Start = Start;
            this.End = End;
        }

        public TimeInterval(DateTime dateTime, TimeSpan timeSpanStart, TimeSpan timeSpanEnd)
        {
            Start = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeSpanStart.Hours,
                timeSpanStart.Minutes, timeSpanStart.Seconds);
            End = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeSpanEnd.Hours,
                timeSpanEnd.Minutes, timeSpanEnd.Seconds);
        }

        public TimeInterval(TimeInterval interval)
        {
            this.Start = interval.Start;
            this.End = interval.End;
        }

        public bool IsOverlaping(TimeInterval interval)
        {
            return IsIntervalInside(interval, this)
                   || IsIntervalInside(this, interval)
                   || IsDateInsideInterval(this, interval.Start)
                   || IsDateInsideInterval(this, interval.End);
        }

        public bool IsLongerThanDuration(double duration)
        {
            return (this.End - this.Start).TotalHours >= duration;
        }

        public static bool IsIntervalInside(TimeInterval outside, TimeInterval inside)
        {
            return outside.Start <= inside.Start && outside.End >= inside.End;
        }

        private bool IsDateInsideInterval(TimeInterval interval, DateTime date)
        {
            return interval.Start <= date && interval.End >= date;
        }

        public bool IsSameOrNextDate(TimeInterval timeInterval)
        {
            return End.CompareTo(timeInterval.Start) == 0 || End.AddDays(1).CompareTo(timeInterval.Start) == 0;
        }

        public bool IsThereGapInDates(TimeInterval newInterval)
        {
            return End.AddDays(1).CompareTo(newInterval.Start) < 0;
        }

        public bool IsThereGapInIntervals(TimeInterval newInterval)
        {
            return End.CompareTo(newInterval.Start) < 0;
        }

        public bool IntervalsTouching(TimeInterval newInterval)
        {
            return End.CompareTo(newInterval.Start) == 0;
        }
        public int IntervalDurationInMinutes()
        {
            return (int)(this.End.Subtract(this.Start)).TotalMinutes;
        }
        public int NumberOfTimespans(TimeSpan duration)
        {
            return (int)(this.IntervalDurationInMinutes()/duration.TotalMinutes);
        }

        public string ToDateString()
        {
            return Start.ToString("MMMM dd, yyyy") + " - " + End.ToString("MMMM dd, yyyy");
        }

        
        public override string ToString()
        {
            return Start + " - " + End;
        }
    }
}