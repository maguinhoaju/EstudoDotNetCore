using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public ProdutoController (ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // o include é necessário para que a categoria seja retornada na consulta
            // Este include é necessário apenas quando o recurso optionsBuilder.UseLazyLoadingProxies() NÃO está declarado
            //var produtos = _contexto.Produtos.Include(p => p.Categoria).ToList();

            // Quando o LayLoading é habilitado no DBContext, o INCLUDE não deve ser informado
            //var produtos = _contexto.Produtos.ToList();

            //Fazendo a consulta com filtro com "Where"
            var result = _contexto.Produtos.Where(p => p.Ativo && p.Categoria.PermiteEstoque);

            // Com a utilização do "Where" o retorno muda de "List" para "IQueryable" e daí podemos verificar se retornou alguma coisa
            if (!result.Any())
                return View(new List<Produto>());

            // O "IQueryable" possui um método "ToList"
            return View(result.ToList());
        }

        [HttpGet]
        public IActionResult ListarInativos(){
            var result = _contexto.Produtos.Where(p => !p.Ativo && p.Categoria.PermiteEstoque);

            if (!result.Any())
                return View(new List<Produto>());

            return View("Index", result.ToList());
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var produto = _contexto.Produtos.FirstOrDefault(c => c.Id == id);
            ViewBag.Categorias = _contexto.Categorias.ToList();
            return View("Salvar", produto);
        }

        public async Task<IActionResult> Deletar(int id)
        {
            var produto = _contexto.Produtos.FirstOrDefault(c => c.Id == id);
            _contexto.Produtos.Remove(produto);
            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            ViewBag.Categorias = _contexto.Categorias.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Produto modelo)
        {
            if(modelo.Id == 0){
            _contexto.Produtos.Add(modelo);
            } else {
                var produto = _contexto.Produtos.FirstOrDefault(p => p.Id == modelo.Id);
                produto.Nome = modelo.Nome;
                produto.Ativo = modelo.Ativo;
                produto.CategoriaId = modelo.CategoriaId;
            }
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}