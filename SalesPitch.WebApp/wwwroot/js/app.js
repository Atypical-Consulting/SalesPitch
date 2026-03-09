// JavaScript functions for SalesPitch web app

window.scrollToBottom = (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollTop = element.scrollHeight;
    }
};

window.copyToClipboard = async (text) => {
    try {
        await navigator.clipboard.writeText(text);
        return true;
    } catch (err) {
        console.error('Failed to copy text: ', err);
        return false;
    }
};

// Auto-scroll behavior for streaming content
window.enableAutoScroll = (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        const observer = new MutationObserver(() => {
            element.scrollTop = element.scrollHeight;
        });
        
        observer.observe(element, {
            childList: true,
            subtree: true,
            characterData: true
        });
        
        return observer;
    }
    return null;
};

// Disable auto-scroll
window.disableAutoScroll = (observer) => {
    if (observer) {
        observer.disconnect();
    }
};