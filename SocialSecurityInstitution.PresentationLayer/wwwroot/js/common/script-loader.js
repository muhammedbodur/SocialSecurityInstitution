window.ScriptLoader = {
    // Yüklenmiş scriptleri takip et
    loadedScripts: [],

    // Yüklenmekte olan scriptleri takip et (Promise cache)
    loadingPromises: {},

    // Dependency kontrolü ve script yükleme
    loadScript: function (scriptPath, dependencies = [], callback = null) {
        // Zaten yüklendiyse tekrar yükleme
        if (this.loadedScripts.includes(scriptPath)) {
            if (callback) callback();
            return Promise.resolve();
        }

        // Eğer aynı script zaten yükleniyorsa, aynı promise'i döndür
        if (this.loadingPromises[scriptPath]) {
            return this.loadingPromises[scriptPath];
        }

        // Yeni promise oluştur ve cache'le
        const promise = new Promise((resolve, reject) => {
            // Dependency kontrolü - daha detaylı
            const checkDependencies = () => {
                const missingDeps = [];

                dependencies.forEach(dep => {
                    switch (dep.toLowerCase()) {
                        case 'jquery':
                            if (typeof $ === 'undefined' || typeof jQuery === 'undefined') {
                                missingDeps.push('jQuery');
                            }
                            break;
                        case 'select2':
                            // Select2 kontrolü - hem window hem de jQuery plugin olarak kontrol et
                            if (typeof $ === 'undefined' ||
                                typeof $.fn === 'undefined' ||
                                typeof $.fn.select2 === 'undefined') {
                                missingDeps.push('Select2');
                            }
                            break;
                        case 'bootstrap':
                            if (typeof bootstrap === 'undefined' &&
                                (typeof $ === 'undefined' || typeof $.fn.modal === 'undefined')) {
                                missingDeps.push('Bootstrap');
                            }
                            break;
                        case 'toastr':
                            if (typeof toastr === 'undefined') {
                                missingDeps.push('Toastr');
                            }
                            break;
                        case 'sweetalert':
                            if (typeof Swal === 'undefined') {
                                missingDeps.push('SweetAlert');
                            }
                            break;
                        default:
                            // Bilinmeyen dependency'ler için window objesi kontrolü
                            if (typeof window[dep] === 'undefined') {
                                console.warn(`Unknown dependency: ${dep}`);
                            }
                            break;
                    }
                });

                return missingDeps;
            };

            const attemptLoad = () => {
                const missingDeps = checkDependencies();

                if (missingDeps.length > 0) {
                    console.log(`Waiting for dependencies for ${scriptPath}:`, missingDeps);

                    // 200ms bekleyip tekrar dene (max 25 kez = 5 saniye)
                    if (this.retryCount < 25) {
                        this.retryCount++;
                        setTimeout(attemptLoad, 200);
                        return;
                    } else {
                        const error = `Failed to load dependencies: ${missingDeps.join(', ')}`;
                        console.error(error);
                        // Promise'i cache'den kaldır
                        delete this.loadingPromises[scriptPath];
                        reject(new Error(error));
                        return;
                    }
                }

                console.log(`✅ All dependencies ready for ${scriptPath}`);

                // Dependency'ler hazır, script'i yükle
                $.getScript(scriptPath)
                    .done(() => {
                        this.loadedScripts.push(scriptPath);
                        console.log(`✅ Script loaded: ${scriptPath}`);

                        // Promise'i cache'den kaldır
                        delete this.loadingPromises[scriptPath];

                        if (callback) callback();
                        resolve();
                    })
                    .fail((jqxhr, textStatus, error) => {
                        // 404 hatalarını daha sessiz handle et
                        if (jqxhr.status === 404) {
                            console.log(`ℹ️ Script not found (expected): ${scriptPath}`);
                            delete this.loadingPromises[scriptPath];
                            resolve(); // 404'ü başarı olarak say
                        } else {
                            console.error(`❌ Failed to load script: ${scriptPath}`, error);
                            delete this.loadingPromises[scriptPath];
                            reject(error);
                        }
                    });
            };

            this.retryCount = 0;

            // DOM ready olana kadar bekle
            $(document).ready(() => {
                // Biraz daha bekle ki tüm dependency'ler yüklensin
                setTimeout(attemptLoad, 100);
            });
        });

        // Promise'i cache'e ekle
        this.loadingPromises[scriptPath] = promise;

        return promise;
    },

    // Page-specific script yükleme
    loadPageScript: function (controller, action, dependencies = ['jquery']) {
        const scriptPath = `/js/views/${controller.toLowerCase()}/${action.toLowerCase()}.js`;
        return this.loadScript(scriptPath, dependencies);
    },

    // Multiple script yükleme
    loadMultipleScripts: function (scripts) {
        const promises = scripts.map(script => {
            return this.loadScript(script.path, script.dependencies || [], script.callback);
        });

        return Promise.all(promises);
    },

    // Script cache'ini temizle (debugging için)
    clearCache: function () {
        this.loadedScripts = [];
        this.loadingPromises = {};
        console.log('Script cache cleared');
    },

    // Yüklenen scriptleri listele (debugging için)
    getLoadedScripts: function () {
        return this.loadedScripts.slice(); // copy döndür
    },

    // Dependency'lerin yüklü olup olmadığını kontrol et
    checkDependency: function (depName) {
        switch (depName.toLowerCase()) {
            case 'jquery':
                return typeof $ !== 'undefined' && typeof jQuery !== 'undefined';
            case 'select2':
                return typeof $ !== 'undefined' && typeof $.fn !== 'undefined' && typeof $.fn.select2 !== 'undefined';
            case 'bootstrap':
                return typeof bootstrap !== 'undefined' || (typeof $ !== 'undefined' && typeof $.fn.modal !== 'undefined');
            case 'toastr':
                return typeof toastr !== 'undefined';
            default:
                return typeof window[depName] !== 'undefined';
        }
    }
};