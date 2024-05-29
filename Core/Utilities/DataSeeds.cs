using Entities.Concrete.DBModels;
using Entities.Enums;

namespace Core.Utilities
{
    public class DataSeeds
    {

        public List<List<(string name, decimal r, decimal price)>> types = new List<List<(string name, decimal r, decimal price)>>
        {
            new List<(string name, decimal r, decimal price)>
            {
                ("Yalıtım ve Sıva Yok", 1.0M, 50M),
                ("Sadece Sıva", 2.0M, 70M),
                ("Taşyünü Yalıtım", 3.0M, 120M),
                ("Selüloz Yalıtım", 3.5M, 150M),
                ("Ekspande Polistiren (EPS) Yalıtım", 4.0M, 100M),
                ("Ekstrüde Polistiren (XPS) Yalıtım", 5.0M, 120M),
                ("Sprey Poliüretan Köpük Yalıtım", 6.0M, 250M),
                ("Poliüretan Köpük Yalıtım", 6.5M, 200M),
            },

            new List<(string name, decimal r, decimal price)>
            {
                ("Tek Cam Pencere", 0.8M, 500M),
                ("Çift Camlı Pencere", 1.2M, 800M),
                ("Düşük-E Kaplamalı Çift Camlı Pencere", 1.8M, 1200M),
                ("Üç Camlı Pencere", 2.0M, 1500M),
                ("Gaz Doldurulmuş Üç Camlı Pencere", 2.5M, 1800M),
            },

            new List<(string name, decimal r, decimal price)>
            {
                ("Ahşap Kapı", 0.5M, 700M),
                ("İzolasyonlu Metal Kapı", 1.0M, 900M),
                ("Çelik Kapı", 1.2M, 1300M),
                ("Fiberglass Kapı", 1.5M, 1200M),
                ("Vinil Kapı", 1.8M, 750M),
            },

            new List<(string name, decimal r, decimal price)>
            {
                ("Asfalt Shingle Çatı", 2M, 80M),
                ("Metal Çatı", 3M, 200M),
                ("Seramik Kiremit Çatı", 3.5M, 250M),
                ("İzolasyonlu Çatı", 4M, 300M),
                ("Yeşil Çatı", 5M, 600M),
            }
        };

        public List<Category> categories = new List<Category>
        {
            new Category { Id = 0, CategoryName = "İzolasyon" },
            new Category { Id = 0, CategoryName = "Pencere" },
            new Category { Id = 0, CategoryName = "Kapı" },
            new Category { Id = 0, CategoryName = "Çatı"},

        };
        public List<Premium> premiums = new()
        {
            new Premium { Id = 0,PremiumName="Full"},
        };
        public List<PremiumMode> premiumModes = new()
        {
            new PremiumMode { Id = 0,PremiumId=0, PremiumModel=PremiumModels.Daily},
            new PremiumMode { Id = 0,PremiumId=0, PremiumModel=PremiumModels.Monthly},
            new PremiumMode { Id = 0,PremiumId=0, PremiumModel=PremiumModels.Yearly},
        };
    }
}
