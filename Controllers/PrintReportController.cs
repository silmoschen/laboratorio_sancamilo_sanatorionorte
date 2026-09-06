using System.Collections.Generic;
using System.Web.Mvc;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using Microsoft.Reporting.WebForms;

namespace laboratoriobioquimico.Controllers
{
    public class PrintReportController : Controller
    {
        public ActionResult PrintReport(string id)
        {
            LocalReport localReport = new LocalReport();            
            localReport.ReportPath = Server.MapPath("/Report/" + id + ".rdlc");
            
            List<Reports> list = null;

            list = (List<Reports>) Session["__entitiesreport"];
            if (list != null)
            {
                ReportDataSource rdc = new ReportDataSource("DataSet", list);
                localReport.DataSources.Add(rdc);
            }

            string reportType = "pdf";
            string mimeType = "application/pdf";
            string encoding = string.Empty;
            string fileNameExtension = string.Empty;
            //The DeviceInfo settings should be changed based on the reportType             
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx             
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report             
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        public ActionResult ExportReportExcel(string id)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("/Report/" + id + ".rdlc");

            List<Reports> list = null;

            list = (List<Reports>)Session["__entitiesreport"];
            if (list != null)
            {
                ReportDataSource rdc = new ReportDataSource("DataSet", list);
                localReport.DataSources.Add(rdc);
            }

            string ContentType = "application/vnd.ms-excel";
            string reportType = "Excel";
            string FileType = reportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + FileType + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            return File(renderedBytes, ContentType, string.Format(id + ".{0}", fileNameExtension));
        }

        public ActionResult PrintReportPDF(string id, string p2)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("/Report/" + id + ".rdlc");

            List<Reports> list = null;

            list = (List<Reports>)Session["__entitiesreport"];
            if (list != null)
            {
                ReportDataSource rdc = new ReportDataSource("DataSet", list);
                localReport.DataSources.Add(rdc);
            }

            string reportType = "pdf";
            string mimeType = "application/pdf";
            string encoding = string.Empty;
            string fileNameExtension = string.Empty;
            //The DeviceInfo settings should be changed based on the reportType             
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx             
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report             
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            string pdfPath = Server.MapPath("/work/pdf/" + p2 + ".pdf");

            System.IO.FileStream pdfFile = new System.IO.FileStream(pdfPath, System.IO.FileMode.Create);
            pdfFile.Write(renderedBytes, 0, renderedBytes.Length);
            pdfFile.Close();

            return File(renderedBytes, mimeType);
        }

    }
}
