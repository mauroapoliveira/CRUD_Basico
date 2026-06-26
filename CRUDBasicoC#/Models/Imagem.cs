using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CRUDBasico.Models
{
    public class Imagem
    {
        public string NomeArquivo { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public void Salvar(Imagem imagem)
        {
            string fileName = Path.GetFileNameWithoutExtension(imagem.ImageFile.FileName);
            string extension = Path.GetExtension(imagem.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imagem.NomeArquivo = "Imagem/" + fileName;
            fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagem/"), fileName);
            imagem.ImageFile.SaveAs(fileName);
        }
    }
}