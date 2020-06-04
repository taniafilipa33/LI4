﻿using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Configuration;
using System.IO;
using System.Web;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Specialized;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private Model model = new Model();
        private IHostingEnvironment _environment;

        public DateTime SelectedDate { get; set; }
        public DateTime SelectedDateTo { get; set; }

        public AdminController(IHostingEnvironment environment)
        {
            _environment = environment;
        }


        public ActionResult Index()
        {
            var local = (from x in model.Utilizador select x);
            List<Utilizador> res = local.ToList<Utilizador>();
            return View(res);
        }



        public ActionResult AlterarDadosUtilizador()
        {
            return View();
        }


        public ActionResult AdicionarCliente()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


        public IActionResult ErrorSearch()
        {
            return View();
        }


        public ActionResult Perfil()
        {
            string uti = Helpers.CacheController.utilizador;
            Administrador std = model.Administrador.Where(x => x.Email.Equals(uti)).FirstOrDefault();

            var vouchers = from alu in model.Voucher where (alu.IdUtilizador.Equals(uti)) select alu;
            /**
            List<Voucher> v = vouchers.ToList<Voucher>();

            foreach (Voucher a in v)
            {
                std.Voucher.Add(a);
            }
            */

            return View(std);
        }

        public ActionResult UploadDocument()
        {
            return View();
        }

        public ActionResult Details(int idArtigo)
        {
            Console.WriteLine(idArtigo);

            Artigo ss = model.Artigo.FirstOrDefault(x => x.IdArtigo.Equals(idArtigo));

            var cenas = ss.IdArtigo;
            Console.WriteLine(cenas);

            var comentarios = from x in model.Comentarios where (x.IdArtigo.Equals(idArtigo)) select x;
            List<Comentarios> c = comentarios.ToList<Comentarios>();

            foreach (Comentarios a in c)
            {
                ss.Comentarios.Add(a);
            }


            return View(ss);

        }

        public ActionResult verArtigos()
        {
            string user = Helpers.CacheController.utilizador;

            var artigos = (from m in model.Artigo where (m.IdDono == user) select m);
            List<Artigo> lista = artigos.ToList<Artigo>();

            return View(lista);
        }

        public ActionResult Alterar(int idArtigo)
        {

            Artigo ss = model.Artigo.FirstOrDefault(x => x.IdArtigo.Equals(idArtigo));

            Helpers.CacheController.IdArtigo = idArtigo;
            var cenas = ss.IdArtigo;
            Console.WriteLine(cenas);
            return View(ss);
        }

        [HttpPost]
        public ActionResult Password(string password)
        {
            string mail = Helpers.CacheController.utilizador;
            if (ModelState.IsValid)
            {

                Administrador std = model.Administrador.Where(x => x.Email.Equals(mail)).FirstOrDefault();

                Administrador u = model.Administrador.FirstOrDefault(x => x.Email.Equals(std.Email));
                u.Password = password;
                u.ContaBancaria = std.ContaBancaria;
                u.Password = MyHelpers.HashPassword(u.Password);
                model.SaveChanges();
            }

            return RedirectToAction("VerInfo", "Administrador");
        }

        /**
        * Permite vizualisar a view que permite alterar a conta Bancaria e efetua a sua mudança
        * */

        public ActionResult CBanc()
        {
            return View("CBanc");
        }

        public ActionResult Password()
        {
            return View("Password");
        }

        [HttpPost]
        public ActionResult CBanc(long conta)
        {
            string mail = Helpers.CacheController.utilizador;
            if (ModelState.IsValid)
            {

                Administrador std = model.Administrador.Where(x => x.Email.Equals(mail)).FirstOrDefault();

                Administrador u = model.Administrador.FirstOrDefault(x => x.Email.Equals(std.Email));
                u.Password = std.Password;
                u.ContaBancaria = conta;
                model.SaveChanges();
            }
            return RedirectToAction("VerInfo", "Administrador");
        }

        public ActionResult AlteraNome()
        {
            return View("AlteraNome");
        }

        [HttpPost]
        public ActionResult AlteraNome(string nome)
        {

            int id = Helpers.CacheController.IdArtigo;
            if (ModelState.IsValid)
            {

                Artigo std = model.Artigo.Where(x => x.IdArtigo.Equals(id)).FirstOrDefault();


                Artigo a = model.Artigo.FirstOrDefault(x => x.IdArtigo.Equals(std.IdArtigo));
                a.Nome = nome;
                a.Preco = std.Preco;
                a.Modo = std.Modo;
                a.Quantidade = std.Quantidade;
                a.Categoria = std.Categoria;
                a.Etiquetas = std.Etiquetas;
                a.Estado = std.Estado;
                a.Descricao = std.Descricao;
                a.Imagem = std.Imagem;
                model.SaveChanges();
            }
            return RedirectToAction("verArtigos", "Administrador");
        }

        public ActionResult AlteraCategoria()
        {
            return View("AlteraCategoria");
        }

        [HttpPost]
        public ActionResult AlteraCategoria(string categoria)
        {

            int id = Helpers.CacheController.IdArtigo;
            if (ModelState.IsValid)
            {

                Artigo std = model.Artigo.Where(x => x.IdArtigo.Equals(id)).FirstOrDefault();


                Artigo a = model.Artigo.FirstOrDefault(x => x.IdArtigo.Equals(std.IdArtigo));
                a.Nome = std.Nome;
                a.Preco = std.Preco;
                a.Modo = std.Modo;
                a.Quantidade = std.Quantidade;
                a.Categoria = categoria;
                a.Etiquetas = std.Etiquetas;
                a.Estado = std.Estado;
                a.Descricao = std.Descricao;
                a.Imagem = std.Imagem;
                model.SaveChanges();
            }
            return RedirectToAction("verArtigos", "Administrador");
        }

        public ActionResult AlteraDescricao()
        {
            return View("AlteraDescricao");
        }

        [HttpPost]
        public ActionResult AlteraDescricao(string descricao)
        {

            int id = Helpers.CacheController.IdArtigo;
            if (ModelState.IsValid)
            {

                Artigo std = model.Artigo.Where(x => x.IdArtigo.Equals(id)).FirstOrDefault();


                Artigo a = model.Artigo.FirstOrDefault(x => x.IdArtigo.Equals(std.IdArtigo));
                a.Nome = std.Nome;
                a.Preco = std.Preco;
                a.Modo = std.Modo;
                a.Quantidade = std.Quantidade;
                a.Categoria = std.Categoria;
                a.Etiquetas = std.Etiquetas;
                a.Estado = std.Estado;
                a.Descricao = descricao;
                a.Imagem = std.Imagem;
                model.SaveChanges();
            }
            return RedirectToAction("verArtigos", "Administrador");
        }

        public ActionResult AlteraEtiquetas()
        {
            return View("AlteraEtiquetas");
        }

        [HttpPost]
        public ActionResult AlteraEtiquetas(string etiquetas)
        {

            int id = Helpers.CacheController.IdArtigo;
            if (ModelState.IsValid)
            {

                Artigo std = model.Artigo.Where(x => x.IdArtigo.Equals(id)).FirstOrDefault();


                Artigo a = model.Artigo.FirstOrDefault(x => x.IdArtigo.Equals(std.IdArtigo));
                a.Nome = std.Nome;
                a.Preco = std.Preco;
                a.Modo = std.Modo;
                a.Quantidade = std.Quantidade;
                a.Categoria = std.Categoria;
                a.Etiquetas = etiquetas;
                a.Estado = std.Estado;
                a.Descricao = std.Descricao;
                a.Imagem = std.Imagem;
                model.SaveChanges();
            }
            return RedirectToAction("verArtigos", "Administrador");
        }

        [HttpPost]
        public ActionResult RemoverArtigo(int idArtigo)
        {
            if (ModelState.IsValid)
            {
                Artigo artigo = model.Artigo.FirstOrDefault(x => x.IdArtigo.Equals(idArtigo));

                if (artigo != null)
                {
                    model.Artigo.Remove(artigo);

                    model.SaveChanges();

                    /*
                    //Para adicionar uma denuncia ao dono do artigo visto que para remover um artigo o administrador tem que
                    //determinar que o artigo é inapropriado ou viola alguma regra.

                    Denuncias d = new Denuncias()
                    {
                    	IdDenuncia = 
                    }
                    */
                }
            }

            return RedirectToAction("VerArtigos", "Administrador");
        }

        public ActionResult RemoverUtilizador(string utilizador)
        {
            string user = Helpers.CacheController.utilizador;

            if (ModelState.IsValid)
            {
                Utilizador u = model.Utilizador.FirstOrDefault(x => x.Email.Equals(utilizador));

                if (u != null /*&& u.Administrador.Equals("admin@gmail.com")*/)
                {
                    model.Utilizador.Remove(u);

                    model.SaveChanges();
                }
            }

            return RedirectToAction("VerArtigos", "Administrador");
        }

        public ActionResult VerUtis()
        {
            var utilizadores = from x in model.Utilizador select x;
            List<Utilizador> lista = utilizadores.ToList<Utilizador>();
            foreach(Utilizador uti in lista)
            {
                var artigos = from x in model.Artigo where (x.IdDono.Equals(uti.Email)) select x;
                List<Artigo> art = artigos.ToList<Artigo>();
                foreach (Artigo g in art)
                    uti.Artigo.Add(g);
                var vendas = from x in model.Venda where (x.IdUtilizador.Equals(uti.Email)) select x;
                List<Venda> ven = vendas.ToList<Venda>();
                uti.Venda = ven;
                var Alugueres = from x in model.Aluguer where (x.IdUtilizador.Equals(uti.Email)) select x;
                List<Aluguer> ar = Alugueres.ToList<Aluguer>();
                uti.Aluguer = ar;

            }
            return View(lista);
        }

        public ActionResult ProcurarUtilizadoresCom3DenunciasOuMais()
        {
            var local = (from x in model.Utilizador select x);
            List<Utilizador> res = local.ToList<Utilizador>();

            Dictionary<Utilizador, double> mp = new Dictionary<Utilizador, double>();
            foreach (Utilizador u in res)
            {
                if (u.NDenuncias >= 3) mp.Add(u, u.NDenuncias);
            }

            if (mp.Count >= 20)
            {
                mp = mp.OrderBy(p => p.Value).Reverse().Take(20).ToDictionary(p => p.Key, p => p.Value);
            }
            else
            {
                mp = mp.OrderBy(p => p.Value).Reverse().ToDictionary(p => p.Key, p => p.Value);
            }
            List<Utilizador> res2 = mp.Keys.ToList();
            return View(res2);
        }

        public ActionResult Denuncias()
        {
            var den = from x in model.Denuncias select x;
            List<Denuncias> lista = den.ToList<Denuncias>(); 
            foreach(Denuncias d in lista)
            {
                var artigos = from f in model.Artigo where (f.IdArtigo == d.IdArtigo) select f;
                Artigo al = artigos.ToList<Artigo>().FirstOrDefault();
                d.IdArtigoNavigation.IdArtigo = al.IdArtigo;
                d.IdArtigoNavigation.Nome = al.Nome;
                d.IdArtigoNavigation.IdDono = al.IdDono; // acho que não é preciso mais info

                var ut = from x in model.Utilizador where (x.Email.Equals(al.IdDono)) select x;
                Utilizador u = ut.ToList<Utilizador>().FirstOrDefault();
                //d.IdUtilizadorNavigation.Nome = u.Nome;          //Nao está a dar ????
                //d.IdUtilizadorNavigation.Email = u.Email;
                //d.IdUtilizadorNavigation.NDenuncias = u.NDenuncias;

            }
            return View(lista);
        }


        public ActionResult searchDenuncias(string utilizador)
        {
            var local1 = (from y in model.Artigo where (y.IdDono.Equals(utilizador)) select y);
            List<Artigo> art = local1.ToList<Artigo>();
            List<int> l = new List<int>();
            foreach (Artigo a in art)
            {
                l.Add(a.IdArtigo);
            }
            var local = (from x in model.Denuncias select x);
            List<Denuncias> den = local.ToList<Denuncias>();
            List<Denuncias> res = new List<Denuncias>();
            foreach (Denuncias d in den)
            {
                if (l.Contains(d.IdArtigo))
                {
                    res.Add(d);
                }
            }

            return View(res);
        }

        public ActionResult viewDenuncias(int IdArtigo)
        {
            Denuncias local = (from x in model.Denuncias where (x.IdArtigo == IdArtigo) select x).FirstOrDefault();
            return View(local);
        }

        public ActionResult rejeitarDenuncia(int idDenuncia)
        {
            List<Denuncias> lista = (from den in model.Denuncias where (den.IdDenuncia >= idDenuncia) select den).ToList<Denuncias>();
            Denuncias k = lista.ElementAt<Denuncias>(0);
            lista.Remove(k);
            Artigo a = (from x in model.Artigo where (k.IdArtigo == x.IdArtigo) select x).FirstOrDefault();
            Utilizador u = model.Utilizador.FirstOrDefault(x => x.Email.Equals(a.IdDono));
            model.Denuncias.Remove(k);
            u.NDenuncias--;
            foreach (Denuncias d in lista)
            {
                d.IdDenuncia--;
            }
            model.SaveChanges();
            return View();
        }

        public ActionResult bloquearUtilizador(string utilizador)
        {
            Utilizador u = model.Utilizador.Where(x => x.Email.Equals(utilizador)).FirstOrDefault();
            u.Estado = 2;
            model.SaveChanges();
            return View();
        }

    }

}
