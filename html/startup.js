document.addEventListener('DOMContentLoaded', function() {
    const sidenavs = document.querySelectorAll('.sidenav');
    M.Sidenav.init(sidenavs, {});
    
    const tooltips = document.querySelectorAll('.tooltipped');
    M.Tooltip.init(tooltips, {});
});