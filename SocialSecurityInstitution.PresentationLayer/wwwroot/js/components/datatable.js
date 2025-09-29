(function (global) {
    // Namespace
    global.App = global.App || {};
    App.DataTables = App.DataTables || {};

    var SEARCH_MARKER = '__appDtNormalizeAdded__';

    function ensureDependencies() {
        if (typeof $ === 'undefined' || !$.fn || !$.fn.DataTable) return false;
        return true;
    }

    function addNormalizeSearch(searchColumns) {
        if (!$.fn.dataTable || !$.fn.dataTable.ext || !$.fn.dataTable.ext.search) return;
        if (window[SEARCH_MARKER]) return;

        var normalize = function (term) { return (term || '').toLocaleLowerCase('tr'); };

        $.fn.dataTable.ext.search.push(function (settings, data) {
            var $filter = $(settings.nTableWrapper).find('.dataTables_filter input');
            if (!$filter.length) return true;
            var term = normalize($filter.val());
            if (!term) return true;

            var indices = settings.oInit && settings.oInit._appSearchColumns || null;
            var text = '';
            if (indices && indices.length) {
                for (var i = 0; i < indices.length; i++) {
                    var idx = indices[i];
                    if (typeof data[idx] !== 'undefined') text += ' ' + data[idx];
                }
            } else {
                text = (data || []).join(' ');
            }
            return normalize(text).indexOf(term) > -1;
        });

        window[SEARCH_MARKER] = true;
    }

    function buildButtons(api, opts) {
        var hasButtons = $.fn.dataTable && $.fn.dataTable.Buttons;
        var extBtns = $.fn.dataTable && $.fn.dataTable.ext && $.fn.dataTable.ext.buttons ? $.fn.dataTable.ext.buttons : {};
        var can = function (key) { return !!extBtns && !!extBtns[key]; };
        if (!hasButtons) return { can: can };

        var titleTs = 'Personel_Listesi_' + new Date().toISOString().slice(0, 19).replace(/[:T]/g, '-');
        var list = [];
        if (can('copyHtml5')) list.push({ extend: 'copyHtml5', name: 'copy' });
        if (can('csvHtml5')) list.push({ extend: 'csvHtml5', name: 'csv', title: titleTs });
        if (can('excelHtml5')) list.push({ extend: 'excelHtml5', name: 'excel', title: titleTs });
        if (can('pdfHtml5')) list.push({ extend: 'pdfHtml5', name: 'pdf', title: 'Liste', orientation: 'landscape', pageSize: 'A4' });
        if (can('print')) list.push({ extend: 'print', name: 'print' });
        if (can('colvis')) list.push({ extend: 'colvis', name: 'colvis' });

        if (list.length) {
            new $.fn.dataTable.Buttons(api, { buttons: list }).container();
        }
        return { can: can };
    }

    function buildToolbar($wrapper, api, can) {
        var $filter = $wrapper.find('.dataTables_filter');
        var $length = $wrapper.find('.dataTables_length');
        if (!$filter.length || !$length.length) return;

        // Length kontrolünü düzenle
        var $lenSelect = $length.find('select');
        $lenSelect.removeClass().addClass('form-select form-select-sm w-auto');

        // Filter kontrolünü düzenle
        var $input = $filter.find('input');
        $input.removeClass().addClass('form-control form-control-sm').attr('placeholder', 'Tabloda ara...');

        // Export select oluştur
        var $select = $('<select class="form-select form-select-sm w-auto ms-2" aria-label="Dışa Aktar">\
      <option selected disabled>Dışa aktar...</option>\
    </select>');

        if (can('copyHtml5')) $select.append('<option value="copy">Kopyala</option>');
        if (can('csvHtml5')) $select.append('<option value="csv">CSV</option>');
        if (can('excelHtml5')) $select.append('<option value="excel">Excel</option>');
        var pdfAvailable = can('pdfHtml5') && window.pdfMake && window.pdfMake.createPdf && window.pdfMake.vfs;
        if (pdfAvailable) $select.append('<option value="pdf">PDF</option>');
        if (can('print')) $select.append('<option value="print">Yazdır</option>');
        if (can('colvis')) $select.append('<option value="colvis">Sütunlar</option>');

        // Export'u filter'ın yanına ekle
        $filter.find('label').append($select);

        $select.on('change', function () {
            var val = $(this).val();
            if (!val) return;
            try { if (api.button) api.button(val + ':name').trigger(); } catch (e) { console.warn(e); }
            $(this).prop('selectedIndex', 0);
        });
    }

    function makeCompact($wrapper) {
        var $pg = $wrapper.find('.dataTables_paginate');
        $pg.addClass('d-flex justify-content-end');
        $pg.find('ul.pagination').addClass('pagination-sm mb-0');
    }

    // Public API
    App.DataTables.init = function (selector, options) {
        if (!ensureDependencies()) return null;

        var $table = $(selector);
        if (!$table.length) return null;

        // Çift başlatma kontrolü
        if ($.fn.DataTable.isDataTable(selector)) {
            console.warn('DataTable already initialized for ' + selector + '. Destroying...');
            $(selector).DataTable().destroy();
            $(selector).empty(); // Tabloyu temizle
        }

        var opts = $.extend(true, {
            paging: true,
            searching: true,
            ordering: true,
            info: true,
            responsive: true,
            fixedHeader: false,
            autoWidth: false,
            scrollX: false,
            scrollY: false,
            scrollCollapse: false,
            pagingType: 'simple_numbers',
            pageLength: 25,
            lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, 'Tümü']],
            language: {
                emptyTable: 'Kayıt bulunamadı',
                info: 'Toplam _TOTAL_ kayıt gösteriliyor',
                infoEmpty: '0 kayıt',
                infoFiltered: '(_MAX_ kayıt içerisinden filtrelendi)',
                lengthMenu: '_MENU_ kayıt göster',
                loadingRecords: 'Yükleniyor...',
                processing: 'İşleniyor...',
                search: '',
                searchPlaceholder: 'Tabloda ara...',
                zeroRecords: 'Eşleşen kayıt yok',
                paginate: {
                    first: 'İlk', last: 'Son', next: 'Sonraki', previous: 'Önceki'
                }
            },
            _appSearchColumns: (options && options.searchColumns) || null,
            dom: "<'row align-items-center mb-3'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6 text-end'f>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row align-items-center mt-3'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 text-end'p>>",
            drawCallback: function () {
                makeCompact($(this).closest('.dataTables_wrapper'));
                try { $(this).DataTable().columns.adjust().responsive.recalc(); } catch (e) { }
            },
            initComplete: function () {
                var api = this.api ? this.api() : $table.DataTable();
                var $wrapper = $(api.table().container());

                var tableNode = $(api.table().node());
                tableNode.addClass('dt-wrap').removeClass('nowrap');

                // .table-responsive wrapper'ını kaldır
                var $resp = tableNode.closest('.table-responsive');
                if ($resp.length) { $resp.removeClass('table-responsive'); }

                var helper = buildButtons(api, options || {});
                buildToolbar($wrapper, api, helper.can);
                makeCompact($wrapper);

                // UI düzenlemeleri
                try {
                    var $length = $wrapper.find('.dataTables_length');
                    var $lenSelect = $length.find('select');
                    var $lenLabel = $length.find('label');

                    // Length label'ını düzenle
                    $lenLabel.empty();
                    $lenLabel.append('<span class="text-muted me-2">Sayfa başı:</span>');
                    $lenLabel.append($lenSelect);
                    $lenLabel.append('<span class="text-muted ms-2">kayıt</span>');

                    // Filter'ı düzenle
                    var $filter = $wrapper.find('.dataTables_filter');
                    var $input = $filter.find('input');
                    var $filterLabel = $filter.find('label');
                    $filterLabel.contents().filter(function () { return this.nodeType === 3; }).remove();
                    $input.attr('placeholder', 'Tabloda ara...');

                } catch (e) { console.warn(e); }

                // Sütunları ayarla
                try { api.columns.adjust().responsive.recalc(); } catch (e) { }

                // Window resize
                $(window).off('resize.appDt' + selector).on('resize.appDt' + selector, function () {
                    try { api.columns.adjust().responsive.recalc(); } catch (e) { }
                });

                if (typeof options.onInit === 'function') {
                    try { options.onInit(api); } catch (e) { console.warn(e); }
                }

                console.log('✅ DataTable initialized for ' + selector);
            }
        }, options || {});

        addNormalizeSearch(opts._appSearchColumns);

        var api = $table.DataTable(opts);

        var $filterInput = $(api.table().container()).find('.dataTables_filter input');
        $filterInput.on('keyup', function () { api.draw(); });

        return api;
    };

})(window);