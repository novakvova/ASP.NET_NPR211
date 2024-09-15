namespace WebPizzaSite.Models.Product
{
    public class ProductSearchViewModel
    {
        //Назва продуктка по якому проводимо пошук
        public string? Name { get; set; }
        //Номер сторіки для пошуку
        public int? Page { get; set; }
    }
}
