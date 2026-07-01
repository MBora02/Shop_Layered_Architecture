using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data.Data;
using Shop.Data.Migrations;
using Shop.Model;
using System.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Shop.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            var result=_context.Products.ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ExportToPdf()
        {
            // 1. Veri tabanından güncel listeyi çekin
            var products = _context.Products.ToList();

            // 2. QuestPDF ile PDF dökümanını tasarlayın
            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    // Üst Bilgi (Header)
                    page.Header()
                        .Text("Ürün Listesi Raporu")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    // İçerik (Tablo Oluşturma)
                    page.Content()
                        .PaddingTop(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            // Sütun genişliklerini tanımlayın
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50);  // ID sütunu genişliği
                                columns.RelativeColumn();    // Ürün adı sütunu (esnek)
                                columns.RelativeColumn();    // Ürün Kategori sütunu (esnek)
                                columns.RelativeColumn(); // fiyat sütunu genişliği
                                columns.RelativeColumn(); // stok sütunu genişliği
                                columns.RelativeColumn(); // kritik stok sütunu genişliği
                            });

                            // Tablo Başlıkları (Header Row)
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("ID").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Ürün Adı").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Kategori").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Fiyat").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Stok").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Kritik Stok").Bold();
                            });

                            // Veri Satırları (Döngü ile verileri basıyoruz)
                            foreach (var item in products)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.ProductId.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.ProductName);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Category);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.UnitPrice.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Stock.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.CriticalStockLevel.ToString());
                            }
                        });

                    // Alt Bilgi (Footer)
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sayfa ");
                            x.CurrentPageNumber();
                        });
                });
            });

            // 3. PDF'i byte dizisine çevirip tarayıcıya indirtme
            var pdfBytes = pdfDocument.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Kategori_Listesi_{DateTime.Now:yyyyMMdd}.pdf");
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Backend softito");

            // 2. Veri tabanından güncel listenizi çekin
            var products = _context.Products.ToList();

            // 3. Bellekte (Memory) boş bir Excel dosyası oluşturun
            using (var package = new ExcelPackage())
            {
                // Excel içinde "Ürün Listesi" adında bir sayfa aç
                var worksheet = package.Workbook.Worksheets.Add("Ürün Listesi");

                // 4. Tablo Başlıklarını Yazın (1. Satır)
                worksheet.Cells[1, 1].Value = "Ürün ID";
                worksheet.Cells[1, 2].Value = "Ürün Adı";
                worksheet.Cells[1, 3].Value = "Kategori";
                worksheet.Cells[1, 4].Value = "Fiyat";
                worksheet.Cells[1, 5].Value = "Stok";
                worksheet.Cells[1, 6].Value = "Kritik Stok";

                // 5. Başlık Satırını Şıklaştırın (Arka plan rengi, kalın yazı vb.)
                using (var range = worksheet.Cells[1, 1, 1, 6]) // 1. satır, 1'den 6. sütuna kadar seç
                {
                    range.Style.Font.Bold = true; // Yazıyı kalın yap
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(41, 128, 185)); // Mavi arka plan
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White); // Beyaz yazı rengi
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Ortala
                }

                // 6. Verileri Döngü ile Excel Satırlarına Basın
                int rowNumber = 2; // Veriler 2. satırdan başlayacak
                foreach (var item in products)
                {
                    worksheet.Cells[rowNumber, 1].Value = item.ProductId;
                    worksheet.Cells[rowNumber, 2].Value = item.ProductName;
                    worksheet.Cells[rowNumber, 3].Value = item.Category;
                    worksheet.Cells[rowNumber, 4].Value = item.UnitPrice;
                    worksheet.Cells[rowNumber, 5].Value = item.Stock;
                    worksheet.Cells[rowNumber, 6].Value = item.CriticalStockLevel;



                    rowNumber++;
                }



                //7.Sütun genişliklerini içeriğe göre otomatik ayarla

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // 8. Excel dosyasını byte dizisine çevirip tarayıcıya fırlat
                var fileBytes = package.GetAsByteArray();
                string fileName = $"Ürün_Listesi_{DateTime.Now:yyyyMMdd}.xlsx";

                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);







            }
        }
    }
}
