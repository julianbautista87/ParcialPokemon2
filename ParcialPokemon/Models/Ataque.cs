using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ParcialPokemon.Models
{
    public class Ataque
    {
        [Key]
        public int IdAtaque { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int Nivel { get; set; }


        public int IdPokemon { get; set; }
        [ForeignKey("IdPokemon")]
        public virtual Pokemon Pokemon { get; set; }
    }
}