﻿// Select Theme
'use strict';

document.addEventListener('DOMContentLoaded', function () {
    var htmlElement = document.documentElement;
    var checkSwitchTheme = document.getElementById('checkSwitchTheme');
    var btnDarkTheme = document.getElementById('btnDarkTheme');
    var btnLightTheme = document.getElementById('btnLightTheme');
    checkSwitchTheme.addEventListener('click', function () {
        if (checkSwitchTheme.checked) {
            htmlElement.setAttribute('data-bs-theme', 'light');
        } else {
            htmlElement.setAttribute('data-bs-theme', 'dark');
        }
    });
    btnDarkTheme.addEventListener('click', function () {
        checkSwitchTheme.checked = false;
        htmlElement.setAttribute('data-bs-theme', 'dark');
    });
    btnLightTheme.addEventListener('click', function () {
        checkSwitchTheme.checked = true;
        htmlElement.setAttribute('data-bs-theme', 'light');
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

