let verticalScrollSpeed = 0;
let isScrolling = false;

function performVerticalScroll() {
    const container = document.getElementById('main-scroll-container');
    if (verticalScrollSpeed !== 0 && container) {
        container.scrollBy(0, verticalScrollSpeed);
        requestAnimationFrame(performVerticalScroll);
        isScrolling = true;
    } else {
        isScrolling = false;
    }
}

window.initDragScroll = () => {
    const scrollContainer = document.getElementById('main-scroll-container');
    if (!scrollContainer) return;

    const endDrag = () => {
        if (scrollContainer.classList.contains('is-dragging')) {
            scrollContainer.classList.remove('is-dragging');
            verticalScrollSpeed = 0;
        }
    };

    document.addEventListener('dragstart', (e) => {
        if (e.target.closest('.mud-drop-item')) {
            scrollContainer.classList.add('is-dragging');
        }
    });

    document.addEventListener('dragend', endDrag, true);
    document.addEventListener('drop', endDrag, true);
    document.addEventListener('mouseup', endDrag, true);

    scrollContainer.addEventListener('dragover', (e) => {
        e.preventDefault();

        if (e.dataTransfer) {
            e.dataTransfer.dropEffect = 'move';
        }

        const rect = scrollContainer.getBoundingClientRect();
        const relativeY = e.clientY - rect.top;

        const threshold = 60;
        const maxSpeed = 15;

        if (relativeY < threshold && relativeY > 0) {
            const intensity = (threshold - relativeY) / threshold;
            verticalScrollSpeed = -(maxSpeed * intensity);

            if (!isScrolling) performVerticalScroll();

        } else if (relativeY > rect.height - threshold && relativeY < rect.height) {
            const distanceFromBottom = rect.height - relativeY;
            const intensity = (threshold - distanceFromBottom) / threshold;
            verticalScrollSpeed = (maxSpeed * intensity);

            if (!isScrolling) performVerticalScroll();

        } else {
            verticalScrollSpeed = 0;
        }
    });
};
