using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MeldingAppX.Access;
using MeldingAppX.Models;
using MeldingAppX.Mvc.Models;

namespace MeldingAppX.Mvc.Controllers
{
    public class NoticeController : Controller
    {
        public NoticeController()
        {
            _proxy = new NoticeProxy("http://localhost:1101");
            _photoProxy = new PhotoProxy("http://localhost:1101");
        }

        public async Task<ActionResult> Index()
        {
            return View(await _proxy.GetNotice());
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

        [HttpPost]
        public async Task<ActionResult> Create(NoticeForm form, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                var notice = new Notice
                {
                    AdditionalLocation = form.AdditionalLocation,
                    Building = Int32.Parse(form.Building),
                    Category = Int32.Parse(form.Category),
                    Comment = form.Comment,
                    PhoneNumber = form.PhoneNumber,
                    ReporterName = form.ReporterName
                };

                await _proxy.PostNotice(notice);

                if (IsImage(photo.ContentType))
                {
                    // if photo is not lager than 2 MB
                    if (photo.ContentLength < 2000000)
                    {
                        var stream = new MemoryStream();
                        photo.InputStream.CopyTo(stream);
                        var photoBytes = stream.ToArray();
                        var base64Photo = Convert.ToBase64String(photoBytes);

                        var jsonPhoto = new Photo
                        {
                            ContentType = photo.ContentType,
                            ContentLength = photo.ContentLength,
                            EncodedFile = base64Photo,
                            FileName = photo.FileName,
                            Name = form.PhotoName
                        };

                        await _photoProxy.Post(jsonPhoto); 
                    }
                }

                return RedirectToAction("Index", "Notice");
            }

            form.Buildings = _buildings;
            form.Categories = _categories;

            return View(form);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _proxy.DeleteNotice(id);

            return RedirectToAction("Index", "Notice");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var notice = await _proxy.GetNotice(id);

            var form = new NoticeForm
            {
                Id = notice.Id,
                AdditionalLocation = notice.AdditionalLocation,
                Building = notice.Building.ToString(),
                Buildings = _buildings,
                Categories = _categories,
                Category = notice.Category.ToString(),
                Comment = notice.Comment,
                PhoneNumber = notice.PhoneNumber,
                ReporterName = notice.ReporterName
            };

            return View(form);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(NoticeForm form)
        {
            if (ModelState.IsValid)
            {
                var notice = new Notice
                {
                    Id = form.Id,
                    AdditionalLocation = form.AdditionalLocation,
                    Building = Int32.Parse(form.Building),
                    Category = Int32.Parse(form.Category),
                    Comment = form.Comment,
                    PhoneNumber = form.PhoneNumber,
                    ReporterName = form.ReporterName
                };


                await _proxy.PutNotice(form.Id, notice);

                return RedirectToAction("Index", "Notice");
            }

            form.Buildings = _buildings;
            form.Categories = _categories;

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
            new SelectListItem{Text = "Wartburg College", Value = "25"}
        };

        private readonly IEnumerable<SelectListItem> _categories = new[]
        {
            new SelectListItem{Text = "EHBO", Value = "1"},
            new SelectListItem{Text = "Vechtpartij", Value = "2"},
            new SelectListItem{Text = "Drugs", Value = "3"},
            new SelectListItem{Text = "Diefstal", Value = "4"},
            new SelectListItem{Text = "Overig", Value = "5"}
        };

        private NoticeProxy _proxy;
        private PhotoProxy _photoProxy;

        private bool IsImage(string contentType)
        {
            string[] allowedMime = {"image"};
            return allowedMime.Any(contentType.Contains);
        }
    }
}