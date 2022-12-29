namespace WebApplication1.Models.Domain
{
    public class Osoba
    {
        
        public Guid Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string GlavniBr { get; set; }
        public string? BrojMob { get; set; }
        public string? Fax { get; set; }
        public string Ulica { get; set; }
        public int KucniBr { get; set; }
        public string Grad { get; set; }

        public int PostanskiBr { get; set; }

        public string? Email { get; set; }
    }
}
