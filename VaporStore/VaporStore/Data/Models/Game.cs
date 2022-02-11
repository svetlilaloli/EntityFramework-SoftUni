using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class Game
    {
        public Game()
        {
            Purchases = new HashSet<Purchase>();
            GameTags = new HashSet<GameTag>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(Constants.MIN_PRICE, Constants.MAX_PRICE), Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [ForeignKey(nameof(Developer)), Required]
        public int DeveloperId { get; set; }
        [Required]
        public virtual Developer Developer { get; set; }
        [ForeignKey(nameof(Genre)), Required]
        public int GenreId { get; set; }
        [Required]
        public virtual Genre Genre { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<GameTag> GameTags { get; set; }
    }
}
