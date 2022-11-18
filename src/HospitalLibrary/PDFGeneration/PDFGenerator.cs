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
        private const int THERAPY_LABEL_WIDTH = 504;
        private const int THERAPY_LABEL_HEIGHT = 20;
        private const int THERAPIES_HEADER_HEIGHT = 100;
        private const int HEADER_FONT_SIZE = 18;
        private const int LABEL_FONT_SIZE = 14;
        public byte[] GenerateTherapyPdf(Hospitalization hospitalization)
        {
            List<Therapy> therapies = hospitalization.Therapies;
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);
            
            GenerateHeaderForTherapies(page, hospitalization.MedicalRecord.Patient);
            GenerateTherapiesList(page, therapies);
            return document.Draw();
        }

        private void GenerateTherapiesList(Page page, List<Therapy> therapies)
        {
            var y = 60;
            var x = 0;
            foreach (var therapy in therapies)
            {
                GenerateTherapyListRow(page, therapy, x, y);
                y += 20;
            }
        }

        private void GenerateTherapyListRow(Page page, Therapy therapy, int x, int y)
        {
            string txt = GenerateTextByTherapyType(therapy);
            Label lbl = new Label(txt, x, y, THERAPY_LABEL_WIDTH, THERAPY_LABEL_HEIGHT, Font.Helvetica, LABEL_FONT_SIZE, TextAlign.Left);
            page.Elements.Add(lbl);
        }

        private void GenerateHeaderForTherapies(Page page, Patient patient)
        {
            var labelText = $"Therapies({patient.Name} {patient.Surname})";
            var label = new Label(labelText, 0, 0, THERAPY_LABEL_WIDTH, THERAPIES_HEADER_HEIGHT, Font.Helvetica, HEADER_FONT_SIZE, TextAlign.Center);
            page.Elements.Add(label);
        }

        private string GenerateTextByTherapyType(Therapy therapy)
        {
            if (therapy is BloodTherapy bt)
            {
                return $"Blood therapy given at {bt.GivenAt} BloodType: {bt.BloodType} Quantity: {bt.Quantity}";
            }
            var mt = therapy as MedicineTherapy;
            return $"Medicine therapy given at {mt.GivenAt} Medicine: {mt.MedicineId} Quantity: {mt.Quantity}";
        }
    }
}