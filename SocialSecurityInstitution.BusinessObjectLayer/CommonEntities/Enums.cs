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
        public enum YetkiTipleri
        {
            [Display(Name = "Gör")]
            Gor,

            [Display(Name = "Görme")]
            Gorme,

            [Display(Name = "Düzenle")]
            Duzenle
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
    }
}
