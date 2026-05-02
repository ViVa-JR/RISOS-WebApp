window.risosAnalytics = {
    trackEvent: function(eventName, eventParams) {
        if (typeof window.gtag !== "function") {
            return;
        }

        window.gtag("event", eventName, eventParams || {});
    }
};

