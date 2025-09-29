using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes
{
    /// <summary>
    /// TC Kimlik No algoritması ile doğrulama yapar
    /// </summary>
    public class TcKimlikNoValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not string tcKimlikNo || string.IsNullOrWhiteSpace(tcKimlikNo))
                return false;

            // TC Kimlik No 11 haneli olmalı
            if (tcKimlikNo.Length != 11)
                return false;

            // Sadece rakam içermeli
            if (!tcKimlikNo.All(char.IsDigit))
                return false;

            // İlk hane 0 olamaz
            if (tcKimlikNo[0] == '0')
                return false;

            // TC Kimlik No algoritması kontrolü
            var digits = tcKimlikNo.Select(c => int.Parse(c.ToString())).ToArray();
            
            var sum1 = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            var sum2 = digits[1] + digits[3] + digits[5] + digits[7];
            var check1 = ((sum1 * 7) - sum2) % 10;
            var check2 = (sum1 + sum2 + digits[9]) % 10;
            
            return check1 == digits[9] && check2 == digits[10];
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} geçerli bir TC Kimlik Numarası olmalıdır.";
        }
    }

    /// <summary>
    /// Yaş aralığı doğrulaması yapar
    /// </summary>
    public class AgeValidationAttribute : ValidationAttribute
    {
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 65;

        public override bool IsValid(object value)
        {
            if (value is DateTime birthDate)
            {
                var today = DateTime.Today;
                var age = today.Year - birthDate.Year;
                
                // Doğum günü henüz geçmediyse yaşı bir azalt
                if (birthDate.Date > today.AddYears(-age))
                    age--;
                
                return age >= MinAge && age <= MaxAge;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} için yaş {MinAge}-{MaxAge} arasında olmalıdır.";
        }
    }

    /// <summary>
    /// HTML içerik kontrolü - XSS koruması
    /// </summary>
    public class NoHtmlContentAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string content && !string.IsNullOrWhiteSpace(content))
            {
                // HTML tag'leri kontrol et
                if (content.Contains('<') || content.Contains('>'))
                    return false;

                // Script tag'leri kontrol et
                if (content.Contains("script", StringComparison.OrdinalIgnoreCase))
                    return false;

                // Diğer potansiyel XSS vektörleri
                var dangerousPatterns = new[]
                {
                    "javascript:", "vbscript:", "onload=", "onerror=", "onclick=",
                    "onmouseover=", "onfocus=", "onblur=", "onchange=", "onsubmit="
                };

                return !dangerousPatterns.Any(pattern => 
                    content.Contains(pattern, StringComparison.OrdinalIgnoreCase));
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} HTML içerik içeremez.";
        }
    }

    /// <summary>
    /// Türkiye telefon numarası doğrulaması
    /// </summary>
    public class TurkishPhoneValidationAttribute : ValidationAttribute
    {
        public PhoneType PhoneType { get; set; } = PhoneType.Mobile;

        public override bool IsValid(object value)
        {
            if (value is not string phone || string.IsNullOrWhiteSpace(phone))
                return true; // Nullable field'lar için

            // Boşlukları ve özel karakterleri temizle
            var cleanPhone = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            return PhoneType switch
            {
                PhoneType.Mobile => IsValidMobilePhone(cleanPhone),
                PhoneType.Landline => IsValidLandlinePhone(cleanPhone),
                PhoneType.Any => IsValidMobilePhone(cleanPhone) || IsValidLandlinePhone(cleanPhone),
                _ => false
            };
        }

        private bool IsValidMobilePhone(string phone)
        {
            // Türkiye cep telefonu formatları: 05xxxxxxxxx, +905xxxxxxxxx, 5xxxxxxxxx
            if (phone.StartsWith("+90"))
                phone = phone.Substring(3);
            else if (phone.StartsWith("0"))
                phone = phone.Substring(1);

            return phone.Length == 10 && phone[0] == '5' && phone.All(char.IsDigit);
        }

        private bool IsValidLandlinePhone(string phone)
        {
            // Türkiye sabit telefon formatları: 0xxxxxxxxxx, +90xxxxxxxxxx
            if (phone.StartsWith("+90"))
                phone = phone.Substring(3);
            else if (phone.StartsWith("0"))
                phone = phone.Substring(1);

            return phone.Length == 10 && 
                   (phone[0] == '2' || phone[0] == '3' || phone[0] == '4') && 
                   phone.All(char.IsDigit);
        }

        public override string FormatErrorMessage(string name)
        {
            return PhoneType switch
            {
                PhoneType.Mobile => $"{name} geçerli bir cep telefonu numarası olmalıdır (örn: 05xxxxxxxxx).",
                PhoneType.Landline => $"{name} geçerli bir sabit telefon numarası olmalıdır (örn: 02xxxxxxxx).",
                PhoneType.Any => $"{name} geçerli bir telefon numarası olmalıdır.",
                _ => $"{name} geçerli bir telefon numarası olmalıdır."
            };
        }
    }

    /// <summary>
    /// Telefon tipi enum'u
    /// </summary>
    public enum PhoneType
    {
        Mobile,
        Landline,
        Any
    }

    /// <summary>
    /// Pozitif sayı doğrulaması
    /// </summary>
    public class PositiveNumberAttribute : ValidationAttribute
    {
        public bool AllowZero { get; set; } = false;

        public override bool IsValid(object value)
        {
            if (value is int intValue)
                return AllowZero ? intValue >= 0 : intValue > 0;
            
            if (value is decimal decimalValue)
                return AllowZero ? decimalValue >= 0 : decimalValue > 0;
            
            if (value is double doubleValue)
                return AllowZero ? doubleValue >= 0 : doubleValue > 0;
            
            if (value is float floatValue)
                return AllowZero ? floatValue >= 0 : floatValue > 0;

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return AllowZero 
                ? $"{name} sıfır veya pozitif bir sayı olmalıdır."
                : $"{name} pozitif bir sayı olmalıdır.";
        }
    }

    /// <summary>
    /// Turkish character support için string validation
    /// </summary>
    public class TurkishTextAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not string text || string.IsNullOrWhiteSpace(text))
                return true; // Nullable field'lar için

            // Türkçe karakterler ve temel karakterler
            var allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZğüşıöçĞÜŞİÖÇ ";
            
            return text.All(c => allowedChars.Contains(c));
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} sadece harf ve boşluk karakteri içerebilir.";
        }
    }
}
