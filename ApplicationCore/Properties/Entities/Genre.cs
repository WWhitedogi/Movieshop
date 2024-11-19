using System.ComponentModel.DataAnnotations.Schema; // 提供 Table 等特性
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities{
    [Table("Genre")]


    public class Genre{
        public int Id{get;set;}
        [MaxLength(64)]
        public string Name{get;set;}

        //导航属性
        public ICollection<MovieGenre> MovieGenre{get;set;}
    }

}