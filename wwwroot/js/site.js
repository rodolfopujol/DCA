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

// Toggle sidebar for mobile
document.addEventListener('DOMContentLoaded', function () {
    const sidebar = document.querySelector('.sidebar');
    const toggle = document.getElementById('sidebarToggle');

    if (sidebar && toggle) {
        toggle.addEventListener('click', function () {
            sidebar.classList.toggle('open');
        });
    }
});
