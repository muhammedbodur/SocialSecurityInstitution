// Sabitleme durumunu kontrol etmek için değişken
var isPinned = false;

// Sabitleme ikonu elemanı
var panelHeader = document.getElementById('panel-header');
var panel = document.getElementById('template-customizer');
var button = document.getElementById('callPanelToggleCustomizerButton');
var body = document.body;

// Sayfa yüklendiğinde panelin durumunu kontrol et
document.addEventListener('DOMContentLoaded', function () {
    var isPinnedFromStorage = localStorage.getItem('isPinned');
    var isPanelVisibleFromStorage = localStorage.getItem('isPanelVisible');

    if (isPinnedFromStorage === 'true') {
        isPinned = true;
    } else {
        isPinned = false;
    }

    updatePanelHeaderBackground();

    if (isPanelVisibleFromStorage === 'true') {
        panel.classList.add('show');
        button.classList.add('hide');
        body.classList.add('layout-shifted');
    } else {
        panel.classList.remove('show');
        button.classList.remove('hide');
        body.classList.remove('layout-shifted');
    }
});

// Paneli açma/kapatma butonu olay dinleyicisi
button.addEventListener('click', function () {
    panel.classList.toggle('show');  // Paneli aç/kapa
    button.classList.toggle('hide'); // Butonu gizle/göster
    body.classList.toggle('layout-shifted');  // Sayfa içeriğini kaydır
    isPinned = false; // Butona basıldığında sabitleme iptal edilsin

    // Panelin görünürlüğünü kaydet
    localStorage.setItem('isPanelVisible', panel.classList.contains('show') ? 'true' : 'false');

});

// Sabitleme simgesine tıklanarak sabitleme durumu değiştirilir
panelHeader.addEventListener('click', function (event) {
    isPinned = !isPinned;  // Sabitleme durumu değiştirilir
    localStorage.setItem('isPinned', isPinned);  // Sabitleme durumunu kaydet
    event.stopPropagation();  // Panel içindeki tıklamaları diğer olay dinleyicilerinden izole et

    updatePanelHeaderBackground();
});

// Sayfada herhangi bir yere tıklandığında panelin kapanması için olay dinleyicisi
document.addEventListener('click', function (event) {
    // Eğer tıklanan yer panelin dışı ve toggle butonu değilse ve panel sabitlenmemişse paneli kapat
    if (!panel.contains(event.target) && !button.contains(event.target) && !isPinned) {
        panel.classList.remove('show');
        button.classList.remove('hide');
        body.classList.remove('layout-shifted');
        localStorage.setItem('isPanelVisible', 'false');  // Panelin kapandığını kaydet
    }
});

function updatePanelHeaderBackground() {
    if (isPinned) {
        panelHeader.style.backgroundColor = '#696bff';
    } else {
        panelHeader.style.backgroundColor = '#d2e2fc';
    }
}