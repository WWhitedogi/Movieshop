using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User
    {
     public int Id{get;set;}
     public string Name{get;set;}
    public string Email { get; set; } // 添加 Email 属性
     public string HashedPassword{get;set;}
     public string Salt {get;set;}
     public ICollection<Role> Roles{get;set;}
     public ICollection<Purchase>Purchases{get;set;}
     public ICollection<Favorite> Favorites{get;set;}
     public ICollection<Review> Reviews{get;set;}
    }
}
