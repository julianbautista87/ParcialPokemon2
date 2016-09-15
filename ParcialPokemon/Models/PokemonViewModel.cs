using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParcialPokemon.Models
{
    public class PokemonViewModel
    {
        public List<Pokemon> Pokemones { get; set; }
        public List<Ataque> Ataques { get; set; }
    }
}