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
    const originalStyles = new Map();

    const saveStyle = (el) => {
        if (el) originalStyles.set(el, el.getAttribute('style') || '');
    };

    const restoreStyles = () => {
        originalStyles.forEach((style, el) => {
            if (style) el.setAttribute('style', style);
            else el.removeAttribute('style');
        });
    };

    try {
        // 1. Prepare for capture: Expand everything to full scroll size
        saveStyle(element);
        saveStyle(scrollContainer);

        if (scrollContainer) {
            // Force the scroll container to show all its horizontal content
            scrollContainer.style.overflow = 'visible';
            scrollContainer.style.width = 'max-content';
            scrollContainer.style.height = 'auto';
            scrollContainer.style.maxWidth = 'none';
        }

        element.style.overflow = 'visible';
        element.style.width = 'max-content';
        element.style.height = 'auto';
        element.style.maxWidth = 'none';

        // 2. Perform capture
        const canvas = await html2canvas(element, {
            backgroundColor: getComputedStyle(document.body).getPropertyValue('--mud-palette-background'),
            scale: 2,
            useCORS: true,
            logging: false,
            // Calculate actual scroll dimensions
            width: element.scrollWidth,
            height: element.scrollHeight,
            windowWidth: element.scrollWidth,
            windowHeight: element.scrollHeight,
            onclone: (clonedDoc) => {
                const clonedExportZone = clonedDoc.getElementById(elementId);
                const clonedScrollContainer = clonedDoc.getElementById('main-scroll-container');
                
                if (clonedExportZone) {
                    clonedExportZone.style.width = 'max-content';
                    clonedExportZone.style.height = 'auto';
                    clonedExportZone.style.overflow = 'visible';
                }
                if (clonedScrollContainer) {
                    clonedScrollContainer.style.width = 'max-content';
                    clonedScrollContainer.style.height = 'auto';
                    clonedScrollContainer.style.overflow = 'visible';
                    clonedScrollContainer.style.maxWidth = 'none';
                }
                
                // Hide scroll indicators and marked control elements in the export
                const toHide = clonedDoc.querySelectorAll('.indicator-anchor, .hide-in-export');
                toHide.forEach(el => el.style.setProperty('display', 'none', 'important'));

                // Show elements that are marked to be shown only in export
                const toShow = clonedDoc.querySelectorAll('.show-only-in-export');
                toShow.forEach(el => el.style.setProperty('display', 'flex', 'important'));
            }
        });

        // 3. Download
        const image = canvas.toDataURL("image/png");
        const link = document.createElement('a');
        link.download = fileName;
        link.href = image;
        link.click();
    } catch (err) {
        console.error('Failed to capture image:', err);
    } finally {
        restoreStyles();
    }
};
