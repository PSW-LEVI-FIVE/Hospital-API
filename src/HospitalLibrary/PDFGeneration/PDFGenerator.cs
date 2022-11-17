using System.Collections.Generic;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Patients;
using HospitalLibrary.Therapies.Model;

namespace HospitalLibrary.PDFGeneration
{
    public class PdfGenerator: IPDFGenerator
    {
        public byte[] GenerateTherapyPdf(Hospitalization hospitalization, Patient patient)
        {
            List<Therapy> therapies = hospitalization.Therapies;
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            string labelText = "Therapies(" + patient.Name + " " + patient.Surname + ")";
            Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            var starty = 60;
            foreach (Therapy therapy in therapies)
            {
                string txt = GenerateTextByTherapyType(therapy);
                Label lbl = new Label(txt, 0, starty, 504, 20, Font.Helvetica, 14, TextAlign.Left);
                page.Elements.Add(lbl);
                starty += 20;
            }

            return document.Draw();
        }


        private string GenerateTextByTherapyType(Therapy therapy)
        {
            if (therapy is BloodTherapy)
            {
                BloodTherapy bt = therapy as BloodTherapy;
                return "Blood therapy given at " + bt.GivenAt + " BloodType: " + bt.BloodType + " Quantity: " + bt.Quantity;
            }
            MedicineTherapy mt = therapy as MedicineTherapy;
            return "Medicine therapy given at " + mt.GivenAt + " Medicine: " + mt.MedicineId + " Quantity: " + mt.Quantity;
        }
    }
}