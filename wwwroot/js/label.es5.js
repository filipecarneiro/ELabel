// Function to switch theme
'use strict';

function switchTheme(theme) {
    var checkSwitchTheme = document.getElementById('checkSwitchTheme');

    document.documentElement.setAttribute('data-bs-theme', theme);
    switch (theme) {
        case 'light':
            checkSwitchTheme.checked = true;
            break;
        case 'dark':
            checkSwitchTheme.checked = false;
            break;
    }
}

// Select Theme
document.addEventListener('DOMContentLoaded', function () {
    var checkSwitchTheme = document.getElementById('checkSwitchTheme');
    var btnDarkTheme = document.getElementById('btnDarkTheme');
    var btnLightTheme = document.getElementById('btnLightTheme');
    checkSwitchTheme.addEventListener('click', function () {
        if (checkSwitchTheme.checked) {
            switchTheme('light');
        } else {
            switchTheme('dark');
        }
    });
    btnDarkTheme.addEventListener('click', function () {
        switchTheme('dark');
    });
    btnLightTheme.addEventListener('click', function () {
        switchTheme('light');
    });
});

// Select Language
document.addEventListener('DOMContentLoaded', function () {
    var formLanguage = document.getElementById('formLanguage');
    var selectlanguage = document.getElementById('selectlanguage');
    selectlanguage.onchange = function () {
        formLanguage.submit();
    };
});

//Auto Theme Selection
(function () {
    // Select the <html> element
    var htmlElement = document.querySelector("html");

    // Check if the data-bs-theme attribute is set to 'auto'
    if (htmlElement.getAttribute("data-bs-theme") === 'auto') {
        // Check user's color scheme preference
        if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
            // Switch to dark theme
            switchTheme('dark');
        }
        // Check user's color scheme preference
        else if (window.matchMedia && window.matchMedia('(prefers-color-scheme: light)').matches) {
                // Switch to light theme
                switchTheme('light');
            } else {
                //deafaults to dark as previously
                switchTheme('dark');
            }
    }
})();

