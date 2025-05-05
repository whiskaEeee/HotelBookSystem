function autoResizeTextarea(el) {
    el.style.height = 'auto';
    el.style.height = el.scrollHeight + 'px';
}

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('textarea.auto-resize').forEach(function (el) {
        autoResizeTextarea(el);
    });
});

document.addEventListener('input', function (e) {
    if (e.target.classList.contains('auto-resize')) {
        autoResizeTextarea(e.target);
    }
});