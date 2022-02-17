﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class Prisoner
    {
        public Prisoner()
        {
            Mails = new HashSet<Mail>();
            PrisonerOfficers = new HashSet<OfficerPrisoner>();
        }
        [Key]
        public int Id { get; set; }
        [MinLength(Constants.Name_Min_Length), MaxLength(Constants.Name_Max_Length), Required]
        public string FullName { get; set; }
        [RegularExpression(Constants.Nickname_Pattern), Required]
        public string Nickname { get; set; }
        [Range(Constants.Min_Age, Constants.Max_Age), Required]
        public int Age { get; set; }
        [Required]
        public DateTime IncarcerationDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Range(Constants.Bail_Min_Value, Constants.Bail_Max_Value)]
        public decimal? Bail { get; set; }
        [ForeignKey(nameof(Cell))]
        public int? CellId { get; set; }
        public virtual Cell Cell { get; set; }
        public virtual ICollection<Mail> Mails { get; set; }
        public virtual ICollection<OfficerPrisoner> PrisonerOfficers { get; set; }
    }
}