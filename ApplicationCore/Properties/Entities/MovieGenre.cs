using System.ComponentModel.DataAnnotations.Schema; // 提供 Table 等特性
namespace ApplicationCore.Entities{
        [Table("MovieGenre")]// 告诉 EF 映射到 MovieGenre 表
    public class MovieGenre{
        //外建到Movie表,外建到Genre表
        public int MovieId{get;set;}
        public int GenreId{get;set;}
        // 导航属性，关联 Movie,Genre 实体
        public Movie Movie{get;set;}
        public Genre Genre{get;set;}
        //ex:根据对应id找到Movie，genre
    }
}