using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Kursach.Class
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int? EmployeesId { get; set; }
        public virtual Employee Employees { get; set; }

        public ICollection<OrderInfo> OrderInfos { get; set; }
        public Order()
        {
            OrderInfos = new List<OrderInfo>();
        }
        public void SavePDF()
        {
            //тип накладной кирилицей (русский вариант)
            Order sourceList = this;

            //Объект документа пдф
            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            //определеяем ориентацию
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            //получаем путь сохранения, исходя из типа накладной 
            string fileName = Directory.GetCurrentDirectory() + "\\"+ "Заказ" + sourceList.Date.ToString().Replace(":", "-") + "_" + sourceList.Employees.Surname + sourceList.Employees.Name + ".pdf";//Замена ':' на '-' обусловленна тем, что название файла не может содержать ':'

            //if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\" + type))//создаем соответсвущую папку, если ее нет
            //{
            //    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + type);
            //}

            //Создаем объект записи пдф-документа в файл
            PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));

            //Открываем документ
            doc.Open();


            //Определение шрифта необходимо для сохранения кириллического текста
            //Иначе мы не увидим кириллический текст
            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
            PdfPTable table = new PdfPTable(new float[] { 7, 12, 7, 7,});

            //Создаем ячейки для форматирования вывода
            PdfPCell ProgramLabel = new PdfPCell(new Phrase("Магазин стрелкового оружия", new iTextSharp.text.Font(baseFont, 24)));
            PdfPCell Type = new PdfPCell(new Phrase("Заказ", new iTextSharp.text.Font(baseFont, 24)));
            

            //Ячейки пробелов
            PdfPCell dottedSpace = new PdfPCell(new Phrase("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new iTextSharp.text.Font(baseFont, 10)));
            PdfPCell cleanSpace = new PdfPCell(new Phrase("                                                                                                                                                                                                       ", new iTextSharp.text.Font(baseFont, 20)));


            //Устанавливаем занчение количества объединения колонок, для визуального форматирования
            ProgramLabel.Colspan = 4;
            Type.Colspan = 4;
            dottedSpace.Colspan = 8;
            cleanSpace.Colspan = 8;
           

            //Убираем границу ячеек что бы они выглядели как текст
            ProgramLabel.Border = 0;
            dottedSpace.Border = 0;
            Type.Border = 0;
            cleanSpace.Border = 0;
           

            //Выравнивание
           
            Type.HorizontalAlignment = Element.ALIGN_RIGHT;

            //добваляем необходимые элементы в таблицу
            table.AddCell(dottedSpace);
            table.AddCell(ProgramLabel);
            table.AddCell(Type);
            table.AddCell(dottedSpace);

            string[] Headers = new string[] { "ID", "Наименование", "Цена", "Количество", };
            //Добавляем заголовки таблицы
            for (int j = 0; j < Headers.Length; j++)
            {
                ProgramLabel = new PdfPCell(new Phrase(new Phrase(Headers[j], new iTextSharp.text.Font(font.BaseFont, 10))));

                //Фоновый цвет
                ProgramLabel.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(ProgramLabel);
            }


            //Добавляем в таблицу сами продукты
            foreach (OrderInfo product in sourceList.OrderInfos)
            {
                string name;
                if (product.Goods.Accessories == null)
                    name = product.Goods.Weapon.CodeName;
                else
                    name = product.Goods.Accessories.Name;
                //Каждая добавляемая ячейка - свойство товара
                table.AddCell(new Phrase(string.Format("{0:c}", product.Id), new iTextSharp.text.Font(font.BaseFont, 10)));
                table.AddCell(new Phrase(name, new iTextSharp.text.Font(font.BaseFont, 10)));
                table.AddCell(new Phrase(string.Format("{0:c}", product.Goods.SellPrice), new iTextSharp.text.Font(font.BaseFont, 10)));
                table.AddCell(new Phrase(string.Format("{0:n}", product.Count), new iTextSharp.text.Font(font.BaseFont, 10)));
            }
            table.AddCell(dottedSpace);
            table.AddCell(cleanSpace);

            //Если накладная имеет соответствующий тип, то документ должен иметь дополнительные поля получателя и отправителя

            PdfPCell receiver = new PdfPCell(new Phrase("Составил :", new iTextSharp.text.Font(baseFont, 12)));
            PdfPCell receiverName = new PdfPCell(new Phrase(sourceList.Employees.Surname + " " + sourceList.Employees.Name, new iTextSharp.text.Font(baseFont, 12)));
           
            receiver.Border = 0;
           
            receiverName.Border = 0;
         



           
            receiver.Colspan = 2;
          
            receiverName.Colspan = 4;

            table.AddCell(receiver);
            table.AddCell(receiverName);

            table.AddCell(cleanSpace);
           



            //Ячейки с датой
            PdfPCell DateWord = new PdfPCell(new Phrase("Дата", new iTextSharp.text.Font(baseFont, 10)));
            PdfPCell datecell = new PdfPCell(new Phrase(sourceList.Date.ToString(), new iTextSharp.text.Font(baseFont, 10)));

            datecell.HorizontalAlignment = Element.ALIGN_RIGHT;

            datecell.Border = 0;
            DateWord.Border = 0;

            DateWord.Colspan = 4;
            datecell.Colspan = 4;

            table.AddCell(cleanSpace);
            table.AddCell(DateWord);
            table.AddCell(datecell);

            //Добавляем таблицу в документ
            doc.Add(table);

            //Закрываем документ
            doc.Close();
            System.Diagnostics.Process.Start(fileName);
            //MessageBox.Show("Накладная сохранена по адрессу\n" + fileName, "Отчет сохранен", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
