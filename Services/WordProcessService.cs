using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Interfaces;
using System.Diagnostics;
using BuildingMaterials.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Word = Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Globalization;

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
            string[] array = new string[6] { "<item1>", "<item2>", "<item3>" , "<item4>", "<item5>", "<item6>" };
            string[] array1 = new string[6] { "<c1>", "<c2>", "<c3>" , "<c4>", "<c5>", "<c6>" };

            int iters = _basket.DistinctBy(x => x.ProviderTitle).Count();
            var order = _context.Orders.OrderBy(x => x.Id).Last();
            var prods = _context.Products.ToList();
            var user = _context.Users.Where(x => x.Id == order.UserId).FirstOrDefault();

            for (int i = 0; i < iters; i++)
            {
                Dictionary<string,string> arr = new Dictionary<string, string>()
                {
                    {"<Number>",order.Id.ToString() },
                    {"<Date>", DateTime.Now.ToString("D") },
                    {"<ProviderFirst>", _basket[i].ProviderTitle },
                    {"<ProviderAdress>", _basket[i].ProviderAddres },
                    {"<Inn>", _basket[i].ProviderInn},
                    {"<FullName>" ,user.FullName },
                    {"<UserAdress>",user.Adress },
                   {"<FullPrice>", order.FullPrice.ToString() }
                };

                var app = new Microsoft.Office.Interop.Word.Application();
                FileInfo file = new FileInfo(Path);
                Object missing = Type.Missing;

                Word.Document doc = null;
                try
                {

                    doc = app.Documents.Open(file.FullName);
                    doc.Activate();
                    Word.Bookmarks vs = doc.Bookmarks;
                    foreach(var item in arr)
                    {
                        Word.Find find = app.Selection.Find;
                        find.Text = item.Key;
                        find.Replacement.Text = item.Value;

                        Object wrap = Word.WdFindWrap.wdFindContinue;
                        Object replace = Word.WdReplace.wdReplaceAll;

                        find.Execute(FindText: Type.Missing,
                            MatchCase: false,
                            MatchWholeWord: false,
                            MatchWildcards: false,
                            MatchAllWordForms: false,
                            MatchSoundsLike: missing,
                            Forward: true,
                            Wrap: wrap,
                            Format: false,
                            ReplaceWith: missing, Replace: replace);
                    }

                    for(int j =0; j< 6; j++)
                    {
                        Word.Find find = app.Selection.Find;
                        find.Text = array[j];
                        if( j+1 > _basket.Count)
                        {
                            find.Replacement.Text = "";
                        }
                        else
                        {
                            find.Replacement.Text = _basket[j].ProductTitle;
                        }
                       

                        Object wrap = Word.WdFindWrap.wdFindContinue;
                        Object replace = Word.WdReplace.wdReplaceAll;

                        find.Execute(FindText: Type.Missing,
                            MatchCase: false,
                            MatchWholeWord: false,
                            MatchWildcards: false,
                            MatchAllWordForms: false,
                            MatchSoundsLike: missing,
                            Forward: true,
                            Wrap: wrap,
                            Format: false,
                            ReplaceWith: missing, Replace: replace);
                    }

                    for (int j = 0; j < 6; j++)
                    {
                        Word.Find find = app.Selection.Find;
                        find.Text = array1[j];
                        if (j + 1 > _basket.Count)
                        {
                            find.Replacement.Text = "";
                        }
                        else
                        {
                            find.Replacement.Text = _basket[j].Counts.ToString();
                        }


                        Object wrap = Word.WdFindWrap.wdFindContinue;
                        Object replace = Word.WdReplace.wdReplaceAll;

                        find.Execute(FindText: Type.Missing,
                            MatchCase: false,
                            MatchWholeWord: false,
                            MatchWildcards: false,
                            MatchAllWordForms: false,
                            MatchSoundsLike: missing,
                            Forward: true,
                            Wrap: wrap,
                            Format: false,
                            ReplaceWith: missing, Replace: replace);
                    }

                    string fullpath = @$"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}{DateTime.Now.ToString("yyyyMMdd HHmmss")}.docx";
                    doc.SaveAs2(fullpath);
                    doc.Close();
                    app.Quit();

                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo(fullpath)
                    {
                        UseShellExecute = true
                    };
                    p.Start();

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
