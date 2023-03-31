using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Users
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter girilmelidir.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter girilmelidir.")]
        public string? SurName { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez")]
        //[Display(Name = "E-Posta")]
        //[StringLength(50, ErrorMessage = "Max 50 Karakter girilmelidir.")]
        //[EmailAddress(ErrorMessage = "E-mail formatı şeklinde giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Boş Geçilemez")]
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50, ErrorMessage = "Max 50 Karakter girilmelidir.")]
        public string? UserName { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez")]
        //[Display(Name = "Şifre")]
        //[StringLength(50, ErrorMessage = "Max 50 Karakter girilmelidir.")]
        //[DataType(DataType.Password)]
        public string? Password { get; set; }

        //[Required(ErrorMessage = "Boş Geçilemez")]
        //[Display(Name = "Şifre Tekrar")]
        //[StringLength(50, ErrorMessage = "Max 50 Karakter girilmelidir.")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string? RePassword { get; set; }


        [StringLength(10, ErrorMessage = "Max 10 Karakter girilmelidir.")]
        public string? Role { get; set; }

    }
}
