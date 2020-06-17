window.onclick = function (event) {
    var targetClasses = event.target.classList;
    if (!(targetClasses.contains('context-menu-btn') || targetClasses.contains('context-menu-btn-icon'))) {
        var dropdowns = this.document.getElementsByClassName("context-menu");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}