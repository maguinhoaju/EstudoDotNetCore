namespace Core
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        //virtual é utilizado juntamente com o LayLoading declarado na classe DbContext
        public virtual Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }

        public bool Ativo { get; set; }
    }
}