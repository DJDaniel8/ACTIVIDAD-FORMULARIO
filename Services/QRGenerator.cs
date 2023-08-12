using iTextSharp.text.pdf;
using iTextSharp.text;
using MessagingToolkit.QRCode.Codec;
using MimeKit;
using System.Drawing;

namespace ACTIVIDAD_FORMULARIO.Services
{
    public class QRGenerator
    {
        public static void Generar(string texto, BodyBuilder bodyBuilder, string body)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode(texto);
            System.Drawing.Image Qr = (System.Drawing.Image)img;

            MemoryStream qrStream = new MemoryStream();

            Qr.Save(qrStream, System.Drawing.Imaging.ImageFormat.Png);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                iTextSharp.text.Image qrImage = iTextSharp.text.Image.GetInstance(qrStream.ToArray());
                doc.Add(qrImage);

                doc.Add(new Paragraph(body));

                doc.Close();
                byte[] pdfBytes = memoryStream.ToArray();

                var pdfAttachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(new MemoryStream(pdfBytes)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = "archivo.pdf"
                };
                
                bodyBuilder.Attachments.Add(pdfAttachment);
            }

        }
    }
}
