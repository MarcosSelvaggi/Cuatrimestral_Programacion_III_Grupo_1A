document.addEventListener("DOMContentLoaded", function () {
    const toggles = [document.getElementById("sidebarToggle"), document.getElementById("sidebarToggleMobile")];

    toggles.forEach(function (btn) {
        if (btn) {
            btn.addEventListener("click", function (e) {
                e.preventDefault();
                document.body.classList.toggle("sidenav-toggled");
            });
        }
    });

    const gestionToggle = document.querySelector('[data-bs-target="#collapseGestion"]');
    const collapseGestion = document.getElementById("collapseGestion");
    const arrowIcon = gestionToggle.querySelector(".rotate-icon");

    collapseGestion.addEventListener('show.bs.collapse', function () {
        arrowIcon.classList.add("rotated");
    });

    collapseGestion.addEventListener('hide.bs.collapse', function () {
        arrowIcon.classList.remove("rotated");
    });
});