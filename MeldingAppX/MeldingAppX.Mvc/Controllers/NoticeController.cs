using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using MeldingAppX.Models;
using MeldingAppX.Mvc.Models;

namespace MeldingAppX.Mvc.Controllers
{
    public class NoticeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1101");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/Notice/");

                if (response.IsSuccessStatusCode)
                {
                    var notices = await response.Content.ReadAsAsync<IEnumerable<Notice>>();
                    return View(notices);
                }
            }

            return View();
        }

        public ActionResult Create()
        {
            var form = new NoticeForm
            {
                Buildings = _buildings,
                Categories = _categories
            };

            return View(form);
        }

        private readonly IEnumerable<SelectListItem> _buildings = new []
        {
            new SelectListItem{Text = "Selecteer..", Value = "0", Selected = true},
            new SelectListItem{Text = "Buiten", Value = "1"},
            new SelectListItem{Text = "Azzurro", Value = "2"},
            new SelectListItem{Text = "Romboutslaan", Value = "3"}, 
            new SelectListItem{Text = "Syndion", Value = "4"},
            new SelectListItem{Text = "Samenwerkingsgebouw", Value = "5"},
            new SelectListItem{Text = "Drechtsteden College", Value = "6"},
            new SelectListItem{Text = "Appartementen", Value = "7"},
            new SelectListItem{Text = "Brandweerkazerne", Value = "8"},
            new SelectListItem{Text = "Lilla", Value = "9"},
            new SelectListItem{Text = "Marrone", Value = "10"},
            new SelectListItem{Text = "Rosa", Value = "11"},
            new SelectListItem{Text = "Verde", Value = "12"},
            new SelectListItem{Text = "Giallo", Value = "13"},
            new SelectListItem{Text = "Indaco", Value = "14"},
            new SelectListItem{Text = "Bianco", Value = "15"},
            new SelectListItem{Text = "Orca", Value = "16"},
            new SelectListItem{Text = "Arcobaleno", Value = "17"},
            new SelectListItem{Text = "Celeste", Value = "18"},
            new SelectListItem{Text = "Duurzaamheidsfabriek", Value = "19"},
            new SelectListItem{Text = "Betaalde parkeerplaats", Value = "20"},
            new SelectListItem{Text = "Schippersinternaat", Value = "21"},
            new SelectListItem{Text = "Sporthal", Value = "22"},
            new SelectListItem{Text = "Tennisvereniging D.L.T.C", Value = "23"},
            new SelectListItem{Text = "Bogermanschool", Value = "24"},
            new SelectListItem{Text = "Wartburg College", Value = "25"},
            new SelectListItem{Text = "Villa Volta", Value = "26"},
            new SelectListItem{Text = "Eljakim", Value = "27"},
            new SelectListItem{Text = "Jubal", Value = "28"},
        };

        private readonly IEnumerable<SelectListItem> _categories = new[]
        {
            new SelectListItem{Text = "EHBO", Value = "1"},
            new SelectListItem{Text = "Vechtpartij", Value = "2"},
            new SelectListItem{Text = "Drugs", Value = "3"},
            new SelectListItem{Text = "Diefstal", Value = "4"},
            new SelectListItem{Text = "Overig", Value = "5"}
        };

    }
}