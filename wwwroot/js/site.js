// Validaciones básicas para formularios.
(function () {
    const forms = document.querySelectorAll('form');
    forms.forEach(f => {
        f.addEventListener('submit', function (e) {
            if (!f.checkValidity()) {
                e.preventDefault();
                e.stopPropagation();
            }
            f.classList.add('was-validated');
        });
    });
})();
