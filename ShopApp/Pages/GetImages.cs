using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAppUI.Pages
{
    public static class GetImages
    {
        public static IEnumerable<SelectListItem> GetImagesNames()
        {
            IEnumerable<SelectListItem> images = new List<SelectListItem>();
            var img = Directory.GetFiles("wwwroot/images");
            for (var i = 0; i < img.Length; i++)
                images = images.Append(new SelectListItem { Value = $"{Path.GetFileName(img[i])}", Text = $"{Path.GetFileName(img[i])}" });
            return images;
        }
    }
}
