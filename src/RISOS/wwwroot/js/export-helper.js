window.downloadFile = function (fileName, content) {
    const blob = new Blob([content], { type: 'application/json' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');

    a.href = url;
    a.download = fileName;

    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);

    URL.revokeObjectURL(url);
};

window.captureElementAsImage = async function (elementId, fileName) {
    const element = document.getElementById(elementId);
    if (!element) {
        console.error('Element not found:', elementId);
        return;
    }

    const scrollContainer = document.getElementById('main-scroll-container');
    
    const captureWidth = scrollContainer ? Math.max(element.offsetWidth, scrollContainer.scrollWidth) : element.scrollWidth;

    const scrollContainerVisibleHeight = scrollContainer ? scrollContainer.offsetHeight : 0;
    const scrollContainerFullHeight = scrollContainer ? scrollContainer.scrollHeight : 0;
    const captureHeight = element.offsetHeight - scrollContainerVisibleHeight + scrollContainerFullHeight;

    try {
        const canvas = await html2canvas(element, {
            backgroundColor: getComputedStyle(document.documentElement).getPropertyValue('--mud-palette-background') || '#ffffff',
            scale: 2,
            useCORS: true,
            logging: false,
            width: captureWidth,
            height: captureHeight,
            windowWidth: captureWidth,
            windowHeight: captureHeight,
            onclone: (clonedDoc) => {
                const clonedElement = clonedDoc.getElementById(elementId);
                const clonedScrollContainer = clonedDoc.getElementById('main-scroll-container');
                
                if (clonedElement) {
                    clonedElement.style.width = captureWidth + 'px';
                    clonedElement.style.height = 'auto';
                    clonedElement.style.overflow = 'visible';
                }
                
                if (clonedScrollContainer) {
                    clonedScrollContainer.style.width = '100%';
                    clonedScrollContainer.style.height = 'auto';
                    clonedScrollContainer.style.overflow = 'visible';
                    clonedScrollContainer.style.maxWidth = 'none';
                    clonedScrollContainer.style.display = 'block';
                }
                
                const yearStack = clonedDoc.querySelector('#main-scroll-container > .mud-stack');
                if (yearStack) {
                    yearStack.style.width = 'max-content';
                    yearStack.style.minWidth = '100%';
                    yearStack.style.display = 'flex';
                    yearStack.style.flexDirection = 'row';
                }

                clonedDoc.querySelectorAll('.indicator-anchor, .hide-in-export').forEach(el => {
                    el.style.setProperty('display', 'none', 'important');
                });

                clonedDoc.querySelectorAll('.show-only-in-export').forEach(el => {
                    el.style.setProperty('display', 'flex', 'important');
                });
            }
        });

        const image = canvas.toDataURL("image/png");
        const link = document.createElement('a');
        link.download = fileName;
        link.href = image;
        link.click();
    } catch (err) {
        console.error('Failed to capture image:', err);
    }
};
