(function () {
  function initSelect2() {
    if (typeof $ === 'undefined' || typeof $.fn === 'undefined' || typeof $.fn.select2 === 'undefined') {
      setTimeout(initSelect2, 200);
      return;
    }
    $('.select2').select2({
      theme: 'bootstrap4',
      width: '100%',
      placeholder: function () {
        return $(this).data('placeholder');
      }
    });
  }

  function showToast(type, message) {
    if (typeof toastr !== 'undefined') {
      toastr[type](message);
    } else if (typeof Swal !== 'undefined') {
      Swal.fire({
        icon: type === 'error' ? 'error' : type === 'success' ? 'success' : 'info',
        title: message,
        timer: 3000,
        showConfirmButton: false
      });
    } else {
      alert(message);
    }
  }

  function loadIlceler(ilId, targetSelectId, emptyText) {
    var $target = $(targetSelectId);

    if (!ilId || ilId === '0' || ilId === '') {
      $target.empty().append(`<option value="">${emptyText}</option>`);
      if ($target.hasClass('select2')) $target.trigger('change.select2');
      return;
    }

    $target.prop('disabled', true);
    $target.empty().append('<option value="">Yükleniyor...</option>');
    if ($target.hasClass('select2')) $target.trigger('change.select2');

    $.ajax({
      url: '/Personel/GetIlcelerByIlId',
      method: 'GET',
      data: { ilId: ilId },
      success: function (response) {
        $target.empty().append(`<option value="">${emptyText}</option>`);
        if (response && response.length > 0) {
          $.each(response, function (index, item) {
            $target.append(`<option value="${item.ilceId}">${item.ilceAdi}</option>`);
          });
        }
        $target.prop('disabled', false);
        if ($target.hasClass('select2')) $target.trigger('change.select2');
      },
      error: function (xhr, status, error) {
        console.error('İlçeler yüklenirken hata:', error);
        $target.empty().append('<option value="">Hata oluştu</option>');
        $target.prop('disabled', false);
        if ($target.hasClass('select2')) $target.trigger('change.select2');
        showToast('error', 'İlçeler yüklenirken hata oluştu');
      }
    });
  }

  function loadServisler(departmanId) {
    var $target = $('#ServisId');

    if (!departmanId || departmanId === '0') {
      $target.empty().append('<option value="">Önce departman seçiniz</option>');
      if ($target.hasClass('select2')) $target.trigger('change.select2');
      return;
    }

    $target.prop('disabled', true);
    $target.empty().append('<option value="">Yükleniyor...</option>');
    if ($target.hasClass('select2')) $target.trigger('change.select2');

    $.ajax({
      url: '/Personel/GetServislerByDepartmanId',
      method: 'GET',
      data: { departmanId: departmanId },
      success: function (response) {
        $target.empty().append('<option value="">Servis seçiniz</option>');
        if (response && response.length > 0) {
          $.each(response, function (index, item) {
            $target.append(`<option value="${item.id}">${item.servisAdi}</option>`);
          });
        }
        $target.prop('disabled', false);
        if ($target.hasClass('select2')) $target.trigger('change.select2');
      },
      error: function (xhr, status, error) {
        console.error('Servisler yüklenirken hata:', error);
        $target.empty().append('<option value="">Hata oluştu</option>');
        $target.prop('disabled', false);
        if ($target.hasClass('select2')) $target.trigger('change.select2');
        showToast('error', 'Servisler yüklenirken hata oluştu');
      }
    });
  }

  function initializeCascadeDropdowns() {
    $('#IlId').change(function () {
      loadIlceler($(this).val(), '#IlceId', 'İlçe seçiniz');
    });
    $('#EsininIsIlId').change(function () {
      loadIlceler($(this).val(), '#EsininIsIlceId', 'İlçe seçiniz');
    });
    $('#DepartmanId').change(function () {
      loadServisler($(this).val());
    });
  }

  function initializeFormValidation() {
    $('#formAccountSettings').on('submit', function (e) {
      var isValid = true;
      var errors = [];
      if ($('#IlId').val() && !$('#IlceId').val()) { errors.push('İl seçtikten sonra ilçe seçimi zorunludur'); isValid = false; }
      if ($('#EsininIsIlId').val() && !$('#EsininIsIlceId').val()) { errors.push('Eşinin ili seçtikten sonra ilçe seçimi zorunludur'); isValid = false; }
      if ($('#DepartmanId').val() && !$('#ServisId').val()) { errors.push('Departman seçtikten sonra servis seçimi zorunludur'); isValid = false; }
      if (!isValid) {
        e.preventDefault();
        errors.forEach(function (msg) { showToast('error', msg); });
        return false;
      }
      return true;
    });
  }

  function bootstrap() {
    if (typeof $ === 'undefined') { setTimeout(bootstrap, 200); return; }
    $(document).ready(function () {
      initSelect2();
      initializeCascadeDropdowns();
      initializeFormValidation();
    });
  }

  bootstrap();
})();
