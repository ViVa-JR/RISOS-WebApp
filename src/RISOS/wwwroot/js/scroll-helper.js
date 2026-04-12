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

    document.addEventListener('dragstart', (e) => {
        if (e.target.closest('.mud-drop-item')) {
            scrollContainer.classList.add('is-dragging');
        }
    });

    document.addEventListener('dragend', () => {
        scrollContainer.classList.remove('is-dragging');
        verticalScrollSpeed = 0;
    });

    scrollContainer.addEventListener('dragover', (e) => {
        // 1. MUST HAVE: Tells the browser the entire scrolling area is a valid drag zone
        e.preventDefault();

        // 2. MUST HAVE: Forces the cursor to stay as the 'move/grabbing' hand
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