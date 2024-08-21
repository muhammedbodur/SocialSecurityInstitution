document.getElementById('callPanelToggleCustomizerButton').addEventListener('click', function () {
    var panel = document.getElementById('template-customizer');
    var button = document.getElementById('callPanelToggleCustomizerButton');
    panel.classList.toggle('show');
    button.classList.toggle('hide');
});

document.addEventListener('click', function (event) {
    var panel = document.getElementById('template-customizer');
    var button = document.getElementById('callPanelToggleCustomizerButton');

    // Eğer panelin dışına tıklanırsa
    if (!panel.contains(event.target) && !button.contains(event.target)) {
        panel.classList.remove('show');
        button.classList.remove('hide');
    }
});