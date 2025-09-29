
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonEntities
{
    public class Enums
    {
        public enum YetkiTurleri
        {
            [Display(Name = "Ana Yetki")]
            AnaYetki,

            [Display(Name = "Orta Yetki")]
            OrtaYetki,

            [Display(Name = "Alt Yetki")]
            AltYetki
        }
        public enum YetkiTipleri : int
        {
            [Display(Name = "Görme")]
            None = 0,

            [Display(Name = "Gör")]
            View = 1,

            [Display(Name = "Düzenle")]
            Edit = 2
        }
        public enum BankoGrup
        {
            [Display(Name = "Ana Grup")]
            AnaGrup,

            [Display(Name = "Orta Grup")]
            OrtaGrup,

            [Display(Name = "Alt Grup")]
            AltGrup
        }

        public enum PersonelUzmanlik : int
        {
            [Display(Name = "Konusunda Uzman")]
            Uzman = 1,

            [Display(Name = "Bilgisi Yok")]
            BilgisiYok = 0,

            [Display(Name = "Konusunda Yrd. Uzman")]
            YrdUzman = 2
        }

        public enum Aktiflik : int
        {
            [Display(Name = "Aktif")]
            Aktif = 1,

            [Display(Name = "Pasif")]
            Pasif = 0,
        }
        public enum Cinsiyet
        {
            [Display(Name = "Erkek")]
            erkek,

            [Display(Name = "Kadın")]
            kadin
        }
        public enum PersonelTipi
        {
            [Display(Name = "Memur")]
            memur,

            [Display(Name = "İşçi")]
            isci,

            [Display(Name = "Taşeron")]
            taseron
        }

        public enum DatabaseAction
        {
            [Display(Name = "INSERT")]
            INSERT,

            [Display(Name = "UPDATE")]
            UPDATE,

            [Display(Name = "DELETE")]
            DELETE,

            [Display(Name = "NONE")]
            NONE
        }

        public enum OgrenimDurumu
        {
            [Display(Name = "İlk Okul")]
            ilkokul,

            [Display(Name = "İlk Öğretim")]
            ilkogretim,

            [Display(Name = "Lise")]
            lise,

            [Display(Name = "Yüksek Okul")]
            yuksekokul,

            [Display(Name = "Lisans")]
            lisans,

            [Display(Name = "Yüksek Lisans")]
            yukseklisans,

            [Display(Name = "Doktora")]
            doktora,
        }
        public enum EvDurumu
        {
            [Display(Name = "Ev Sahibi")]
            ev_sahibi,

            [Display(Name = "Kiracı")]
            kiraci,

            [Display(Name = "Diğer")]
            diger
        }
        public enum KanGrubu
        {
            [Display(Name = "0 Rh(+)")]
            sifir_arti,

            [Display(Name = "0 Rh(-)")]
            sifir_eksi,

            [Display(Name = "AB Rh(+)")]
            ab_arti,

            [Display(Name = "AB Rh(-)")]
            ab_eksi,

            [Display(Name = "A Rh(+)")]
            a_arti,

            [Display(Name = "A Rh(-)")]
            a_eksi,

            [Display(Name = "B Rh(+)")]
            b_arti,

            [Display(Name = "B Rh(-)")]
            b_eksi
        }
        public enum MedeniDurumu
        {
            [Display(Name = "Evli")]
            evli,

            [Display(Name = "Bekar")]
            bekar
        }
        public enum SehitYakinligi
        {
            [Display(Name = "Yok")]
            yok,

            [Display(Name = "Babası")]
            babasi,

            [Display(Name = "Annesi")]
            annesi,

            [Display(Name = "Kardeşi")]
            kardesi,

            [Display(Name = "Çocuğu")]
            cocugu,

            [Display(Name = "Şehit Yakını Eşi")]
            sehit_yakini_esi,

            [Display(Name = "Gazi Yakını")]
            gazi_yakini
        }
        public enum EsininIsDurumu
        {
            [Display(Name = "Belirtilmemiş")]
            belirtilmemis,

            [Display(Name = "Bekar")]
            bekar,

            [Display(Name = "Çalışmıyor")]
            calismiyor,

            [Display(Name = "Ev Hanımı - Çalışmıyor")]
            evhanimi,

            [Display(Name = "Emekli - Çalışmıyor")]
            emekli,

            [Display(Name = "Çalışıyor - Özel")]
            ozel,

            [Display(Name = "Çalışıyor - Kamu")]
            kamu,
        }
        public enum PersonelAktiflikDurum : int
        {
            [Display(Name = "Aktif Personel")]
            Aktif = 1,

            [Display(Name = "Pasif Personel")]
            Pasif = 0,

            [Display(Name = "Emekli Personel")]
            Emekli = 2
        }
        public enum IslemBasari : int
        {
            [Display(Name = "İşlem Başarılı")]
            Basarili = 1,

            [Display(Name = "İşlem Başarısız!")]
            Basarisiz = 0,
        }

        public enum BeklemeDurum : int
        {
            [Display(Name = "İşlem Bitti")]
            Bitti = 2,

            [Display(Name = "İşlem Çağrıldı")]
            Cagrildi = 1,

            [Display(Name = "İşlem Beklemede!")]
            Beklemede = 0,
        }

        public enum ConnectionStatus : int
        {
            [Display(Name = "Çevrim İçi")]
            online = 1,

            [Display(Name = "Çevrim Dışı")]
            offline = 0,
        }

        public enum BankoTipi
        {
            [Display(Name = "Banko")]
            banko = 1,
            [Display(Name = "Masa")]
            masa = 2,
        }

        public enum KatTipi : int
        {
            [Display(Name = "Zemin")]
            zemin = 0,

            [Display(Name = "1.Kat")]
            bir = 1,

            [Display(Name = "2.Kat")]
            iki = 2,
            
            [Display(Name = "3.Kat")]
            uc = 3,
            
            [Display(Name = "4.Kat")]
            dort = 4,
            
            [Display(Name = "5.Kat")]
            bes = 5,
            
            [Display(Name = "6.Kat")]
            alti = 6
        }
    }
}
