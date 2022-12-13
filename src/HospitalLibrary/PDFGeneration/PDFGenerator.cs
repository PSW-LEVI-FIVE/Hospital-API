using System.Collections.Generic;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using HospitalLibrary.Examination;
using HospitalLibrary.Hospitalizations;
using HospitalLibrary.Patients;
using HospitalLibrary.Symptoms;
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
        
        public byte[] GenerateExaminationReportPdf(ExaminationReport examinationReport, Patient patient)
        {
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            GenerateHeaderForExaminationReports(page, patient);
            int y = GenerateSymptomsList(page, examinationReport.Symptoms);
            y = GenerateContent(page, examinationReport.Content, y);
            GeneratePrescriptionsList(page, examinationReport.Prescriptions, y);
            return document.Draw();
        }

        private int GenerateContent(Page page, string examinationReportContent, int y)
        {
            var x = 20;
            string header = "Content:";
            Label label = new Label(header, 0, y, THERAPY_LABEL_WIDTH, THERAPY_LABEL_HEIGHT, Font.Helvetica, LABEL_FONT_SIZE, TextAlign.Left);
            page.Elements.Add(label);
            y += 20;
            string txt = examinationReportContent;
            Label lbl = new Label(txt, x, y, THERAPY_LABEL_WIDTH, THERAPY_LABEL_HEIGHT, Font.Helvetica, LABEL_FONT_SIZE, TextAlign.Left);
            page.Elements.Add(lbl);
            y += 20;

            return y;
        }

        private void GeneratePrescriptionsList(Page page, List<Prescription> examinationReportPrescriptions, int y)
        {
            var x = 20;
            string header = "Prescriptions:";
            Label label = new Label(header, 0, y, THERAPY_LABEL_WIDTH, THERAPY_LABEL_HEIGHT, Font.Helvetica, LABEL_FONT_SIZE, TextAlign.Left);
            page.Elements.Add(label);
            y += 20;
            foreach (var prescription in examinationReportPrescriptions)
            {
                string txt = prescription.Medicine.Name;
                Label lbl = new Label(txt, x, y, THERAPY_LABEL_WIDTH, THERAPY_LABEL_HEIGHT, Font.Helvetica, LABEL_FONT_SIZE, TextAlign.Left);
                page.Elements.Add(lbl);
                y += 20;
            }
        }

        private int GenerateSymptomsList(Page page, List<Symptom> examinationReportSymptoms)
        {
            var y = 60;
            var x = 20;
            string header = "Symptoms:";
            Label label = new Label(header, 0, y, THERAPY_LABEL_WIDTH, THERAPY_LABEL_HEIGHT, Font.Helvetica, LABEL_FONT_SIZE, TextAlign.Left);
            page.Elements.Add(label);
            y += 20;
            foreach (var symptom in examinationReportSymptoms)
            {
                string txt = symptom.Name;
                Label lbl = new Label(txt, x, y, THERAPY_LABEL_WIDTH, THERAPY_LABEL_HEIGHT, Font.Helvetica, LABEL_FONT_SIZE, TextAlign.Left);
                page.Elements.Add(lbl);
                y += 20;
            }

            return y;
        }

        private void GenerateHeaderForExaminationReports(Page page, Patient patient)
        {
            var labelText = $"Examination report for patient: ({patient.Name} {patient.Surname})";
            var label = new Label(labelText, 0, 0, THERAPY_LABEL_WIDTH, THERAPIES_HEADER_HEIGHT, Font.Helvetica, HEADER_FONT_SIZE, TextAlign.Center);
            page.Elements.Add(label);
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