using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Printing;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class PrintService
    {
        public byte[] GenerateImage(string sgmAdi, int number, string bmpLogoPath)
        {
            int width = 275;
            int height = 200;
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                // Çerçeveyi çiz
                Pen blackPen = new Pen(Color.Black, 6);
                g.DrawRectangle(blackPen, 0, 0, width - 1, height - 1);

                // BMP logoyu çiz
                Image logo = Image.FromFile(bmpLogoPath);
                g.DrawImage(logo, 5, 5, 90, 50);

                // SGM adını çiz
                Font sgmFont = new Font("Arial", 12, FontStyle.Bold);
                RectangleF sgmRect = new RectangleF(100, 5, width - 105, 50);
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(sgmAdi, sgmFont, Brushes.Black, sgmRect, format);

                // Yatay çizgileri çiz
                g.DrawLine(blackPen, 1, 60, width - 2, 60);
                g.DrawLine(blackPen, 1, 130, width - 2, 130);

                // Numarayı çiz
                Font numberFont = new Font("Arial", 60, FontStyle.Bold);
                SizeF numberSize = g.MeasureString(number.ToString(), numberFont);
                g.DrawString(number.ToString(), numberFont, Brushes.Black, new PointF((width - numberSize.Width) / 2, 50));

                // Zaman damgasını çiz
                Font timestampFont = new Font("Arial", 12, FontStyle.Bold);
                string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss dddd");
                SizeF timestampSize = g.MeasureString(timestamp, timestampFont);
                g.DrawString(timestamp, timestampFont, Brushes.Black, new PointF((width - timestampSize.Width) / 2, 155));
            }

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public void Print(byte[] imageBytes)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (sender, args) =>
            {
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(ms);
                    args.Graphics.DrawImage(image, new Point(0, 0));
                }
            };

            try
            {
                printDoc.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Yazdırma hatası: {ex.Message}");
            }
        }
    }
}
