// Global flag ile script'in birden fazla yüklenmesini önle
if (window.duzenleScriptLoaded) {
    // jQuery.getScript ile değerlendirme sırasında top-level return kullanılamaz.
    // İkinci yüklemede unbindAllEvents() zaten tekrar bağlamayı güvenle yapacak.
    console.warn('⚠️ duzenle.js already loaded! Continuing safely (no top-level return).');
} else {
    window.duzenleScriptLoaded = true;
}

$(window).on('load', function () {
    console.log('🔄 duzenle.js window.load event fired');

    // Event handler sayısını kontrol et
    var existingHandlers = $._data($(document)[0], 'events');
    if (existingHandlers && existingHandlers.click) {
        console.log('📊 Existing click handlers on document:', existingHandlers.click.length);
    }

    // Önce tüm event handler'ları temizle
    unbindAllEvents();

    // Select2 initialization
    if (typeof $.fn.select2 !== 'undefined') {
        $('.select2').select2({
            theme: 'bootstrap4',
            width: '100%'
        });
    }

    // Initialize cascade dropdowns
    initializeCascadeDropdowns();

    // Initialize form validation
    initializeFormValidation();

    // Load existing data for edit mode
    loadExistingDataForEdit();

    // Initialize real-time validation
    initializeRealTimeValidation();

    // Phone input class add
    $('#CepTelefonu, #CepTelefonu2, #EvTelefonu').addClass('phone-input');

    // Form native reset event'i dinle (click handler tetiklenmese bile çalışır)
    // Namespace ile bağla ve varlığını logla
    $('#formAccountSettings').off('reset.duzenleForm').on('reset.duzenleForm', function () {
        console.log('🟡 formAccountSettings reset event fired');
        // Bir sonraki tick'te Select2 UI'larını model başlangıç değerlerine senkronize et
        setTimeout(function () {
            try {
                $('.select2').each(function () {
                    // Mevcut değeri koru ve Select2'yi haberdar et
                    $(this).trigger('change.select2');
                });
                clearAllErrors();
                showToast('info', 'Form sıfırlandı');
            } catch (err) {
                console.error('Reset sync error:', err);
            }
        }, 0);
    });
    console.log('✅ Reset handlers attached');
});

// Tüm event handler'ları temizle
function unbindAllEvents() {
    console.log('🧹 Cleaning all events...');

    // Form submit event'ini temizle
    $('#formAccountSettings').off('submit');

    // Dropdown change event'lerini temizle
    $('#IlId, #EsininIsIlId, #DepartmanId, #MedeniDurumu').off('change');

    // Input event'lerini temizle
    $('#Email, .phone-input, #TcKimlikNo, #SicilNo, #Dahili, #OgrenimSuresi, #DogumTarihi, #KartNoAktiflikTarihi').off('blur input change');

    // Required fields event'lerini temizle
    $('input[required], select[required]').off('blur');

    // Reset button event'ini namespace ile temizle
    $(document).off('click.duzenleForm', 'button[type="reset"]');

    // Tüm reset button handler'larını temizle (güvenlik için)
    $('button[type="reset"]').off('click');

    console.log('✅ All events cleaned');
}

function initializeCascadeDropdowns() {
    // İl -> İlçe Cascade
    $('#IlId').off('change').on('change', function () {
        var ilId = $(this).val();
        loadIlceler(ilId, '#IlceId', 'İlçe seçiniz');
    });

    // Eşinin İl -> Eşinin İlçe Cascade  
    $('#EsininIsIlId').off('change').on('change', function () {
        var ilId = $(this).val();
        loadIlceler(ilId, '#EsininIsIlceId', 'İlçe seçiniz');
    });

    // Departman -> Servis Cascade
    $('#DepartmanId').off('change').on('change', function () {
        var departmanId = $(this).val();
        loadServisler(departmanId);
        loadHizmetBinalari(departmanId);
    });

    // Medeni durum değiştiğinde eş bilgilerini kontrol et
    $('#MedeniDurumu').off('change').on('change', function () {
        var medeniDurum = $(this).val();
        if (medeniDurum === '0') { // evli
            $('#EsininAdi').attr('required', true);
            showToast('info', 'Medeni durumu evli seçildi. Eş bilgilerini doldurmayı unutmayın.');
        } else {
            $('#EsininAdi').removeAttr('required');
            clearError('#EsininAdi');
        }
    });
}

function loadHizmetBinalari(departmanId) {
    $.ajax({
        url: '/Personel/GetHizmetBinalariByDepartmanId',
        data: { departmanId: departmanId },
        success: function (data) {
            var $dropdown = $('#HizmetBinasiId');
            $dropdown.empty().append('<option value="">Seçiniz</option>');
            $.each(data, function () {
                $dropdown.append($('<option></option>').val(this.hizmetBinasiId).text(this.hizmetBinasiAdi));
            });

            if ($dropdown.hasClass('select2')) {
                $dropdown.trigger('change.select2');
            }
        },
        error: function (xhr, status, error) {
            console.error('Hizmet binaları yüklenirken hata:', error);
            showToast('error', 'Hizmet binaları yüklenirken hata oluştu');
        }
    });
}

function loadIlceler(ilId, targetSelectId, emptyText) {
    var $target = $(targetSelectId);

    if (!ilId || ilId == '0' || ilId == '') {
        $target.empty().append('<option value="">' + emptyText + '</option>');
        if ($target.hasClass('select2')) {
            $target.trigger('change.select2');
        }
        clearError(targetSelectId);
        return;
    }

    // Loading state
    $target.prop('disabled', true);
    $target.empty().append('<option value="">Yükleniyor...</option>');
    if ($target.hasClass('select2')) {
        $target.trigger('change.select2');
    }

    $.ajax({
        url: '/Personel/GetIlcelerByIlId',
        method: 'GET',
        data: { ilId: ilId },
        success: function (response) {
            var selectedValue = $target.data('selected-value');

            $target.empty().append('<option value="">' + emptyText + '</option>');

            if (response && response.length > 0) {
                $.each(response, function (index, item) {
                    var isSelected = selectedValue && selectedValue == item.ilceId ? 'selected' : '';
                    $target.append('<option value="' + item.ilceId + '" ' + isSelected + '>' + item.ilceAdi + '</option>');
                });
            }

            $target.prop('disabled', false);
            if ($target.hasClass('select2')) {
                $target.trigger('change.select2');
            }
            clearError(targetSelectId);
        },
        error: function (xhr, status, error) {
            console.error('İlçeler yüklenirken hata:', error);
            $target.empty().append('<option value="">Hata oluştu</option>');
            $target.prop('disabled', false);
            if ($target.hasClass('select2')) {
                $target.trigger('change.select2');
            }
            showToast('error', 'İlçeler yüklenirken hata oluştu');
            highlightError(targetSelectId);
        }
    });
}

function loadServisler(departmanId) {
    var $target = $('#ServisId');

    if (!departmanId || departmanId == '0') {
        $target.empty().append('<option value="">Önce departman seçiniz</option>');
        if ($target.hasClass('select2')) {
            $target.trigger('change.select2');
        }
        return;
    }

    var selectedValue = $target.data('selected-value');

    $target.prop('disabled', true);
    $target.empty().append('<option value="">Yükleniyor...</option>');
    if ($target.hasClass('select2')) {
        $target.trigger('change.select2');
    }

    $.ajax({
        url: '/Personel/GetServislerByDepartmanId',
        method: 'GET',
        data: { departmanId: departmanId },
        success: function (response) {
            $target.empty().append('<option value="">Servis seçiniz</option>');

            if (response && response.length > 0) {
                $.each(response, function (index, item) {
                    var isSelected = selectedValue && selectedValue == item.id ? 'selected' : '';
                    $target.append('<option value="' + item.id + '" ' + isSelected + '>' + item.servisAdi + '</option>');
                });
            }

            $target.prop('disabled', false);
            if ($target.hasClass('select2')) {
                $target.trigger('change.select2');
            }
            clearError('#ServisId');
        },
        error: function (xhr, status, error) {
            console.error('Servisler yüklenirken hata:', error);
            $target.empty().append('<option value="">Hata oluştu</option>');
            $target.prop('disabled', false);
            if ($target.hasClass('select2')) {
                $target.trigger('change.select2');
            }
            showToast('error', 'Servisler yüklenirken hata oluştu');
            highlightError('#ServisId');
        }
    });
}

function loadExistingDataForEdit() {
    // İlçeleri yükle (edit mode)
    var ilId = $('#IlId').val();
    if (ilId && ilId !== '0') {
        loadIlceler(ilId, '#IlceId', 'İlçe seçiniz');
    }

    // Eşinin ilçelerini yükle (edit mode)
    var esIlId = $('#EsininIsIlId').val();
    if (esIlId && esIlId !== '0') {
        loadIlceler(esIlId, '#EsininIsIlceId', 'İlçe seçiniz');
    }

    // Servisleri yükle (edit mode)
    var departmanId = $('#DepartmanId').val();
    if (departmanId && departmanId !== '0') {
        loadServisler(departmanId);
    }
}

function initializeFormValidation() {
    $('#formAccountSettings').off('submit').on('submit', function (e) {
        // Tüm hataları temizle
        clearAllErrors();

        var isValid = true;

        // Cascade selections kontrolü
        if (!validateCascadeSelections()) {
            isValid = false;
        }

        // Business rules kontrolü
        if (!validateBusinessRules()) {
            isValid = false;
        }

        // Required field kontrolü
        if (!validateRequiredFields()) {
            isValid = false;
        }

        if (!isValid) {
            e.preventDefault();
            showToast('error', 'Lütfen formdaki hataları düzeltin ve tekrar deneyin');
            return false;
        }

        // Form gönderiliyor mesajı
        showToast('info', 'Personel bilgileri güncelleniyor...');
    });
}

function validateRequiredFields() {
    var isValid = true;
    var requiredFields = [
        { id: '#TcKimlikNo', name: 'T.C. Kimlik Numarası' },
        { id: '#AdSoyad', name: 'Ad Soyad' },
        { id: '#SicilNo', name: 'Sicil Numarası' },
        { id: '#DepartmanId', name: 'Departman' },
        { id: '#ServisId', name: 'Servis' },
        { id: '#UnvanId', name: 'Ünvan' },
        { id: '#AtanmaNedeniId', name: 'Atanma Nedeni' },
        { id: '#Email', name: 'E-mail' },
        { id: '#DogumTarihi', name: 'Doğum Tarihi' }
    ];

    for (var i = 0; i < requiredFields.length; i++) {
        var field = requiredFields[i];
        var value = $(field.id).val();
        if (!value || value === '' || value === '0') {
            highlightError(field.id);
            showToast('error', field.name + ' alanı zorunludur');
            isValid = false;
        }
    }

    return isValid;
}

function validateCascadeSelections() {
    var isValid = true;
    var errors = [];

    // İl seçilmiş ama ilçe seçilmemiş
    var ilId = $('#IlId').val();
    var ilceId = $('#IlceId').val();

    if (ilId && ilId !== '0' && (!ilceId || ilceId === '0')) {
        errors.push('İl seçtikten sonra ilçe seçimi zorunludur');
        highlightError('#IlceId');
        isValid = false;
    }

    // Eşinin ili seçilmiş ama ilçesi seçilmemiş  
    var esIlId = $('#EsininIsIlId').val();
    var esIlceId = $('#EsininIsIlceId').val();

    if (esIlId && esIlId !== '0' && (!esIlceId || esIlceId === '0')) {
        errors.push('Eş iş ili seçildikten sonra ilçe seçimi zorunludur');
        highlightError('#EsininIsIlceId');
        isValid = false;
    }

    // Departman seçilmiş ama servis seçilmemiş
    var departmanId = $('#DepartmanId').val();
    var servisId = $('#ServisId').val();

    if (departmanId && departmanId !== '0' && (!servisId || servisId === '0')) {
        errors.push('Departman seçildikten sonra servis seçimi zorunludur');
        highlightError('#ServisId');
        isValid = false;
    }

    for (var i = 0; i < errors.length; i++) {
        showToast('error', errors[i]);
    }

    return isValid;
}

function validateBusinessRules() {
    var isValid = true;
    var errors = [];

    // Doğum tarihi kontrolü
    var dogumTarihi = new Date($('#DogumTarihi').val());
    var today = new Date();

    if (dogumTarihi && !isNaN(dogumTarihi.getTime())) {
        var age = today.getFullYear() - dogumTarihi.getFullYear();
        var monthDiff = today.getMonth() - dogumTarihi.getMonth();

        if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dogumTarihi.getDate())) {
            age--;
        }

        if (age < 18) {
            errors.push('Personel en az 18 yaşında olmalıdır');
            highlightError('#DogumTarihi');
            isValid = false;
        }

        if (age > 70) {
            errors.push('Personel yaşı 70\'den büyük olamaz');
            highlightError('#DogumTarihi');
            isValid = false;
        }

        if (dogumTarihi > today) {
            errors.push('Doğum tarihi gelecek tarih olamaz');
            highlightError('#DogumTarihi');
            isValid = false;
        }
    }

    // Kart aktiflik tarihi kontrolü
    var kartTarihi = $('#KartNoAktiflikTarihi').val();
    if (kartTarihi && new Date(kartTarihi) > today) {
        errors.push('Kart aktiflik tarihi gelecek tarih olamaz');
        highlightError('#KartNoAktiflikTarihi');
        isValid = false;
    }

    // Medeni durumu kontrolü
    var medeniDurum = $('#MedeniDurumu').val();
    var esininAdi = $('#EsininAdi').val();

    if (medeniDurum === '0' && (!esininAdi || esininAdi.trim() === '')) { // evli
        errors.push('Medeni durumu evli olan personelin eş adı zorunludur');
        highlightError('#EsininAdi');
        isValid = false;
    }

    // Öğrenim süresi kontrolü
    var ogrenimSuresi = parseInt($('#OgrenimSuresi').val());
    if (!isNaN(ogrenimSuresi) && ogrenimSuresi > 15) {
        errors.push('Öğrenim süresi 15 yıldan fazla olamaz');
        highlightError('#OgrenimSuresi');
        isValid = false;
    }

    if (ogrenimSuresi < 0) {
        errors.push('Öğrenim süresi negatif olamaz');
        highlightError('#OgrenimSuresi');
        isValid = false;
    }

    // Sicil numarası kontrolü
    var sicilNo = parseInt($('#SicilNo').val());
    if (isNaN(sicilNo) || sicilNo <= 0) {
        errors.push('Geçerli bir sicil numarası giriniz');
        highlightError('#SicilNo');
        isValid = false;
    }

    // Dahili telefon kontrolü
    var dahili = parseInt($('#Dahili').val());
    if (!isNaN(dahili) && (dahili < 0 || dahili > 9999)) {
        errors.push('Dahili telefon 0-9999 arasında olmalıdır');
        highlightError('#Dahili');
        isValid = false;
    }

    for (var i = 0; i < errors.length; i++) {
        showToast('error', errors[i]);
    }

    return isValid;
}

function initializeRealTimeValidation() {
    // Email validation
    $('#Email').off('blur').on('blur', function () {
        var email = $(this).val();
        var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (email && email.trim() !== '') {
            if (!emailRegex.test(email)) {
                highlightError('#Email');
                showToast('warning', 'Geçerli bir e-mail adresi giriniz');
            } else {
                clearError('#Email');
            }
        }
    });

    // Telefon validation
    $('.phone-input').off('input blur').on('input blur', function () {
        var phone = $(this).val();
        var phoneRegex = /^[0-9\s\-\+\(\)]{10,15}$/;

        if (phone && phone.trim() !== '') {
            if (!phoneRegex.test(phone.replace(/\s/g, ''))) {
                highlightError('#' + $(this).attr('id'));
                showToast('warning', 'Geçerli bir telefon numarası giriniz');
            } else {
                clearError('#' + $(this).attr('id'));
            }
        } else {
            clearError('#' + $(this).attr('id'));
        }
    });

    // TC Kimlik No validation
    $('#TcKimlikNo').off('input').on('input', function () {
        var tc = $(this).val();
        if (tc && tc.length === 11 && /^\d+$/.test(tc)) {
            clearError('#TcKimlikNo');
        } else if (tc && tc.length > 0) {
            highlightError('#TcKimlikNo');
            if (tc.length !== 11) {
                showToast('warning', 'T.C. Kimlik No 11 haneli olmalıdır');
            } else {
                showToast('warning', 'T.C. Kimlik No sadece rakam içermelidir');
            }
        }
    });

    // Sicil No validation
    $('#SicilNo').off('input blur').on('input blur', function () {
        var sicilNo = $(this).val();
        if (sicilNo && sicilNo.trim() !== '') {
            var sicilInt = parseInt(sicilNo);
            if (isNaN(sicilInt) || sicilInt <= 0) {
                highlightError('#SicilNo');
                showToast('warning', 'Geçerli bir sicil numarası giriniz');
            } else {
                clearError('#SicilNo');
            }
        }
    });

    // Dahili telefon validation
    $('#Dahili').off('input blur').on('input blur', function () {
        var dahili = $(this).val();
        if (dahili && dahili.trim() !== '') {
            var dahiliInt = parseInt(dahili);
            if (isNaN(dahiliInt) || dahiliInt < 0 || dahiliInt > 9999) {
                highlightError('#Dahili');
                showToast('warning', 'Dahili telefon 0-9999 arasında olmalıdır');
            } else {
                clearError('#Dahili');
            }
        }
    });

    // Öğrenim süresi validation
    $('#OgrenimSuresi').off('input blur').on('input blur', function () {
        var sure = $(this).val();
        if (sure && sure.trim() !== '') {
            var sureInt = parseInt(sure);
            if (isNaN(sureInt) || sureInt < 0 || sureInt > 15) {
                highlightError('#OgrenimSuresi');
                showToast('warning', 'Öğrenim süresi 0-15 yıl arasında olmalıdır');
            } else {
                clearError('#OgrenimSuresi');
            }
        }
    });

    // Doğum tarihi validation
    $('#DogumTarihi').off('change blur').on('change blur', function () {
        var dogumTarihi = new Date($(this).val());
        var today = new Date();

        if (dogumTarihi && !isNaN(dogumTarihi.getTime())) {
            if (dogumTarihi > today) {
                highlightError('#DogumTarihi');
                showToast('warning', 'Doğum tarihi gelecek tarih olamaz');
            } else {
                var age = today.getFullYear() - dogumTarihi.getFullYear();
                var monthDiff = today.getMonth() - dogumTarihi.getMonth();

                if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dogumTarihi.getDate())) {
                    age--;
                }

                if (age < 18) {
                    highlightError('#DogumTarihi');
                    showToast('warning', 'Personel en az 18 yaşında olmalıdır');
                } else if (age > 70) {
                    highlightError('#DogumTarihi');
                    showToast('warning', 'Personel yaşı 70\'den büyük olamaz');
                } else {
                    clearError('#DogumTarihi');
                }
            }
        }
    });

    // Kart aktiflik tarihi validation
    $('#KartNoAktiflikTarihi').off('change blur').on('change blur', function () {
        var kartTarihi = new Date($(this).val());
        var today = new Date();

        if (kartTarihi && !isNaN(kartTarihi.getTime())) {
            if (kartTarihi > today) {
                highlightError('#KartNoAktiflikTarihi');
                showToast('warning', 'Kart aktiflik tarihi gelecek tarih olamaz');
            } else {
                clearError('#KartNoAktiflikTarihi');
            }
        }
    });

    // Required fields için focus out kontrolü
    $('input[required], select[required]').off('blur').on('blur', function () {
        var value = $(this).val();
        if (!value || value.trim() === '' || value === '0') {
            highlightError('#' + $(this).attr('id'));
        } else {
            clearError('#' + $(this).attr('id'));
        }
    });

    // İl-İlçe real-time validation
    $('#IlId').off('change').on('change', function () {
        var ilId = $(this).val();
        var ilceId = $('#IlceId').val();

        if (ilId && ilId !== '0') {
            clearError('#IlId');
            if (ilceId && ilceId !== '0') {
                clearError('#IlceId');
            }
        }
    });

    $('#EsininIsIlId').off('change').on('change', function () {
        var ilId = $(this).val();
        var ilceId = $('#EsininIsIlceId').val();

        if (ilId && ilId !== '0') {
            clearError('#EsininIsIlId');
            if (ilceId && ilceId !== '0') {
                clearError('#EsininIsIlceId');
            }
        }
    });
}

function highlightError(selector) {
    $(selector).addClass('is-invalid');
    $(selector).removeClass('is-valid');

    // Select2 elementleri için özel styling
    if ($(selector).hasClass('select2')) {
        $(selector).next('.select2-container').find('.select2-selection').addClass('is-invalid');
        $(selector).next('.select2-container').find('.select2-selection').removeClass('is-valid');
    }
}

function clearError(selector) {
    $(selector).removeClass('is-invalid');
    $(selector).addClass('is-valid');

    // Select2 elementleri için özel styling
    if ($(selector).hasClass('select2')) {
        $(selector).next('.select2-container').find('.select2-selection').removeClass('is-invalid');
        $(selector).next('.select2-container').find('.select2-selection').addClass('is-valid');
    }
}

function clearAllErrors() {
    $('.form-control, .form-select').removeClass('is-invalid is-valid');
    $('.select2-container .select2-selection').removeClass('is-invalid is-valid');
}

function showToast(type, message) {
    if (typeof toastr !== 'undefined') {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        toastr[type](message);
    } else if (typeof Swal !== 'undefined') {
        var icon = 'info';
        switch (type) {
            case 'error': icon = 'error'; break;
            case 'success': icon = 'success'; break;
            case 'warning': icon = 'warning'; break;
            default: icon = 'info'; break;
        }

        Swal.fire({
            icon: icon,
            title: message,
            timer: 3000,
            showConfirmButton: false,
            toast: true,
            position: 'top-end'
        });
    } else {
        alert(message);
    }
}

// Form temizleme onayı - Document level event handler - Namespace kullan ve debug ekle
$(document).off('click.duzenleForm', 'button[type="reset"]').on('click.duzenleForm', 'button[type="reset"]', function (e) {
    console.log('🔴 Reset button clicked! Handler count check...');

    // Inline onclick varsa kaldır (güvenlik önlemi)
    $(this).removeAttr('onclick');

    // Handler sayısını kontrol et
    var handlers = $._data($(document)[0], 'events');
    if (handlers && handlers.click) {
        var resetHandlers = handlers.click.filter(h => h.selector === 'button[type="reset"]');
        console.log('📊 Reset button handlers count:', resetHandlers.length);

        if (resetHandlers.length > 1) {
            console.error('❌ Multiple handlers detected!', resetHandlers);
        }
    }

    e.preventDefault();
    e.stopPropagation();

    if (typeof Swal !== 'undefined') {
        Swal.fire({
            title: 'Emin misiniz?',
            text: 'Formu temizlemek istediğinize emin misiniz? Doldurduğunuz tüm veriler silinecek.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Evet, temizle!',
            cancelButtonText: 'İptal'
        }).then((result) => {
            if (result.isConfirmed) {
                // Native reset tetikle (reset event handler Select2 ve validasyonları senkronize edecek)
                $('#formAccountSettings')[0].reset();
                showToast('success', 'Form temizlendi');
            }
        });
    } else {
        var confirmed = confirm('Formu temizlemek istediğinize emin misiniz? Doldurduğunuz tüm veriler silinecek.');
        if (confirmed) {
            // Native reset tetikle (reset event handler Select2 ve validasyonları senkronize edecek)
            $('#formAccountSettings')[0].reset();
            showToast('success', 'Form temizlendi');
        }
    }
});