using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class GameTag
    {
        [ForeignKey(nameof(Game)), Required]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(Tag)), Required]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}