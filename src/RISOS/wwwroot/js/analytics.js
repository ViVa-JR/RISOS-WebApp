window.dataLayer = window.dataLayer || [];
function gtag() { dataLayer.push(arguments); }

if (localStorage.getItem('analytics_consent') === 'granted') {
    gtag('consent', 'default', {
        'analytics_storage': 'granted',
        'ad_storage': 'granted',
        'ad_user_data': 'granted',
        'ad_personalization': 'granted'
    });
} else {
    gtag('consent', 'default', {
        'analytics_storage': 'denied',
        'ad_storage': 'denied',
        'ad_user_data': 'denied',
        'ad_personalization': 'denied'
    });
}

gtag('js', new Date());
gtag('config', 'G-NTFQJ99LG4');

window.risosAnalytics = {
    trackEvent: function(eventName, eventParams) {
        if (typeof window.gtag !== "function") {
            return;
        }

        window.gtag("event", eventName, eventParams || {});
    },

    updateConsent: function(granted) {
        if (typeof window.gtag !== "function") {
            return;
        }

        const status = granted ? "granted" : "denied";
        window.gtag("consent", "update", {
            "analytics_storage": status,
            "ad_storage": status,
            "ad_user_data": status,
            "ad_personalization": status
        });
    }
};

