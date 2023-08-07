using MessagingToolkit.QRCode.Codec;
using MimeKit;
using System.Drawing;

namespace ACTIVIDAD_FORMULARIO.Services
{
    public class QRGenerator
    {
        public static void Generar(string texto, BodyBuilder bodyBuilder)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode(texto);
            System.Drawing.Image Qr = (System.Drawing.Image)img;


            MimePart qrAttachment;
            MemoryStream qrStream = new MemoryStream();

            Qr.Save(qrStream, System.Drawing.Imaging.ImageFormat.Png);

            qrAttachment = new MimePart("image", "png")
            {
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "codigo_qr.png"
            };

            qrAttachment.Content = new MimeContent(qrStream);

            bodyBuilder.Attachments.Add(qrAttachment);

        }
    }
}
