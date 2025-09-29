"use strict";

const urlParams = new URLSearchParams(window.location.search);
const tvId = urlParams.get('TvId');

if (tvId) {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/TvHub", { accessTokenFactory: () => tvId })
        .withAutomaticReconnect() // Otomatik yeniden bağlanma
        .configureLogging(signalR.LogLevel.Information)
        .build();

    if (connection.state === signalR.HubConnectionState.Disconnected) {
        connection.start().then(function () {
            console.log('Hub\'a Bağlanıldı');
            OnConnected();
            hidePersistentAlert(); // Bağlantı kurulduğunda uyarıyı kaldır

            // İstemci ayarları
            connection.serverTimeoutInMilliseconds = 120000; // 2 dakika (Sunucudan mesaj almayı beklediği süre)
            connection.keepAliveIntervalInMilliseconds = 60000; // 1 dakika (Sunucuya keep-alive mesajı gönderme aralığı)
        }).catch(function (err) {
            console.error("Hub'a bağlanırken hata oluştu: " + err.toString());
            setTimeout(() => connection.start(), 5000); // Bağlantı hatasında tekrar bağlanmayı deniyoruz
            showPersistentAlert("Sunucuya bağlanılamadı. Bağlantı yeniden sağlanana kadar bekleyin."); // Uyarıyı göster
        });
    }
} else {
    console.error("TvId parametresi eksik!");
    showPersistentAlert("Bağlantı Kurulamadı: TvId Bilgisi Eksik!");
}

async function OnConnected() {
    try {
        // Kullanıcı bilgilerini doğrudan sunucudan alıyoruz
        await connection.invoke("SaveTvConnection");
        console.log("Kullanıcı bağlantısı başarıyla kaydedildi.");
    } catch (err) {
        console.error("SaveTvConnection çağrılırken hata oluştu: " + err.toString());
        // İstemciye bir hata bildirimi gönderiyorum ama script'in devam etmesini sağlıyorum
        showPersistentAlert("Sunucuya bağlanırken bir hata oluştu, lütfen tekrar deneyin.");
    }
}

// Sunucuya, client'ın halen daha login olup olmadığını sormakta
setInterval(function () {
    connection.invoke("PingServer").catch(function (err) {
        console.error(err.toString());
    });
}, 60000); // Her dakika sormakta

// Kalıcı uyarı elementini ekliyoruz, amacımız connection sorunu var bilgisi vermek
function showPersistentAlert(message) {
    var alertBox = document.getElementById('persistent-alert');
    if (!alertBox) {
        alertBox = document.createElement('div');
        alertBox.id = 'persistent-alert';
        alertBox.style.position = 'fixed';
        alertBox.style.top = '0';
        alertBox.style.width = '100%';
        alertBox.style.backgroundColor = 'red';
        alertBox.style.color = 'white';
        alertBox.style.textAlign = 'center';
        alertBox.style.padding = '10px';
        alertBox.style.zIndex = '9999';
        document.body.appendChild(alertBox);
    }
    alertBox.textContent = message;
}

// Kalıcı uyarıyı kaldır
function hidePersistentAlert() {
    var alertBox = document.getElementById('persistent-alert');
    if (alertBox) {
        alertBox.remove();
    }
}

let siraQueue = [];
let isProcessing = false;

if (connection) {
    connection.on("ReceiveTvSiraBilgisi", function (siralar, action) {
        if (siralar && siralar.length > 0) {
            siraQueue.push(siralar);
            processQueue();
        } else {
            console.error("Sira listesi boş.");
        }
    });
}

function processQueue() {
    if (isProcessing || siraQueue.length === 0) {
        return;
    }

    isProcessing = true;
    let siralar = siraQueue.shift(); // Kuyruktaki ilk elemanı alıyorum

    try {
        // Sıra bilgilerini güncelleme
        $("#banko-numarasi").text(siralar[0].bankoNo);
        $("#sira-numarasi").text(siralar[0].siraNo);

        for (let i = 0; i < 5; i++) {
            if (i < siralar.length) {
                $("#banko_no_" + (i + 1)).text(siralar[i].bankoNo);
                $("#banko_kat_" + (i + 1)).text(siralar[i].katTipiDisplayName);
                $("#banko_sira_" + (i + 1)).text(siralar[i].siraNo);
            } else {
                $("#banko_no_" + (i + 1)).text("-");
                $("#banko_kat_" + (i + 1)).text("-");
                $("#banko_sira_" + (i + 1)).text("-");
            }
        }

        toggleSections();
        playDingDongSound();
        setTimeout(() => {
            toggleSections();
            isProcessing = false; // İşlem bitti
            processQueue(); // Kuyruğu tekrar işleme alıyorum
        }, 4000);

    } catch (err) {
        console.error("Sıra işlem hatası: ", err);
        isProcessing = false; // Hata durumunda da işlem bitti sayılır
        processQueue(); // Kuyruğu tekrar işleme alıyorum
    }
}

function playDingDongSound() {
    var dingDongSound = document.getElementById("dingDongSound");

    // Sesi durdurup başa sarıyorum
    dingDongSound.pause();
    dingDongSound.currentTime = 0;

    // Sesi tekrar çalıyorum
    dingDongSound.play().catch(function (err) {
        console.error("Ses çalma hatası: ", err);
    });
}

function toggleSections() {
    const $header = $('#header');
    const $siraSection = $('#SiraSection');
    const $listeSection = $('#ListeSection');

    if ($siraSection.is(':visible')) {
        $header.show();
        $siraSection.hide();
        $listeSection.show();
    } else {
        $listeSection.hide();
        $siraSection.show();
        $header.hide();
    }    
}
