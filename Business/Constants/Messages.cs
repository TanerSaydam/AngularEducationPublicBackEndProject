using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {        
        public static string ProductAdded = "Ürün başarıyla eklendi";
        public static string ProductDeleted = "Ürün başarıyla silindi";
        public static string ProductUpdated = "Ürün başarıyla güncellendi";
        public static string ProductCountOfCategoryError = "Bir kategori de en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Bu ürün isminde bir kayıt mevcut";
        
        public static string MaintenanceTime = "Sistem bakımda";

        public static string CategoryAdded = "Kategori başarıyla eklendi";
        public static string CategoryDeleted = "Kategori başarıyla silindi";
        public static string CategoryUpdated = "Kategori başarıyla güncellendi";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";

        public static string CustomerAdded = "Müşteri başarıyla eklendi";
        public static string CustomerDeleted = "Müşteri başarıyla silindi";
        public static string CustomerUpdated = "Müşteri başarıyla güncellendi";

        public static string OrderAdded = "Sipariş başarıyla eklendi";
        public static string OrderDeleted = "Sipariş başarıyla silindi";
        public static string OrderUpdated = "Sipariş başarıyla güncellendi";

        public static string AddedBasket = "Ürün sepetinize eklendi";
        public static string UpdatedBasket = "Sepetteki ürünüz güncellendi";
        public static string DeletedBasket = "Ürün sepetinizden kaldırıldı";
        public static string QuantityIsBiggerThanStock = "Eklenecek ürün stok adediden büyük olamaz";

        public static string AuthorizationDenied = "İşlem yapmaya yetkiniz yok";

        public static string UserRegistered = "Kullanıcı kayıtı oluşturuldu";
        public static string UserUpdated = "Kullanıcı güncellendi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Bu bilgilere ait bir kullanıcı var";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}
