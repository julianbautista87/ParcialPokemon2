using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParcialPokemon.Models
{
    public class Pokemon
    {
        [Key]
        public int IdPokemon { get; set; }
        public string Nombre { get; set; }
        public string ImagenFrontal { get; set; }
        public string ImagenTrasera { get; set; }
        public string Icono { get; set; }

        public int Tipo { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
        public int VidaActual { get; set; }

        public void Atacar(Pokemon enemigo, Ataque ataque)
        {

            float multiplicador = 1;

            //Piedra
            if (ataque.Tipo.Equals(Funciones.Tipos.Piedra) && (enemigo.Tipo.Equals(Funciones.Tipos.Tierra) || enemigo.Tipo.Equals(Funciones.Tipos.Lucha)))
                multiplicador = 0.5f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Piedra) && (enemigo.Tipo.Equals(Funciones.Tipos.Fuego)))
                multiplicador = 2f;

            //Tierra
            if (ataque.Tipo.Equals(Funciones.Tipos.Tierra) && (enemigo.Tipo.Equals(Funciones.Tipos.Planta)))
                multiplicador = 0.5f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Tierra) && (enemigo.Tipo.Equals(Funciones.Tipos.Piedra) || enemigo.Tipo.Equals(Funciones.Tipos.Fuego)))
                multiplicador = 2f;

            //Agua
            if (ataque.Tipo.Equals(Funciones.Tipos.Agua) && (enemigo.Tipo.Equals(Funciones.Tipos.Dragon) || enemigo.Tipo.Equals(Funciones.Tipos.Planta) || enemigo.Tipo.Equals(Funciones.Tipos.Agua)))
                multiplicador = 0.5f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Agua) && (enemigo.Tipo.Equals(Funciones.Tipos.Fuego) || enemigo.Tipo.Equals(Funciones.Tipos.Tierra) || enemigo.Tipo.Equals(Funciones.Tipos.Piedra)))
                multiplicador = 2f;

            //Fuego
            if (ataque.Tipo.Equals(Funciones.Tipos.Fuego) && (enemigo.Tipo.Equals(Funciones.Tipos.Dragon) || enemigo.Tipo.Equals(Funciones.Tipos.Fuego) || enemigo.Tipo.Equals(Funciones.Tipos.Piedra) || enemigo.Tipo.Equals(Funciones.Tipos.Agua)))
                multiplicador = 0.5f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Fuego) && (enemigo.Tipo.Equals(Funciones.Tipos.Planta)))
                multiplicador = 2f;

            //Planta
            if (ataque.Tipo.Equals(Funciones.Tipos.Planta) && (enemigo.Tipo.Equals(Funciones.Tipos.Dragon) || enemigo.Tipo.Equals(Funciones.Tipos.Fuego) || enemigo.Tipo.Equals(Funciones.Tipos.Planta)))
                multiplicador = 0.5f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Planta) && (enemigo.Tipo.Equals(Funciones.Tipos.Piedra) || enemigo.Tipo.Equals(Funciones.Tipos.Tierra) || enemigo.Tipo.Equals(Funciones.Tipos.Agua)))
                multiplicador = 2f;

            //Lucha
            if (ataque.Tipo.Equals(Funciones.Tipos.Lucha) && (enemigo.Tipo.Equals(Funciones.Tipos.Psiquico)))
                multiplicador = 0.5f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Lucha) && (enemigo.Tipo.Equals(Funciones.Tipos.Normal) || enemigo.Tipo.Equals(Funciones.Tipos.Piedra)))
                multiplicador = 2f;

            //Normal
            if (ataque.Tipo.Equals(Funciones.Tipos.Normal) && (enemigo.Tipo.Equals(Funciones.Tipos.Piedra)))
                multiplicador = 0.5f;

            //Psiquico
            if (ataque.Tipo.Equals(Funciones.Tipos.Psiquico) && (enemigo.Tipo.Equals(Funciones.Tipos.Psiquico)))
                multiplicador = 0.5f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Psiquico) && (enemigo.Tipo.Equals(Funciones.Tipos.Lucha)))
                multiplicador = 2f;

            //Fantasma
            if (ataque.Tipo.Equals(Funciones.Tipos.Fantasma) && (enemigo.Tipo.Equals(Funciones.Tipos.Normal) || enemigo.Tipo.Equals(Funciones.Tipos.Psiquico)))
                multiplicador = 0f;
            if (ataque.Tipo.Equals(Funciones.Tipos.Fantasma) && (enemigo.Tipo.Equals(Funciones.Tipos.Fantasma)))
                multiplicador = 2f;

            //Dragon
            if (ataque.Tipo.Equals(Funciones.Tipos.Dragon) && (enemigo.Tipo.Equals(Funciones.Tipos.Dragon)))
                multiplicador = 2f;



            int nivel = 50;
            int dano = (int)(((2 * nivel + 10) / 250f) * ((float)Ataque / enemigo.Defensa * ataque.Nivel) * multiplicador);

            enemigo.VidaActual = enemigo.VidaActual - dano > 0 ? enemigo.VidaActual - dano : 0;
        }

        public virtual List<Ataque> Ataques { get; set; }
    }
}