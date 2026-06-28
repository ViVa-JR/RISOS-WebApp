window.searchShortcut = {
    dotNetHelper: null,
    init: function (dotNetHelper) {
        this.dotNetHelper = dotNetHelper;
        window.addEventListener('keydown', this.handleKeyDown.bind(this));
    },
    handleKeyDown: function (e) {
        if ((e.ctrlKey || e.metaKey) && e.key === 'f') {
            e.preventDefault();
            if (window.risosAnalytics) {
                window.risosAnalytics.trackEvent('global_search_shortcut');
            }
            if (this.dotNetHelper) {
                this.dotNetHelper.invokeMethodAsync('OpenSearchFromJs');
            }
        }
        if (e.key === 'Escape') {
            if (this.dotNetHelper) {
                this.dotNetHelper.invokeMethodAsync('CloseSearchFromJs');
            }
        }
    },
    dispose: function () {
        window.removeEventListener('keydown', this.handleKeyDown.bind(this));
    },
    followAnchor: function (durationMs) {
        const start = performance.now();
        const tick = function (now) {
            window.dispatchEvent(new Event('resize'));
            window.dispatchEvent(new Event('scroll'));
            if (now - start < durationMs) {
                requestAnimationFrame(tick);
            }
        };
        requestAnimationFrame(tick);
    }
};
