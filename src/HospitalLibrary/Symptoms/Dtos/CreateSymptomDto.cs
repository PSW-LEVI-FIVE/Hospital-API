using HospitalLibrary.Examination;

namespace HospitalLibrary.Symptoms.Dtos
{
    public class CreateSymptomDto
    {
        public string Name { get; set; }


        public Symptom MapToModel()
        {
            return new Symptom()
            {
                Name = Name
            };
        }
    }
}