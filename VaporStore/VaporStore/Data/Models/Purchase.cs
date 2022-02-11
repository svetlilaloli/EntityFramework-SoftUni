﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PurchaseType Type { get; set; }
        [RegularExpression(Constants.PRODUCT_KEY_PATTERN), Required]
        public string ProductKey { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Card)), Required]
        public int CardId { get; set; }
        [Required]
        public virtual Card Card { get; set; }
        [ForeignKey(nameof(Game)), Required]
        public int GameId { get; set; }
        [Required]
        public virtual Game Game { get; set; }
    }
}