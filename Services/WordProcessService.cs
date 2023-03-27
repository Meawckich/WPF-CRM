using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Interfaces;
using BuildingMaterials.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Word = Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace BuildingMaterials.Services
{
    public class WordProcessService : IDocHelper
    {
        private SqlServerDbContext _context;
        private ObservableCollection<OrderProductsDTO> _basket;
        public string Path { get; }
        public FileInfo FileInfo { get; }

        public WordProcessService(string path, SqlServerDbContext context, ObservableCollection<OrderProductsDTO> basket)
        {
            _basket= basket;
            _context = context;
            Path = path;
            if (File.Exists(Path))
            {
                FileInfo = new FileInfo(Path);
                Open();
            }
        }

        public void Open()
        {
            int iters = _basket.DistinctBy(x => x.ProviderTitle).Count();
            var order = _context.Orders.OrderBy(x => x.Id).Last();
            var prods = _context.Products.ToList();
            var user = _context.Users.Where(x => x.Id == order.UserId).FirstOrDefault();

            for (int i = 0; i < iters; i++)
            {
                var arr = new string[]
                {
                    order.Id.ToString(),
                    DateTime.Now.ToString("D") ,
                    _basket[i].ProviderTitle,
                    _basket[i].ProviderAddres,
                    _basket[i].ProviderInn,
                    _basket[i].ProductTitle,
                    DateTime.Now.ToString("D"),
                    user.FullName,
                    user.Adress,
                    user.FullName
                };

                var app = new Microsoft.Office.Interop.Word.Application();
                Object file = Path;
                Object missing = Type.Missing;

                Word.Document doc = null;
                try
                {

                    doc = app.Documents.Open(Path);
                    doc.Activate();
                    Word.Bookmarks vs = doc.Bookmarks;
                    Word.Range range;
                    int j = 0;
                    foreach (Word.Bookmark mark in vs)
                    {
                        range = mark.Range;
                        range.Text = arr[j];
                        j++;
                    }
                    doc.SaveAs2(@$"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}/СчетФактураНомер{order.Id}.docx");
                    doc.Close();

                    System.Diagnostics.Process.Start(@$"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}/СчетФактураНомер{order.ToString}.docx");
                    }
                catch(Exception ex)
                {

                }
            }

        }

        public void Precess()
        {
            throw new System.NotImplementedException();
        }
    }
}
