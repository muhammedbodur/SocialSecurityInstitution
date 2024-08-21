"use strict";
function getJwtToken() {
    const name = "JwtToken=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookies = decodedCookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let c = cookies[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/dashboardHub", {
        accessTokenFactory: () => getJwtToken() // JWT token'ı burada gönderiyoruz
    })
    .withAutomaticReconnect() // Otomatik yeniden bağlanma
    .configureLogging(signalR.LogLevel.Information)
    .build();

// İstemci ayarları
connection.serverTimeoutInMilliseconds = 120000; // 2 dakika (Sunucudan mesaj almayı beklediği süre)
connection.keepAliveIntervalInMilliseconds = 60000; // 1 dakika (Sunucuya keep-alive mesajı gönderme aralığı)

// Sunucuya, client'ın halen daha login olup olmadığını sormakta
setInterval(function () {
    connection.invoke("PingServer").catch(function (err) {
        console.error(err.toString());
    });
}, 60000); // Her dakika sormakta

connection.on("SessionExpired", function () {
    showPersistentAlert("Oturum süreniz dolmuş. Lütfen yeniden giriş yapın!");
    // Oturum süresinin dolduğunun bilgisini veriyoruz
});

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

// SignalR bağlantısı başlatıldığında
connection.start().then(function () {
    console.log('Hub\'a Bağlanıldı');
    OnConnected();
    updateUserStatus(true);
    hidePersistentAlert(); // Bağlantı kurulduğunda uyarıyı kaldır
}).catch(function (err) {
    console.error("Hub'a bağlanırken hata oluştu: " + err.toString());
    setTimeout(() => connection.start(), 5000); // Bağlantı hatasında tekrar bağlanmayı deniyoruz
    updateUserStatus(false);
    showPersistentAlert("Sunucuya bağlanılamadı. Bağlantı yeniden sağlanana kadar bekleyin."); // Uyarıyı göster
});

// SignalR'dan gelen güncellemeleri yönet
connection.on("ReceiveUpdates", function (tableName, entity, action) {
    console.log(`${tableName} tablosu için ${action} işlemi yapıldı!`);
    console.log(entity);

    if (action === "INSERT") {
        toastr.success(`${tableName} tablosunda ${action} işlemi yapıldı`);
    } else if (action === "UPDATE") {
        toastr.info(`${tableName} tablosunda ${action} işlemi yapıldı`);
    } else if (action === "DELETE") {
        toastr.error(`${tableName} tablosunda ${action} işlemi yapıldı`);
    }
});

connection.on("ReceiveNotification", function (message, title) {
    toastr.info(message, title);
});

connection.on("ReceiveError", function (errorMessage) {
    toastr.error(errorMessage, "Hata!");
});

// Bağlantı yeniden sağlanırken çalışacak
connection.onreconnecting((error) => {
    console.warn(`SignalR bağlantısı yeniden bağlanıyor: ${error}`);
    updateUserStatus(false); // Bağlantı tekrar sağlanırken kullanıcı çevrimdışı olarak işaretlenebilir
    showPersistentAlert("Sunucuya yeniden bağlanılıyor. Lütfen bekleyin."); // Uyarıyı gösteriyorum
});

// Bağlantı yeniden sağlandığında
connection.onreconnected((connectionId) => {
    console.log(`SignalR bağlantısı yeniden bağlandı. ConnectionId: ${connectionId}`);
    OnConnected(); // Yeniden bağlandığında bağlantıyı kaydediyorum
    updateUserStatus(true);
    hidePersistentAlert(); // Uyarıyı kaldırıyorum
});

// Bağlantı kesildiğinde (Kalıcı uyarı göster)
connection.onclose((error) => {
    console.error(`SignalR bağlantısı kapandı: ${error}`);
    updateUserStatus(false); // Kullanıcı çevrimdışı olarak işaretleniyor
    showPersistentAlert("Bağlantı kurulamıyor. Lütfen internet bağlantınızı kontrol edin."); // Uyarıyı gösteriyorum

    // Kullanıcının durumu güncelleniyor
    connection.invoke("UpdateUserConnectionStatus", "offline").catch(function (err) {
        return console.error(err.toString());
    });
});

async function OnConnected() {
    try {
        // Kullanıcı bilgilerini doğrudan sunucudan alıyoruz
        await connection.invoke("SaveUserConnection");
        console.log("Kullanıcı bağlantısı başarıyla kaydedildi.");
        updateUserStatus(true);
    } catch (err) {
        console.error("SaveUserConnection çağrılırken hata oluştu: " + err.toString());
        updateUserStatus(false);
        // İstemciye bir hata bildirimi gönderiyorum ama script'in devam etmesini sağlıyorum
        toastr.error("Sunucuya bağlanırken bir hata oluştu, lütfen tekrar deneyin.", "Bağlantı Hatası");
    }
}

// Sıra alma butonuna tıklama olayını dinle
document.getElementById("callNextButton").addEventListener("click", async function () {
    var button = document.getElementById("callNextButton");

    // Butonu devre dışı bırak
    button.disabled = true;

    try {
        await connection.invoke("GetSiraCagirma");
    } catch (err) {
        console.error("Sıra alma işlemi sırasında hata oluştu: " + err.toString());
        toastr.error("Sıra alma işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin.", "Hata");
    }
    
    setTimeout(function () {
        button.disabled = false;
    }, 5000);
});

connection.on("ReceiveSiraCagirmaBilgisi", function (siraCagirma) {
    try {
        if (siraCagirma != null) {
            //console.log(siraCagirma);
            toastr.success(`Sıra Sizin Tarafınızdan Çağrıldı: ${siraCagirma.siraNo}`);
            // Mevcut sıranın arkaplan renginin güncellenmesi
            $("#sira_" + siraCagirma.siraId).css("background-color", '#dff0d8');
        }
    } catch (err) {
        console.error("Sıra Çağırma İşlem hatası: ", err);
    }
});

connection.on("ReceiveSiraBilgisi", function (siralar, action) {
    console.log(siralar);
    try {
        const siraListeUl = $('#siraListe');
        siraListeUl.empty();
        if (siralar != null && siralar.length > 0) {
            $.each(siralar, function (index, siraBilgisi) {
                if ((siraBilgisi.islemiYapan == 'kendisi' && siraBilgisi.beklemeDurum == '1') || siraBilgisi.beklemeDurum == '0') {
                    const backGroundColor = (siraBilgisi.islemiYapan == 'kendisi' && siraBilgisi.beklemeDurum == '1' ? ' background-color:#dff0d8;' : '');
                    const liHtml = `
                        <li id="sira_${siraBilgisi.siraId}" class="card mb-2 p-2 shadow-sm w-100" style="height: auto; min-height: 70px;${backGroundColor}">
                            <div class="card-body text-center p-2">
                                <h5 class="card-title" style="font-size: 1.50rem; font-weight: bold; margin-bottom: 0.25rem;">
                                    ${siraBilgisi.siraNo}
                                </h5>
                                <p class="card-text" style="font-size: 1rem; color: #555; margin-bottom: 0;">
                                    ${siraBilgisi.kanalAltAdi}
                                </p>
                            </div>
                        </li>
                    `;
                    siraListeUl.append(liHtml);
                }
            });
        }
    } catch (err) {
        console.error("Sıra işlem hatası: ", err);
    }
});

connection.on("ReceiveError", function (errorMessage) {
    toastr.error(errorMessage, "Hata!");
});

// Kullanıcının durumunu güncelliyorum
async function updateUserStatus(isOnline) {
    if (isOnline) {
        $(".avatar").removeClass("avatar-offline").addClass("avatar-online");
        $("#layout-navbar").attr("style", "background-color: #edfade !important;");
        $("#callPanelToggleCustomizerButton").prop("disabled", false);
    } else {
        $(".avatar").removeClass("avatar-online").addClass("avatar-offline");
        $("#layout-navbar").attr("style", "background-color: #ededed !important;");
        $("#callPanelToggleCustomizerButton").prop("disabled", true);
    }
}