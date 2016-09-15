using ParcialPokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParcialPokemon.Controllers
{
    [Authorize]
    public class PeleaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private static Dictionary<string, Pokemon> DictionaryPokemonPropio = new Dictionary<string, Pokemon>();
        private static Dictionary<string, Pokemon> DictionaryPokemonEnemigo = new Dictionary<string, Pokemon>();
        private static Dictionary<string, int> DictionaryNumeroVictoriasPropias = new Dictionary<string, int>();
        private static Dictionary<string, int> DictionaryNumeroVictoriasEnemigo = new Dictionary<string, int>();


        public ActionResult Index()
        {
            DictionaryNumeroVictoriasPropias[User.Identity.Name] = 0;
            DictionaryNumeroVictoriasEnemigo[User.Identity.Name] = 0;
            DictionaryPokemonPropio[User.Identity.Name] = new Pokemon();
            DictionaryPokemonEnemigo[User.Identity.Name] = new Pokemon();
            PokemonViewModel pokemones = new PokemonViewModel();
            pokemones.Pokemones = db.Pokemones.ToList();
            if (pokemones.Pokemones.Count > 0)
                pokemones.Ataques = db.Ataques.ToList();
            else
                pokemones.Ataques = new List<Ataque>();
            return View(pokemones);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult GetPartida(string NombrePokemon)
        {
            //var tasks = db.Tasks
            //    .Include(t => t.Project)
            //    .Where(t => t.ProjectId == id)
            //    .ToList();


            var Pokemons = db.Pokemones;
            var ListaPokemones = Pokemons.ToList();
            var PokemonPropioEnLista = ListaPokemones.Where(t => t.Nombre == NombrePokemon).First();

            DictionaryPokemonPropio[User.Identity.Name] = new Pokemon() { Ataque = PokemonPropioEnLista.Ataque, Defensa = PokemonPropioEnLista.Defensa, Ataques = PokemonPropioEnLista.Ataques, Icono = PokemonPropioEnLista.Icono, ImagenFrontal = PokemonPropioEnLista.ImagenFrontal, ImagenTrasera = PokemonPropioEnLista.ImagenTrasera, Nombre = PokemonPropioEnLista.Nombre, Tipo = PokemonPropioEnLista.Tipo, Velocidad = PokemonPropioEnLista.Velocidad, Vida = PokemonPropioEnLista.Vida, VidaActual = PokemonPropioEnLista.VidaActual };
            ViewBag.PokemonPropio = DictionaryPokemonPropio[User.Identity.Name];

            Random rnd = new Random();
            int r = rnd.Next(0, ListaPokemones.Count);

            DictionaryPokemonEnemigo[User.Identity.Name] = new Pokemon() { Ataque = ListaPokemones[r].Ataque, Defensa = ListaPokemones[r].Defensa, Ataques = ListaPokemones[r].Ataques, Icono = ListaPokemones[r].Icono, ImagenFrontal = ListaPokemones[r].ImagenFrontal, ImagenTrasera = ListaPokemones[r].ImagenTrasera, Nombre = ListaPokemones[r].Nombre, Tipo = ListaPokemones[r].Tipo, Velocidad = ListaPokemones[r].Velocidad, Vida = ListaPokemones[r].Vida, VidaActual = ListaPokemones[r].VidaActual };
            ViewBag.PokemonEnemigo = DictionaryPokemonEnemigo[User.Identity.Name];

            
            return PartialView("_Pelea");
        }

        public ActionResult GetAtaques(string NombrePokemon)
        {
            var Pokemons = db.Pokemones;
            var ListaPokemones = Pokemons.ToList();
            var PokemonPropioEnLista = ListaPokemones.Where(t => t.Nombre == NombrePokemon).First();

            DictionaryPokemonPropio[User.Identity.Name] = new Pokemon() { Ataque = PokemonPropioEnLista.Ataque, Defensa = PokemonPropioEnLista.Defensa, Ataques = PokemonPropioEnLista.Ataques, Icono = PokemonPropioEnLista.Icono, ImagenFrontal = PokemonPropioEnLista.ImagenFrontal, ImagenTrasera = PokemonPropioEnLista.ImagenTrasera, Nombre = PokemonPropioEnLista.Nombre, Tipo = PokemonPropioEnLista.Tipo, Velocidad = PokemonPropioEnLista.Velocidad, Vida = PokemonPropioEnLista.Vida, VidaActual = PokemonPropioEnLista.VidaActual };
            ViewBag.PokemonPropio = DictionaryPokemonPropio[User.Identity.Name];
            
            return PartialView("_Ataques");
        }


        public ActionResult IniciarCicloAtaque(string NombreAtaque)
        {
            //var tasks = db.Tasks
            //    .Include(t => t.Project)
            //    .Where(t => t.ProjectId == id)
            //    .ToList();

            var ListaAtaques = db.Ataques.ToList();
            Ataque AtaquePropio = ListaAtaques.Where(t => t.Nombre == NombreAtaque).First();

            Random rnd = new Random();
            int r = rnd.Next(DictionaryPokemonEnemigo[User.Identity.Name].Ataques.Count);
            Ataque AtaqueEnemigo = DictionaryPokemonEnemigo[User.Identity.Name].Ataques[r];

            if (DictionaryPokemonEnemigo[User.Identity.Name].VidaActual > 0 && DictionaryPokemonPropio[User.Identity.Name].VidaActual > 0)
            {
                if (DictionaryPokemonPropio[User.Identity.Name].Velocidad > DictionaryPokemonEnemigo[User.Identity.Name].Velocidad)
                {
                    DictionaryPokemonPropio[User.Identity.Name].Atacar(DictionaryPokemonEnemigo[User.Identity.Name], AtaquePropio);
                    if (DictionaryPokemonEnemigo[User.Identity.Name].VidaActual > 0)
                    {
                        DictionaryPokemonEnemigo[User.Identity.Name].Atacar(DictionaryPokemonPropio[User.Identity.Name], AtaqueEnemigo);
                        if(DictionaryPokemonPropio[User.Identity.Name].VidaActual == 0)
                        {
                            DictionaryNumeroVictoriasEnemigo[User.Identity.Name] += 1;
                        }
                        
                    }
                    
                    else
                        DictionaryNumeroVictoriasPropias[User.Identity.Name] += 1;
    }
                else
                {
                    DictionaryPokemonEnemigo[User.Identity.Name].Atacar(DictionaryPokemonPropio[User.Identity.Name], AtaqueEnemigo);
                    if (DictionaryPokemonPropio[User.Identity.Name].VidaActual > 0)
                    {
                        DictionaryPokemonPropio[User.Identity.Name].Atacar(DictionaryPokemonEnemigo[User.Identity.Name], AtaquePropio);
                        if (DictionaryPokemonEnemigo[User.Identity.Name].VidaActual == 0)
                            DictionaryNumeroVictoriasPropias[User.Identity.Name] += 1;
                    }
                    else
                        DictionaryNumeroVictoriasEnemigo[User.Identity.Name] += 1;
                }
            }
            else
            {

            }

            ViewBag.PokemonPropio = DictionaryPokemonPropio[User.Identity.Name];
            ViewBag.PokemonEnemigo = DictionaryPokemonEnemigo[User.Identity.Name];

            return PartialView("_Pelea");
        }

        public ActionResult AudioInicioPelea()
        {
            var file = Server.MapPath("~/Files/inicioBatalla.mp3");
            return File(file, "audio/mp3");
        }

        public ActionResult AudioDurantePelea()
        {
            var file = Server.MapPath("~/Files/duranteBatalla.mp3");
            return File(file, "audio/mp3");
        }

        public ActionResult AudioFinalPelea()
        {
            var file = Server.MapPath("~/Files/finBatalla.mp3");
            return File(file, "audio/mp3");
        }

        public ActionResult GetPokeResultado()
        {
            
            ViewBag.PokemonPropio = DictionaryPokemonPropio[User.Identity.Name];
            ViewBag.PokemonEnemigo = DictionaryPokemonEnemigo[User.Identity.Name];
            ViewBag.NumeroVictoriasPropias = DictionaryNumeroVictoriasPropias[User.Identity.Name];
            ViewBag.NumeroVictoriasEnemigo = DictionaryNumeroVictoriasEnemigo[User.Identity.Name];    

            return PartialView("_Marcador");
        }

    }
}
