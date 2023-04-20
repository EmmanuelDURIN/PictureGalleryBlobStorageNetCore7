using Microsoft.AspNetCore.Mvc;
using PictureGalleryBlobStorageNetCore.BlobStorage;
using PictureGalleryBlobStorageNetCore.ViewModels;

namespace PictureGalleryBlobStorageNetCore.Controllers
{
    public class PictureController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // TODO : obtenir la liste des images de BlobStorageManager
            var pictures = await new BlobStorageManager().GetPicturesAsync();
            return View(model: pictures);
        }
        // Action pour voir la page du formulaire
        public IActionResult Upload()
        {
            PictureUploadViewModel p = new PictureUploadViewModel
            {
                Date = DateTime.Now,
            };
            return View(model: p);
        }
        // Action d'envoi d'image
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Upload(PictureUploadViewModel pictureViewModel)
        {
            if (!ModelState.IsValid)
                return View();
            // TODO : utiliser BlobStorageManager pour sauvegarder l'image uploadée
            await new BlobStorageManager().SavePictureAsync(pictureViewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}