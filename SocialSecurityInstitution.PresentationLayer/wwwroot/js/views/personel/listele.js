(function () {
    function initializeDataTable() {
        // Wait for jQuery and DataTables
        if (typeof $ === 'undefined' || typeof $.fn === 'undefined' || typeof $.fn.DataTable === 'undefined') {
            setTimeout(initializeDataTable, 300);
            return;
        }

        // Optional: collapse left menu as in original code
        $('#socialSecurityIsntution').addClass('layout-menu-collapsed');

        // Initialize DataTable via global helper for consistent UI/UX
        const table = (window.App && App.DataTables && typeof App.DataTables.init === 'function')
            ? App.DataTables.init('#userTable', {
                // Responsive ayarları düzeltildi
                responsive: true,
                autoWidth: false,
                searchColumns: [2, 3, 4, 5, 6, 7, 8],
                columnDefs: [
                    // 0: Sıra/checkbox (highest priority, narrow)
                    { targets: 0, orderable: false, className: 'text-center', width: 36, responsivePriority: 1 },
                    // -1: Actions (keep visible, nowrap)
                    { targets: -1, className: 'text-nowrap', width: 200, responsivePriority: 2, orderable: false },
                    // 2-3: Primary identity columns (e.g., Ad Soyad, TCKN)
                    { targets: 2, responsivePriority: 3, width: 160 },
                    { targets: 3, responsivePriority: 4, width: 140 },
                    // 4-5: Secondary info
                    { targets: 4, responsivePriority: 5, width: 160 },
                    { targets: 5, responsivePriority: 6, width: 140 },
                    // 6-7-8: Least critical; collapse first on small screens
                    { targets: 6, responsivePriority: 80 },
                    { targets: 7, responsivePriority: 90 },
                    { targets: 8, responsivePriority: 100 },
                    // Default: allow wrapping
                    { targets: '_all', className: 'text-wrap align-middle' }
                ],
                onInit: function (api) {
                    try {
                        api.columns.adjust().responsive.recalc();
                    } catch (e) {
                        console.warn('Initial column adjust error:', e);
                    }
                    setTimeout(function () {
                        try {
                            api.columns.adjust().responsive.recalc();
                        } catch (e) {
                            console.warn('Delayed column adjust error:', e);
                        }
                    }, 250);
                }
            })
            : $('#userTable').DataTable({
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                responsive: true,
                fixedHeader: false,
                autoWidth: false,
                scrollX: false,
                pagingType: 'simple_numbers',
                pageLength: 25,
                lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, 'Tümü']],
                columnDefs: [{ orderable: false, targets: [0, -1] }],
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
                }
            });

        // Bind search input to redraw
        $(document).off('keyup', '#userTable_filter input').on('keyup', '#userTable_filter input', function () {
            if (table && typeof table.draw === 'function') {
                table.draw();
            }
        });

        // Active/Passive toggle handler
        $(document).off('click', '.active-passive-button').on('click', '.active-passive-button', function (e) {
            e.preventDefault();
            const tcKimlikNo = $(this).data('bs-whatever');
            if (!tcKimlikNo) return;

            $.ajax({
                url: '/Personel/AktifPasifEt',
                type: 'GET',
                data: { TcKimlikNo: tcKimlikNo },
                success: function (result) {
                    try {
                        if (result && typeof result.islemDurum !== 'undefined') {
                            if (window.toastr && result.mesaj) {
                                if (result.islemDurum == 1) {
                                    toastr.success(result.mesaj);
                                } else {
                                    toastr.error(result.mesaj);
                                }
                            }

                            if (result.islemDurum == 1) {
                                // Update badge class/text
                                const durum = (result.aktiflikDurum || '').toString();
                                const badgeClass = durum === 'Aktif' ? 'bg-label-success' : (durum === 'Emekli' ? 'bg-label-warning' : 'bg-label-danger');
                                const $badge = $('#aktiflik_span_' + tcKimlikNo);
                                if ($badge.length) {
                                    $badge.removeClass('bg-label-success bg-label-danger bg-label-warning').addClass(badgeClass).text(durum);
                                }

                                // Update button icon/text
                                const $btn = $('#activePassiveButton_' + tcKimlikNo);
                                if ($btn.length) {
                                    if (durum === 'Aktif') {
                                        $btn.find('i').removeClass('bx-play-circle').addClass('bx-pause-circle');
                                        $btn.find('span').text('Pasif Et');
                                    } else {
                                        $btn.find('i').removeClass('bx-pause-circle').addClass('bx-play-circle');
                                        $btn.find('span').text('Aktif Et');
                                    }
                                }
                            }
                        }
                    } catch (err) {
                        console.error('Ajax success handler error:', err);
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Ajax error:', error);
                    if (window.toastr) {
                        toastr.error('İşlem sırasında hata oluştu: ' + error);
                    }
                }
            });
        });

        // Window resize handler
        $(window).off('resize.userTableResize').on('resize.userTableResize', function () {
            if (table && typeof table.columns === 'object') {
                try {
                    table.columns.adjust().responsive.recalc();
                } catch (e) {
                    console.warn('Resize adjust error:', e);
                }
            }
        });

        console.log('✅ Personel/Listele DataTable initialized');
    }

    // Wait DOM ready
    if (typeof $ !== 'undefined') {
        $(document).ready(function () {
            // Defer slightly to let vendor scripts load via _Scripts
            setTimeout(initializeDataTable, 500);
        });
    } else {
        // Fallback if jQuery not yet available
        setTimeout(initializeDataTable, 1000);
    }
})();