window.makeDraggableWithHandle = function (containerId, handleId) {
    const container = document.getElementById(containerId);
    const handle = document.getElementById(handleId);
    if (!container || !handle) return;

    // Cargar posición guardada
    const savedPos = localStorage.getItem(containerId);
    if (savedPos) {
        const { top, left } = JSON.parse(savedPos);
        container.style.top = top + "px";
        container.style.left = left + "px";
        container.style.bottom = "";
        container.style.right = "";
    }

    let offsetX, offsetY, isDragging = false;

    handle.onmousedown = function (e) {
        isDragging = true;
        offsetX = e.clientX - container.getBoundingClientRect().left;
        offsetY = e.clientY - container.getBoundingClientRect().top;
        document.addEventListener("mousemove", onMouseMove);
        document.addEventListener("mouseup", onMouseUp);
    };

    function onMouseMove(e) {
        if (!isDragging) return;
        container.style.top = (e.clientY - offsetY) + "px";
        container.style.left = (e.clientX - offsetX) + "px";
        container.style.bottom = "";
        container.style.right = "";
    }

    function onMouseUp() {
        if (!isDragging) return;
        isDragging = false;
        const rect = container.getBoundingClientRect();
        localStorage.setItem(containerId, JSON.stringify({ top: rect.top, left: rect.left }));
        document.removeEventListener("mousemove", onMouseMove);
        document.removeEventListener("mouseup", onMouseUp);
    }
};
